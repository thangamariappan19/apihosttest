using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizResponse.Masters.CollectionMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CollectionMasterLookUpController : ApiController
    {
        public IHttpActionResult GetCollectionMasterLookUp()
        {
            try
            {
                var RequestData = new SelectCollectionLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectCollectionLookUpResponse response = null;
                var ResponseData = new CollectionMasterBLL();
                response = ResponseData.CollectionLookUp(RequestData);
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
