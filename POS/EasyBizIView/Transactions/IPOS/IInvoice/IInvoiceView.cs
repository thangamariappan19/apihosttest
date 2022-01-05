using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesOrder;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.IInvoice
{
    public interface IInvoiceView : IBaseView
    {
        long ID { get; set; }
        int TaxID { get; set; }
        int CustomerID { get; set; }
        int CustomerGroupID { get; set; }
        DateTime InvoiceDate { get; set; }
        int AppliedCustomerSpecialPricesID { get; set; }
        int TotalQty { get; set; }
        Decimal TotalPrice { get; set; }
        Decimal TaxAmount { get; set; }
        string SkuCode { get; set; }        
        List<CustomerMaster> CustomerMasterList { get; set; }
        List<SKUMasterTypes> SKUMasterTypesList { get; set; }
        List<CustomerSpecialPriceMasterTypes> CustomerSpecialPriceMasterList { get; set; }        
        List<InvoiceDetails> InvoiceDetailsList { get; set; }
        List<StylePricing> StylePricingList { get; set; }
        List<PriceListType> PriceListType { get; set; }
        string PriceListIDs { get; set; }
        string BillNo { get; set; }    
        string InvoiceNo { get; set; }
        string PaymentMode { get; set; }
        Decimal ReceivedCash { get; set; }
        Decimal BalanceCash { get; set; } 
        string SalesManager { get; set; }
        List<TaxMaster> TaxList { get; set; }
        List<InvoiceHeader> InvoiceHeaderList { get; set; }
        List<InvoiceDetails> InvoiceAllDetailsList { get; set; }
        int InvoiceHeaderID { get; set; }        
        DateTime DocumentDate { get; set; }
        string TotalDiscountType { get; set; }
        Decimal TotalDiscountAmount { get; set; }
        Decimal TotalDiscountPercentage { get; set; }
        Decimal LineTotalAmount { get; set; }
        DateTime BusinessDate { get; set; } 
        List<TransactionLog> TransactionLogList { get; set; }        
        List<SKUMasterTypes> StyleSKUList { set; }    
        List<ItemImageMaster> SKUImageList { get; set; }       
        ShiftLOGTypes ShiftLOGTypesList { get; set; }
        int ShiftID { get; set; }
        //int ShiftCode { get; set; }    
        long SKUID { get; set; }
        UsersSettings UserInformation { get; }
        List<WNPromotion> WNPromotionList { get; set; }
        string PaymentCurrency { get; }
        List<CurrencyMaster> CurrencyMasterList { get; set; }
        RetailSettingsType RetailSetting { get; set; }
        Enums.InvoiceStatus InvoiceType { get; set; }
        int EmployeeID { get; set; }
        EmployeeMaster EmployeeMasterRecord { get; set; }
        Decimal SubTotalAmount { get; set; }
        Decimal SubTotalWithTaxAmount { get; set; }
        int SalesManagerID { get; set; }
        List<EmployeeMaster> EmployeeList { get; set; }
        List<InvoiceReceiptTypes> InvoiceReceiptList { get; set; }
        List<InvoiceSubReceiptTypes> InvoiceSubReceiptList { get; set; }
        List<ApprovalNumReceipt> ApprovalNumReceiptList { get; set; }
        List<HoldReceipt> IHoldReceiptList { get; set; }
        string HoldBillNo { get; set; }
        string RefNumber { get; set; }

        ManagerOverride DefaultManagerOverrideSetting { get; set; }
        ManagerOverride ManagerOverrideSetting { get; set; }
        long ManagerOverrideID { get; set; }
        string PrintInvoiceNo { get; set; }
        Enums.OpStatusCode InvoiceStatus { set; }
        
        long StyleID { get; set; }
        string CustomerSearchString { get; set; }

        string SKUSearchString { get; set; }

        string CountryCode { get; }
        TransactionLog StockData { get; set; }

        string StoreCode { get; }
        string PosCode { get; }
        string CustomerCode { get; set; }
        string SalesEmployeeCode { get; }
        List<PromotionsMaster> PromotionsMasterList { get; set; }
        string PromotionCode { get; set; }
        List<PromotionCriteria> PromotionCriteriaList { get; set; }

        string[] BrandNames { set; }
        string DiscountRemarks { get; set; }
        DiscountMasterTypes DiscountMasterRecord { get; set; }
        List<EmployeeDiscountInfo> EmployeeDiscountInfoList { get; set; }

        SalesOrderHeader SalesOrderRecord { get; set; }
        
        Decimal BeforeRoundOffAmount { get; }        
        Decimal RoundOffAmount { get; }

        // New field added by Senthamil selvan @ 04.09.2018        
        //Decimal SubTotalWithOutDiscount { get; set; }
        //bool EnableCashDrawer { get; set; }
    }
}

