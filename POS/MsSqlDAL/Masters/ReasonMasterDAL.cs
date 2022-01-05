using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizResponse;
using EasyBizRequest.Masters.ReasonMasterRequest;
using EasyBizResponse.Masters.ReasonMasterResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using EasyBizAbsDAL.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MsSqlDAL.Masters
{
    public class ReasonMasterDAL : BaseReasonMasterDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveReasonMasterRequest)RequestObj;
            var ResponseData = new SaveReasonMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertReasonMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ReasonID = _CommandObj.Parameters.Add("@ReasonID", SqlDbType.Int);
                ReasonID.Direction = ParameterDirection.Input;
                ReasonID.Value = RequestData.ReasonMasterData.ReasonID;

                var ReasonCode = _CommandObj.Parameters.Add("@ReasonCode", SqlDbType.NVarChar);
                ReasonCode.Direction = ParameterDirection.Input;
                ReasonCode.Value = RequestData.ReasonMasterData.ReasonCode;

                var ReasonName = _CommandObj.Parameters.Add("@ReasonName", SqlDbType.NVarChar);
                ReasonName.Direction = ParameterDirection.Input;
                ReasonName.Value = RequestData.ReasonMasterData.ReasonName;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.ReasonMasterData.Description;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ReasonMasterData.Active;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.ReasonMasterData.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Reason");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Reason");
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Reason");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Reason Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var RequestData = (UpdateReasonMasterRequest)RequestObj;
            var ResponseData = new UpdateReasonMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateReasonMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.ReasonMasterData.ReasonID;

                var ReasonCode = _CommandObj.Parameters.Add("@ReasonCode", SqlDbType.NVarChar);
                ReasonCode.Direction = ParameterDirection.Input;
                ReasonCode.Value = RequestData.ReasonMasterData.ReasonCode;

                var ReasonName = _CommandObj.Parameters.Add("@ReasonName", SqlDbType.NVarChar);
                ReasonName.Direction = ParameterDirection.Input;
                ReasonName.Value = RequestData.ReasonMasterData.ReasonName;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.ReasonMasterData.Description;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ReasonMasterData.Active;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.ReasonMasterData.SCN;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.ReasonMasterData.UpdateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Reason");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Reason");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Reason");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Reason Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var ReasonMasterRecord = new ReasonMaster();

            var RequestData = (DeleteReasonMasterRequest)RequestObj;
            var ResponseData = new DeleteReasonMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sSql = "Delete from ReasonMaster where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Reason Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Reason Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var ReasonMasterRecord = new ReasonMaster();
            var RequestData = (SelectByIDReasonMasterRequest)RequestObj;
            var ResponseData = new SelectByIDReasonMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from ReasonMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objReasonMaster = new ReasonMaster();
                        objReasonMaster.ReasonID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objReasonMaster.ReasonCode = Convert.ToString(objReader["ReasonCode"]);
                        objReasonMaster.ReasonName = Convert.ToString(objReader["ReasonName"]);
                        objReasonMaster.Description = Convert.ToString(objReader["Description"]);
                        objReasonMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objReasonMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objReasonMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objReasonMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objReasonMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objReasonMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ResponseData.ReasonMasterRecord = objReasonMaster;
                        ResponseData.ResponseDynamicData = objReasonMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Reason Master");
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

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var ReasonMasterList = new List<ReasonMaster>();
            var RequestData = (SelectAllReasonMasterRequest)RequestObj;
            var ResponseData = new SelectAllReasonMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sQuery = "Select * from ReasonMaster with(NoLock)";
               

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objReasonMaster = new ReasonMaster();
                        objReasonMaster.ReasonID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objReasonMaster.ReasonCode = Convert.ToString(objReader["ReasonCode"]);
                        objReasonMaster.ReasonName = Convert.ToString(objReader["ReasonName"]);
                        objReasonMaster.Description = Convert.ToString(objReader["Description"]);
                        objReasonMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objReasonMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objReasonMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objReasonMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objReasonMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objReasonMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ReasonMasterList.Add(objReasonMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ReasonMasterList = ReasonMasterList;
                    ResponseData.ResponseDynamicData = ReasonMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Reason Master");
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

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectReasonMasterLookUpResponse SelectReasonMasterLookUp(SelectReasonMasterLookUpRequest RequestObj)
        {
            var ReasonMasterList = new List<ReasonMaster>();


            SelectReasonMasterLookUpRequest RequestData = new SelectReasonMasterLookUpRequest();

            SelectReasonMasterLookUpResponse ResponseData = new SelectReasonMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,ReasonName from ReasonMaster with(NoLock) where Active='True' ";
                
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objReasonMaster = new ReasonMaster();
                        objReasonMaster.ReasonID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objReasonMaster.ReasonName = Convert.ToString(objReader["ReasonName"]);
                        ReasonMasterList.Add(objReasonMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ReasonMasterList = ReasonMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Reason Master");
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

        public override SelectReasonMasterLookUpResponse API_SelectReasonMasterLookUp(SelectReasonMasterLookUpRequest objRequest)
        {
            var ReasonMasterList = new List<ReasonMaster>();
            SelectReasonMasterLookUpRequest RequestData = new SelectReasonMasterLookUpRequest();
            SelectReasonMasterLookUpResponse ResponseData = new SelectReasonMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,ReasonCode,ReasonName from ReasonMaster with(NoLock) where Active='True' ";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objReasonMaster = new ReasonMaster();
                        objReasonMaster.ReasonID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objReasonMaster.ReasonCode = Convert.ToString(objReader["ReasonCode"]);
                        objReasonMaster.ReasonName = Convert.ToString(objReader["ReasonName"]);
                        ReasonMasterList.Add(objReasonMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ReasonMasterList = ReasonMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Reason Master");
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

        public override SelectAllReasonMasterResponse API_SelectALL(SelectAllReasonMasterRequest requestData)
        {
            var ReasonMasterList = new List<ReasonMaster>();
            var RequestData = (SelectAllReasonMasterRequest)requestData;
            var ResponseData = new SelectAllReasonMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sQuery = "Select ID,ReasonCode,ReasonName,Description,Active, RecordCount = COUNT(*) OVER() from ReasonMaster with(NoLock)" +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or ReasonCode = isnull('" + RequestData.SearchString + "','') " +
                       "or ReasonName = isnull('" + RequestData.SearchString + "','') " +
                       "or Description = isnull('" + RequestData.SearchString + "','')) " +
                       "order by ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";


                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objReasonMaster = new ReasonMaster();
                        objReasonMaster.ReasonID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objReasonMaster.ReasonCode = Convert.ToString(objReader["ReasonCode"]);
                        objReasonMaster.ReasonName = Convert.ToString(objReader["ReasonName"]);
                        objReasonMaster.Description = Convert.ToString(objReader["Description"]);
                        /*objReasonMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objReasonMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objReasonMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objReasonMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objReasonMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;*/
                        objReasonMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ReasonMasterList.Add(objReasonMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ReasonMasterList = ReasonMasterList;
                    ResponseData.ResponseDynamicData = ReasonMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Reason Master");
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
    }
}
