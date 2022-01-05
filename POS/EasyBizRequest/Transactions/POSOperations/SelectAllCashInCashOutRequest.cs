﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.PaymentDetails
{
    [DataContract]
    [Serializable]
    public class SelectAllCashInCashOutRequest : BaseRequestType
    {
        [DataMember]
        public string Type { get; set; }
    }
}