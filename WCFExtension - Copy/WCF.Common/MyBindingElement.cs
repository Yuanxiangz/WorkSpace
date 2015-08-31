using System;
using System.ServiceModel.Channels;

namespace WCF.Common
{
	public class MyBindingElement : BindingElement
	{
		public override BindingElement Clone()
		{
			return new MyBindingElement();
		}

		public override T GetProperty<T>(BindingContext context)
		{
			return context.GetInnerProperty<T>();
		}

		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			Console.WriteLine("MyBindingElement.BuildChannelFactory()");
			return new MyChannelFactory<TChannel>(context) as IChannelFactory<TChannel>;
		}

		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			Console.WriteLine("MyBindingElement.BuildChannelListener()");
			return new MyChannelListener<TChannel>(context) as IChannelListener<TChannel>;
		}
	}

}
