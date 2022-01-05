﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.CouponReceipt
{
    [Serializable]
    [DataContract]
    public class SelectByIDCouponReceiptRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}