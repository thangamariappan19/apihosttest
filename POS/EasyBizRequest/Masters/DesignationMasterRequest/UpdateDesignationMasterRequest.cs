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
    public class UpdateDesignationMasterRequest : BaseRequestType
    {
        [DataMember]
        public DesignationMaster DesignationMasterData { get; set; }
    }
}
