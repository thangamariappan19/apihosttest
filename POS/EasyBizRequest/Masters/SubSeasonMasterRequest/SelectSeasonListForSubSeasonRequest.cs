using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SubSeasonMasterRequest
{
    public class SelectSeasonListForSubSeasonRequest : BaseRequestType
    {
        [DataMember]
        public long SeasonID { get; set; }
    }
}
