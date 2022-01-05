using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Pricing
{
    [DataContract]
    [Serializable]
    public class PricePoint : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string PricePointCode { get; set; }

        [DataMember]
        public string PricePointName { get; set; }

        [DataMember]
        public int BrandID { get; set; }

        [DataMember]
        public int BaseCurrencyID { get; set; }

        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public string BaseCurrencyCode { get; set; }
        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string BrandName { get; set; }

        [DataMember]
        public string CurrencyName { get; set; }

        [DataMember]
        public List<PricePointRange> PricePointRangeList { get; set; }
        [DataMember]
        public List<PricePoint> PricePointList { get; set; }

    }
    [DataContract]
    [Serializable]
    public class PricePointRange
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int PricePointID { get; set; }

        [DataMember]
        public Decimal RangeFrom { get; set; }

        [DataMember]
        public Decimal RangeTo { get; set; }

        [DataMember]
        public int CurrencyID { get; set; }

        [DataMember]
        public string InternationalCode { get; set; }

        [DataMember]
        public Decimal Price { get; set; }
        [DataMember]
        public string PricePointCode { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
    }
}
