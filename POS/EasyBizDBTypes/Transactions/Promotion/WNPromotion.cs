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
    public class WNPromotion : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string PromotionCode { get; set; }

        [DataMember]
        public string PromotionName { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public int PricePointID { get; set; }

        [DataMember]
        public int PriceListID { get; set; }

        [DataMember]
        public string Countries { get; set; }
        [DataMember]
        public string PriceListCode { get; set; }

        [DataMember]
        public string UploadType { get; set; }

        [DataMember]
        public bool PricePointApplicable { get; set; }

        [DataMember]
        public int DefaultCountryID { get; set; }

        [DataMember]
        public List<WNPromotionDetails> WNPromotionDetailsList { get; set; }

       
    }
}
