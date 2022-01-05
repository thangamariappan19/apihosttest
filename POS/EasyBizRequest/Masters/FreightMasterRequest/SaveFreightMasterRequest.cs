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
    public class SaveFreightMasterRequest:BaseRequestType
    {

        [DataMember]
        public FreightMaster FreightMasterData { get; set; }
    }
}
