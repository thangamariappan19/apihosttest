using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CustomerMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectCustomerMasterLookUpResponse:BaseResponseType
    {
        [DataMember]
        public List<CustomerMaster> CustomerMasterList { get; set; }
    }
}
