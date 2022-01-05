using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.TillSettingsResponse
{
    [DataContract]
    [Serializable]
    public class SelectByIDTillSettingsResponse:BaseResponseType
    {

        [DataMember]
        public TillSettings TillSettingsRecord { get; set; }
    }
}
