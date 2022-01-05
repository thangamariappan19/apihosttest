using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.WarehouseMasterRequest;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizResponse.Masters.WarehouseMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class WarehouseMasterBLL
    {
        public SaveWarehouseMasterResponse SaveWarehouseMaster(SaveWarehouseMasterRequest objRequest)
        {
            SaveWarehouseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseWarehouseMasterDAL = objFactory.GetDALRepository().GetWarehouseMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objWarehouseMaster = new WarehouseMaster();
                    objWarehouseMaster = (WarehouseMaster)objRequest.RequestDynamicData;
                    objRequest.WarehouseMasterData = objWarehouseMaster;
                }
                objResponse = (SaveWarehouseMasterResponse)objBaseWarehouseMasterDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.WarehouseMasterData.ID = Convert.ToInt32(objResponse.IDs);
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.WAREHOUSE;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;
            }
            catch (Exception ex)
            {
                objResponse = new SaveWarehouseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Warehouse Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllWarehouseMasterResponse API_SelectAllWarehouseMaster(SelectAllWarehouseMasterRequest objRequest)
        {
            SelectAllWarehouseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseWarehouseMasterDAL = objFactory.GetDALRepository().GetWarehouseMasterDAL();
                objResponse = (SelectAllWarehouseMasterResponse)objBaseWarehouseMasterDAL.API_SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllWarehouseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Warehouse Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllWarehouseMasterResponse SelectAllWarehouseMaster(SelectAllWarehouseMasterRequest objRequest)
        {
            SelectAllWarehouseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseWarehouseMasterDAL = objFactory.GetDALRepository().GetWarehouseMasterDAL();
                objResponse = (SelectAllWarehouseMasterResponse)objBaseWarehouseMasterDAL.SelectAll(objRequest);
         
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllWarehouseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Warehouse Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDWarehouseMasterResponse SelectWarehouseMasterRecord(SelectByIDWarehouseMasterRequest objRequest)
        {
            SelectByIDWarehouseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseWarehouseMasterDAL = objFactory.GetDALRepository().GetWarehouseMasterDAL();
                objResponse = (SelectByIDWarehouseMasterResponse)objBaseWarehouseMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDWarehouseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Warehouse Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateWarehouseMasterResponse UpdateWarehouseMaster(UpdateWarehouseMasterRequest objRequest)
        {
            UpdateWarehouseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseWarehouseMasterDAL = objFactory.GetDALRepository().GetWarehouseMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objWarehouseMaster = new WarehouseMaster();
                    objWarehouseMaster = (WarehouseMaster)objRequest.RequestDynamicData;
                    objRequest.WarehouseMasterData = objWarehouseMaster;
                }
                objResponse = (UpdateWarehouseMasterResponse)objBaseWarehouseMasterDAL.UpdateRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objRequest.WarehouseMasterData.ID);
                //    objRequest.DocumentType = Enums.DocumentType.WAREHOUSE;
                //    objRequest.ProcessMode = Enums.ProcessMode.Edit;
                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.WarehouseMasterBLL", "UpdateWarehouseMaster");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new UpdateWarehouseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Warehouse Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteWarehouseMasterResponse DeleteWarehouseMaster(DeleteWarehouseMasterRequest objRequest)
        {
            DeleteWarehouseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseWarehouseMasterDAL = objFactory.GetDALRepository().GetWarehouseMasterDAL();
                objResponse = (DeleteWarehouseMasterResponse)objBaseWarehouseMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                 //   objRequest.DocumentIDs = Convert.ToString(objRequest.WarehouseMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.WAREHOUSE;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.WarehouseMasterBLL", "DeleteWarehouseMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteWarehouseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Warehouse Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectWhareouseLookUpResponse SelectWhareHouseLookUp(EasyBizRequest.Transactions.Stocks.StockRequest.SelectWhareHouseLookUpRequest objRequest)
        {
            SelectWhareouseLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseWarehouseMasterDAL = objFactory.GetDALRepository().GetWarehouseMasterDAL();
                objResponse = (SelectWhareouseLookUpResponse)objBaseWarehouseMasterDAL.SelectWhareHouseLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectWhareouseLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
