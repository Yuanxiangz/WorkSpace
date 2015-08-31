using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Diagnostics;

namespace QQServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChatService
    {
        string username = string.Empty;
        static object obj = new object();
        IChatCallBack callback = null;
        delegate void ChatEventHandler(object sender, ChatEventArgs e);
        static ChatEventHandler broadcastList = null;
        ChatEventHandler myHandler = null;

        static Dictionary<string, ChatEventHandler> chatChannelList = new Dictionary<string, ChatEventHandler>();

        public string[] Join(string name)
        {
            string[] list = new string[chatChannelList.Count];
            try
            {
                Trace.WriteLine("User " + name + " join the chat room: start");
                username = name;
                callback = OperationContext.Current.GetCallbackChannel<IChatCallBack>();
                ChatEventArgs e = new ChatEventArgs();
                e.name = name;
                e.msgType = MessageType.UserEnter;
                BroadcastMessage(e);
                broadcastList += MessageHandling;

                lock (obj)
                {
                    chatChannelList.Keys.CopyTo(list, 0);
                }

                myHandler = new ChatEventHandler(MessageHandling);
                lock (obj)
                {
                    if (!chatChannelList.Keys.Contains(name))
                    {
                        chatChannelList.Add(name, myHandler);
                    }
                }
                Trace.WriteLine("User " + username + " join the chat room: end");

                Console.WriteLine("Callback instance number: " + chatChannelList.Count);
                Console.WriteLine("broadcastList Delegete number: " + broadcastList.GetInvocationList().Count());
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Servcie join method exception: " + ex.Message);
            }

            return list;
        }

        public void Leave()
        {
            try
            {
                Trace.WriteLine("User " + username + " leave the chat room: start");
                lock (obj)
                {
                    if (chatChannelList.Keys.Contains(username))
                    {
                        chatChannelList.Remove(username);
                    }
                }

                broadcastList -= myHandler;
                ChatEventArgs e = new ChatEventArgs();
                e.name = username;
                e.msgType = MessageType.UserLeave;
                BroadcastMessage(e);
                //System.Threading.Thread.Sleep(3000);
                
                Trace.WriteLine("User " + username + " leave the chat room: end");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Say(string message)
        {
            try
            {
                Trace.WriteLine("User " + username + " SAY: " + message + " : start");
                if (message == string.Empty)
                    return;

                ChatEventArgs e = new ChatEventArgs();
                e.name = username;
                e.msgType = MessageType.Receive;
                e.message = message;
                BroadcastMessage(e);
                Trace.WriteLine("User " + username + " SAY: " + message + " : end");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Servcie SAY method exception: " + ex.Message);
            }
        }

        public void Whisper(string name, string message)
        {
            try
            {
                Trace.WriteLine("User " + username + " WHISPER: " + message + " : start");
                if (name == string.Empty || message == string.Empty)
                    return;

                ChatEventArgs e = new ChatEventArgs();
                e.name = name;
                e.msgType = MessageType.ReceiveWhisper;
                e.message = message;
                chatChannelList[name](username, e);
                Trace.WriteLine("User " + username + " WHISPER: " + message + " : end");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Servcie Whisper method exception: " + ex.Message);
            }
        }

        #region private method
        private void MessageHandling(object sender, ChatEventArgs e)
        {
            try
            {
                switch (e.msgType)
                {
                    case MessageType.UserEnter:
                        callback.UserEnter(e.name);
                        break;
                    case MessageType.UserLeave:
                        callback.UserLeave(e.name);
                        break;
                    case MessageType.Receive:
                        callback.Receive(e.name, e.message);
                        break;
                    case MessageType.ReceiveWhisper:
                        callback.ReceiveWhisper((string)sender, e.message);
                        break;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Servcie MessageHandling method exception: " + ex.Message);
                Leave();
            }
        }

        private void BroadcastMessage(ChatEventArgs e)
        {
            ChatEventHandler t = broadcastList;

            if (t != null)
            {
                foreach (ChatEventHandler c in t.GetInvocationList())
                {
                    c.BeginInvoke(this, e, new AsyncCallback(EndAsync), null);
                }
            }
        }

        private void EndAsync(IAsyncResult ar)
        {
            ChatEventHandler d = null;

            try
            {
                System.Runtime.Remoting.Messaging.AsyncResult asres = (System.Runtime.Remoting.Messaging.AsyncResult)ar;
                d = ((ChatEventHandler)asres.AsyncDelegate);
                d.EndInvoke(ar);
            }
            catch
            {
                broadcastList -= d;
            }
        }
        #endregion
    }

    #region Data Structure
    public enum MessageType { Receive, UserEnter, UserLeave, ReceiveWhisper };

    public class ChatEventArgs : EventArgs
    {
        public MessageType msgType;
        public string name;
        public string message;
    }
    #endregion
}
