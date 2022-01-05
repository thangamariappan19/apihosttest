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
    public class SelectAllComboOfferRequest : BaseRequestType
    {
        [DataMember]
        public int StoreID { get; set; }
    }
}
