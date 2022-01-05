using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.RequestTypeMasterRequest;
using EasyBizResponse.Masters.RequestTypeMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class RequestTypeMasterBLL
    {
          public SaveRequestTypeMasterResponse SaveRequestTypeMaster(SaveRequestTypeMasterRequest objRequest)
        {
            SaveRequestTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseRequestTypeMasterDAL = objFactory.GetDALRepository().GetRequestTypeMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objRequestTypeMaster = new RequestTypeMaster();
                    objRequestTypeMaster = (RequestTypeMaster)objRequest.RequestDynamicData;
                    objRequest.RequestTypeMasterData = objRequestTypeMaster;
                }
                objResponse = (SaveRequestTypeMasterResponse)objBaseRequestTypeMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.RequestTypeMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.REQUESTTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.RequestTypeMasterBLL", "SaveRequestTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveRequestTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "RequestType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllRequestTypeMasterResponse SelectAllRequestTypeMaster(SelectAllRequestTypeMasterRequest objRequest)
        {
            SelectAllRequestTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseRequestTypeMasterDAL = objFactory.GetDALRepository().GetRequestTypeMasterDAL();
                objResponse = (SelectAllRequestTypeMasterResponse)objBaseRequestTypeMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllRequestTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "RequestType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDRequestTypeMasterResponse SelectRequestTypeMasterRecord(SelectByIDRequestTypeMasterRequest objRequest)
        {
            SelectByIDRequestTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseRequestTypeMasterDAL = objFactory.GetDALRepository().GetRequestTypeMasterDAL();
                objResponse = (SelectByIDRequestTypeMasterResponse)objBaseRequestTypeMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDRequestTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "RequestType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateRequestTypeMasterResponse UpdateRequestTypeMaster(UpdateRequestTypeMasterRequest objRequest)
        {
            UpdateRequestTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseRequestTypeMasterDAL = objFactory.GetDALRepository().GetRequestTypeMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objRequestTypeMaster = new RequestTypeMaster();
                    objRequestTypeMaster = (RequestTypeMaster)objRequest.RequestDynamicData;
                    objRequest.RequestTypeMasterData = objRequestTypeMaster;
                }
                objResponse = (UpdateRequestTypeMasterResponse)objBaseRequestTypeMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;                  
                    objRequest.DocumentIDs = Convert.ToString(objRequest.RequestTypeMasterData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.REQUESTTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.RequestTypeMasterBLL", "UpdateRequestTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateRequestTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "RequestType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteRequestTypeMasterResponse DeleteRequestTypeMaster(DeleteRequestTypeMasterRequest objRequest)
        {
            DeleteRequestTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseRequestTypeMasterDAL = objFactory.GetDALRepository().GetRequestTypeMasterDAL();
                objResponse = (DeleteRequestTypeMasterResponse)objBaseRequestTypeMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.RequestTypeMasterData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.REQUESTTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.RequestTypeMasterBLL", "DeleteRequestTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteRequestTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "RequestType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectRequestTypeMasterLookUpResponse SelectRequestTypeMasterLookUp(SelectRequestTypeMasterLookUpRequest objRequest)
        {
            SelectRequestTypeMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseRequestTypeMasterDAL = objFactory.GetDALRepository().GetRequestTypeMasterDAL();
                objResponse = (SelectRequestTypeMasterLookUpResponse)objBaseRequestTypeMasterDAL.SelectRequestTypeMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectRequestTypeMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "RequestType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }    
}
