using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SubSeasonMasterRequest
{
    [DataContract]
    [Serializable]
    public class UpdateSubSeasonRequest : BaseRequestType
    {
        [DataMember]
        public SubSeasonMaster SubSeasonData { get; set; }
    }
}
