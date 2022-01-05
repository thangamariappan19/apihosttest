using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.CollectionMasterResponse;
using EasyBizRequest.Masters.SubCollectionRequest;
using EasyBizRequest.Masters.SubCollectionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    [Authorize]
    public class SubCollectionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetSubCollectionList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllSubCollectionRequest();

                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                //RequestData.Limit = limit == null || limit == ""  ? "10" : limit;
                //RequestData.Offset = offset == null || offset == "" ? "0" : offset;
                RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";



                SelectAllSubCollectionResponse response = null;
                var ResponseData = new SubCollectionBLL();
                response = ResponseData.API_SelectALL(RequestData);
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
        //public IHttpActionResult GetSubCollectionList()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllSubCollectionRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        SelectAllSubCollectionResponse response = null;
        //        var ResponseData = new SubCollectionBLL();
        //        response = ResponseData.SelectAllSubCollectionRecords(RequestData);
        //        //return Ok(response);
        //        if (response.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            return Ok(response);
        //        }
        //        else
        //        {
        //            return BadRequest(response.DisplayMessage);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        public IHttpActionResult GetSubCollectionByID(int ID)
        {
            try
            {
                var RequestData = new SelectSubCollectionListForCollectionRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.CollectionID = ID;
                SelectSubCollectionListForCollectionResponse response = null;
                var ResponseData = new SubCollectionBLL();
                response = ResponseData.SelectSubCollectionByCollection(RequestData);
                //return Ok(response);
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

        public IHttpActionResult PostSubCollection(List<SubCollectionMaster> SubCollectionMasterlist)
        {
            try
            {
                //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                //var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                //int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveSubCollectionRequest();
                RequestData.SubCollectionMasterlist = SubCollectionMasterlist;
                //RequestData.SubCollectionMasterData.CreateBy = UserID;
                SaveSubCollectionResponse response = null;
                var ResponseData = new SubCollectionBLL();
                response = ResponseData.SaveSubCollection(RequestData);
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
