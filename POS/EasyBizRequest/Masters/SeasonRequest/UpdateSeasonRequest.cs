using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SeasonRequest
{
    [Serializable]
    [DataContract]
   public class UpdateSeasonRequest:BaseRequestType
    {
        [DataMember]
        public SeasonMaster SeasonMasterData { get; set; }
    }
}
