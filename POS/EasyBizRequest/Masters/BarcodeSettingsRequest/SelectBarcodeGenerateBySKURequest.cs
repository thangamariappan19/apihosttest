using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.BarcodeSettingsRequest
{
    [Serializable]
    [DataContract]
    public class SelectBarcodeGenerateBySKURequest : BaseRequestType
    {
        [DataMember]
        public int DocumentTypeID { get; set; }
        [DataMember]
        public int CountryID { get; set; }       
        [DataMember]
        public int StoreID { get; set; }

    }
}
