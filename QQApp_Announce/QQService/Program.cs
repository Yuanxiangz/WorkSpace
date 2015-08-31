using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Configuration;
using System.ServiceModel.Channels;

namespace QQServer
{
    class Program
    {
        static void Main(string[] args)
        {
			//NetTcpBinding bind = new NetTcpBinding();
			//Uri uri = new Uri(ConfigurationManager.AppSettings["baseAddress"]);
			//ServiceHost host = new ServiceHost(typeof(QQServer.ChatService), uri);
            
			//if (host.Description.Behaviors.Find<System.ServiceModel.Description.ServiceMetadataBehavior>() == null)
			//{
			//    BindingElement metaElement = new TcpTransportBindingElement();
			//    CustomBinding metaBind = new CustomBinding(metaElement);
			//    host.Description.Behaviors.Add(new System.ServiceModel.Description.ServiceMetadataBehavior());
			//    host.AddServiceEndpoint(typeof(System.ServiceModel.Description.IMetadataExchange), metaBind, "MEX");
			//}

			//host.Open();
			//Console.WriteLine("Chat service start to listen: endpoint {0}", uri.ToString());
			//Console.WriteLine("Press Enter to stop listen...");
			//Console.ReadLine();
			//host.Abort();
			//host.Close();

			using(ServiceHost host = new ServiceHost(typeof(QQServer.ChatService)))
			{
				host.Open();
				Console.WriteLine("Press Enter to stop listen...");
				Console.ReadLine();
				host.Close();
			}
        }
    }
}
