using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.AgentMasterRequest;
using EasyBizResponse.Masters.AgentMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class AgentBLL
    {
        public SaveAgentResponse SaveAgent(SaveAgentRequest objRequest)
        {
            SaveAgentResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if(objRequest.RequestDynamicData != null)
                {
                    objRequest.AgentRecord = (AgentMaster)objRequest.RequestDynamicData;
                }
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetAgentMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objAgent = new AgentMaster();
                    objAgent = (AgentMaster)objRequest.RequestDynamicData;
                    objRequest.AgentRecord = objAgent;
                 
                }
                objResponse = (SaveAgentResponse)objBaseAgentMasterDAL.InsertRecord(objRequest);

                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.AgentRecord.ID = Convert.ToInt32(objResponse.IDs);                    
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentNos = objRequest.AgentRecord.AgentCode;
                    objRequest.DocumentType = Enums.DocumentType.AGENTMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AgentBLL", "SaveAgent");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveAgentResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Agent Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateAgentResponse UpdateAgent(UpdateAgentRequest objRequest)
        {
            UpdateAgentResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetAgentMasterDAL();
                if(objRequest.RequestDynamicData != null)
                {
                    var objAgent = new AgentMaster();
                    objAgent = (AgentMaster)objRequest.RequestDynamicData;
                    objRequest.AgentRecord = objAgent;

                }
                objResponse = (UpdateAgentResponse)objBaseAgentMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.AgentRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.AGENTMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AgentBLL", "UpdateAgent");
                }

            }
            catch (Exception ex)
            {
                objResponse = new UpdateAgentResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Agent Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteAgentResponse DeleteAgent(DeleteAgentRequest objRequest)
        {
            DeleteAgentResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetAgentMasterDAL();
                objResponse = (DeleteAgentResponse)objBaseAgentMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.AgentRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.AGENTMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AgentBLL", "DeleteAgent");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteAgentResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Agent Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllAgentResponse SelectAllAgent(SelectAllAgentRequest objRequest)
        {
            SelectAllAgentResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetAgentMasterDAL();
                objResponse = (SelectAllAgentResponse)objBaseAgentMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllAgentResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Agent Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByAgentIDResponse SelectAgentRecord(SelectByAgentIDRequest objRequest)
        {
            SelectByAgentIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if(objRequest.ID == 0 )
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }               
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetAgentMasterDAL();
                objResponse = (SelectByAgentIDResponse)objBaseAgentMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByAgentIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Agent Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAgentLookUpResponse VendorAgentLookUp(SelectAgentLookUpRequest objRequest)
        {
            SelectAgentLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetAgentMasterDAL();
                objResponse = (SelectAgentLookUpResponse)objBaseAgentMasterDAL.SelectAgentLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAgentLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Agent Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
