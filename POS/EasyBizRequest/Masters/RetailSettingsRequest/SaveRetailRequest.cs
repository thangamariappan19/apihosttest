using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.RetailSettingsRequest
{
    [DataContract]
    [Serializable]
    public class SaveRetailRequest : BaseRequestType
    {
        [DataMember]
        public RetailSettingsType RetailRecord { get; set; }
    }
}
