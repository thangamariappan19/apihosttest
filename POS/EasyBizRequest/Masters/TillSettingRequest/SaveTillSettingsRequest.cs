using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.TillSettingRequest
{  
    [Serializable]
    [DataContract]
    public class SaveTillSettingsRequest : BaseRequestType
    {

        [DataMember]
        public TillSettings TillSettingsData { get; set; }
    }
}
