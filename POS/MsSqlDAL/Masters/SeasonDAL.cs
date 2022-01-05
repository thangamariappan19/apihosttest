using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.SeasonResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Masters
{
   public class SeasonDAL:BaseSeasonDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
       string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveSeasonRequest)RequestObj;
            var ResponseData = new SaveSeasonResponse();

            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                //transaction = _ConnectionObj.BeginTransaction();

                _CommandObj = new SqlCommand("InsertSeasonMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter SeasonID = _CommandObj.Parameters.Add("@SeasonID", SqlDbType.Int);
                SeasonID.Direction = ParameterDirection.Input;
                SeasonID.Value = RequestData.SeasonRecord.ID;

                SqlParameter SeasonCode = _CommandObj.Parameters.Add("@SeasonCode", SqlDbType.NVarChar);
                SeasonCode.Direction = ParameterDirection.Input;
                SeasonCode.Value = RequestData.SeasonRecord.SeasonCode;

                SqlParameter SeasonName = _CommandObj.Parameters.Add("@SeasonName", SqlDbType.NVarChar);
                SeasonName.Direction = ParameterDirection.Input;
                SeasonName.Value = RequestData.SeasonRecord.SeasonName;

                SqlParameter SeasonStartDate = _CommandObj.Parameters.Add("@SeasonStartDate", SqlDbType.DateTime);
                SeasonStartDate.Direction = ParameterDirection.Input;
                SeasonStartDate.Value = RequestData.SeasonRecord.SeasonStartDate;

                SqlParameter SeasonEndDate = _CommandObj.Parameters.Add("@SeasonEndDate", SqlDbType.DateTime);
                SeasonEndDate.Direction = ParameterDirection.Input;
                SeasonEndDate.Value = RequestData.SeasonRecord.SeasonEndDate;

                SqlParameter NoOfWeeks = _CommandObj.Parameters.Add("@NoOfWeeks", SqlDbType.Int);
                NoOfWeeks.Direction = ParameterDirection.Input;
                NoOfWeeks.Value = RequestData.SeasonRecord.NoOfWeeks;

                SqlParameter NoOfDays = _CommandObj.Parameters.Add("@NoOfDays", SqlDbType.Int);
                NoOfDays.Direction = ParameterDirection.Input;
                NoOfDays.Value = RequestData.SeasonRecord.NoOfDays;

                SqlParameter SeasonDrop = _CommandObj.Parameters.Add("@SeasonDrop", SqlDbType.Int);
                SeasonDrop.Direction = ParameterDirection.Input;
                SeasonDrop.Value = RequestData.SeasonRecord.SeasonDrop;

                SqlParameter IsSelected = _CommandObj.Parameters.Add("@IsSelected", SqlDbType.Bit);
                IsSelected.Direction = ParameterDirection.Input;
                IsSelected.Value = RequestData.SeasonRecord.IsSelected;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.SeasonRecord.CreateBy;

                //SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                //UpdateBy.Direction = ParameterDirection.Input;
                //UpdateBy.Value = RequestData.RoleMasterData.UpdateBy;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Season");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                    //transaction.Commit();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Season");
                    //transaction.Rollback();
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Season");
                    //transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Season Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
                //transaction.Rollback();
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
             var RequestData = (UpdateSeasonRequest)RequestObj;
            var ResponseData = new UpdateSeasonResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("UpdateSeasonMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.SeasonMasterData.ID;

                SqlParameter SeasonCode = _CommandObj.Parameters.Add("@SeasonCode", SqlDbType.NVarChar);
                SeasonCode.Direction = ParameterDirection.Input;
                SeasonCode.Value = RequestData.SeasonMasterData.SeasonCode;

                SqlParameter SeasonName = _CommandObj.Parameters.Add("@SeasonName", SqlDbType.NVarChar);
                SeasonName.Direction = ParameterDirection.Input;
                SeasonName.Value = RequestData.SeasonMasterData.SeasonName;

                SqlParameter SeasonDrop = _CommandObj.Parameters.Add("@SeasonDrop", SqlDbType.Int);
                SeasonDrop.Direction = ParameterDirection.Input;
                SeasonDrop.Value = RequestData.SeasonMasterData.SeasonDrop;

                SqlParameter SeasonStartDate = _CommandObj.Parameters.Add("@SeasonStartDate", SqlDbType.DateTime);
                SeasonStartDate.Direction = ParameterDirection.Input;
                SeasonStartDate.Value = RequestData.SeasonMasterData.SeasonStartDate;

                SqlParameter SeasonEndDate = _CommandObj.Parameters.Add("@SeasonEndDate", SqlDbType.DateTime);
                SeasonEndDate.Direction = ParameterDirection.Input;
                SeasonEndDate.Value = RequestData.SeasonMasterData.SeasonEndDate;

                SqlParameter NoOfWeeks = _CommandObj.Parameters.Add("@NoOfWeeks", SqlDbType.Int);
                NoOfWeeks.Direction = ParameterDirection.Input;
                NoOfWeeks.Value = RequestData.SeasonMasterData.NoOfWeeks;

                SqlParameter NoOfDays = _CommandObj.Parameters.Add("@NoOfDays", SqlDbType.Int);
                NoOfDays.Direction = ParameterDirection.Input;
                NoOfDays.Value = RequestData.SeasonMasterData.NoOfDays;

                SqlParameter IsSelected = _CommandObj.Parameters.Add("@IsSelected", SqlDbType.Bit);
                IsSelected.Direction = ParameterDirection.Input;
                IsSelected.Value = RequestData.SeasonMasterData.IsSelected;

                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.SeasonMasterData.UpdateBy;

                //SqlParameter UpdateOn = _CommandObj.Parameters.Add("@UpdateOn", SqlDbType.DateTime);
                //UpdateOn.Direction = ParameterDirection.Input;
                //UpdateOn.Value = RequestData.SeasonMasterData.UpdateOn;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Season");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //transaction.Commit();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Season");
                    //transaction.Rollback();
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Season");
                    //transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Season Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
                //transaction.Rollback();
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SeasonRecord = new SeasonMaster();
            var RequestData = (DeleteSeasonRequest)RequestObj;
            var ResponseData = new DeleteSeasonResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("Delete from SeasonMaster where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Season Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Season Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SeasonRecord = new SeasonMaster();
            var RequestData = (SelectBySeasonIDRequest)RequestObj;
            var ResponseData = new SelectBySeasonIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from SeasonMaster  with(NoLock) where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSeasonMaster = new SeasonMaster();

                        objSeasonMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSeasonMaster.SeasonCode = Convert.ToString(objReader["SeasonCode"]);
                        objSeasonMaster.SeasonName = Convert.ToString(objReader["SeasonName"]);
                        objSeasonMaster.SeasonDrop = objReader["SeasonDrop"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonDrop"]) : 0;
                        objSeasonMaster.SeasonStartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objSeasonMaster.SeasonEndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objSeasonMaster.NoOfWeeks = objReader["NoOfWeeks"] != DBNull.Value ? Convert.ToInt32(objReader["NoOfWeeks"]) : 0;
                        objSeasonMaster.NoOfDays = objReader["NoOfDays"] != DBNull.Value ? Convert.ToInt32(objReader["NoOfDays"]) : 0;
                        objSeasonMaster.IsSelected = objReader["IsSelected"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSelected"]) : true;
                        objSeasonMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSeasonMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSeasonMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSeasonMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSeasonMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSeasonMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.SeasonMasterRecord = objSeasonMaster;
                        ResponseData.ResponseDynamicData = objSeasonMaster;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Season Master");
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
        public override BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SeasonMasterList = new List<SeasonMaster>();
            var RequestData = (SelectAllSeasonRequest)RequestObj;
            var ResponseData = new SelectAllSeasonResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select * from SeasonMaster with(NoLock) order by id desc";
               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSeasonMaster = new SeasonMaster();

                        objSeasonMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSeasonMaster.SeasonCode = Convert.ToString(objReader["SeasonCode"]);
                        objSeasonMaster.SeasonName = Convert.ToString(objReader["SeasonName"]);
                        objSeasonMaster.SeasonDrop = objReader["SeasonDrop"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonDrop"]) : 0;
                        objSeasonMaster.SeasonStartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objSeasonMaster.SeasonEndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objSeasonMaster.NoOfWeeks = objReader["NoOfWeeks"] != DBNull.Value ? Convert.ToInt32(objReader["NoOfWeeks"]) : 0;
                        objSeasonMaster.NoOfDays = objReader["NoOfDays"] != DBNull.Value ? Convert.ToInt32(objReader["NoOfDays"]) : 0;
                        objSeasonMaster.IsSelected = objReader["IsSelected"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSelected"]) : true;
                        objSeasonMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSeasonMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSeasonMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSeasonMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSeasonMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSeasonMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        SeasonMasterList.Add(objSeasonMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SeasonMasterList = SeasonMasterList;
                    ResponseData.ResponseDynamicData = SeasonMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Season  Master");
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

        public override BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectSeasonLookUpResponse SelectSeasonLookUp(SelectSeasonLookUpRequest ObjRequest)
        {
            var SeasonLookUpList = new List<SeasonMaster>();
            var RequestData = (SelectSeasonLookUpRequest)ObjRequest;
            var ResponseData = new SelectSeasonLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[SeasonName],SeasonCode from SeasonMaster with(NoLock) where Active='True'";
               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSeasonMaster = new SeasonMaster();
                        objSeasonMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSeasonMaster.SeasonName = Convert.ToString(objReader["SeasonName"]);
                        objSeasonMaster.SeasonCode = Convert.ToString(objReader["SeasonCode"]);
                        objSeasonMaster.SeasonCodeName = Convert.ToString(objReader["SeasonName"]) + " - " + Convert.ToString(objReader["SeasonCode"]);
                            
                        SeasonLookUpList.Add(objSeasonMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SeasonList = SeasonLookUpList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Season Master");
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

        public override SelectAllSeasonResponse API_SelectALL(SelectAllSeasonRequest requestData)
        {
            var SeasonMasterList = new List<SeasonMaster>();
            var RequestData = (SelectAllSeasonRequest)requestData;
            var ResponseData = new SelectAllSeasonResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                //string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                var sQuery = new StringBuilder();
                int myInt;
                bool isNumerical = int.TryParse(RequestData.SearchString, out myInt);

                if (isNumerical)
                {
                    sQuery.Append("Select ID, SeasonCode, SeasonName, SeasonDrop, StartDate, EndDate, NoOfWeeks, NoOfDays, Active, RC.TOTAL_CNT [RecordCount]  from SeasonMaster with(NoLock)");
                    sQuery.Append("LEFT JOIN(Select  count(SM.ID) As TOTAL_CNT From SeasonMaster SM with(NoLock) ");
                    sQuery.Append("where SM.Active = " + RequestData.IsActive + " ");
                    sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sQuery.Append("or SM.SeasonCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or SM.SeasonName like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or SM.SeasonDrop like isnull('%" + int.Parse(RequestData.SearchString) + "%','') ");
                    if (RequestData.SearchString.Contains("-"))
                    {
                        sQuery.Append("or SM.StartDate like isnull('%" + RequestData.SearchString + "%','') ");
                        sQuery.Append("or SM.EndDate like isnull('%" + RequestData.SearchString + "%','') ");
                    }
                    sQuery.Append("or SM.NoOfWeeks like isnull('%" + int.Parse(RequestData.SearchString) + "%',0) ");
                    sQuery.Append("or SM.NoOfDays like isnull('%" + int.Parse(RequestData.SearchString) + "%',0))) As RC ON 1 = 1  ");

                    sQuery.Append("where Active = " + RequestData.IsActive + " ");
                    sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sQuery.Append("or SeasonCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or SeasonName like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or SeasonDrop like isnull('%" + int.Parse(RequestData.SearchString) + "%','') ");
                    if (RequestData.SearchString.Contains("-"))
                    { 
                    sQuery.Append("or StartDate like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or EndDate like isnull('%" + RequestData.SearchString + "%','') ");
                    }
                    sQuery.Append("or NoOfWeeks like isnull('%" + int.Parse(RequestData.SearchString) + "%',0) ");
                    sQuery.Append("or NoOfDays like isnull('%" + int.Parse(RequestData.SearchString) + "%',0)) ");
                    sQuery.Append("order by ID asc ");
                    sQuery.Append("offset " + RequestData.Offset + " rows ");
                    sQuery.Append("fetch first " + RequestData.Limit + " rows only");
                }
                else
                {
                    sQuery.Append("Select ID, SeasonCode, SeasonName, SeasonDrop, StartDate, EndDate, NoOfWeeks, NoOfDays, Active, RC.TOTAL_CNT [RecordCount]  from SeasonMaster with(NoLock)");
                    sQuery.Append("LEFT JOIN(Select  count(SM.ID) As TOTAL_CNT From SeasonMaster SM with(NoLock) ");
                    sQuery.Append("where SM.Active = " + RequestData.IsActive + " ");
                    sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sQuery.Append("or SM.SeasonCode like isnull('%" + RequestData.SearchString + "%','') ");
                    //sQuery.Append("or SeasonName like isnull('%" + RequestData.SearchString + "%','') ");
                    //sQuery.Append("or SeasonDrop like isnull('%" + RequestData.SearchString + "%','') ");
                    if (RequestData.SearchString.Contains("-"))
                    {
                        sQuery.Append("or SM.StartDate like isnull('%" + RequestData.SearchString + "%','') ");
                        sQuery.Append("or SM.EndDate like isnull('%" + RequestData.SearchString + "%','') ");
                    }
                    //sQuery.Append("or NoOfWeeks = isnull('%" + RequestData.SearchString + "%','') ");
                    //sQuery.Append("or NoOfDays = isnull('%" + RequestData.SearchString + "%','')) ");
                    sQuery.Append("or SM.SeasonName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");

                    sQuery.Append("where Active = " + RequestData.IsActive + " ");
                    sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sQuery.Append("or SeasonCode like isnull('%" + RequestData.SearchString + "%','') ");
                    //sQuery.Append("or SeasonName like isnull('%" + RequestData.SearchString + "%','') ");
                    //sQuery.Append("or SeasonDrop like isnull('%" + RequestData.SearchString + "%','') ");
                    if (RequestData.SearchString.Contains("-"))
                    {
                        sQuery.Append("or StartDate like isnull('%" + RequestData.SearchString + "%','') ");
                        sQuery.Append("or EndDate like isnull('%" + RequestData.SearchString + "%','') ");
                    }
                    //sQuery.Append("or NoOfWeeks = isnull('%" + RequestData.SearchString + "%','') ");
                    //sQuery.Append("or NoOfDays = isnull('%" + RequestData.SearchString + "%','')) ");
                    sQuery.Append("or SeasonName like isnull('%" + RequestData.SearchString + "%','')) ");
                    sQuery.Append("order by ID asc ");
                    sQuery.Append("offset " + RequestData.Offset + " rows ");
                    sQuery.Append("fetch first " + RequestData.Limit + " rows only");
                }

                
                //sQuery = "Select ID,SeasonCode,SeasonName,SeasonDrop,StartDate,EndDate,NoOfWeeks,NoOfDays,Active, RecordCount = COUNT(*) OVER()  from SeasonMaster with(NoLock)" +
                //   "where Active = " + RequestData.IsActive + " " +
                //       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                //       "or SeasonCode = isnull('" + RequestData.SearchString + "','') " +
                //       "or SeasonName = isnull('" + RequestData.SearchString + "','') " +
                //       "or SeasonDrop = " + RequestData.SearchString + ") " +
                //       "order by ID asc " +
                //       "offset " + RequestData.Offset + " rows " +
                //       "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sQuery.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSeasonMaster = new SeasonMaster();

                        objSeasonMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSeasonMaster.SeasonCode = Convert.ToString(objReader["SeasonCode"]);
                        objSeasonMaster.SeasonName = Convert.ToString(objReader["SeasonName"]);
                        objSeasonMaster.SeasonDrop = objReader["SeasonDrop"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonDrop"]) : 0;
                        objSeasonMaster.SeasonStartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objSeasonMaster.SeasonEndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objSeasonMaster.NoOfWeeks = objReader["NoOfWeeks"] != DBNull.Value ? Convert.ToInt32(objReader["NoOfWeeks"]) : 0;
                        objSeasonMaster.NoOfDays = objReader["NoOfDays"] != DBNull.Value ? Convert.ToInt32(objReader["NoOfDays"]) : 0;
                        /*objSeasonMaster.IsSelected = objReader["IsSelected"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSelected"]) : true;
                        objSeasonMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSeasonMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSeasonMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSeasonMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSeasonMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;*/
                        objSeasonMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        SeasonMasterList.Add(objSeasonMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SeasonMasterList = SeasonMasterList;
                    ResponseData.ResponseDynamicData = SeasonMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Season  Master");
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
