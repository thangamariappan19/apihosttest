using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.BrandDivisionMapRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizResponse.Masters.BrandDivisionMapResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class BrandDivisionMappingController : ApiController
    {       
        [Authorize]
        public IHttpActionResult GetAllMappingRecord(int BrandID)
        {
            try
            {
                
                var RequestData = new SelectAllBrandDivisionRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.BrandID = BrandID;
                SelectAllBrandDivisionMapResponse response = null;
                var ResponseData = new BrandDivisionMapBLL();
                response = ResponseData.SelectAllBrandDivisionRecords(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult PostBrandDivisionMapping(List<BrandDivisionTypes> BrandDivisionList)
        {
            try
            {
                /*ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;*/

                var RequestData = new SaveBrandDivisionMapRequest();
               
                RequestData.BrandDivisionList = BrandDivisionList;
               
                SaveBrandDivisionMapResponse response = null;
                var ResponseData = new BrandDivisionMapBLL();
                response = ResponseData.SaveBrandDivision(RequestData);
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
