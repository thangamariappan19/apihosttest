using EasyBizBLL.Masters;
using EasyBizRequest.Masters.LoginRequest;
using EasyBizResponse.Masters.LogInResponse;
using Microsoft.Owin.Security.OAuth;
using PosAPI.DTOs;
using PosAPI.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PosAPI.Providers
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public long ToUnixTime { get; private set; }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //return base.ValidateClientAuthentication(context);
            context.Validated();
        }

        public bool compareString(string a, string b)
        {
            return a.Equals(b);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string user_name = context.UserName;
            //string password = EncrypterDecrypter.EncryptPassword(context.Password);
            string password = context.Password;
            SelectCommonLoginRequest req = new SelectCommonLoginRequest();
            req.UserName = user_name;
            req.Password = password;
            //LoginModule db = new LoginModule();
            SelectLogInResponse res = new SelectLogInResponse();
            UsersBLL _UsersBLL = new UsersBLL();
            res = _UsersBLL.SelectCommonLogIn(req);

            if (res == null ||res.StatusCode != EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            //if (!(context.UserName == "user" && context.Password == "api_integration"))
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //    return;
            //}


            var UserIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            DateTime dt = DateTime.Now;
            long InTime = ToUnixTime1(dt);
            long ExpTime = ToUnixTime1(dt.AddHours(5));
            //long ExpTime = ToUnixTime1(dt.AddMinutes(30));

            
            UserIdentity.AddClaims(new List<Claim> {
                new Claim("UserID", res.UsersRecord.ID.ToString()),
                new Claim("InTime", InTime.ToString()),
                new Claim("ExpTime", ExpTime.ToString()),
            });

            //UserIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            //UserIdentity.AddClaim(new Claim(ClaimTypes.Surname, user.UserName));
            //UserIdentity.AddClaim(new Claim(ClaimTypes.Role, user.UserRoleID.ToString()));
            //UserIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, InTime.ToString()));
            //UserIdentity.AddClaim(new Claim(ClaimTypes.OtherPhone, ExpTime.ToString()));

            context.Validated(UserIdentity);

        }

        public long ToUnixTime1(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }
}

//  Installed Packages

//  Update-package Microsoft.AspNet.WebApi
//  Install-Package Microsoft.AspNet.Identity.Owin
//  Install-Package Microsoft.Owin.Host.SystemWeb
//  Install-Package Microsoft.Owin.Security.OAuth
//  Install-Package Microsoft.Owin.Cors
//  Install-Package Microsoft.AspNet.WebApi.Core
//  Install-Package Microsoft.AspNet.WebApi.Owin