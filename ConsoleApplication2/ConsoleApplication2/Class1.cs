using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.A
{
	public class Class1 : IClass1
	{
		static readonly Class1 instance = new Class1();
		Item item = new Item() { ID = "111" };
		List<Item> itemList = new List<Item>();

		public class Item
		{
			public string ID { get; set; }
		}

		public static IClass1 Instance { get { return instance; } }

		static int Count { get; set; }

		public string Name
		{
			get { return "Jim"; }
		}

		public string ID
		{
			get { return item.ID; }
		}

		public string GetFromListID
		{
			get { return itemList.First().ID; }
		}
	}

	public interface IClass1
	{
		string Name { get; }
	}

	[MyInterceptor]
	public class Class2 : ContextBoundObject
	{
		public string Name { get; set; }

		[MyInterceptorMethod]
		public void Run()
		{
			
		}

	}

	[AttributeUsage(AttributeTargets.Method)]
	public class GetInvokeNumAttribute : System.Attribute
	{
		private static int count = 0;

		public GetInvokeNumAttribute()
		{
			count++;
		}

		public int Count
		{
			get { return count; }
			set { count = value; }
		}
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class GetInvokeNum2Attribute : System.Attribute
	{
		private int count = 0;

		public GetInvokeNum2Attribute()
		{
			count++;
		}

		public int Count
		{
			get { return count; }
			set { count = value; }
		}
	}

	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class MyInterceptorMethodAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class MyInterceptorAttribute : ContextAttribute, IContributeObjectSink
	{
		public MyInterceptorAttribute()
			: base("MyInterceptor")
		{ }

		public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink next)
		{
			return new MyAopHandler(next);
		}
	}

	public sealed class MyAopHandler : IMessageSink
	{
		public static int Count = 0;
		private IMessageSink nextSink;

		public IMessageSink NextSink
		{
			get { return nextSink; }
		}

		public MyAopHandler(IMessageSink nextSink)
		{
			this.nextSink = nextSink;
		}

		public IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage retMsg = null;
			IMethodCallMessage call = msg as IMethodCallMessage;
			if (call == null || (Attribute.GetCustomAttribute(call.MethodBase, typeof(MyInterceptorMethodAttribute))) == null)
			{
				retMsg = nextSink.SyncProcessMessage(msg);
			}
			else
			{
				retMsg = nextSink.SyncProcessMessage(msg);
				Count++;
			}
			return retMsg;
		}

		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return null;
		}
	}
}
