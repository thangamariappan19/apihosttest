using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.BinRequest;
using EasyBizResponse.Masters.BinMasterRespose;
using PosAPI.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class BinLevelController : ApiController
    {
        public IHttpActionResult GetBinByStoreID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDBinMasterRequest();
                RequestData.ID = ID;
                SelectAllBinConfigMasterResponse response = null;
                var ResponseData = new BinLevelMasterBLL();
                response = ResponseData.SelectAllBinConfig(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult PostBinConfigMaster(BinLevelMasterTypes _objBinConfig)
        {
            try
            {
                var RequestData = new SaveBinLevelMasterRequest();
                RequestData.BinLevelMasterRecord = new BinLevelMasterTypes();
                RequestData.BinLevelMasterRecord.StoreID = _objBinConfig.StoreID;
                RequestData.BinLevelMasterRecord.BinLevelMasterList = _objBinConfig.BinLevelMasterList;
                SaveBinLevelResponse response = null;
                var ResponseData = new BinLevelMasterBLL();
                response = ResponseData.SaveBinConfigMaster(RequestData);
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
