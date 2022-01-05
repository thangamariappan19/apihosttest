using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using EasyBizFactory;
using EasyBizRequest.Transactions.Stocks.OpeningStock;
using EasyBizResponse.Transactions.Stocks.OpeningStock;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Stocks
{
   public class OpeningStockBLL
    {
       public SelectAllOpeningStockResponse SelectAllOpeningStock(SelectAllOpeningStockRequest objRequest)
       {
           SelectAllOpeningStockResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseOpeningStockDAL = objFactory.GetDALRepository().GetOpeningStockDAL();
               objResponse = (SelectAllOpeningStockResponse)objBaseOpeningStockDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllOpeningStockResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "OpeningStock");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
        public SelectAllOpeningStockResponse API_SelectALL(SelectAllOpeningStockRequest objRequest)
        {
            SelectAllOpeningStockResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseOpeningStockDAL = objFactory.GetDALRepository().GetOpeningStockDAL();
                objResponse = (SelectAllOpeningStockResponse)objBaseOpeningStockDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllOpeningStockResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "OpeningStock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDOpeningStockHeaderResponse SelectStockRequestRecord(SelectByIDOpeningStockHeaderRequest objRequest)
       {
           SelectByIDOpeningStockHeaderResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStockRequestDAL = objFactory.GetDALRepository().GetOpeningStockDAL();
               objResponse = (SelectByIDOpeningStockHeaderResponse)objBaseStockRequestDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByIDOpeningStockHeaderResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "OpeningStock");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectByOpeningStockDetailsResponse SelectOpeningStockDetails(SelectByOpeningStockDetailsRequest objRequest)
       {
           SelectByOpeningStockDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseOpeningStockDAL = objFactory.GetDALRepository().GetOpeningStockDAL();
               objResponse = (SelectByOpeningStockDetailsResponse)objBaseOpeningStockDAL.SelectByStockRequestDetails(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByOpeningStockDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockRequest");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }

           return objResponse;
       }
       public SaveOpeningStockResponse SaveOpeningStock(SaveOpeningStockRequest objRequest)
       {
           SaveOpeningStockResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
               var objBaseOpeningStockRequestDAL = objFactory.GetDALRepository().GetOpeningStockDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objStockRequest = new OpeningStockHeader();
                   objStockRequest = (OpeningStockHeader)objRequest.RequestDynamicData;
                   objRequest.OpeningStockHeaderRecord = objStockRequest;
                   objRequest.OpeningStockDetailsList = objStockRequest.OpeningStockDetailsList;
                   objRequest.TransactionLogList = objStockRequest.TransactionLogList;
               }
               objResponse = (SaveOpeningStockResponse)objBaseOpeningStockRequestDAL.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.OPENINGSTOCK;
                   objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.OpeningStockBLL", "SaveOpeningStock");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveOpeningStockResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "OpeningStock");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
}
