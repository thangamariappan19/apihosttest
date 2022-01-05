using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SeasonResponse
{
    [DataContract]
    [Serializable]
    public class SelectSeasonLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<SeasonMaster> SeasonList { get; set; }
    }
}
