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
        public int PromotionPriorityID { get; set; }
        [DataMember]
        public string PromotionCode { get; set; }
        [DataMember]
        public string pricelistcode { get; set; }
        [DataMember]
        public string PromotionName { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public Decimal MinBillAmount { get; set; }
        [DataMember]
        public int MinQuantity { get; set; }
        [DataMember]
        public string Discount { get; set; }
        [DataMember]
        public Decimal DiscountValue { get; set; }
        [DataMember]
        public Boolean AllowMultiPromotion { get; set; }
        [DataMember]
        public Boolean LowestValue { get; set; }
        [DataMember]
        public Boolean LowestValueWithGroup { get; set; }
        [DataMember]
        public Boolean ExculdeDiscountItems { get; set; }
        [DataMember]
        public Boolean Prompt { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public int PriceListID { get; set; }
        [DataMember]
        public int BuyOptionalCount { get; set; }
        [DataMember]
        public int GetOptionalCount { get; set; }
        [DataMember]
        public Decimal GetItematFixedPrice { get; set; }
        [DataMember]
        public int MinPromotionQty { get; set; }
        [DataMember]
        public int MaxGiftPerInvoice { get; set; }
        [DataMember]
        public int GiftQuantity { get; set; }
        [DataMember]
        public Decimal GiftBillAmount { get; set; }
        [DataMember]
        public Boolean MultiApplyForReceipt { get; set; }

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
        public int PriorityNo { get; set; }

        [DataMember]
        public string PromotionType { get; set; }

        [DataMember]
        public string AppliedType { get; set; }

        [DataMember]
        public List<ApplicablePromo> ApplicablePromoList { get; set; }

        [DataMember]
        public decimal BuyItemOptionalAmount { get; set; }
        //public 
    }
    public class ApplicablePromo
    {
        public string PromotionCode { get; set; }
        public int BrandID { get; set; }
        public string SKUCode { get; set; }
        public int SkipRows { get; set; }
        public int Qty { get; set; }
        public Decimal Amount { get; set; }
        public int ApplicableTimes { get; set; }
        public int PriorityNo { get; set; }
        public string ItemType { get; set; }
        public int ItemDocumentID { get; set; }
        public string ListType { get; set; }
        public string DiscountType { get; set; }
        public Decimal DiscountValue { get; set; }
    }
}
