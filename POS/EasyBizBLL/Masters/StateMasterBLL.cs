using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizResponse.Masters.StateMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class StateMasterBLL
    {
        public SaveStateResponse SaveStateMaster(SaveStateRequest objRequest)
        {
            SaveStateResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseStateMasterDAL = objFactory.GetDALRepository().GetStateDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStateMaster = new StateMaster();
                    objStateMaster = (StateMaster)objRequest.RequestDynamicData;
                    objRequest.StateRecord = objStateMaster;
                }
                objResponse = (SaveStateResponse)objBaseStateMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.StateRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STATE;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StateMasterBLL", "SaveStateMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStateResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "State Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectStateAloneLookUPResponse SelectStateAloneLookUp(SelectStateAloneLookUPRequest objRequest)
        {
            SelectStateAloneLookUPResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseStateMasterDAL objBasestateDAL = objFactory.GetDALRepository().GetStateDAL();
                objResponse = (SelectStateAloneLookUPResponse)objBasestateDAL.SelectStateAloneLookup(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectStateAloneLookUPResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "State Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllStateResponse API_SelectAllStateMaster(SelectAllStateRequest objRequest)
        {
            SelectAllStateResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStateMasterDAL = objFactory.GetDALRepository().GetStateDAL();
                objResponse = (SelectAllStateResponse)objBaseStateMasterDAL.API_SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStateResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "State Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateStateResponse UpdateState(UpdateStateRequest objRequest)
        {
            UpdateStateResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseStateMasterDAL = objFactory.GetDALRepository().GetStateDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStateMaster = new StateMaster();
                    objStateMaster = (StateMaster)objRequest.RequestDynamicData;
                    objRequest.StateRecord = objStateMaster;
                }
                objResponse = (UpdateStateResponse)objBaseStateMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.StateRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.STATE;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StateMasterBLL", "UpdateState");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateStateResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "State Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteStateResponse DeleteStateMaster(DeleteStateRequest objRequest)
        {
            DeleteStateResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseStateMasterDAL = objFactory.GetDALRepository().GetStateDAL();
                objResponse = (DeleteStateResponse)objBaseStateMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest..ID);
                    objRequest.DocumentType = Enums.DocumentType.STATE;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StateMasterBLL", "DeleteStateMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteStateResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "State Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllStateResponse SelectAllRecordStateMaster(SelectAllStateRequest objRequest)
        {
            SelectAllStateResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStateMasterDAL = objFactory.GetDALRepository().GetStateDAL();
                objResponse = (SelectAllStateResponse)objBaseStateMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStateResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "State Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByStateIDResponse SelectStateRecord(SelectByStateIDRequest objRequest)
        {
            SelectByStateIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseStateMasterDAL = objFactory.GetDALRepository().GetStateDAL();
                objResponse = (SelectByStateIDResponse)objBaseStateMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStateIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "State Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectStateLookUpResponse SelectStateLookUp(SelectStateLookUpRequest objRequest)
        {
            SelectStateLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStateMasterDAL = objFactory.GetDALRepository().GetStateDAL();
                objResponse = (SelectStateLookUpResponse)objBaseStateMasterDAL.SelectStateLookUp(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectStateLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "State Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

    }
 }
   
    
   

