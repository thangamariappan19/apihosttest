using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using EasyBizDBTypes.Masters;
namespace EasyBizRequest.Masters.BarcodeSettingsRequest
{
    [Serializable]
    [DataContract]
    public class UpdateBarcodeSettingsRequest:BaseRequestType
    {
        [DataMember]
        public BarcodeSettings BarcodeSettingsData { get; set; }
    }
}
