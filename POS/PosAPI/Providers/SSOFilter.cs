using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PosAPI.Providers
{
    public class SSOFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //var str1 = "hi";
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //try
            //{
            //    ClaimsPrincipal principal = actionContext.Request.GetRequestContext().Principal as ClaimsPrincipal;
            //    var myClaim = principal.Claims.Where(x => x.Type == "LoginID").SingleOrDefault().Value;
            //    if (myClaim != null)
            //    {
            //        int LogInID = myClaim != null ? int.Parse(myClaim) : 0;
            //        var db = new DatabaseContext();
            //        var login = db.LogIns.Where(x => x.ID == LogInID).FirstOrDefault();
            //        if (login != null && login.LogoutTime != null)
            //        {
            //            actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            //        }
            //    }
            //}
            //catch { }
        }
    }
}