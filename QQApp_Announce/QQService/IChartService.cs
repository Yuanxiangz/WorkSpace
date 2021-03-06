﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace QQServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallBack))]
    public interface IChatService
    {
        [OperationContract(IsOneWay=false, IsInitiating=true, IsTerminating=false)]
        string[] Join(string name);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void Leave();

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Say(string message);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Whisper(string name, string message);
    }
}
