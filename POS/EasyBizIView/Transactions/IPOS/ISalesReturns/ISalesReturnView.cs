using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesReturn;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.ISalesReturns
{
    public interface ISalesReturnView : IBaseView
    {
        int ID { get; set; }        
        string DocumentNo { get; set; }
        DateTime DocumentDate { get; set; }
        long InvoiceHeaderID { get; set; } 
        //List<SalesReturnDetail> SalesReturnDetailList { get; set; }
        //List<InvoiceHeader> InvoiceHeaderList { get; set; }
        int CountryID { get; set; }        
        int StoreID { get; set; }        
        List<TransactionLog> TransactionLogList { get; set; }        
        DateTime SalesDate { get; set; }
        int StateID { get; set; }
        SalesReturnHeader SalesReturnData { get; set; }
        int POSID { get; set; }
        List<InvoiceReturnReceipt> InvoiceReturnReceipt { get; set; }
        List<InvoiceDetails> InvoiceDetailsList { get; set; }
        long ManagerOverrideID { get; set; }
        ManagerOverride DefaultManagerOverrideSetting { get; set; }
        ManagerOverride ManagerOverrideSetting { get; set; }

        string SearchString { get; }

        RetailSettingsType RetailSetting { get; set; }

        bool WithOutInvoice { get; set; }

        SKUMasterTypes SkuRecord { get; set; }

        string ReturnMode { get; set; }

        string PrintInvoiceNo { get; set; }

        List<EasyBizDBTypes.Transactions.POS.SalesExchange.SalesExchangeDetail> ExchangeList { get; set; }
        List<InvoiceSubReceiptTypes> InvoiceSubReceiptList { get; set; }
        List<ApprovalNumReceipt> ApprovalNumReceiptList { get; set; }
        List<InvoiceReceiptTypes> InvoiceReceiptList { get; set; }
        List<StylePricing> StylePricingList { get; set; }
        UsersSettings UserInfo { get; }
        string CountryCode { get; }
        string StoreCode { get; }
        string PosCode { get; }
        List<PromotionsMaster> PromotionsMasterList { get; set; }
        List<TaxMaster> TaxList { get; set; }
        OnAccountPayment OnAccountPaymentRecord { get; set; }
        bool CreditSales { get; set; }
        List<InvoiceDetails> ExchangeInvoiceDetailsList { get; set; }
    }
}
