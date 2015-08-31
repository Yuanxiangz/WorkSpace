using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMQ.Server
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				System.Messaging.MessageQueue Queue;
				Queue = new System.Messaging.MessageQueue(@"FormatName:DIRECT=TCP:172.26.230.2\ptest1");

				System.Messaging.Message Msg;
				Msg = new System.Messaging.Message();
				Msg.Formatter = new System.Messaging.BinaryMessageFormatter();
				Msg.Body = "Testing   3   times";
				Queue.Send(Msg);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
