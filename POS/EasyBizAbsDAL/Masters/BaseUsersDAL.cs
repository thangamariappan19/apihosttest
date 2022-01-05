using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.LoginRequest;
using EasyBizRequest.Masters.UsersRequest;
using EasyBizResponse.Masters.LogInResponse;
using EasyBizResponse.Masters.UsersResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseUsersDAL :BaseDAL
    {
        public abstract SelectLogInResponse SelectLogIn(SelectLogInRequest RequestObj);

         public abstract SelectLogInResponse SelectUserDetails(SelectLogInRequest RequestObj);

         public abstract UpdateUsersResponse PasswordReset(EasyBizRequest.Masters.UsersRequest.UpdateUsersRequest RequestObj);

         public abstract SelectLogInResponse SelectUserDeatilsfromFingerPrint(SelectLogInByFingerPrintRequest RequestObj);

        public abstract SelectLogInResponse SelectCommonLogIn(SelectCommonLoginRequest objRequest);

        public abstract SelectUserDetailsResponse SelectCommonUserDetailsInfo(SelectCommonLoginRequest RequestObj);

        public abstract SelectUserDetailsResponse API_SelectCommonUserDetailsInfo(SelectCommonLoginRequest RequestObj);
        public abstract SelectAllUsersResponse API_SelectALL(SelectAllUsersRequest requestData);
        public abstract SelectAllUsersResponse API_SelectRecordInStoreID(SelectAllUsersRequest requestData);
    }
}
