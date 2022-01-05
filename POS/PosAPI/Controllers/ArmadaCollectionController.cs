using EasyBizBLL.Masters;
using EasyBizRequest.Masters.ArmadaCollectionsMasterRequest;
using EasyBizResponse.Masters.ArmadaCollectionsMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ArmadaCollectionController : ApiController
    {
        public IHttpActionResult GetArmadaCollection()
        {
            try
            {
                var RequestData = new SelectArmadaCollectionLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectArmadaCollectionLookUpResponse response = null;
                var ResponseData = new ArmadaCollectionBLL();
                response = ResponseData.ArmadaCollectionLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
