using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.BarcodeSettingsResponse
{
    [Serializable]
    [DataContract]
    public class SelectBarcodeGenerateBySKUResponse : BaseResponseType
    {
        [DataMember]
        public BarcodeSettings BarcodeGenerateBySKURecord { get; set; }
    }
}
