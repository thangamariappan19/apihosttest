using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockStaging;
using EasyBizFactory;
using EasyBizRequest.Transactions.Stocks.StockAdjustment;
using EasyBizResponse.Transactions.Stocks.StockAdjustment;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Stocks
{
    public class StockAdjustmentBLL
    {
        public SaveStockAdjustmentResponse SaveStockAdjustment(SaveStockAdjustmentRequest objRequest)
        {
            SaveStockAdjustmentResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockAdjustmentDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStockAdjustmentHeader = new StockAdjustmentHeader();
                    objStockAdjustmentHeader = (StockAdjustmentHeader)objRequest.RequestDynamicData;
                    objRequest.StockAdjustmentRecord = objStockAdjustmentHeader;
                    objRequest.TransactionLogList = objStockAdjustmentHeader.TransactionLogList;
                }
                objResponse = (SaveStockAdjustmentResponse)objBaseStockReceiptDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKADJUSTMENT;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockAdjustmentBLL", "SaveStockAdjustment");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStockAdjustmentResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Stock Adjustment");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public GetAllStockAdjustmentRecordResponse SelectStockAdjustment(GetAllStockAdjustmentRecordRequest objRequest)
        {
            GetAllStockAdjustmentRecordResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockAdjustmentDAL();
                objResponse = (GetAllStockAdjustmentRecordResponse)objBaseStockReceiptDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetAllStockAdjustmentRecordResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Stock Adjustment");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public GetAllStockAdjustmentRecordResponse API_SelectALL(GetAllStockAdjustmentRecordRequest objRequest)
        {
            GetAllStockAdjustmentRecordResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockAdjustmentDAL();
                objResponse = (GetAllStockAdjustmentRecordResponse)objBaseStockReceiptDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetAllStockAdjustmentRecordResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Stock Adjustment");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectRecordStockAdjustmentResponse SelectStockAdjustmentRecord(SelectRecordStockAdjustmentRequest objRequest)
        {
            SelectRecordStockAdjustmentResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReceiptDAL = objFactory.GetDALRepository().GetStockAdjustmentDAL();
                if(objRequest.ID == 0)
                {
                    int doc_id;
                    int.TryParse(objRequest.DocumentIDs, out doc_id);
                    objRequest.ID = doc_id;
                }
                objResponse = (SelectRecordStockAdjustmentResponse)objBaseStockReceiptDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectRecordStockAdjustmentResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Stock Adjustment");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
