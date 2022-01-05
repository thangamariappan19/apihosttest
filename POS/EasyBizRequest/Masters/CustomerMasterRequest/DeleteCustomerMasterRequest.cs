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
    public class DeleteCustomerMasterRequest:BaseRequestType
    {

        [DataMember]
        public CustomerMaster CustomerMasterData { get; set; }
    }
}
