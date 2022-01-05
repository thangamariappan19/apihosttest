using EasyBizAbsDAL.FCPasses;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.FCPasses;
using EasyBizRequest;
using EasyBizRequest.FCPasses;
using EasyBizResponse;
using EasyBizResponse.FCPasses;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.FCPasses
{
    public class PassMasterDAL : EasyBizAbsDAL.FCPasses.BassPassMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;

        public override SelectPassMasterLookUpResponse API_SelectPassMasterLookUp(SelectPassMasterLookUpRequest ObjRequest)
        {
            var PassMasterList = new List<PassMaster>();
            var RequestData = (SelectPassMasterLookUpRequest)ObjRequest;
            var ResponseData = new SelectPassMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                
                    sQuery = "Select ID,PassCode,PassName from PassMaster ";               
                
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPassMaster = new PassMaster();
                        objPassMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt16(objReader["ID"]) : 0;
                        objPassMaster.PassCode = Convert.ToString(objReader["PassCode"]);
                        objPassMaster.PassName = Convert.ToString(objReader["PassName"]);

                        PassMasterList.Add(objPassMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PassMasterTypeData = PassMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Pass Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (PassMasterRequest)RequestObj;
            var ResponseData = new PassMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_Insert_Pass_Master", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var PassCode = _CommandObj.Parameters.Add("@PassCode", SqlDbType.NVarChar);
                PassCode.Direction = ParameterDirection.Input;
                PassCode.Value = RequestData.PassMasterRequestData.PassCode;

                var PassName = _CommandObj.Parameters.Add("@PassName", SqlDbType.NVarChar);
                PassName.Direction = ParameterDirection.Input;
                PassName.Value = RequestData.PassMasterRequestData.PassName;

                var Points = _CommandObj.Parameters.Add("@Points", SqlDbType.Int);
                Points.Direction = ParameterDirection.Input;
                Points.Value = RequestData.PassMasterRequestData.Points;

                var CardType = _CommandObj.Parameters.Add("@CardType", SqlDbType.NVarChar);
                CardType.Direction = ParameterDirection.Input;
                CardType.Value = RequestData.PassMasterRequestData.CardType;
                             
                var ValidFrom = _CommandObj.Parameters.Add("@ValidFrom", SqlDbType.Date);
                ValidFrom.Direction = ParameterDirection.Input;
                ValidFrom.Value = RequestData.PassMasterRequestData.ValidFrom;

                var ValidTo = _CommandObj.Parameters.Add("@ValidTo", SqlDbType.Date);
                ValidTo.Direction = ParameterDirection.Input;
                ValidTo.Value = RequestData.PassMasterRequestData.ValidTo;

                var ScanMethod = _CommandObj.Parameters.Add("@ScanMethod", SqlDbType.NVarChar);
                ScanMethod.Direction = ParameterDirection.Input;
                ScanMethod.Value = RequestData.PassMasterRequestData.ScanMethod;

                var Notes = _CommandObj.Parameters.Add("@Notes", SqlDbType.NVarChar);
                Notes.Direction = ParameterDirection.Input;
                Notes.Value = RequestData.PassMasterRequestData.Notes;


                _CommandObj.Parameters.AddWithValue("@IsOneTimePass", RequestData.PassMasterRequestData.IsOneTimePass);

                _CommandObj.Parameters.AddWithValue("@Active", RequestData.PassMasterRequestData.Active);

                _CommandObj.Parameters.AddWithValue("@IsSync", RequestData.PassMasterRequestData.IsSync);

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.PassMasterRequestData.CreateBy;

                
                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Pass Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = "Pass Code / Pass Name Already Exists.";
                }
                else
                {
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Pass Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var PassList = new List<PassMaster>();
            var RequestData = (PassMasterRequest)RequestObj;
            var ResponseData = new PassMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                sQuery = "Select ID, PassCode, PassName, Points, CardType, ValidFrom, ValidTo" +
                    ", ScanMethod, Notes, IsOneTimePass, Active " +
                    "from PassMaster with(NoLock) " +
                    "where 1=1 " +
                        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                        "or PassCode = isnull('" + RequestData.SearchString + "','') " +
                        "or PassName = isnull('" + RequestData.SearchString + "','') " +
                        "or convert(varchar(50),Points) = isnull('" + RequestData.SearchString + "','')) " +
                        "or CardType = isnull('" + RequestData.SearchString + "','') " +
                        "or ScanMethod = isnull('" + RequestData.SearchString + "','') " +
                        "or Notes = isnull('" + RequestData.SearchString + "','') ";
                   

                if (!string.IsNullOrWhiteSpace(RequestData.IsActive))
                    sQuery = sQuery + " and Active = " + RequestData.IsActive;

                sQuery = sQuery + " order by ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPassMaster = new PassMaster();
                        objPassMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPassMaster.PassCode = objReader["PassCode"] != DBNull.Value ? Convert.ToString(objReader["PassCode"]) : "";
                        objPassMaster.PassName = objReader["PassName"] != DBNull.Value ? Convert.ToString(objReader["PassName"]) : "";
                        objPassMaster.Points = objReader["Points"] != DBNull.Value ? Convert.ToInt32(objReader["Points"]) : 0;
                        objPassMaster.CardType = objReader["CardType"] != DBNull.Value ? Convert.ToString(objReader["CardType"]) : "";
                        objPassMaster.ValidFrom = objReader["ValidFrom"] != DBNull.Value ? Convert.ToDateTime(objReader["ValidFrom"]) : DateTime.Now;
                        objPassMaster.ValidTo = objReader["ValidTo"] != DBNull.Value ? Convert.ToDateTime(objReader["ValidTo"]) : DateTime.Now;
                        objPassMaster.ScanMethod = objReader["ScanMethod"] != DBNull.Value ? Convert.ToString(objReader["ScanMethod"]) : "";
                        objPassMaster.Notes = objReader["Points"] != DBNull.Value ? Convert.ToString(objReader["Notes"]) : "";
                        objPassMaster.IsOneTimePass = objReader["IsOneTimePass"] != DBNull.Value ? Convert.ToBoolean(objReader["IsOneTimePass"]) : true;
                        objPassMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        
                        PassList.Add(objPassMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PassMasterResponseList = PassList;
                    //ResponseData.ResponseDynamicData = CityList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Pass Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var PassMasterRecord = new PassMaster();
            var RequestData = (PassMasterRequest)RequestObj;
            var ResponseData = new PassMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select ID, PassCode, PassName, Points, CardType, ValidFrom, ValidTo" +
                    ", ScanMethod, Notes, IsOneTimePass, Active " +
                    "from PassMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPassMaster = new PassMaster();
                        objPassMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPassMaster.PassCode = objReader["PassCode"] != DBNull.Value ? Convert.ToString(objReader["PassCode"]) : "";
                        objPassMaster.PassName = objReader["PassName"] != DBNull.Value ? Convert.ToString(objReader["PassName"]) : "";
                        objPassMaster.Points = objReader["Points"] != DBNull.Value ? Convert.ToInt32(objReader["Points"]) : 0;
                        objPassMaster.CardType = objReader["CardType"] != DBNull.Value ? Convert.ToString(objReader["CardType"]) : "";
                        objPassMaster.ValidFrom = objReader["ValidFrom"] != DBNull.Value ? Convert.ToDateTime(objReader["ValidFrom"]) : DateTime.Now;
                        objPassMaster.ValidTo = objReader["ValidTo"] != DBNull.Value ? Convert.ToDateTime(objReader["ValidTo"]) : DateTime.Now;
                        objPassMaster.ScanMethod = objReader["ScanMethod"] != DBNull.Value ? Convert.ToString(objReader["ScanMethod"]) : "";
                        objPassMaster.Notes = objReader["Points"] != DBNull.Value ? Convert.ToString(objReader["Notes"]) : "";
                        objPassMaster.IsOneTimePass = objReader["IsOneTimePass"] != DBNull.Value ? Convert.ToBoolean(objReader["IsOneTimePass"]) : true;
                        objPassMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.PassMasterResponseData = objPassMaster;
                        //ResponseData.ResponseDynamicData = objStateMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Pass Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var RequestData = (PassMasterRequest)RequestObj;
            var ResponseData = new PassMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_Update_Pass_Master", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.PassMasterRequestData.ID;

                var PassCode = _CommandObj.Parameters.Add("@PassCode", SqlDbType.NVarChar);
                PassCode.Direction = ParameterDirection.Input;
                PassCode.Value = RequestData.PassMasterRequestData.PassCode;

                var PassName = _CommandObj.Parameters.Add("@PassName", SqlDbType.NVarChar);
                PassName.Direction = ParameterDirection.Input;
                PassName.Value = RequestData.PassMasterRequestData.PassName;

                var Points = _CommandObj.Parameters.Add("@Points", SqlDbType.Int);
                Points.Direction = ParameterDirection.Input;
                Points.Value = RequestData.PassMasterRequestData.Points;

                var CardType = _CommandObj.Parameters.Add("@CardType", SqlDbType.NVarChar);
                CardType.Direction = ParameterDirection.Input;
                CardType.Value = RequestData.PassMasterRequestData.CardType;

                var ValidFrom = _CommandObj.Parameters.Add("@ValidFrom", SqlDbType.Date);
                ValidFrom.Direction = ParameterDirection.Input;
                ValidFrom.Value = RequestData.PassMasterRequestData.ValidFrom;

                var ValidTo = _CommandObj.Parameters.Add("@ValidTo", SqlDbType.Date);
                ValidTo.Direction = ParameterDirection.Input;
                ValidTo.Value = RequestData.PassMasterRequestData.ValidTo;

                var ScanMethod = _CommandObj.Parameters.Add("@ScanMethod", SqlDbType.NVarChar);
                ScanMethod.Direction = ParameterDirection.Input;
                ScanMethod.Value = RequestData.PassMasterRequestData.ScanMethod;

                var Notes = _CommandObj.Parameters.Add("@Notes", SqlDbType.NVarChar);
                Notes.Direction = ParameterDirection.Input;
                Notes.Value = RequestData.PassMasterRequestData.Notes;

                _CommandObj.Parameters.AddWithValue("@IsOneTimePass", RequestData.PassMasterRequestData.IsOneTimePass);

                _CommandObj.Parameters.AddWithValue("@Active", RequestData.PassMasterRequestData.Active);

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.PassMasterRequestData.UpdateBy;


                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Pass Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = "Pass Code / Pass Name Already Exists.";
                }
                else
                {
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Pass Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
    }
}
