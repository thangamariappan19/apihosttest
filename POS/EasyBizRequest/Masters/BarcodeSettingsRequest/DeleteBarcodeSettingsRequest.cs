using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EasyBizRequest.Masters.BarcodeSettingsRequest
{
    [Serializable]
    [DataContract]
    public class DeleteBarcodeSettingsRequest:BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
