using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizDBTypes.Transactions.Stocks.StockReturn;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IStockReturn
{
    public interface IStockReturnView : IBaseView
    {
        List<WarehouseMaster> WarehouseMasterLookUp { set; }      
        int ID { get; set; }
        int HeaderID { get; set; }
        string DocumentNo { get; set; }
        DateTime DocumentDate { get; set; }
        int TotalQuantity { get; set; }
        int ToWareHouseID { get; set; }
        string Status { get; set; }
        List<StockReturnDetails> NewStockReturnDetailsList { get; set; }
        List<StockReturnDetails> StockReturnDetailsList { get; set; }
        List<StockReturnDetails> StockReturnDetailsList1 { get; set; }
        List<SKUMasterTypes> SKUMasterTypesList { get; set; }
        List<TransactionLog> ScaleWiseStockList { get; set; }

        //List<SKUMasterTypes> SKUMasterList { get; set; }
        int CountryID { get; }
        int StateID { get; }
        int StoreID { get; }
        int DocumentTypeID { get; set; }
        List<TransactionLog> TransactionLogList { get; set; }
        string SKUSearchString { get; }
        string Remarks { get; set; }       
        TransactionLog StockData { get; set; }
        string ToWareHouseCode { get; }
        string StoreCode { get;  }
        List<int_stockreturn> int_stockreturnList { get; set; }
        int StockReturnHeaderID { get; set; }  
        string ReturnType { get; set; }
        UsersSettings UserInformation1 { get;  }
    }

    public interface IStockReturnHeaderReportView
    {
        DateTime FromDate { get; }
        DateTime ToDate { get; }
        List<StockReturnHeader> StockReturnHeaderList { set; }
    }
    public interface IStockReturnDetailsReportView
    {
        DateTime FromDate { get; }
        DateTime ToDate { get; }
        List<StockReturnDetails> StockReturnDetailsList { set; }
    }
}
