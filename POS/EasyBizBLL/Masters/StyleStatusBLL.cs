using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.StyleStatusMasterRequest;
using EasyBizRequest.Masters.StyleStatusMasterResponse;
using EasyBizResponse.Masters.StyleStatusMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class StyleStatusBLL
    {

        public SaveStyleStatusMasterResponse SaveStyleStatus(SaveStyleStatusMasterRequest objRequest)
        {
            SaveStyleStatusMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseStyleStatusMasterDAL = objFactory.GetDALRepository().GetStyleStatusMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStyleStatusMasterTypes = new StyleStatusMasterType();
                    objStyleStatusMasterTypes = (StyleStatusMasterType)objRequest.RequestDynamicData;
                    objRequest.StyleStatusMasterTypeRecord = objStyleStatusMasterTypes;
                }
                objResponse = (SaveStyleStatusMasterResponse)objBaseStyleStatusMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.StyleStatusMasterTypeRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STYLESTATUS;
                    objRequest.ProcessMode = Enums.ProcessMode.New;


                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StyleStatusBLL", "SaveStyleStatus");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStyleStatusMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Status Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllStyleStatusMasterResponse API_SelectALL(SelectAllStyleStatusMasterRequest requestData)
        {
            SelectAllStyleStatusMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStyleStatusMasterDAL = objFactory.GetDALRepository().GetStyleStatusMasterDAL();
                objResponse = (SelectAllStyleStatusMasterResponse)objBaseStyleStatusMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStyleStatusMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Status Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateStyleStatusMasterResponse UpdateStyleStatus(UpdateStyleStatusMasterRequest objRequest)
        {
            UpdateStyleStatusMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                var objBaseStyleStatusMasterDAL = objFactory.GetDALRepository().GetStyleStatusMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStyleStatusMasterTypes = new StyleStatusMasterType();
                    objStyleStatusMasterTypes = (StyleStatusMasterType)objRequest.RequestDynamicData;
                    objRequest.StyleStatusMasterTypeRecord = objStyleStatusMasterTypes;
                }
                objResponse = (UpdateStyleStatusMasterResponse)objBaseStyleStatusMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.StyleStatusMasterTypeRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.STYLESTATUS;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StyleStatusBLL", "UpdateStyleStatus");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateStyleStatusMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Status Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }



        public DeleteStyleStatusMasterResponse DeleteStyleStatus(DeleteStyleStatusMasterRequest objRequest)
        {
            DeleteStyleStatusMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                var objBaseStyleStatusMasterDAL = objFactory.GetDALRepository().GetStyleStatusMasterDAL();
                objResponse = (DeleteStyleStatusMasterResponse)objBaseStyleStatusMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.StyleStatusMasterTypeRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.STYLESTATUS;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StyleStatusBLL", "DeleteStyleStatus");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteStyleStatusMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Status Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllStyleStatusMasterResponse SelectAllStyleStatus(SelectAllStyleStatusMasterRequest objRequest)
        {
            SelectAllStyleStatusMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStyleStatusMasterDAL = objFactory.GetDALRepository().GetStyleStatusMasterDAL();
                objResponse = (SelectAllStyleStatusMasterResponse)objBaseStyleStatusMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStyleStatusMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Status Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectByIDStyleStatusMasterResponse SelectByIDStyleStatus(SelectByIDStyleStatusMasterRequest objRequest)
        {
            SelectByIDStyleStatusMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == null || objRequest.ID == 0)
                {
                    if(!string.IsNullOrEmpty(objRequest.DocumentIDs))
                    {
                        int id;
                        int.TryParse(objRequest.DocumentIDs, out id);
                        objRequest.ID = id;
                    }                    
                } 
                var objBaseStyleStatusMasterDAL = objFactory.GetDALRepository().GetStyleStatusMasterDAL();
                objResponse = (SelectByIDStyleStatusMasterResponse)objBaseStyleStatusMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDStyleStatusMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Status Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectStyleStatusLookUpResponse SelectStyleStatusLookUp(SelectStyleStatusLookUpRequest objRequest)
        {
            SelectStyleStatusLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStyleStatusMasterDAL = objFactory.GetDALRepository().GetStyleStatusMasterDAL();
                objResponse = (SelectStyleStatusLookUpResponse)objBaseStyleStatusMasterDAL.SelectStyleStatusLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectStyleStatusLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Status Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


    }
}
