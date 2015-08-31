using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace QQApp
{
    public interface IChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void UserEnter(string name);

        [OperationContract(IsOneWay = true)]
        void UserLeave(string name);

        [OperationContract(IsOneWay = true)]
        void Receive(string name, string message);

        [OperationContract(IsOneWay = true)]
        void ReceiveWhisper(string name, string message);
    }
}
