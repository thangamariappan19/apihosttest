using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Stocks.NonTradingItemStock;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.NonTradingStockRequest;
using EasyBizResponse.Transactions.POS.NonTradingStockResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Stocks
{
   public class NonTradingItemStockBLL
    {

         public SaveNonTradingItemResponse SaveNonTradingItemStock(SaveNonTradingItemRequest objRequest)
        {
            SaveNonTradingItemResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseNonTradingItemStockDAL = objFactory.GetDALRepository().GetNonTradingItemStockDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objNonTradingItemRequest = new NonTradingStockHeaderTypes();
                    objNonTradingItemRequest = (NonTradingStockHeaderTypes)objRequest.RequestDynamicData;
                    objRequest.NonTradingItemRecord = objNonTradingItemRequest;
                    //objRequest.NonTradingStockDetailsList = objNonTradingItemRequest.NonTradingStockDetailsList;
                    objRequest.NonTradingStockDetailsList = objNonTradingItemRequest.NonTradingStockDetailsList;
                    objRequest.TransactionLogList = objNonTradingItemRequest.TransactionLogList;                    
                }
                objResponse = (SaveNonTradingItemResponse)objBaseNonTradingItemStockDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.NONTRADINGITEMDISTRIBUTION;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.NonTradingItemStockBLL", "SaveNonTradingItemStock");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveNonTradingItemResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "NonTradingItemStock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectALLNonTradingStockResponse SelectALLNonTradingStock(SelectALLNonTradingStockRequest objRequest)
        {
            SelectALLNonTradingStockResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseNonTradingStockDAL = objFactory.GetDALRepository().GetNonTradingItemStockDAL();
                objResponse = (SelectALLNonTradingStockResponse)objBaseNonTradingStockDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectALLNonTradingStockResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "NonTradingItemStock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectALLNonTradingStockResponse API_SelectALL(SelectALLNonTradingStockRequest objRequest)
        {
            SelectALLNonTradingStockResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseNonTradingStockDAL = objFactory.GetDALRepository().GetNonTradingItemStockDAL();
                objResponse = (SelectALLNonTradingStockResponse)objBaseNonTradingStockDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectALLNonTradingStockResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "NonTradingItemStock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByNonTradingStockIDResponse SelectNonTradingHeaderRecord(SelectByNonTradingHeaderIDRequest objRequest)
        {
            SelectByNonTradingStockIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseNonTradingStockDAL = objFactory.GetDALRepository().GetNonTradingItemStockDAL();
                if (objRequest.ID == 0)
                {
                    int doc_id;
                    int.TryParse(objRequest.DocumentIDs, out doc_id);
                    objRequest.ID = doc_id;
                }
                objResponse = (SelectByNonTradingStockIDResponse)objBaseNonTradingStockDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByNonTradingStockIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "NonTradingItemStock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByNonTradingDetailsIDResponse  SelectNonTradingStockDetails(SelectByNonTraddingDetailsIDRequest objRequest)
        {
            SelectByNonTradingDetailsIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasNonTradingStockDetailsDAL = objFactory.GetDALRepository().GetNonTradingItemStockDAL();
                objResponse = (SelectByNonTradingDetailsIDResponse)objBasNonTradingStockDetailsDAL.SelectByNonTradingStockDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByNonTradingDetailsIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "NonTradingItemStock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
