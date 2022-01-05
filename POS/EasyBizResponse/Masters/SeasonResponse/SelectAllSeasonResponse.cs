using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SeasonResponse
{
    [Serializable]
    [DataContract]
   public class SelectAllSeasonResponse:BaseResponseType
    {
        [DataMember]
        public List<SeasonMaster> SeasonMasterList { get; set; }
    }
}
