using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ComboOfferRequest
{
    [DataContract]
    [Serializable]
    public class SelectByComboOfferIDRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SKUcode { get; set; }
    }
}
