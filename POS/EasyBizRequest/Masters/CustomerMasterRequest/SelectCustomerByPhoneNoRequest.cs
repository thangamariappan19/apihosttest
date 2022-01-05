using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CustomerMasterRequest
{
    [DataContract]
    [Serializable]
    public class SelectCustomerByPhoneNoRequest : BaseRequestType
    {
        [DataMember]
        public string PhoneNumber { get; set; }

       
    }
}
