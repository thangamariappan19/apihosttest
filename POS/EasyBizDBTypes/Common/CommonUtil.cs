using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Common
{
    [DataContract]
    [Serializable]
    public class CommonUtil
    {

        [DataMember]
        public int PromotionHeaderID { get; set; }

        [DataMember]
        public int CouponID { get; set; }

        [DataMember]
        public int ID { get; set; } 

        [DataMember]
        public int TypeID { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public int DocumentID { get; set; }


        [DataMember]
        public string DocumentCode { get; set; }

        [DataMember]
        public string DocumentName { get; set; }

        [DataMember]
        public string StyleCode { get; set; } 
        [DataMember]
        public Decimal Quantity { get; set; }
        [DataMember]
        public Decimal GetQuantity { get; set; }
        [DataMember]
        public Decimal BuyQuantity { get; set; }

        [DataMember]
        public Decimal Amount { get; set; }

        [DataMember]
        public Decimal DiscountValue { get; set; }

        [DataMember]
        public string DiscountType { get; set; }

        [DataMember]
        public bool Prompt { get; set; }
        [DataMember]
        public bool Active { get; set; }
        

        [DataMember]
        public bool UpdateFlag { get; set; }

        [DataMember]
        public string PromotionFrom { get; set; }

        [DataMember]
        public List<BaseLookUp> BaseLookUpList { get; set; }

        [DataMember]
        public List<LookUp> LookUpList { get; set; }

        [DataMember]
        public List<BaseLookUp> DiscountTypeLookupList { get; set; }
        [DataMember]
        public List<StyleCodeLookUp> StyleCodeLookUp { get; set; }

        [DataMember]
        public bool IsMandatory { get; set; }
      
    }
    [DataContract]
    [Serializable]
    public class BaseLookUp
    {
        [DataMember]
        public int TypeID { get; set; }

        [DataMember]
        public string TypeName { get; set; }
    }
    public class StyleCodeLookUp
    {
        [DataMember]
        public int StyleID { get; set; }

        [DataMember]
        public string StyleCode { get; set; }
    }
    [DataContract]
    [Serializable]
    public class LookUp
    {
        [DataMember]
        public int TypeID { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public int DocumentID { get; set; }

        [DataMember]
        public string DocumentCode { get; set; }

        [DataMember]
        public string DocumentName { get; set; }
    }    
}
