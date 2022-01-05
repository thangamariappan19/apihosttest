using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizResponse.Masters.StoreGroupResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class StoreGroupBLL
    {
        public SaveStoreGroupResponse SaveStoreGroupMaster(SaveStoreGroupRequest objRequest)
        {
            SaveStoreGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseStoreGroupMasterDAL = objFactory.GetDALRepository().GetStoreGroupMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStoreGroupMaster = new StoreGroupMaster();
                    objStoreGroupMaster = (StoreGroupMaster)objRequest.RequestDynamicData;
                    objRequest.StoreGroupMasterData = objStoreGroupMaster;
                    objRequest.StoreGroupDetailsList = objStoreGroupMaster.StoreGroupDetailsList;
                }
                objResponse = (SaveStoreGroupResponse)objBaseStoreGroupMasterDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.StoreGroupMasterData.ID = Convert.ToInt32(objResponse.IDs);
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.STOREGROUP;
                //    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;


                //    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StoreGroupBLL", "SaveStoreGroupMaster");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveStoreGroupResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllStoreGroupResponse API_SelectALL(SelectAllStoreGroupRequest requestData)
        {
            SelectAllStoreGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreGroupMasterDAL = objFactory.GetDALRepository().GetStoreGroupMasterDAL();
                objResponse = (SelectAllStoreGroupResponse)objBaseStoreGroupMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStoreGroupResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllStoreGroupResponse SelectAllStoreGroupMaster(SelectAllStoreGroupRequest objRequest)
        {
            SelectAllStoreGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreGroupMasterDAL = objFactory.GetDALRepository().GetStoreGroupMasterDAL();
                objResponse = (SelectAllStoreGroupResponse)objBaseStoreGroupMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStoreGroupResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDStoreGroupResponse SelectStoreGroupMasterRecord(SelectByIDStoreGroupRequest objRequest)
        {
            SelectByIDStoreGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                var objBaseStoreGroupMasterDAL = objFactory.GetDALRepository().GetStoreGroupMasterDAL();
                objResponse = (SelectByIDStoreGroupResponse)objBaseStoreGroupMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDStoreGroupResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateStoreGroupResponse UpdateStoreGroupMaster(UpdateStoreGroupRequest objRequest)
        {
            UpdateStoreGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseStoreGroupMasterDAL = objFactory.GetDALRepository().GetStoreGroupMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStoreGroupMaster = new StoreGroupMaster();
                    objStoreGroupMaster = (StoreGroupMaster)objRequest.RequestDynamicData;
                    objRequest.StoreGroupMasterData = objStoreGroupMaster;
                    
                }
                objResponse = (UpdateStoreGroupResponse)objBaseStoreGroupMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.StoreGroupMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.STOREGROUP;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StoreGroupBLL", "UpdateStoreGroupMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateStoreGroupResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteStoreGroupResponse DeleteStoreGroupMaster(DeleteStoreGroupRequest objRequest)
        {
            DeleteStoreGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseStoreGroupMasterDAL = objFactory.GetDALRepository().GetStoreGroupMasterDAL();
                objResponse = (DeleteStoreGroupResponse)objBaseStoreGroupMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.StoreGroupMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.STOREGROUP;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StoreGroupBLL", "DeleteStoreGroupMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteStoreGroupResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectStoreGroupLookUpResponse SelectStoreGroupMasterLookUp(SelectStoreGroupLookUpRequest objRequest)
        {
            SelectStoreGroupLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreGroupMasterDAL = objFactory.GetDALRepository().GetStoreGroupMasterDAL();
                objResponse = (SelectStoreGroupLookUpResponse)objBaseStoreGroupMasterDAL.SelectStoreGroupLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectStoreGroupLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectAllStoreGroupDetailsResponse SelectAllStoreGroupDetails(SelectAllStoreGroupDetailsRequest objRequest)
        {
            SelectAllStoreGroupDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreGroupMasterDAL = objFactory.GetDALRepository().GetStoreGroupMasterDAL();
                objResponse = (SelectAllStoreGroupDetailsResponse)objBaseStoreGroupMasterDAL.SelectAllStoreGroupDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStoreGroupDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectStoreGroupDetailsResponse SelectStoreGroupDetails(SelectStoreGroupDetailsRequest objRequest)
        {
            SelectStoreGroupDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreGroupMasterDAL = objFactory.GetDALRepository().GetStoreGroupMasterDAL();
                objResponse = (SelectStoreGroupDetailsResponse)objBaseStoreGroupMasterDAL.SelectStoreGroupDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectStoreGroupDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
    }
}
