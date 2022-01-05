using EasyBizBLL.Transactions.Coupens;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizResponse.Masters.CouponMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class CouponLookUpController : ApiController
    {
        public IHttpActionResult GetCouponList()
        {
            try
            {
                var RequestData = new SelectAllCouponMasterRequest();
                RequestData.ShowInActiveRecords = false;
                SelectAllCouponMasterResponse response = null;
                var ResponseData = new CouponMasterBLL();
                response = ResponseData.SelectAllCouponMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetCouponList(int i)
        {
            try
            {
                var RequestData = new SelectCouponMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectCouponMasterLookUpResponse response = null;
                var ResponseData = new CouponMasterBLL();
                response = ResponseData.SelectCouponMasterLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
