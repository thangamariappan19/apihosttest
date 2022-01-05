using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS
{
    [DataContract]
    [Serializable]
   public class InvoiceDetails:BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int SKUID { get; set; }
        [DataMember]
        public string StyleCode { get; set; }

        [DataMember]
        public int InvoiceHeaderID { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public int SubBrandID { get; set; }     
       
        [DataMember]
        public int Category { get; set; }
        [DataMember]
        public int Qty { get; set; }
        [DataMember]
        public int DummyQty { get; set; }
        [DataMember]
        public Decimal Price { get; set; }
        [DataMember]
        public Decimal SellingPrice { get; set; }
        [DataMember]
        public Decimal SellingLineTotal { get; set; }
        [DataMember]
        public Decimal DummyPrice { get; set; }
        [DataMember]
        public String DiscountType { get; set; }
        [DataMember]
        public Decimal DiscountAmount { get; set; }
        [DataMember]
        public Decimal SingleDiscountAmount { get; set; }

        [DataMember]
        public int AppliedPriceListID { get; set; }
        [DataMember]
        public String AppliedCustomerSpecialPricesID { get; set; }
        [DataMember]
        public String AppliedPromotionID { get; set; }
        [DataMember]
        public bool SalesStatus { get; set; }
        [DataMember]
        public String ModifiedSalesEmployee { get; set; }
        [DataMember]
        public String ModifiedSalesManager { get; set; }
        [DataMember]
        public bool IsDataSyncToCountryServer { get; set; }
        [DataMember]
        public bool IsDataSyncToMainServer { get; set; }
        [DataMember]
        public DateTime CountryServerSyncTime { get; set; }
        [DataMember]
        public DateTime MainServerSyncTime { get; set; }
        [DataMember]
        public string SyncFailedReason { get; set; }

        [DataMember]
        public Decimal LineTotal { get; set; }
        
        [DataMember]
        public Decimal PromotionAmount { get; set; }

        [DataMember]
        public int SerialNo { get; set; }
        [DataMember]
        public string BillNo { get; set; }

        [DataMember]
        public string PromotionName { get; set; }

        [DataMember]
        public bool PromtionApplied { get; set; }

        [DataMember]
        public string InvoiceType { get; set; }

        [DataMember]
        public int LinkedSrlNo { get; set; }

        [DataMember]
        public bool IsRecordVisible { get; set; }

        [DataMember]
        public int CountryID { get; set; }

        [DataMember]
        public int StoreID { get; set; }
      
        [DataMember]
        public int TaxID { get; set; }

        [DataMember]
        public Decimal TaxAmount { get; set; }
        [DataMember]
        public string CustomerName { get; set; }       
        [DataMember]
        public int InvoiceDetailID { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }
        [DataMember]
        public int SalesReturnID { get; set; }
        public string PosID { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public DateTime InvoiceDate { get; set; }
        [DataMember]
        public bool IsExchanged { get; set; }
        [DataMember]
        public long ExchangeRefID { get; set; }
        [DataMember]
        public int ExchangeQty { get; set; }
        [DataMember]
        public int OldExchangeQty { get; set; }
        [DataMember]
        public bool IsReturned { get; set; }
        [DataMember]
        public long ReturnRefID { get; set; }
        [DataMember]
        public int ReturnQty { get; set; }
        [DataMember]
        public int OldReturnQty { get; set; }
        [DataMember]
        public string ExchangeRemarks { get; set; }
        [DataMember]
        public string ReturnRemarks { get; set; }
        [DataMember]
        public string ReturnedSKU { get; set; }
        [DataMember]
        public string ExchangedSKU { get; set; }
        [DataMember]
        public string SubBrandName { get; set; }
        [DataMember]
        public byte[] SKUImage { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string PosCode { get; set; }
        [DataMember]
        public List<PaymentDetail> PaymentList { get; set; }
        [DataMember]
        public decimal NetAmount { get; set; }
        [DataMember]
        public string DiscountRemarks { get; set; }
        [DataMember]
        public Decimal FamilyDiscountAmount { get; set; }
        [DataMember]
        public Decimal EmployeeDiscountAmount { get; set; }
        [DataMember]
        public int EmployeeDiscountID { get; set; }
        [DataMember]
        public string SpecialDiscountType { get; set; }
        [DataMember]
        public bool IsPromoExcludeItem { get; set; }
        [DataMember]
        public string SpecialPromoDiscountType { get; set; }
        [DataMember]
        public Decimal SpecialPromoDiscountPercentage { get; set; }
        [DataMember]
        public Decimal SpecialPromoDiscount { get; set; }
        [DataMember]
        public bool IsFreeItem { get; set; }
        [DataMember]
        public string Tag_Id { get; set; }
        [DataMember]
        public int PromoGroupID { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        /*[DataMember]
        public string SKUImage { get; set; }*/
        [DataMember]
        public bool IsCombo { get; set; }

        [DataMember]
        public bool IsHeader { get; set; }
        [DataMember]
        public int ComboGroupID { get; set; }

        [DataMember]
        public int ProductGroupID { get; set; }
        [DataMember]
        public bool IsGift { get; set; }
        public int StyleSegmentationID { get; set; }
        [DataMember]
        public int YearID { get; set; }
        [DataMember]
        public int SeasonID { get; set; }
        [DataMember]
        public int ProductSubGroupID { get; set; }
        [DataMember]
        public int StyleID { get; set; }
    }
}
