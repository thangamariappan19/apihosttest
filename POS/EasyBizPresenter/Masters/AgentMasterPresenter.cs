using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IAgent;
using EasyBizRequest.Masters.AgentMasterRequest;
using EasyBizResponse.Masters.AgentMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class AgentMasterPresenter
    {
         IAgentView _IAgentView;
        AgentBLL _AgentBLL = new AgentBLL();
        public AgentMasterPresenter(IAgentView ViewObj)
        {
            _IAgentView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IAgentView.AgentCode.Trim() == string.Empty)
            {
                _IAgentView.Message = "Agent Code is missing Please Enter it.";
            }
            else if (_IAgentView.AgentCode.Length > 8)
            {
                _IAgentView.Message = " Agent Code not allow more than eight Character.";
            }
            else if (_IAgentView.AgentName.Trim() == string.Empty)
            {
                _IAgentView.Message = "Agent Name is missing Please Enter it.";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveAgent()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveAgentRequest();
                    RequestData.AgentRecord = new AgentMaster();

                    RequestData.AgentRecord.ID = _IAgentView.ID;                   
                    RequestData.AgentRecord.AgentCode = _IAgentView.AgentCode;
                    RequestData.AgentRecord.AgentName = _IAgentView.AgentName;
                    RequestData.AgentRecord.Remarks = _IAgentView.Remarks;
                    RequestData.AgentRecord.CreateBy = _IAgentView.UserID;
                    RequestData.AgentRecord.CreateOn = DateTime.Now;
                    RequestData.AgentRecord.Active = _IAgentView.Active;
                    RequestData.AgentRecord.SCN = _IAgentView.SCN;
                    var ResponseData = _AgentBLL.SaveAgent(RequestData);
                    _IAgentView.Message = ResponseData.DisplayMessage;
                    _IAgentView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IAgentView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateAgent()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateAgentRequest();
                    RequestData.AgentRecord = new AgentMaster();
                    RequestData.AgentRecord.ID = _IAgentView.ID;
                    RequestData.AgentRecord.AgentCode = _IAgentView.AgentCode;
                    RequestData.AgentRecord.AgentName = _IAgentView.AgentName;
                    RequestData.AgentRecord.Remarks = _IAgentView.Remarks;
                    RequestData.AgentRecord.UpdateBy = _IAgentView.UserID;
                    RequestData.AgentRecord.UpdateOn = DateTime.Now;
                    RequestData.AgentRecord.Active = _IAgentView.Active;
                    RequestData.AgentRecord.SCN = _IAgentView.SCN;
                    var ResponseData = _AgentBLL.UpdateAgent(RequestData);
                    _IAgentView.Message = ResponseData.DisplayMessage;
                    _IAgentView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IAgentView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectAgentRecord()
        {
            try
            {
                var RequestData = new SelectByAgentIDRequest();
                RequestData.ID = _IAgentView.ID;
                var ResponseData = _AgentBLL.SelectAgentRecord(RequestData);
                _IAgentView.AgentCode = ResponseData.AgentRecord.AgentCode;
                _IAgentView.AgentName = ResponseData.AgentRecord.AgentName;
                _IAgentView.Remarks = ResponseData.AgentRecord.Remarks;
                _IAgentView.Active = ResponseData.AgentRecord.Active;
                _IAgentView.SCN = ResponseData.AgentRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IAgentView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IAgentView.Message = ResponseData.DisplayMessage;
                }
                _IAgentView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteAgent()
        {
            try
            {
                var RequestData = new DeleteAgentRequest();
                RequestData.ID = _IAgentView.ID;
                var ResponseData = _AgentBLL.DeleteAgent(RequestData);
                _IAgentView.Message = ResponseData.DisplayMessage;
                _IAgentView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
      public class AgentMasterListPresenter
   {

      AgentBLL _AgentBLL = new AgentBLL();
       IAgentCollectionView _IAgentCollectionView;
       public AgentMasterListPresenter(IAgentCollectionView ViewObj)
       {
           _IAgentCollectionView = ViewObj;
       }
       public void GetAgentList()
       {
           try
           {
               var RequestData = new SelectAllAgentRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllAgentResponse();
               ResponseData = _AgentBLL.SelectAllAgent(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IAgentCollectionView.AgentList = ResponseData.AgentList;
               }
               else
               {

               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
   }
}
