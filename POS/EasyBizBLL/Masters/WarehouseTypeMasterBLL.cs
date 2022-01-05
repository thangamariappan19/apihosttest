using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.WarehouseTypeMasterRequest;
using EasyBizResponse.Masters.WarehouseMasterResponse;
using EasyBizResponse.Masters.WarehouseTypeMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class WarehouseTypeMasterBLL
    {
        public SaveWarehouseTypeMasterResponse SaveWarehouseTypeMaster(SaveWarehouseTypeMasterRequest objRequest)
        {
            SaveWarehouseTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseWarehouseTypeMasterDAL = objFactory.GetDALRepository().GetWarehouseTypeMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objWarehouseTypeMaster = new WarehouseTypeMaster();
                    objWarehouseTypeMaster = (WarehouseTypeMaster)objRequest.RequestDynamicData;
                    objRequest.WarehouseTypMasterData = objWarehouseTypeMaster;
                }
                objResponse = (SaveWarehouseTypeMasterResponse)objBaseWarehouseTypeMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.WarehouseTypMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.WAREHOUSETYPES;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.WarehouseTypeMasterBLL", "SaveWarehouseTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveWarehouseTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "WarehouseType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllWarehouseTypeMasterResponse API_SelectAllWarehouseTypeMaster(SelectAllWarehouseTypeMasterRequest objRequest)
        {
            SelectAllWarehouseTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseWarehouseTypeMasterDAL = objFactory.GetDALRepository().GetWarehouseTypeMasterDAL();
                objResponse = (SelectAllWarehouseTypeMasterResponse)objBaseWarehouseTypeMasterDAL.API_SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllWarehouseTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "WarehouseType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllWarehouseTypeMasterResponse SelectAllWarehouseTypeMaster(SelectAllWarehouseTypeMasterRequest objRequest)
        {
            SelectAllWarehouseTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseWarehouseTypeMasterDAL = objFactory.GetDALRepository().GetWarehouseTypeMasterDAL();
                objResponse = (SelectAllWarehouseTypeMasterResponse)objBaseWarehouseTypeMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllWarehouseTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "WarehouseType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDWarehouseTypeMasterResponse SelectWarehouseTypeMasterRecord(SelectByIDWarehouseTypeMasterRequest objRequest)
        {
            SelectByIDWarehouseTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseWarehouseTypeMasterDAL = objFactory.GetDALRepository().GetWarehouseTypeMasterDAL();
                objResponse = (SelectByIDWarehouseTypeMasterResponse)objBaseWarehouseTypeMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDWarehouseTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "WarehouseType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllWarehouseTypeMasterResponse SelectAllWarehouseTypeMasterLookUp(SelectAllWarehouseTypeMasterRequest requestData)
        {
            SelectAllWarehouseTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseWarehouseTypeMasterDAL = objFactory.GetDALRepository().GetWarehouseTypeMasterDAL();
                objResponse = (SelectAllWarehouseTypeMasterResponse)objBaseWarehouseTypeMasterDAL.SelectAllWarehouseTypeMasterLookUp(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllWarehouseTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "WarehouseType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateWarehouseTypeMasterResponse UpdateWarehouseTypeMaster(UpdateWarehouseTypeMasterRequest objRequest)
        {
            UpdateWarehouseTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseWarehouseTypeMasterDAL = objFactory.GetDALRepository().GetWarehouseTypeMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objWarehouseTypeMaster = new WarehouseTypeMaster();
                    objWarehouseTypeMaster = (WarehouseTypeMaster)objRequest.RequestDynamicData;
                    objRequest.WarehouseTypeMasterData = objWarehouseTypeMaster;
                }
                objResponse = (UpdateWarehouseTypeMasterResponse)objBaseWarehouseTypeMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.WarehouseTypeMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.WAREHOUSETYPES;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.WarehouseTypeMasterBLL", "UpdateWarehouseTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateWarehouseTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "WarehouseType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteWarehouseTypeMasterResponse DeleteWarehouseTypeMaster(DeleteWarehouseTypeMasterRequest objRequest)
        {
            DeleteWarehouseTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseWarehouseTypeMasterDAL = objFactory.GetDALRepository().GetWarehouseTypeMasterDAL();
                objResponse = (DeleteWarehouseTypeMasterResponse)objBaseWarehouseTypeMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                 //   objRequest.DocumentIDs = Convert.ToString(objRequest.WarehouseTypeMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.WAREHOUSETYPES;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.WarehouseTypeMasterBLL", "DeleteWarehouseTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteWarehouseTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "WarehouseType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectWarehouseTypeMasterLookUpResponse SelectWarehouseTypeMasterLookUp(SelectWarehouseTypeMasterLookUpRequest objRequest)
        {
            SelectWarehouseTypeMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseWarehouseTypeMasterDAL = objFactory.GetDALRepository().GetWarehouseTypeMasterDAL();
                objResponse = (SelectWarehouseTypeMasterLookUpResponse)objBaseWarehouseTypeMasterDAL.SelectWarehouseTypeMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectWarehouseTypeMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "WarehouseType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
