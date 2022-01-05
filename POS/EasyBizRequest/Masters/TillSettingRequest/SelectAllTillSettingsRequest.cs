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
    public class SelectAllTillSettingsRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
