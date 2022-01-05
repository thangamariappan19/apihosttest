using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Stocks.NonTradingItemStock;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.INonTradingItem
{
   public interface INonTradingItemView:IBaseView
    {
        TransactionLog NonTradingStockData { get; set; }
        int ID { get; set; }
        string StoreCode { get; }
        string SearchValue { get; }
        string SkuCode { get; set; }
        string ItemCode { get; set; }
        string BarCode { get; set; }
        UsersSettings UserInformation { get; }
        List<EmployeeMaster> EmployeeLookUp { get; set; }
        string DocumentNo { get; set; }
        DateTime DocumentDate { get; set; }
        string Quantity { get; set; }
        string TransactionType { get; set; }
        string ReceivedType { get; set; }
        long EmployeeID { get; set; }
        string EmployeeName { get; set; }
        string EmployeeCode { get; set; }
        int ReceivedQty { get; set; }
        int ReturnQty { get; set; }
        int CountryID { get; }
        int StoreID { get; }
        int SKUID { get; }
        List<TransactionLog> NonTradingStockList { get; set; }
        List<NonTradingStockDetailsTypes> NonTradingStockDetailsList { get; set; }
        List<NonTradingStockDetailsTypes> NewNonTradingDetailsList { get; set; }
        List<TransactionLog> TransactionLogList { get; set; }
        int NonTradingStockHeaderID { get; set; }
        string RefDocumentNo { get; set; }
        //int SelectedStoreId { get; set; }
        //StoreMaster StoreMasterRecord { get; set; }
        //List<StoreMaster> StoreList { get; set; }
    }
}
