using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class PromotionsMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string PromotionCode { get; set; }
        [DataMember]
        public string PromotionName { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        public Double MinBillAmount { get; set; }
        [DataMember]
        public int MinQuantity { get; set; }
        [DataMember]
        public string Discount { get; set; }
        [DataMember]
        public Double DiscountValue { get; set; }
        [DataMember]
        public Boolean AllowMultiPromotion { get; set; }
        [DataMember]
        public Boolean LowestValue { get; set; }
        [DataMember]
        public Boolean ExculdeDiscountItems { get; set; }
        [DataMember]
        public Boolean Prompt { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public int PriceListID { get; set; }
        [DataMember]
        public Double BuyOptionalCount { get; set; }
        [DataMember]
        public Double GetOptionalCount { get; set; }
        [DataMember]
        public Double GetItematFixedPrice { get; set; }

        [DataMember]
        public List<CommonUtil> StoreList { get; set; }
        [DataMember]
        public List<CommonUtil> CustomerList { get; set; }
        [DataMember]
        public List<CommonUtil> ProductTypeList { get; set; }
        [DataMember]
        public List<CommonUtil> BuyItemTypeList { get; set; }
        [DataMember]
        public List<CommonUtil> GetItemTypeList { get; set; }

        [DataMember]
        public int Periority { get; set; }


    }
}
