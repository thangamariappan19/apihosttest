using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IStockReceipt
{
    public interface IStockReceiptView : IBaseView
    {
        int ID { get; set; }
        int HeaderID { get; set; }
        int StockRequestID { get; set; }
        string StockRequestDocumentNo { get; set; }
        string DocumentNo { get; set; }
        string FromWarehouseCode { get; set; }
        string Fromwarehousename { get; set; }
        bool Type { get; set; }
        DateTime DocumentDate { get; set; }
        int TotalQuantity { get; set; }
        int TotalReceivedQuantity { get; set; }
        int FromWareHouseID { get; set; }
        string Status { get; set; }
        string Remarks { get; set; }
        List<StockReceiptDetails> StockReceiptDetailsList { get; set; }
        List<StockReceiptHeader> StockReceiptTransactionDetailsList { get; set; }
        //List<StockReceiptDetails> StockRequestDetailsListDirect { get; set; }
        List<StockRequestHeader> StockRequestHeaderLookUp {  set; }
        List<StockRequestDetails> StockRequestDetailsList { get; set; }
        int CountryID { get; }
        int StateID { get; }
        int StoreID { get; }
        int DocumentTypeID { get; }
        List<TransactionLog> TransactionLogList { get; set; }
        string SKUSearchString { get; }
        //List<SKUMasterTypes> SKUMasterTypesList { get; set; }
        StoreMaster StoreMasterRecord { get; set; }
        string StoreCode { get; }
        StockRequestHeader StockRequestHeaderRecord { get; set; }
        bool fromApplication { get; set; }
        //List<SKUMasterTypes> SKUMasterList { get; set; }

        List<Int_ConfirmTransfer> Int_ConfirmTransferList { get; set; }
        List<int_stockreceipt> int_stockreceiptList { get; set; }
       
        bool WithOutBaseDoc { get; set; }
        bool IsReceiptComplete { get; set; }
        int StockReceiptHeaderID { get; set; }

        string DataFrom { get; set; }

        bool IsFlaged { get; set; }

        string ReceivedType { get; set; }
        DateTime ToDate { get; set; }
        string Report { get; set; }
        List<TagIdItemDetails> RFIDList { get; set; }
    }
    public interface IStockReceiptHeaderReportView
    {
        DateTime FromDate { get; }
        DateTime ToDate { get; }
        List<StockReceiptHeader> StockReceiptHeaderList { set; }
    }
    public interface IStockReceiptDetailsReportView
    {
        DateTime FromDate { get; }
        DateTime ToDate { get; }
        List<StockReceiptDetails> StockReceiptDetailsList { set; }
    }
}
