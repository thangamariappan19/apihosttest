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

    public class SaveSubSeasonRequest : BaseRequestType
    {
        [DataMember]
        public SubSeasonMaster SubSeasonRecord { get; set; }
        [DataMember]
        public List<SubSeasonMaster> SubSeasonlist { get; set; }
    }
}
