using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SubSeasonMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectSeasonListForSubSeasonResponse : BaseResponseType
    {
        [DataMember]
        public List<SubSeasonMaster> SubSeasonList { get; set; }
        public SubSeasonMaster SubSeasonRecord { get; set; }
    }
}
