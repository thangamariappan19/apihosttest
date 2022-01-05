using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IInventoryCounting
{
    public interface IInventoryCountingEntry : IBaseView
    {
        int InventoryCountingID { get; set; }
        string DocumentNumber { get; set; }
        DateTime DocumentDate { get; set; }     
        List<InventoryCountingDetails> InventoryCountingDetailsList { get; set; }        
        int CountryID { get; }
        int StateID { get; }
        int StoreID { get; }  
        List<TransactionLog> TransactionLogList { get; set; }        
        TransactionLog StockData { get; set; }
        InventoryCountingHeader InventoryCountingRecord { get; set; }
        string StoreCode { get; }
    }

    public interface IInventoryCountingInitView : IBaseView
    {        
        string DocumentNo { get; set; }
        string StoreCode { get; }
        int StoreID { get;}
        InventoryInit InventoryInitRecord { get; set; }
        List<InventorySysCount> InventorySysCountList { get; set; }
    }

    public interface IInventoryDocUploadView : IBaseView
    {
        DateTime StockInitializeDate { get; set; }
        string DocumentNo { get; set; }
        string CountingType { get; set; }
        int StoreID { get; }
        InventoryInit InventoryInitRecord { get; set; }
        DateTime DocumentDate { get; }
        InventoryManualCount InventoryManualCountRecord { get; set; }
        List<InventoryManualCountDetail> CompleteSKUList { get; set; }
        SKUMasterTypes SKURecord { get;set; }
        string Status { get; set; }
    }
    public interface IInventorySummaryView : IBaseView
    {
        string DocumentNo { get; }
        InventoryManualCount InventoryManualCountRecord { get; set; }        
    }

    public interface IInventoryCountingApproveView : IBaseView
    {
        string DocumentNo { get; }
        InventoryManualCount InventoryManualCountRecord { get; set; }
        InventoryInit InventoryInitRecord { get; set; }
        List<CountryMaster> CountryList { get; set; }
        List<StoreMaster> StoreList { get; set; }
        int CountryID { get; }
        List<InventoryInit> InventoryInitList { get; set; }
        int StoreID { get; }
        string Status { get; set; }
        List<TransactionLog> TransactionLogList { get; }       
        string RARemarks { get; }
    }
    public interface IInventoryDocumentEditView : IBaseView
    {
        List<CountryMaster> CountryList { get; set; }
        List<StoreMaster> StoreList { get; set; }
        InventoryManualCount InventoryManualCountRecord { get; set; }
        InventoryInit InventoryInitRecord { get; set; }            
        List<InventoryInit> InventoryInitList { get; set; }
        string DocumentNo { get; }
        int CountryID { get; }
        int StoreID { get; }
        SKUMasterTypes SKURecord { get; set; }
    }
    public interface IInventoryReportView
    {
        string DocumentNo { get; }
        string GroupByMode { get;}
        List<CountryMaster> CountryList { get; set; }
        List<StoreMaster> StoreList { get; set; }
        int CountryID { get; }
        int StoreID { get; }
        List<InventoryInit> InventoryInitList { get; set; }
        InventoryManualCount InventoryManualCountRecord { get; set; }
        InventoryInit InventoryInitRecord { get; set; }
    }
}
