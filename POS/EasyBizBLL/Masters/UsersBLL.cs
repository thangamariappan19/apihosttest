using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.LoginRequest;
using EasyBizRequest.Masters.UsersRequest;
using EasyBizResponse.Masters.LogInResponse;
using EasyBizResponse.Masters.UsersResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class UsersBLL
    {
        public SaveUsersResponse SaveUsers(SaveUsersRequest objRequest)
        {
            SaveUsersResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objUsersMaster = new UsersSettings();
                    objUsersMaster = (UsersSettings)objRequest.RequestDynamicData;
                    objRequest.UsersRecord = objUsersMaster;
                }
                objResponse = (SaveUsersResponse)objBaseUsersDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.UsersRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.LOGINUSERS;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.UsersBLL", "SaveUsers");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveUsersResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllUsersResponse API_SelectALL(SelectAllUsersRequest requestData)
        {
            SelectAllUsersResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (SelectAllUsersResponse)objBaseUsersDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllUsersResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllUsersResponse API_SelectBystoreID(SelectAllUsersRequest requestData)
        {
            SelectAllUsersResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (SelectAllUsersResponse)objBaseUsersDAL.API_SelectRecordInStoreID(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllUsersResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateUsersResponse UpdateUsers(UpdateUsersRequest objRequest)
        {
            UpdateUsersResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objUsersMaster = new UsersSettings();
                    objUsersMaster = (UsersSettings)objRequest.RequestDynamicData;
                    objRequest.UsersRecord = objUsersMaster;
                }
                objResponse = (UpdateUsersResponse)objBaseUsersDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.UsersRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.LOGINUSERS;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.UsersBLL", "UpdateUsers");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateUsersResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteUsersResponse DeleteUsers(DeleteUsersRequest objRequest)
        {
            DeleteUsersResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (DeleteUsersResponse)objBaseUsersDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.UsersRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.LOGINUSERS;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.UsersBLL", "DeleteUsers");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteUsersResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Role Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllUsersResponse SelectAllUsers(SelectAllUsersRequest objRequest)
        {
            SelectAllUsersResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (SelectAllUsersResponse)objBaseUsersDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllUsersResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByUsersIDResponse SelectUserMaster(SelectByUsersIDRequest objRequest)
        {
            SelectByUsersIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }   
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (SelectByUsersIDResponse)objBaseUsersDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByUsersIDResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectLogInResponse SelectLogIn(SelectLogInRequest objRequest)
        {
            SelectLogInResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (SelectLogInResponse)objBaseUsersDAL.SelectLogIn(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectLogInResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectLogInResponse SelectCommonLogIn(SelectCommonLoginRequest objRequest)
        {
            SelectLogInResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (SelectLogInResponse)objBaseUsersDAL.SelectCommonLogIn(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectLogInResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectLogInResponse SelectUserDeatils(SelectLogInRequest objRequest)
        {
            SelectLogInResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (SelectLogInResponse)objBaseUsersDAL.SelectUserDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectLogInResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectLogInResponse SelectUserDeatilsfromFingerPrint(SelectLogInByFingerPrintRequest objRequest)
        {
            SelectLogInResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (SelectLogInResponse)objBaseUsersDAL.SelectUserDeatilsfromFingerPrint(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectLogInResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateUsersResponse PasswordReset(UpdateUsersRequest objRequest)
        {
            UpdateUsersResponse objResponse = null;
         
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (UpdateUsersResponse)objBaseUsersDAL.PasswordReset(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new UpdateUsersResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        //public object SelectUserPassword(SelectByUsersIDRequest objRequest)
        //{

        //    SelectByUsersIDResponse objResponse = null;
        //    var objFactory = new DALFactory();
        //    try
        //    {
        //        if(objRequest.ID == 0 )
        //        {
        //            objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
        //        }
        //        var objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
        //        objResponse = (SelectByUsersIDResponse)objBaseUsersDAL.SelectUserPassword(objRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        objResponse = new SelectByUsersIDResponse();
        //        objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Password Reset");
        //        objResponse.ExceptionMessage = ex.Message;
        //        objResponse.StackTrace = ex.StackTrace;
        //    }
        //    return objResponse;        
        //}
        public SelectLogInResponse OneUserLogin(SelectLogInRequest objRequest)
        {
            SelectLogInResponse objResponse = null;

            DALFactory objFactory = new DALFactory();
            try
            {
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (SelectLogInResponse)objBaseUsersDAL.SelectLogIn(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectLogInResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectUserDetailsResponse SelectCommonUserDetailsInfo(SelectCommonLoginRequest RequestData)
        {
            SelectUserDetailsResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseUsersDAL objBaseUsersDAL = objFactory.GetDALRepository().GetUsersDAL();
                objResponse = (SelectUserDetailsResponse)objBaseUsersDAL.API_SelectCommonUserDetailsInfo(RequestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectUserDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Users");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
