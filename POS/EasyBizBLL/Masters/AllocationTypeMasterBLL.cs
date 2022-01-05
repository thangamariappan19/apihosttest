using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.AllocationTypeMasterRequest;
using EasyBizResponse.Masters.AllocationTypeResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public  class AllocationTypeMasterBLL
    {
        public SaveAllocationTypeMasterResponse SaveAllocationTypeMaster(SaveAllocationTypeMasterRequest objRequest)
        {
            SaveAllocationTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseAllocationTypeMasterDAL = objFactory.GetDALRepository().GetAllocationTypeMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objAllocationTypeMaster = new AllocationTypeMaster();
                    objAllocationTypeMaster = (AllocationTypeMaster)objRequest.RequestDynamicData;
                    objRequest.AllocationTypeMasterData = objAllocationTypeMaster;
                }
                objResponse = (SaveAllocationTypeMasterResponse)objBaseAllocationTypeMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.AllocationTypeMasterData.ID = Convert.ToInt32(objResponse.IDs); 
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.ALLOCATIONTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AllocationTypeMasterBLL", "SaveAllocationTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveAllocationTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "AllocationType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllAllocationTypeMasterResponse SelectAllAllocationTypeMaster(SelectAllAllocationTypeMasterRequest objRequest)
        {
            SelectAllAllocationTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseAllocationTypeMasterDAL = objFactory.GetDALRepository().GetAllocationTypeMasterDAL();
                objResponse = (SelectAllAllocationTypeMasterResponse)objBaseAllocationTypeMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllAllocationTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "AllocationType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDAllocationTypeMasterResponse SelectAllocationTypeMasterRecord(SelectByIDAllocationTypeMasterRequest objRequest)
        {
            SelectByIDAllocationTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }   
                var objBaseAllocationTypeMasterDAL = objFactory.GetDALRepository().GetAllocationTypeMasterDAL();
                objResponse = (SelectByIDAllocationTypeMasterResponse)objBaseAllocationTypeMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDAllocationTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "AllocationType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateAllocationTypeMasterResponse UpdateAllocationTypeMaster(UpdateAllocationTypeMasterRequest objRequest)
        {
            UpdateAllocationTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseAllocationTypeMasterDAL = objFactory.GetDALRepository().GetAllocationTypeMasterDAL();
                if(objRequest.RequestDynamicData != null)
                {
                    var objAllocationTypeMaster = new AllocationTypeMaster();
                    objAllocationTypeMaster = (AllocationTypeMaster)objRequest.RequestDynamicData;
                    objRequest.AllocationTypeMasterData = objAllocationTypeMaster;
                }
                objResponse = (UpdateAllocationTypeMasterResponse)objBaseAllocationTypeMasterDAL.UpdateRecord(objRequest);
                 if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.AllocationTypeMasterData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.ALLOCATIONTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AllocationTypeMasterBLL", "UpdateAllocationTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateAllocationTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "AllocationType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteAllocationTypeMasterResponse DeleteAllocationTypeMaster(DeleteAllocationTypeMasterRequest objRequest)
        {
            DeleteAllocationTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseAllocationTypeMasterDAL = objFactory.GetDALRepository().GetAllocationTypeMasterDAL();
                objResponse = (DeleteAllocationTypeMasterResponse)objBaseAllocationTypeMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.AllocationTypeMasterData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.ALLOCATIONTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AllocationTypeMasterBLL", "DeleteAllocationTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteAllocationTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "AllocationType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllocationTypeMasterLookUpResponse SelectAllocationTypeMasterLookUp(SelectAllocationTypeMasterLookUpRequest objRequest)
        {
            SelectAllocationTypeMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseAllocationTypeMasterDAL = objFactory.GetDALRepository().GetAllocationTypeMasterDAL();
                objResponse = (SelectAllocationTypeMasterLookUpResponse)objBaseAllocationTypeMasterDAL.SelectAllocationTypeMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllocationTypeMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "AllocationType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
