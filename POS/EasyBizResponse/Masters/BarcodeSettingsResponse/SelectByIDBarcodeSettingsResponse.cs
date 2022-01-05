using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using EasyBizDBTypes.Masters;

namespace EasyBizResponse.Masters.BarcodeSettingsResponse
{
    [Serializable]
    [DataContract]
    public class SelectByIDBarcodeSettingsResponse:BaseResponseType
    {
        [DataMember]
        public BarcodeSettings BarcodeSettingsRecord { get; set; }
    }
}
