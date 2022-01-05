using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ExpenseMasterRequest;
using EasyBizResponse.Masters.ExpenseMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ExpenseMasterController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetExpensesList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllExpenseMasterRequest();

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



                SelectAllExpenseMasterResponse response = null;
                var ResponseData = new ExpenseMasterBLL();
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
        //public IHttpActionResult GetExpensesList()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllExpenseMasterRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        SelectAllExpenseMasterResponse response = null;
        //        var ResponseData = new ExpenseMasterBLL();
        //        response = ResponseData.SelectAllExpenseMaster(RequestData);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        public IHttpActionResult GetExpensesByID(int ID)
        {
            try
            {
                var RequestData = new SelectIDExpenseMasterRequest();
                RequestData.ID = ID;
                SelectIDExpenseMasterResponse response = null;
                var ResponseData = new ExpenseMasterBLL();
                response = ResponseData.SelectIDAllExpenseMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostExpenses(List<ExpenseMasterTypes> ExpenseMasterTypesData)
        {
            try
            {
                var RequestData = new SaveExpenseMasterRequest();
                RequestData.ExpenseMasterTypesData = ExpenseMasterTypesData;
                SaveExpenseMasterResponse response = null;
                var ResponseData = new ExpenseMasterBLL();
                response = ResponseData.SaveExpenseMaster(RequestData);
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
