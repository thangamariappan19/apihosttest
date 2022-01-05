using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.BinSubLevelRequest;
using EasyBizResponse.Masters.BinSubLevelResponse;
using PosAPI.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class BinLevelDetailsController : ApiController
    {
        public IHttpActionResult GetBinByStoreID(int ID)
        {
            try
            {
                var RequestData = new SelectAllBinLevelRequest();
                RequestData.ID = ID;
                SelectAllBinSubLevelResponse response = null;
                var ResponseData = new BinLevelDetailsBLL();
                response = ResponseData.SelectAllBinConfig(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetBinByStoreID(int ID, int LevelID)
        {
            try
            {
                var RequestData = new SelectByIDBinLevelRequest();
                RequestData.ID = ID;
                RequestData.LevelID = LevelID;
                SelectByIDBinLevelDetailsResponse response = null;
                var ResponseData = new BinLevelDetailsBLL();
                response = ResponseData.SelectBinConfigByID(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetBinByStoreID(int ID, int LevelID, string Code)
        {
            try
            {
                var RequestData = new SelectByIDBinLevelRequest();
                RequestData.ID = ID;
                RequestData.LevelID = LevelID;
                RequestData.HeaderCode = Code;
                SelectByIDBinLevelDetailsResponse response = null;
                var ResponseData = new BinLevelDetailsBLL();
                response = ResponseData.SelectRecordBinConfigByID(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostBinConfigMaster(BinLevelDetailsTypes _objBinConfig)
        {
            try
            {
                var RequestData = new SaveBinLevelRequest();
                RequestData.BinLevelDetailsRecord = new BinLevelDetailsTypes();
                //RequestData.BinLevelMasterRecord.StoreID = _objBinConfig.StoreID;
                RequestData.BinLevelDetailsList = _objBinConfig.BinLevelDetailsList;
                SaveBinSubLevelResponse response = null;
                var ResponseData = new BinLevelDetailsBLL();
                response = ResponseData.SaveBinConfigDetails(RequestData);
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
