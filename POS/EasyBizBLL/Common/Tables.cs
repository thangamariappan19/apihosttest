using EasyBizRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Common
{
   
    public static class Tables
    {
        public static string TableName(string ClassName, string FunctionName)
        {
            string objTableName = string.Empty;            
            if (ClassName == "EasyBizBLL.Masters.CustomerMasterBLL" && FunctionName == "SaveCustomerMaster")
            {
                objTableName = "CustomerMaster";
            }
            else if (ClassName == "EasyBizBLL.Masters.EmployeeMasterBLL" && FunctionName == "SaveEmployeeMaster")
            {
                objTableName = "EmployeeMaster";
            }
            return objTableName;
        }

        public static List<string> ExcludeStoreServerTables()
        {
            List<string> StoreServerTableList = new List<string>();

            StoreServerTableList.Add("DayClosing");
            //StoreServerTableList.Add("DocumentNumberingDetails");
            //StoreServerTableList.Add("DocumentNumberingMaster");
            StoreServerTableList.Add("FailedServerSyncData");
            StoreServerTableList.Add("FailedStoreSyncData");
            StoreServerTableList.Add("InvoiceCardDetails");
            StoreServerTableList.Add("InVoiceCashDetails");
            StoreServerTableList.Add("InvoiceDetail");
            StoreServerTableList.Add("InvoiceHeader");
            StoreServerTableList.Add("ItemStockStaging");
            StoreServerTableList.Add("SalesExchangeDetail");
            StoreServerTableList.Add("SalesExchangeHeader");
            StoreServerTableList.Add("SalesReturnDetail");
            StoreServerTableList.Add("SalesReturnHeader");
            StoreServerTableList.Add("ShiftLOG");
            StoreServerTableList.Add("StockAdjustmentDetails");
            StoreServerTableList.Add("StockAdjustmentHeader");
            StoreServerTableList.Add("StockReceiptDetails");
            StoreServerTableList.Add("StockReceiptHeader");
            StoreServerTableList.Add("StockRequestDetails");
            StoreServerTableList.Add("StockRequestHeader");
            StoreServerTableList.Add("StockReturnDetails");
            StoreServerTableList.Add("StockReturnHeader");
            StoreServerTableList.Add("TransactionLog");            
            return StoreServerTableList;
        }
    }
}
