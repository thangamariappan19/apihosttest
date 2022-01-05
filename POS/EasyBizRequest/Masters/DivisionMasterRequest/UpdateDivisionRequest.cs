using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DivisionMasterRequest
{ 
    [DataContract]
    [Serializable]
    public class UpdateDivisionRequest : BaseRequestType
    {
        [DataMember]
        public DivisionMaster DivisionRecord { get; set; }
    }
}
