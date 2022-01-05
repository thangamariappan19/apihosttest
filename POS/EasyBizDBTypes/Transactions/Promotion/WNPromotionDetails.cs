using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Promotion
{
    [DataContract]
    [Serializable]
    public class WNPromotionDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int WNPromotionID { get; set; }

        [DataMember]
        public int CountryID { get; set; }

        [DataMember]
        public long StyleID { get; set; }

        [DataMember]
        public int BrandID { get; set; }

        [DataMember]
        public Decimal WasPrice { get; set; }

        [DataMember]
        public Decimal Discount { get; set; }

        [DataMember]
        public Decimal NowPrice { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string StyleCode { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ErrorMsg { get; set; }
    }
}
