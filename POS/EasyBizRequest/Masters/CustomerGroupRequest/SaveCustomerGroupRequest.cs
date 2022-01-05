using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using EasyBizDBTypes.Masters;


namespace EasyBizRequest.Masters.CustomerGroupMasterRequest
{

    [DataContract]
    [Serializable]
    public class SaveCustomerGroupRequest : BaseRequestType
    {
        [DataMember]
        public CustomerGroupMaster CustomerGroupMasterData { get; set; }
    }
}
