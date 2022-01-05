using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.AgentMasterRequest;
using EasyBizResponse.Masters.AgentMasterResponse;
using PosAPI.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class AgentController : ApiController
    {
        public IHttpActionResult GetAgent()
        {
            try
            {
				
				
                SelectAllAgentRequest request = new SelectAllAgentRequest();
                SelectAllAgentResponse response = null;
                var bll = new AgentBLL();
                response = bll.SelectAllAgent(request);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //Select by ID - Edit
        public IHttpActionResult GetagentDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByAgentIDRequest();
                RequestData.ID = ID;
                SelectByAgentIDResponse response = null;
                var ResponseData = new AgentBLL();
                response = ResponseData.SelectAgentRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostAgentMaster(AgentMaster _objAgentMaster)
        {
            try
            {
                var RequestData = new SaveAgentRequest();
                RequestData.AgentRecord = new AgentMaster();
                RequestData.AgentRecord = _objAgentMaster;
                SaveAgentResponse response = null;
                var ResponseData = new AgentBLL();
                response = ResponseData.SaveAgent(RequestData);
                if (response.StatusCode== Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutAgentMaster(AgentMaster _objAgentMaster)
        {
            try
            {
                var RequestData = new UpdateAgentRequest();
                RequestData.AgentRecord = new AgentMaster();
                RequestData.AgentRecord = _objAgentMaster;
                UpdateAgentResponse response = null;
                var ResponseData = new AgentBLL();
                response = ResponseData.UpdateAgent(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
