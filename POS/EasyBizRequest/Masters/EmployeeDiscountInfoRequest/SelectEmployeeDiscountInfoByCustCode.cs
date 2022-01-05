using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.EmployeeDiscountInfoRequest
{
    [DataContract]
    [Serializable]
   public class SelectEmployeeDiscountInfoByCustCode : BaseRequestType
    {
        [DataMember]
        public string CustomerCode { get; set; } 
    }
}
