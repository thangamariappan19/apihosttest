using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DesignationMasterRequest
{
    
    [Serializable]
    [DataContract]
    public class SelectDesignationMasterLookUpRequest : BaseRequestType
    {
        [DataMember]
        public List<DesignationMaster> WarehouseTypeMasterList = new List<DesignationMaster>();
    }
}
