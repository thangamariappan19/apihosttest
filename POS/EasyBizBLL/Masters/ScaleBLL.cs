using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizResponse.Masters.ScaleMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class ScaleBLL
    {
        public SaveScaleResponse SaveScale(SaveScaleRequest objRequest)
        {
            SaveScaleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseScaleMasterDAL = objFactory.GetDALRepository().GetScaleMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objScale = new ScaleMaster();
                    objScale = (ScaleMaster)objRequest.RequestDynamicData;
                    objRequest.ScaleRecord = objScale;
                    objRequest.ScaleDetailMasterList = objScale.ScaleDetailMasterList;
                    objRequest.BrandMasterList = objScale.BrandMasterList;
                }
                objResponse = (SaveScaleResponse)objBaseScaleMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
               //  objRequest.ScaleRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.SCALEMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;


                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ScaleBLL", "SaveScale");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveScaleResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Scale Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllScaleResponse API_SelectALL(SelectAllScaleRequest requestData)
        {
            SelectAllScaleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseScaleMasterDAL = objFactory.GetDALRepository().GetScaleMasterDAL();
                objResponse = (SelectAllScaleResponse)objBaseScaleMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllScaleResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Scale Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateScaleResponse UpdateScale(UpdateScaleRequest objRequest)
        {
            UpdateScaleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseScaleMasterDAL = objFactory.GetDALRepository().GetScaleMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objScale = new ScaleMaster();
                    objScale = (ScaleMaster)objRequest.RequestDynamicData;
                    objRequest.ScaleRecord = objScale;
                    var ObjScaleDetail = new ScaleDetailMaster();       
                     ObjScaleDetail = (ScaleDetailMaster)objRequest.RequestDynamicData;
                     objRequest.ScaleDetailMaster = ObjScaleDetail;
                  
                }
                objResponse = (UpdateScaleResponse)objBaseScaleMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.ScaleRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.SCALEMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ScaleBLL", "UpdateScale");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateScaleResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Scale Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteScaleResponse DeleteScale(DeleteScaleRequest objRequest)
        {
            DeleteScaleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseScaleMasterDAL = objFactory.GetDALRepository().GetScaleMasterDAL();
                objResponse = (DeleteScaleResponse)objBaseScaleMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.ScaleRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.SCALEMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ScaleBLL", "DeleteScale");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteScaleResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Scale Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllScaleResponse SelectAllScale(SelectAllScaleRequest objRequest)
        {
            SelectAllScaleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseScaleMasterDAL = objFactory.GetDALRepository().GetScaleMasterDAL();
                objResponse = (SelectAllScaleResponse)objBaseScaleMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllScaleResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Scale Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByScaleIDResponse SelectScaleRecord(SelectByScaleIDRequest objRequest)
        {
            SelectByScaleIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                var objBaseScaleMasterDAL = objFactory.GetDALRepository().GetScaleMasterDAL();
                objResponse = (SelectByScaleIDResponse)objBaseScaleMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByScaleIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Scale Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectScaleDetailsResponse SelectAllStoreGroupDetails(SelectScaleDetailsRequest objRequest)
        {
            SelectScaleDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseScaleDetailsDAL = objFactory.GetDALRepository().GetScaleMasterDAL();
                objResponse = (SelectScaleDetailsResponse)objBaseScaleDetailsDAL.SelectScaleDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectScaleDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectScaleDetailsResponse SelectAllStoreBrandDetails(SelectScaleDetailsRequest objRequest)
        {
            SelectScaleDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseScaleDetailsDAL = objFactory.GetDALRepository().GetScaleMasterDAL();
                objResponse = (SelectScaleDetailsResponse)objBaseScaleDetailsDAL.SelectScaleBrandDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectScaleDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectScaleLookUpResponse ScaleLookUp(SelectScaleLookUpRequest objRequest)
        {
            SelectScaleLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseScaleMasterDAL = objFactory.GetDALRepository().GetScaleMasterDAL();
                objResponse = (SelectScaleLookUpResponse)objBaseScaleMasterDAL.SelectScaleLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectScaleLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Scale Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectScaleDetailsLookUpResponse ScaleDetailsLookUp(SelectScaleDetailsLookUpRequest objRequest)
        {
            SelectScaleDetailsLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseScaleMasterDAL = objFactory.GetDALRepository().GetScaleMasterDAL();
                objResponse = (SelectScaleDetailsLookUpResponse)objBaseScaleMasterDAL.SelectScaleDetailsLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectScaleDetailsLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Scale Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
