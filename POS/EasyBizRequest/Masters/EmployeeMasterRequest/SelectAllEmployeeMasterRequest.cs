﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.EmployeeMasterRequest
{
    [DataContract]
    [Serializable]
   public class SelectAllEmployeeMasterRequest:BaseRequestType
    {
        [DataMember]
        public string ReqFrom { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        public int StoreID { get; set; }
    }
}
