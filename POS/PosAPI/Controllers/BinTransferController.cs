using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.BinTransferMasterRequest;
using EasyBizResponse.Masters.BinTransferResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class BinTransferController : ApiController
    {
        public IHttpActionResult GetBinBySKUCode(string SKUCode,string FromBin)
        {
            try
            {
                var RequestData = new SelectBinDetailsBySKUCodeRequest();
                RequestData.SKUCode = SKUCode;
                RequestData.FromBin = FromBin;
                SelectBinDetailsBySKUCodeResponse response = null;
                var ResponseData = new BinTransferMasterBLL();
                response = ResponseData.SelectBinTransferDetails(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutBinTransferDetails(List<BinLogTypes> objRequest)
        {
            try
            {
                var RequestData = new SaveBinTransferRequest();
                RequestData.BinLogList = objRequest;
                SaveBinTransferResponse response = null;
                var ResponseData = new BinTransferMasterBLL();
                response = ResponseData.UpdateBinTransferDetails(RequestData);
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
