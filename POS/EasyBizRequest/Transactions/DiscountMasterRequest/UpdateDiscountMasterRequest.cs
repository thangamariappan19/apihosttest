﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.DiscountMasterRequest
{
    [Serializable]
    [DataContract]
   public class UpdateDiscountMasterRequest : BaseRequestType
    {
        public int ID { get; set; }
        
    }
}
