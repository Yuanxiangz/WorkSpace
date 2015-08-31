using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            EndpointAddress address = new EndpointAddress("http://127.0.0.1:9999/messagingviabinding");
            BasicHttpBinding binding = new BasicHttpBinding();
            IChannelFactory<IRequestChannel> chananelFactory = binding.BuildChannelFactory<IRequestChannel>();
            chananelFactory.Open();
            IRequestChannel channel = chananelFactory.CreateChannel(address);
            channel.Open();
            Message requestMessage = Message.CreateMessage(MessageVersion.Soap11, "http://artech/messagingviabinding", "The is a request message manually created for the purpose of testing.");
            Message replyMessage = channel.Request(requestMessage);
            Console.WriteLine("Receive a reply message:\n{0}", replyMessage);
            channel.Close();
            chananelFactory.Close();
            Console.Read();
        }
    }

}
