using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PriceTypeRequest
{

    [DataContract]
    [Serializable]
    public class SelectByIDPriceTypeRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
