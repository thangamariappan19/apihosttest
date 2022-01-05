using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.CustomerGroupMasterRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizResponse.Masters.CustomerGroupMasterResponse;
using EasyBizResponse.Masters.CustomerGroupResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class CustomerGroupBLL
    {       
        public SaveCustomerGroupResponse  SaveCustomerGroup(SaveCustomerGroupRequest objRequest)
        {

            SaveCustomerGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCustomerGroupMasterDAL = objFactory.GetDALRepository().GetCustomerGroupMaster();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCustomerGroupMaster = new CustomerGroupMaster();
                    objCustomerGroupMaster = (CustomerGroupMaster)objRequest.RequestDynamicData;
                    objRequest.CustomerGroupMasterData = objCustomerGroupMaster;
                }
                objResponse = (SaveCustomerGroupResponse)objBaseCustomerGroupMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.CustomerGroupMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.CUSTOMERGROUP;
                    objRequest.ProcessMode = Enums.ProcessMode.New;


                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CustomerGroupBLL", "SaveCustomerGroup");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveCustomerGroupResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllCustomerGroupMasterResponse API_SelectALL(SelectAllCustomerGroupMasterRequest requestData)
        {
            SelectAllCustomerGroupMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerGroupMasterDAL = objFactory.GetDALRepository().GetCustomerGroupMaster();
                objResponse = (SelectAllCustomerGroupMasterResponse)objBaseCustomerGroupMasterDAL.API_SelectALL(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerGroupMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateCustomerGroupMasterResponse UpdateCustomerGroup(UpdateCustomerGroupMasterRequest objRequest)
        {

            UpdateCustomerGroupMasterResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCustomerGroupMasterDAL = objFactory.GetDALRepository().GetCustomerGroupMaster();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCustomerGroupMaster = new CustomerGroupMaster();
                    objCustomerGroupMaster = (CustomerGroupMaster)objRequest.RequestDynamicData;
                    objRequest.CustomerGroupMasterData = objCustomerGroupMaster;
                }
                objResponse = (UpdateCustomerGroupMasterResponse)objBaseCustomerGroupMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.CustomerGroupMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.CUSTOMERGROUP;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CustomerGroupBLL", "UpdateCustomerGroup");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCustomerGroupMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeleteCustomerGroupMasterResponse DeleteCustomerGroup(DeleteCustomerGroupMasterRequest objRequest)
        {

            DeleteCustomerGroupMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    var objCustomerGroupMaster = new CustomerGroupMaster();
                    objCustomerGroupMaster = (CustomerGroupMaster)objRequest.RequestDynamicData;
                    objRequest.CustomerGroupMaster = objCustomerGroupMaster;
                }
                var objBaseCustomerGroupMasterDAL = objFactory.GetDALRepository().GetCustomerGroupMaster();
                objResponse = (DeleteCustomerGroupMasterResponse)objBaseCustomerGroupMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.DocumentIDs);
                    objRequest.DocumentType = Enums.DocumentType.CUSTOMERGROUP;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CustomerGroupBLL", "DeleteCustomerGroup");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteCustomerGroupMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Group Master Delete");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }



        public SelectAllCustomerGroupMasterResponse SelectAllCustomerGroupMasterResponse(SelectAllCustomerGroupMasterRequest objRequest)
        {
            SelectAllCustomerGroupMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerGroupMasterDAL = objFactory.GetDALRepository().GetCustomerGroupMaster();
                objResponse = (SelectAllCustomerGroupMasterResponse)objBaseCustomerGroupMasterDAL.SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCustomerGroupMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDCustomerGroupResponse SelectByIDCustomerGroupResponse(SelectByIDCustomerGroupRequest objRequest)
        {
            SelectByIDCustomerGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseCustomerGroupMasterDAL = objFactory.GetDALRepository().GetCustomerGroupMaster();
                objResponse = (SelectByIDCustomerGroupResponse)objBaseCustomerGroupMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDCustomerGroupResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectCustomerGroupLookUpResponse SelectCustomerGroupLookUp(SelectCustomerGroupLookUpRequest objRequest)
        {
            SelectCustomerGroupLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCustomerGroupMasterDAL = objFactory.GetDALRepository().GetCustomerGroupMaster();
                objResponse = (SelectCustomerGroupLookUpResponse)objBaseCustomerGroupMasterDAL.SelectCustomerGroupLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCustomerGroupLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
    }
}
