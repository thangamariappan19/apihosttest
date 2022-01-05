using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MsSqlDAL.Common;
namespace PosAPI.Controllers
{
    public class BarcodeGenerationController : ApiController
    {
        public IHttpActionResult GetBarcode(string invoice)
        {
            try
            {
                MsSqlCommon common = new MsSqlCommon();
                var reponse = common.GetBarcode_Base64(invoice);
              
                return Ok(reponse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
