using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
//using System.ServiceModel.Channels;

namespace QQApp
{
    public partial class QQProxy : System.ServiceModel.DuplexClientBase<IChatService>, IChatService
    {

        public QQProxy(System.ServiceModel.InstanceContext callbackInstance) :
            base(callbackInstance)
        {
        }

        public QQProxy(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) :
            base(callbackInstance, endpointConfigurationName)
        {
        }

        public QQProxy(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) :
            base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public QQProxy(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public QQProxy(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, binding, remoteAddress)
        {
        }

        public System.IAsyncResult BeginJoin(string name, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginJoin(name, callback, asyncState);
        }

        public string[] EndJoin(System.IAsyncResult result)
        {
            return base.Channel.EndJoin(result);
        }

        public void Leave()
        {
            base.Channel.Leave();
        }


        public void Say(string message)
        {
            base.Channel.Say(message);
        }

        public void Whisper(string name, string message)
        {
            base.Channel.Whisper(name, message);
        }
    }
}
