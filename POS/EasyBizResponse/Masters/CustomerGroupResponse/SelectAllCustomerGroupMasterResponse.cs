using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CustomerGroupMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllCustomerGroupMasterResponse:BaseResponseType
    {


        [DataMember]
        public List<CustomerGroupMaster> CustomerGroupMasterList { get; set; }
    }
}
