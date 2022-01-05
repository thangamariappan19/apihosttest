using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizResponse.Masters.YearMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class YearBLL
    {
        public SaveYearResponse SaveYear(SaveYearRequest objRequest)
        {
            SaveYearResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseYearMasterDAL = objFactory.GetDALRepository().GetYearMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objYear = new YearMaster();
                    objYear = (YearMaster)objRequest.RequestDynamicData;
                    objRequest.YearRecord = objYear;
                }
                objResponse = (SaveYearResponse)objBaseYearMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.YearRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.YEAR;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.YearBLL", "SaveYear");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveYearResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Year Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateYearResponse UpdateYear(UpdateYearRequest objRequest)
        {
            UpdateYearResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseYearMasterDAL = objFactory.GetDALRepository().GetYearMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objYear = new YearMaster();
                    objYear = (YearMaster)objRequest.RequestDynamicData;
                    objRequest.YearRecord = objYear;
                }
                objResponse = (UpdateYearResponse)objBaseYearMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.YearRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.YEAR;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.YearBLL", "UpdateYear");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateYearResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Year Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteYearResponse DeleteYear(DeleteYearRequest objRequest)
        {
            DeleteYearResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseYearMasterDAL = objFactory.GetDALRepository().GetYearMasterDAL();
                objResponse = (DeleteYearResponse)objBaseYearMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.YearRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.YEAR;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.YearBLL", "DeleteYear");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteYearResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Year Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllYearResponse SelectAllYear(SelectAllYearRequest objRequest)
        {
            SelectAllYearResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseYearMasterDAL = objFactory.GetDALRepository().GetYearMasterDAL();
                objResponse = (SelectAllYearResponse)objBaseYearMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllYearResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Year Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllYearResponse API_SelectAllYear(SelectAllYearRequest objRequest)
        {
            SelectAllYearResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseYearMasterDAL = objFactory.GetDALRepository().GetYearMasterDAL();
                objResponse = (SelectAllYearResponse)objBaseYearMasterDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllYearResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Year Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByYearIDResponse SelectYearRecord(SelectByYearIDRequest objRequest)
        {
            SelectByYearIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }   
                var objBaseYearMasterDAL = objFactory.GetDALRepository().GetYearMasterDAL();
                objResponse = (SelectByYearIDResponse)objBaseYearMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByYearIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Year Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectYearLookUpResponse YearLookUp(SelectYearLookUpRequest objRequest)
        {
            SelectYearLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseYearMasterDAL = objFactory.GetDALRepository().GetYearMasterDAL();
                objResponse = (SelectYearLookUpResponse)objBaseYearMasterDAL.SelectYearLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectYearLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Year Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
