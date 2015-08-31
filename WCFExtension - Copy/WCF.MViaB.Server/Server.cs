using System;
using System.ServiceModel.Channels;
using WCF.Common;

namespace WCF.MViaB.Server
{
	class Program
	{
		static void Main(string[] args)
		{
			MyBinding binding = new MyBinding();
			IChannelListener<IReplyChannel> channelListener = binding.BuildChannelListener<IReplyChannel>(new Uri("http://127.0.0.1:8888/messagingviabinding"));
			channelListener.Open();

			while (true)
			{
				IReplyChannel channel = channelListener.AcceptChannel(TimeSpan.MaxValue);
				channel.Open();
				RequestContext context = channel.ReceiveRequest(TimeSpan.MaxValue);

				Console.WriteLine("Receive a request message:\n{0}", context.RequestMessage);
				Message replyMessage = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "http://artech.messagingviabinding", "This is a mannualy created reply message for the purpose of testing");
				context.Reply(replyMessage);
				channel.Close();
			}
		}
	}
}
