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
    public class PromotionCriteria : BaseType
    {

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string PromotionCode { get; set; }

        [DataMember]
        public string PromotionName { get; set; }


        [DataMember]
        public string BuyType { get; set; }


        [DataMember]
        public int BuyDocumentID { get; set; }

        [DataMember]
        public string BuyName { get; set; }

        [DataMember]
        public int BuyQty { get; set; }


        [DataMember]
        public Decimal BuyAmount { get; set; }


        [DataMember]
        public string GetItemType { get; set; }

        [DataMember]
        public int GetItemDocumentID { get; set; }

        [DataMember]
        public string GetItemName { get; set; }

        [DataMember]
        public int GetItemQuantity { get; set; }

        [DataMember]
        public string DiscountType { get; set; }


        [DataMember]
        public Decimal DiscountValue { get; set; }


        [DataMember]
        public string PromotionHeaderDiscountType { get; set; }


        [DataMember]
        public Decimal PromotionHeaderDiscountValue { get; set; }


        [DataMember]
        public Decimal GetItemAmount { get; set; }

        [DataMember]
        public bool Prompt { get; set; }

        [DataMember]
        public bool AllowMultiPromotion { get; set; }

        [DataMember]
        public int MinQuantity { get; set; }

        [DataMember]
        public Decimal MinBillAmount { get; set; }

        [DataMember]
        public bool LowestValue { get; set; }

        [DataMember]
        public int PriorityNo { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public bool IsAppliedToAllRow { get; set; }

        [DataMember]
        public string PromotionType { get; set; }

        [DataMember]
        public string ListType { get; set; }

        [DataMember]
        public string AppliedType { get; set; }

        [DataMember]
        public int ApplicableRows { get; set; }

        [DataMember]
        public Decimal AppliedDiscountValue { get; set; }

        public Decimal GetItematFixedPrice { get; set; }
    }
}
