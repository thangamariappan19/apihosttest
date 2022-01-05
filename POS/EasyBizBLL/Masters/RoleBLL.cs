using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizResponse.Masters.RoleResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class RoleBLL
    {
        public SaveRoleResponse SaveRoleMaster(SaveRoleRequest objRequest)
        {
            SaveRoleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseRoleDAL objBaseRoleMasterDAL = objFactory.GetDALRepository().GetRoleDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objRoleMaster = new RoleMaster();
                    objRoleMaster = (RoleMaster)objRequest.RequestDynamicData;
                    objRequest.RoleMasterData = objRoleMaster;
                }
                objResponse = (SaveRoleResponse)objBaseRoleMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.ROLE;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.RoleBLL", "SaveRoleMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveRoleResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Role Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllRoleResponse API_SelectALL(SelectAllRoleRequest requestData)
        {
            SelectAllRoleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseRoleDAL objBaseRoleMasterDAL = objFactory.GetDALRepository().GetRoleDAL();
                objResponse = (SelectAllRoleResponse)objBaseRoleMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllRoleResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Role Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateRoleResponse UpdateRoleMaster(UpdateRoleRequest objRequest)
        {
            UpdateRoleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseRoleDAL objBaseRoleMasterDAL = objFactory.GetDALRepository().GetRoleDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objRoleMaster = new RoleMaster();
                    objRoleMaster = (RoleMaster)objRequest.RequestDynamicData;
                    objRequest.RoleMasterData = objRoleMaster;
                }
                objResponse = (UpdateRoleResponse)objBaseRoleMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.RoleMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.ROLE;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.RoleBLL", "UpdateRoleMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateRoleResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Role Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteRoleResponse DeleteRoleMaster(DeleteRoleRequest objRequest)
        {
            DeleteRoleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseRoleDAL objBaseRoleMasterDAL = objFactory.GetDALRepository().GetRoleDAL();
                objResponse = (DeleteRoleResponse)objBaseRoleMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.RoleMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.ROLE;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.RoleBLL", "DeleteRoleMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteRoleResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Role Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllRoleResponse SelectAllRoleMasterLookUp(SelectAllRoleRequest objRequest)
        {
            SelectAllRoleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseRoleDAL objBaseRoleMasterDAL = objFactory.GetDALRepository().GetRoleDAL();
                objResponse = (SelectAllRoleResponse)objBaseRoleMasterDAL.SelectAllRoleMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllRoleResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Role Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllRoleResponse SelectAllRoleMaster(SelectAllRoleRequest objRequest)
        {
            SelectAllRoleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseRoleDAL objBaseRoleMasterDAL = objFactory.GetDALRepository().GetRoleDAL();
                objResponse = (SelectAllRoleResponse)objBaseRoleMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllRoleResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Role Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDRoleResponse SelectRoleMaster(SelectByIDRoleRequest objRequest)
        {
            SelectByIDRoleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                BaseRoleDAL objBaseRoleMasterDAL = objFactory.GetDALRepository().GetRoleDAL();
                objResponse = (SelectByIDRoleResponse)objBaseRoleMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDRoleResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Role Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectRoleMasterLookUpResponse SelectRoleLookUP(SelectRoleMasterLookUpRequest objRequest)
        {
            SelectRoleMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseRoleDAL objBaseRoleMasterDAL = objFactory.GetDALRepository().GetRoleDAL();
                objResponse = (SelectRoleMasterLookUpResponse)objBaseRoleMasterDAL.SelectRoleLookUp(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectRoleMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Role Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }

}

