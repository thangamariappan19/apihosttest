using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using EasyBizFactory;
using EasyBizRequest.Transactions.Stocks.InventoryCounting;
using EasyBizResponse.Transactions.Stocks.InventoryCounting;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Stocks
{
    public class InventoryCountingBLL
    {
        public SaveInventoryCountingResponse SaveInventoryCounting(SaveInventoryCountingRequest objRequest)
        {
            SaveInventoryCountingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objInventoryCounting = new InventoryCountingHeader();
                    objInventoryCounting = (InventoryCountingHeader)objRequest.RequestDynamicData;
                    objRequest.InventoryCountingHeaderRecord = objInventoryCounting;
                    objRequest.InventoryCountingDetailsList = objInventoryCounting.InventoryCountingDetailList;
                }
                objResponse = (SaveInventoryCountingResponse)objBaseInventoryCountingDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.INVENTORYCOUNTING;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;
                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.InventoryCountingBLL", "SaveInventoryCounting");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveInventoryCountingResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "InventoryCounting");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SaveInventoryCountingResponse InventoryPosting(SaveInventoryCountingRequest objRequest)
        {
            SaveInventoryCountingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (SaveInventoryCountingResponse)objBaseInventoryCountingDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.INVENTORYCOUNTING;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.InventoryCountingBLL", "InventoryPosting");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveInventoryCountingResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "InventoryCounting");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateInventoryCountingResponse UpdateInventoryCounting(UpdateInventoryCountingRequest objRequest)
        {
            UpdateInventoryCountingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (UpdateInventoryCountingResponse)objBaseInventoryCountingDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.INVENTORYCOUNTING;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.InventoryCountingBLL", "UpdateInventoryCounting");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateInventoryCountingResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "InventoryCounting");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteInventoryCountingResponse DeleteInventoryCounting(DeleteInventoryCountingRequest objRequest)
        {
            DeleteInventoryCountingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (DeleteInventoryCountingResponse)objBaseInventoryCountingDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.INVENTORYCOUNTING;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.InventoryCountingBLL", "DeleteInventoryCounting");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteInventoryCountingResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "InventoryCounting");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllInventoryCountingResponse SelectAllInventoryCounting(SelectAllInventoryCountingRequest objRequest)
        {
            SelectAllInventoryCountingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (SelectAllInventoryCountingResponse)objBaseInventoryCountingDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllInventoryCountingResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "InventoryCounting");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByInventoryCountingIDResponse SelectInventoryCountingRecord(SelectByInventoryCountingIDRequest objRequest)
        {
            SelectByInventoryCountingIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                if(objRequest.ID == 0)
                {
                    int doc_id;
                    int.TryParse(objRequest.DocumentIDs, out doc_id);
                    objRequest.ID = doc_id;
                }
                objResponse = (SelectByInventoryCountingIDResponse)objBaseInventoryCountingDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByInventoryCountingIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "InventoryCounting");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByInventoryCountingDetailsResponse SelectInventoryCountingDetails(SelectByInventoryCountingDetailsRequest objRequest)
        {
            SelectByInventoryCountingDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInventoryCountingDetailsDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (SelectByInventoryCountingDetailsResponse)objBaseInventoryCountingDetailsDAL.SelectByInventoryCountingDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByInventoryCountingDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "InventoryCounting");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public GetSystemStockByStoreResponse GetSystemStockByStore(GetSystemStockByStoreRequest objRequest)
        {
            GetSystemStockByStoreResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInventoryCountingDetailsDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (GetSystemStockByStoreResponse)objBaseInventoryCountingDetailsDAL.GetSystemStockByStore(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetSystemStockByStoreResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "System Stock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public GetSystemStockByStoreResponse GetSystemStockByStoreCount(GetSystemStockByStoreRequest objRequest)
        {
            GetSystemStockByStoreResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInventoryCountingDetailsDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (GetSystemStockByStoreResponse)objBaseInventoryCountingDetailsDAL.API_SystemStockByStoreCount(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetSystemStockByStoreResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "System Stock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public GetSystemStockByStoreResponse GetSystemStockByStoreLimit(GetSystemStockByStoreRequest objRequest)
        {
            GetSystemStockByStoreResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInventoryCountingDetailsDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (GetSystemStockByStoreResponse)objBaseInventoryCountingDetailsDAL.API_SystemStockByStorelimit(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetSystemStockByStoreResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "System Stock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SaveSystemStockResponse SaveSystemStock(SaveSystemStockRequest objRequest)
        {
            SaveSystemStockResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objInventoryCounting = new InventoryInit();
                    objInventoryCounting = (InventoryInit)objRequest.RequestDynamicData;
                    objRequest.InventoryManualCountRecord = objInventoryCounting;                    
                }
                objResponse = (SaveSystemStockResponse)objBaseInventoryCountingDAL.SaveSystemStock(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SaveSystemStockResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory Initialize and Freeze");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public GetInventoryCountingInitResponse GetInventoryCountingInitList(GetInventoryCountingInitRequest objRequest)
        {
            GetInventoryCountingInitResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (GetInventoryCountingInitResponse)objBaseInventoryCountingDAL.GetInventoryCountingInitList(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetInventoryCountingInitResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Inventory Counting Init List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public GetInventoryCountingInitResponse API_GetInventoryCountingInitList(GetInventoryCountingInitRequest objRequest)
        {
            GetInventoryCountingInitResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (GetInventoryCountingInitResponse)objBaseInventoryCountingDAL.API_GetInventoryCountingInitList(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetInventoryCountingInitResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Inventory Counting Init List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetInventoryCountingInitRecordResponse GetInventoryCountingInitRecord(GetInventoryCountingInitRecordRequest objRequest)
        {
            GetInventoryCountingInitRecordResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (GetInventoryCountingInitRecordResponse)objBaseInventoryCountingDAL.GetInventoryCountingInitRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetInventoryCountingInitRecordResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Inventory Counting Init Record");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SaveManualStockResponse SaveManualStock(SaveManualStockRequest objRequest)
        {
            SaveManualStockResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();               
                objResponse = (SaveManualStockResponse)objBaseInventoryCountingDAL.SaveManualStock(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SaveManualStockResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory Initialize and Freeze");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetInventoryManualCountRecordResponse GetInventoryManualCountRecord(GetInventoryManualCountRecordRequest objRequest)
        {
            GetInventoryManualCountRecordResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
                objResponse = (GetInventoryManualCountRecordResponse)objBaseInventoryCountingDAL.GetInventoryManualCountRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetInventoryManualCountRecordResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Inventory Counting Init Record");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public InventoryFinalizeResponse InventoryFinalize(InventoryFinalizeRequest objRequest)
        {
            InventoryFinalizeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();                
                objResponse = (InventoryFinalizeResponse)objBaseInventoryCountingDAL.InventoryFinalize(objRequest);                
            }
            catch (Exception ex)
            {
                objResponse = new InventoryFinalizeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory Finalize");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public InventorySyncResponse InventorySyncToServer(InventorySyncRequest objRequest)
        {
            InventorySyncResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseInventoryCountingDAL = objFactory.GetDALRepository().GetInventoryCountingDAL();
               objResponse = (InventorySyncResponse)objBaseInventoryCountingDAL.InventorySyncToServer(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new InventorySyncResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory Sync");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
