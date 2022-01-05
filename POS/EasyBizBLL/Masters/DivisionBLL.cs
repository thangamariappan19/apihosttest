using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.DivisionMasterRequest;
using EasyBizResponse.Masters.DivisionMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
  public  class DivisionBLL
    {
        public SaveDivisionResponse SaveDivision(SaveDivisionRequest objRequest)
        {
            SaveDivisionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseDivisionMasterDAL = objFactory.GetDALRepository().GetDivisionMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objDivision = new DivisionMaster();
                    objDivision = (DivisionMaster)objRequest.RequestDynamicData;
                    objRequest.DivisionRecord = objDivision;
                }
                objResponse = (SaveDivisionResponse)objBaseDivisionMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DivisionRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.DIVISION;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DivisionBLL", "SaveDivision");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveDivisionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Division Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateDivisionResponse UpdateDivision(UpdateDivisionRequest objRequest)
        {
            UpdateDivisionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseDivisionMasterDAL = objFactory.GetDALRepository().GetDivisionMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objDivision = new DivisionMaster();
                    objDivision = (DivisionMaster)objRequest.RequestDynamicData;
                    objRequest.DivisionRecord = objDivision;
                }
                objResponse = (UpdateDivisionResponse)objBaseDivisionMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.DivisionRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.DIVISION;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DivisionBLL", "UpdateDivision");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateDivisionResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Division Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteDivisionResponse DeleteDivision(DeleteDivisionRequest objRequest)
        {
            DeleteDivisionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseDivisionMasterDAL = objFactory.GetDALRepository().GetDivisionMasterDAL();
                objResponse = (DeleteDivisionResponse)objBaseDivisionMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.DivisionRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.DIVISION;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DivisionBLL", "DeleteDivision");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteDivisionResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Division Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllDivisionResponse SelectAllDivision(SelectAllDivisionRequest objRequest)
        {
            SelectAllDivisionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseDivisionMasterDAL = objFactory.GetDALRepository().GetDivisionMasterDAL();
                objResponse = (SelectAllDivisionResponse)objBaseDivisionMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllDivisionResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Division Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllDivisionResponse API_SelectAllDivision(SelectAllDivisionRequest objRequest)
        {
            SelectAllDivisionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseDivisionMasterDAL = objFactory.GetDALRepository().GetDivisionMasterDAL();
                objResponse = (SelectAllDivisionResponse)objBaseDivisionMasterDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllDivisionResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Division Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByDivisionIDResponse SelectDivisionRecord(SelectByDivisionIDRequest objRequest)
        {
            SelectByDivisionIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseDivisionMasterDAL = objFactory.GetDALRepository().GetDivisionMasterDAL();
                objResponse = (SelectByDivisionIDResponse)objBaseDivisionMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByDivisionIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Division Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectDivisionLookUpResponse DivisionLookUp(SelectDivisionLookUpRequest objRequest)
        {
            SelectDivisionLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseDivisionMasterDAL = objFactory.GetDALRepository().GetDivisionMasterDAL();
                objResponse = (SelectDivisionLookUpResponse)objBaseDivisionMasterDAL.SelectDivisionLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectDivisionLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Division Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
