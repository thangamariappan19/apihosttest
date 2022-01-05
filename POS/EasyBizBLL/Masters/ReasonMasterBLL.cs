using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.ReasonMasterRequest;
using EasyBizResponse.Masters.ReasonMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class ReasonMasterBLL
    {
        public SaveReasonMasterResponse SaveReasonMaster(SaveReasonMasterRequest objRequest)
        {
            SaveReasonMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseReasonMasterDAL = objFactory.GetDALRepository().GetReasonMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objReasonMaster = new ReasonMaster();
                    objReasonMaster = (ReasonMaster)objRequest.RequestDynamicData;
                    objRequest.ReasonMasterData = objReasonMaster;
                }
                objResponse = (SaveReasonMasterResponse)objBaseReasonMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.ReasonMasterData.ReasonID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.REASON;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ReasonMasterBLL", "SaveReasonMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveReasonMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Reason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectReasonMasterLookUpResponse API_SelectReasonMasterLookUp(SelectReasonMasterLookUpRequest objRequest)
        {
            SelectReasonMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseReasonMasterDAL = objFactory.GetDALRepository().GetReasonMasterDAL();
                objResponse = (SelectReasonMasterLookUpResponse)objBaseReasonMasterDAL.API_SelectReasonMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectReasonMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Reason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectAllReasonMasterResponse SelectAllReasonMaster(SelectAllReasonMasterRequest objRequest)
        {
            SelectAllReasonMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseReasonMasterDAL = objFactory.GetDALRepository().GetReasonMasterDAL();
                objResponse = (SelectAllReasonMasterResponse)objBaseReasonMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllReasonMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Reason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllReasonMasterResponse API_SelectAllReasonMaster(SelectAllReasonMasterRequest objRequest)
        {
            SelectAllReasonMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseReasonMasterDAL = objFactory.GetDALRepository().GetReasonMasterDAL();
                objResponse = (SelectAllReasonMasterResponse)objBaseReasonMasterDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllReasonMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Reason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByIDReasonMasterResponse SelectReasonMasterRecord(SelectByIDReasonMasterRequest objRequest)
        {
            SelectByIDReasonMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseReasonMasterDAL = objFactory.GetDALRepository().GetReasonMasterDAL();
                objResponse = (SelectByIDReasonMasterResponse)objBaseReasonMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDReasonMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Reason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateReasonMasterResponse UpdateReasonMaster(UpdateReasonMasterRequest objRequest)
        {
            UpdateReasonMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseReasonMasterDAL = objFactory.GetDALRepository().GetReasonMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objReasonMaster = new ReasonMaster();
                    objReasonMaster = (ReasonMaster)objRequest.RequestDynamicData;
                    objRequest.ReasonMasterData = objReasonMaster;
                }
                objResponse = (UpdateReasonMasterResponse)objBaseReasonMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.ReasonMasterData.ReasonID);
                    objRequest.DocumentType = Enums.DocumentType.REASON;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ReasonMasterBLL", "UpdateReasonMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateReasonMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Reason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteReasonMasterResponse DeleteReasonMaster(DeleteReasonMasterRequest objRequest)
        {
            DeleteReasonMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseReasonMasterDAL = objFactory.GetDALRepository().GetReasonMasterDAL();
                objResponse = (DeleteReasonMasterResponse)objBaseReasonMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.ReasonMasterData.ReasonID);
                    objRequest.DocumentType = Enums.DocumentType.REASON;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ReasonMasterBLL", "DeleteReasonMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteReasonMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Reason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectReasonMasterLookUpResponse SelectReasonMasterLookUp(SelectReasonMasterLookUpRequest objRequest)
        {
            SelectReasonMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseReasonMasterDAL = objFactory.GetDALRepository().GetReasonMasterDAL();
                objResponse = (SelectReasonMasterLookUpResponse)objBaseReasonMasterDAL.SelectReasonMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectReasonMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Reason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
