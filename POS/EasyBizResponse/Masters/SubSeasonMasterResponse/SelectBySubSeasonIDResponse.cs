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
    public class SelectBySubSeasonIDResponse : BaseResponseType
    {
        [DataMember]
        public SubSeasonMaster SubSeasonRecord { get; set; }
    }
}
