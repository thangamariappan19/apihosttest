using EasyBizBLL.Masters;
using EasyBizRequest.Masters.PrevilegesRequest;
using EasyBizResponse.Masters.PrevilegesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class MenuScreenNamesController : ApiController
    {
        public IHttpActionResult GetMenuScreenNames()
        {
            try
            {
                GetScreenNamesRequest request = new GetScreenNamesRequest();
                GetScreenNamesResponse response = null;
                var bll = new PrevilegesBLL();
                response = bll.POSScreenNames(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
