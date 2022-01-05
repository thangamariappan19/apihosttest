using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PriceListRequest
{
    [DataContract]
    [Serializable]
    public class UpdatePriceListRequest : BaseRequestType
    {
        [DataMember]
        public PriceListType PriceListTypeRecords { get; set; }
    }
}
