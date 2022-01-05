using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizRequest.Masters.SubSeasonMasterRequest;
using EasyBizResponse.Masters.SeasonResponse;
using EasyBizResponse.Masters.StateMasterResponse;
using EasyBizResponse.Masters.SubSeasonMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class SeasonBLL
    {
        public SaveSeasonResponse SaveSeasonMaster(SaveSeasonRequest objRequest)
        {
            SaveSeasonResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseSeasonDAL objBaseSeasonDAL = objFactory.GetDALRepository().GetSeasonDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objSeasonMaster = new SeasonMaster();
                    objSeasonMaster = (SeasonMaster)objRequest.RequestDynamicData;
                    objRequest.SeasonRecord = objSeasonMaster;
                }
                objResponse = (SaveSeasonResponse)objBaseSeasonDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.SeasonRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.SEASON;
                    objRequest.ProcessMode = Enums.ProcessMode.New;


                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SeasonBLL", "SaveSeasonMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveSeasonResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Season Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateSeasonResponse UpdateSeasonMaster(UpdateSeasonRequest objRequest)
        {
            UpdateSeasonResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseSeasonDAL objBaseSeasonDAL = objFactory.GetDALRepository().GetSeasonDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objSeasonMaster = new SeasonMaster();
                    objSeasonMaster = (SeasonMaster)objRequest.RequestDynamicData;
                    objRequest.SeasonMasterData = objSeasonMaster;
                }
                objResponse = (UpdateSeasonResponse)objBaseSeasonDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.SeasonMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.SEASON;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SeasonBLL", "UpdateSeasonMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateSeasonResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Season Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteSeasonResponse DeleteSeasonMaster (DeleteSeasonRequest objRequest)
        {
            DeleteSeasonResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseSeasonDAL objBaseSeasonDAL = objFactory.GetDALRepository().GetSeasonDAL();
                objResponse = (DeleteSeasonResponse)objBaseSeasonDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.SeasonMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.SEASON;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SeasonBLL", "DeleteSeasonMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteSeasonResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Season Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllSeasonResponse SelectAllSeasonMaster(SelectAllSeasonRequest objRequest)
        {
            var _SubSeasonBLL = new SubSeasonBLL();   
            SelectAllSeasonResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseSeasonDAL objBaseSeasonDAL = objFactory.GetDALRepository().GetSeasonDAL();
                objResponse = (SelectAllSeasonResponse)objBaseSeasonDAL.SelectAll(objRequest);
                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var SubSeasonList = new List<SubSeasonMaster>();
                    foreach (SeasonMaster objSeason in objResponse.SeasonMasterList)
                    {
                        var objSelectSeasonListForSubSeasonRequest = new SelectSeasonListForSubSeasonRequest();
                        objSelectSeasonListForSubSeasonRequest.SeasonID = objSeason.ID;
                        objSelectSeasonListForSubSeasonRequest.ShowInActiveRecords = true;
                        var objSelectSeasonListForSubSeasonResponse = new SelectSeasonListForSubSeasonResponse();
                        objSelectSeasonListForSubSeasonResponse = _SubSeasonBLL.SubSeasonBySeason(objSelectSeasonListForSubSeasonRequest);
                        if (objSelectSeasonListForSubSeasonResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSeason.SubSeasonList = objSelectSeasonListForSubSeasonResponse.SubSeasonList;
                        }
                        else
                        {
                            objSeason.SubSeasonList = new List<SubSeasonMaster>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllSeasonResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Season Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllSeasonResponse API_SelectAllSeasonMaster(SelectAllSeasonRequest objRequest)
        {
            var _SubSeasonBLL = new SubSeasonBLL();
            SelectAllSeasonResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseSeasonDAL objBaseSeasonDAL = objFactory.GetDALRepository().GetSeasonDAL();
                objResponse = (SelectAllSeasonResponse)objBaseSeasonDAL.API_SelectALL(objRequest);
                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var SubSeasonList = new List<SubSeasonMaster>();
                    foreach (SeasonMaster objSeason in objResponse.SeasonMasterList)
                    {
                        var objSelectSeasonListForSubSeasonRequest = new SelectSeasonListForSubSeasonRequest();
                        objSelectSeasonListForSubSeasonRequest.SeasonID = objSeason.ID;
                        objSelectSeasonListForSubSeasonRequest.ShowInActiveRecords = true;
                        var objSelectSeasonListForSubSeasonResponse = new SelectSeasonListForSubSeasonResponse();
                        objSelectSeasonListForSubSeasonResponse = _SubSeasonBLL.SubSeasonBySeason(objSelectSeasonListForSubSeasonRequest);
                        if (objSelectSeasonListForSubSeasonResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSeason.SubSeasonList = objSelectSeasonListForSubSeasonResponse.SubSeasonList;
                        }
                        else
                        {
                            objSeason.SubSeasonList = new List<SubSeasonMaster>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllSeasonResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Season Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectBySeasonIDResponse SelectSeasonMaster(SelectBySeasonIDRequest objRequest)
        {
            SelectBySeasonIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                BaseSeasonDAL objBaseSeasonDAL = objFactory.GetDALRepository().GetSeasonDAL();
                objResponse = (SelectBySeasonIDResponse)objBaseSeasonDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectBySeasonIDResponse();
                objResponse.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Season Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectSeasonLookUpResponse SelectSeasonLookUp(SelectSeasonLookUpRequest objRequest)
        {
            SelectSeasonLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseSeasonDAL objBaseSeasonDAL = objFactory.GetDALRepository().GetSeasonDAL();
                objResponse = (SelectSeasonLookUpResponse)objBaseSeasonDAL.SelectSeasonLookUp(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectSeasonLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Season Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}

