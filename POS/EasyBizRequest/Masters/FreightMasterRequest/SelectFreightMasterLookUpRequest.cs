using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.FreightMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectFreightMasterLookUpRequest: BaseRequestType
    {
        [DataMember]
        public List<FreightMaster> FreightMasterList = new List<FreightMaster>();
    }
}
