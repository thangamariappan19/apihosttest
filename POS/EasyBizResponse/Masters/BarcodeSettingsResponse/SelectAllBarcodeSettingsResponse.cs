using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using EasyBizDBTypes.Masters;

namespace EasyBizResponse.Masters.BarcodeSettingsResponse
{
    [Serializable]
    [DataContract]
    public class SelectAllBarcodeSettingsResponse:BaseResponseType

    {
        [DataMember]
        public List<BarcodeSettings> BarcodeSettingsList { get; set; }
    }
}
