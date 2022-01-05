using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.DropMasterRequest;
using EasyBizRequest.Masters.DropMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class DropMasterBLL
    {
        public SaveDropMasterResponse SaveDropMaster(SaveDropMasterRequest objRequest)
        {
            SaveDropMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var BaseDropMaster = objFactory.GetDALRepository().GetDropMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objDropMasterTypes = new DropMasterTypes();
                    objDropMasterTypes = (DropMasterTypes)objRequest.RequestDynamicData;
                    objRequest.DropMasterTypesRecord = objDropMasterTypes;
                }
                objResponse = (SaveDropMasterResponse)BaseDropMaster.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DropMasterTypesRecord.ID = Convert.ToInt32(objResponse.IDs);
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.DROPMASTER;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DropMasterBLL", "SaveDropMaster");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveDropMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Drop Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllDropMasterResponse API_SelectALL(SelectAllDropMasterRequest requestData)
        {
            SelectAllDropMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseDropMaster = objFactory.GetDALRepository().GetDropMasterDAL();
                objResponse = (SelectAllDropMasterResponse)BaseDropMaster.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllDropMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Drop Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateDropMasterResponse UpdateDropMaster(UpdateDropMasterRequest objRequest)
        {
            UpdateDropMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var BaseDropMaster = objFactory.GetDALRepository().GetDropMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objDropMasterTypes = new DropMasterTypes();
                    objDropMasterTypes = (DropMasterTypes)objRequest.RequestDynamicData;
                    objRequest.DropMasterTypesRequestData = objDropMasterTypes;
                }
                objResponse = (UpdateDropMasterResponse)BaseDropMaster.UpdateRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objRequest.DropMasterTypesRequestData.ID);
                //    objRequest.DocumentType = Enums.DocumentType.DROPMASTER;
                //    objRequest.ProcessMode = Enums.ProcessMode.Edit;
                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DropMasterBLL", "UpdateDropMaster");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new UpdateDropMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Drop Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }



        public DeleteDropMasterResponse DeleteDropMaster(DeleteDropMasterRequest objRequest)
        {
            DeleteDropMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var BaseDropMaster = objFactory.GetDALRepository().GetDropMasterDAL();
                objResponse = (DeleteDropMasterResponse)BaseDropMaster.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.DropMasterTypesRequestData.ID);
                    objRequest.DocumentType = Enums.DocumentType.DROPMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DropMasterBLL", "DeleteDropMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteDropMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Drop Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectAllDropMasterResponse SelectAllDropMaster(SelectAllDropMasterRequest objRequest)
        {
            SelectAllDropMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseDropMaster = objFactory.GetDALRepository().GetDropMasterDAL();
                objResponse = (SelectAllDropMasterResponse)BaseDropMaster.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllDropMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Drop Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByIDDropMasterResponse SelectByIDDropMaster(SelectByIDDropMasterRequest objRequest)
        {
            SelectByIDDropMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                var BaseDropMaster = objFactory.GetDALRepository().GetDropMasterDAL();
                objResponse = (SelectByIDDropMasterResponse)BaseDropMaster.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDDropMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Drop Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


    }
}
