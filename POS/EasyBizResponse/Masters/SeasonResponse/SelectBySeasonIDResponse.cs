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
 public class SelectBySeasonIDResponse:BaseResponseType
    {
        [DataMember]
        public SeasonMaster SeasonMasterRecord { get; set; }
    }
}
