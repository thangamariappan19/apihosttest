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
    public class SelectTillSettingsLookUpResponse: BaseResponseType
    {
        [DataMember]
        public List<TillSettings> TillSettingsList { get; set; }
    }
}
