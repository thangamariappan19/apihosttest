using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.ISalesExchange
{
    public interface ISalesExchangeView : IBaseView
    {
        int ID { get; set; }
        //int InvoiceDetailID { get; set; }

        string DocumentNo { get; set; }
        DateTime DocumentDate { get; set; }
        long InvoiceHeaderID { get; set; }
        //string SalesInvoiceNo { get; set; }              
        List<SalesExchangeDetail> ReturnList { get; set; }
        List<SalesExchangeDetail> ExchangeList { get; set; }
        List<InvoiceHeader> InvoiceHeaderList { get; set; }
        int CountryID { get; set; }
        int StateID { get; set; }
        int StoreID { get; set; }
        int POSID { get; set; }
        List<InvoiceDetails> InvoiceDetailsByIDList { set; }
        List<TransactionLog> TransactionLogList { get; set; }
        string HeaderID { get; set; }
        DateTime SalesDate { get; set; }
        SalesExchangeHeader SalesExchangeData { get; set; }
        long ManagerOverrideID { get; set; }
        ManagerOverride DefaultManagerOverrideSetting { get; set; }
        ManagerOverride ManagerOverrideSetting { get; set; }
        string SearchCriteria { get; }
        string SalesInvoiceNo { get; set; }
        SKUMasterTypes SkuRecord { set; }
        string ReturnSearchString { get; }
        string ExchangeSearchString { get; }
        bool WithOutInvoiceNo { get; set; }
        string ExchangeMode { get; set; }
        List<ExchangeReceipt> ExchangeReceiptList { get; set; }
        UsersSettings UserInfo { get; }
        List<StylePricing> StylePricingList { get; set; }

        string CountryCode { get; }
        string StoreCode { get; }
        string PosCode { get; }

        TransactionLog StockData { get; set; }
        List<TaxMaster> TaxList { get; set; }
        bool CreditSales { get; set; }
    }
}
