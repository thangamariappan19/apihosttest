using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.SubSeasonMasterRequest;
using EasyBizResponse.Masters.SubSeasonMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class SubSeasonBLL
    {
        public SaveSubSeasonResponse SaveSubSeason(SaveSubSeasonRequest objRequest)
        {
            SaveSubSeasonResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseSubSeasonDAL = objFactory.GetDALRepository().GetSubSeasonMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objSubSeason = new SubSeasonMaster();
                    objSubSeason = (SubSeasonMaster)objRequest.RequestDynamicData;
                    objRequest.SubSeasonlist = objSubSeason.SubSeasonlist;
                }
                objResponse = (SaveSubSeasonResponse)objBaseSubSeasonDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.RequestFrom = Enums.RequestFrom.MainServer;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.SUBSEASON;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SubSeasonBLL", "SaveSubSeason");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveSubSeasonResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SubSeason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteSubSeasonResponse DeleteSubSeason(DeleteSubSeasonRequest objRequest)
        {
            DeleteSubSeasonResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseSubSeasonDAL = objFactory.GetDALRepository().GetSubSeasonMasterDAL();
                objResponse = (DeleteSubSeasonResponse)objBaseSubSeasonDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;                   
                    //objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.SUBSEASON;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SubSeasonBLL", "DeleteSubSeason");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteSubSeasonResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "SubSeason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllSubSeasonResponse SelectAllSubSeasonRecords(SelectAllSubSeasonRequest objRequest)
        {
            SelectAllSubSeasonResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSubSeasonDAL = objFactory.GetDALRepository().GetSubSeasonMasterDAL();
                objResponse = (SelectAllSubSeasonResponse)objBaseSubSeasonDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSubSeasonResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SubSeason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public UpdateSubSeasonResponse UpdateSubSeason(UpdateSubSeasonRequest objRequest)
        {
            UpdateSubSeasonResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseSubSeasonDAL = objFactory.GetDALRepository().GetSubSeasonMasterDAL();
                objResponse = (UpdateSubSeasonResponse)objBaseSubSeasonDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.SubSeasonData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.SUBSEASON;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;
                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SubSeasonBLL", "UpdateSubSeason");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateSubSeasonResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "SubSeason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectBySubSeasonIDResponse SelectSubSeasonRecord(SelectBySubSeasonIDRequest objRequest)
        {
            SelectBySubSeasonIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSubSeasonDAL = objFactory.GetDALRepository().GetSubSeasonMasterDAL();
                objResponse = (SelectBySubSeasonIDResponse)objBaseSubSeasonDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectBySubSeasonIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SubSeason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectSubSeasonLookUpResponse SubSeasonLookUp(SelectSubSeasonLookUpRequest objRequest)
        {
            SelectSubSeasonLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSubSeasonDAL = objFactory.GetDALRepository().GetSubSeasonMasterDAL();
                objResponse = (SelectSubSeasonLookUpResponse)objBaseSubSeasonDAL.SelectSubSeasonLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectSubSeasonLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "SubSeason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectSeasonListForSubSeasonResponse SubSeasonBySeason(SelectSeasonListForSubSeasonRequest objRequest)
        {
            SelectSeasonListForSubSeasonResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSubSeasonDAL = objFactory.GetDALRepository().GetSubSeasonMasterDAL();
                objResponse = (SelectSeasonListForSubSeasonResponse)objBaseSubSeasonDAL.SelectSubSeasonListBySeason(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectSeasonListForSubSeasonResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SubSeason Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
    }
}
