using EasyBizDBTypes.Transactions.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Pricing.PricePointRequest
{
    [DataContract]
    [Serializable]
    public class SavePricePointRequest : BaseRequestType
    {
        //[DataMember]
        //public PricePoint PricePointData { get; set; }

        [DataMember]
        public List<PricePoint> PricePointList { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string PricePointCode { get; set; }
        [DataMember]
        public string PricePointName { get; set; }
        [DataMember]
        public int Mode { get; set; }
        [DataMember]
        public List<PricePointRange> PricePointRange { get; set; }
    }
}
