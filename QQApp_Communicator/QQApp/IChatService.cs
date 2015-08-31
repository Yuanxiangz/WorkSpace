using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace QQApp
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallBack))]
    public interface IChatService
    {
        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IChatService/Join")]
        System.IAsyncResult BeginJoin(string name, System.AsyncCallback callback, object asyncState);

        string[] EndJoin(System.IAsyncResult result);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void Leave();

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Say(string message);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Whisper(string name, string message);
    }
}
