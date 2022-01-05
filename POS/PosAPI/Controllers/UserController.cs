using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.LoginRequest;
using EasyBizRequest.Masters.UsersRequest;
using EasyBizResponse.Masters.LogInResponse;
using EasyBizResponse.Masters.UsersResponse;
using PosAPI.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class UserController : ApiController
    {
        [Authorize]
        public IHttpActionResult GetUserDetails()
        {

            SelectUserDetailsResponse response = null;

            try
            {
                var RequestData = new SelectCommonLoginRequest();

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                RequestData.UserID = UserID;
                var ResponseData = new UsersBLL();
                response = ResponseData.SelectCommonUserDetailsInfo(RequestData);

                if (response.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                    return Ok(response);
                else
                    return BadRequest(response.DisplayMessage);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetUserDetails(string UserName, string Password, int StoreID, string StoreCode)
        {
            SelectLogInResponse response = null;
            try
            {
                var RequestData = new SelectLogInRequest();
                RequestData.UserName = UserName;
                //RequestData.Password = EncrypterDecrypter.EncryptPassword(Password);
                RequestData.Password = Password;
                RequestData.FromOrToStoreID = StoreID;
                RequestData.FromOrToStoreCode = StoreCode;
                RequestData.SourceFrom = "STORE";
                RequestData.ShowInActiveRecords = true;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.StoreID = StoreID;

                var ResponseData = new UsersBLL();

                response = ResponseData.SelectLogIn(RequestData);

                if (response.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                    return Ok(response);
                else
                    return BadRequest(response.DisplayMessage);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostUser(UsersSettings _objUserSettings)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                var RequestData = new SaveUsersRequest();
                RequestData.UsersRecord = new UsersSettings();
                RequestData.UsersRecord = _objUserSettings;
                RequestData.UsersRecord.CreateOn = DateTime.Now;
                RequestData.UsersRecord.SCN = 0;
                RequestData.UsersRecord.CreateBy = UserID;
                SaveUsersResponse response = null;
                var ResponseData = new UsersBLL();
                response = ResponseData.SaveUsers(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutUser(UsersSettings _objUserSettings)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                var RequestData = new UpdateUsersRequest();
                RequestData.UsersRecord = new UsersSettings();
                RequestData.UsersRecord = _objUserSettings;
                RequestData.UsersRecord.UpdateOn = DateTime.Now;
                RequestData.UsersRecord.SCN = 0;
                RequestData.UsersRecord.UpdateBy = UserID;
                UpdateUsersResponse response = null;
                var ResponseData = new UsersBLL();
                response = ResponseData.UpdateUsers(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetUserList(int StoreID)
        {
            SelectAllUsersResponse response = null;
            try
            {
                var RequestData = new SelectAllUsersRequest();
                RequestData.StoreID = StoreID;
                RequestData.ShowInActiveRecords = true;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = new UsersBLL();
                response = ResponseData.API_SelectBystoreID(RequestData);

                if (response.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                    return Ok(response);
                else
                    return BadRequest(response.DisplayMessage);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
