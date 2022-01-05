using EasyBizAbsDAL.PatchForm;
using EasyBizRequest.PatchFormRequest;
using EasyBizResponse.PatchFormResponse;
using MsSqlDAL.Common;
using EasyBizDBTypes.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizRequest.Common;
using EasyBizResponse.Common;

namespace MsSqlDAL.PatchForm
{
    public class PatchFormDAL : BasePatchFormDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SavePatchFormRequest)RequestObj;
            var ResponseData = new SavePatchFormResponse();

            var sqlCommon = new MsSqlCommon();
           

            string _ConnectionString; Enums.RequestFrom _RequestFrom;

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("AppLatestVersion", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;      

                var ApplicationType = _CommandObj.Parameters.Add("@ApplicationType", SqlDbType.VarChar);
                ApplicationType.Direction = ParameterDirection.Input;
                ApplicationType.Value = RequestData.PatchFormTypesRecord.ApplicationType;

                var ApplicationVersion = _CommandObj.Parameters.Add("@ApplicationVersion", SqlDbType.VarChar);
                ApplicationVersion.Direction = ParameterDirection.Input;
                ApplicationVersion.Value = RequestData.PatchFormTypesRecord.ApplicationVersion;

                var DBVersion = _CommandObj.Parameters.Add("@DBVersion", SqlDbType.VarChar);
                DBVersion.Direction = ParameterDirection.Input;
                DBVersion.Value = RequestData.PatchFormTypesRecord.DBVersion;

                var Extension = _CommandObj.Parameters.Add("@Extension", SqlDbType.VarChar);
                Extension.Direction = ParameterDirection.Input;
                Extension.Value = RequestData.PatchFormTypesRecord.Extension;

                var AppPatchFile = _CommandObj.Parameters.Add("@AppPatchFile", SqlDbType.Binary);
                AppPatchFile.Direction = ParameterDirection.Input;
                AppPatchFile.Value = RequestData.PatchFormTypesRecord.AppPatchFile;



                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                //SqlParameter ID = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                //ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Patch Details");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.IDs = ID.Value.ToString();

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Patch Details");
                }
                else
                {


                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Patch Details");

                }                         
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Patch Details");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;

            }
          
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }      

       
    }
}
