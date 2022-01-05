using EasyBizBLL.PatchFormBLL;
using EasyBizDBTypes.Transactions.POSOperations;
using EasyBizRequest.PatchFormRequest;
using EasyBizResponse.PatchFormResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class PatchFormController : ApiController
    {
        public IHttpActionResult PostPatch(PatchFormTypes _objPatch)
        {
            try
            {
                var RequestData = new SavePatchFormRequest();
                RequestData.PatchFormTypesRecord = new PatchFormTypes();
                RequestData.PatchFormTypesRecord = _objPatch;
                SavePatchFormResponse response = null;
                var ResponseData = new PatchFormBLL();
                response = ResponseData.SavePatchForm(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
