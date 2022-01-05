using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizResponse;
using EasyBizRequest.Masters.ReasonMasterRequest;
using EasyBizResponse.Masters.WarehouseMasterResponse;
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
using EasyBizRequest.Masters.DesignationMasterRequest;
using System.Data;
using EasyBizResponse.Masters.DesignationMasterResponse;
namespace MsSqlDAL.Masters
{
    public class DesignationMasterDAL : BaseDesignationMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveDesignationMasterRequest)RequestObj;
            var ResponseData = new SaveDesignationMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertDesignationMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter DesignationID = _CommandObj.Parameters.Add("@DesignationID", SqlDbType.Int);
                DesignationID.Direction = ParameterDirection.Input;
                DesignationID.Value = RequestData.DesignationMasterData.ID;

                var DesignationCode = _CommandObj.Parameters.Add("@DesignationCode", SqlDbType.NVarChar);
                DesignationCode.Direction = ParameterDirection.Input;
                DesignationCode.Value = RequestData.DesignationMasterData.DesignationCode;

                var DesignationName = _CommandObj.Parameters.Add("@DesignationName", SqlDbType.NVarChar);
                DesignationName.Direction = ParameterDirection.Input;
                DesignationName.Value = RequestData.DesignationMasterData.DesignationName;

                var RoleId = _CommandObj.Parameters.Add("@RoleId", SqlDbType.Int);
                RoleId.Direction = ParameterDirection.Input;
                RoleId.Value = RequestData.DesignationMasterData.RoleId;

                SqlParameter RoleCode = _CommandObj.Parameters.Add("@RoleCode", SqlDbType.NVarChar);
                RoleCode.Direction = ParameterDirection.Input;
                RoleCode.Value = RequestData.DesignationMasterData.RoleCode;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.DesignationMasterData.Description;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.DesignationMasterData.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.NVarChar);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.DesignationMasterData.Active;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.DesignationMasterData.CreateBy;        

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Designation Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Designation Master");
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Designation Master");
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
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Designation Master");
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
            var RequestData = (UpdateDesignationMasterRequest)RequestObj;
            var ResponseData = new UpdateDesignationMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;


                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateDesignationMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.DesignationMasterData.ID;

                var DesignationCode = _CommandObj.Parameters.Add("@DesignationCode", SqlDbType.NVarChar);
                DesignationCode.Direction = ParameterDirection.Input;
                DesignationCode.Value = RequestData.DesignationMasterData.DesignationCode;

                var DesignationName = _CommandObj.Parameters.Add("@DesignationName", SqlDbType.NVarChar);
                DesignationName.Direction = ParameterDirection.Input;
                DesignationName.Value = RequestData.DesignationMasterData.DesignationName;

                var RoleId = _CommandObj.Parameters.Add("@RoleId", SqlDbType.Int);
                RoleId.Direction = ParameterDirection.Input;
                RoleId.Value = RequestData.DesignationMasterData.RoleId;

                SqlParameter RoleCode = _CommandObj.Parameters.Add("@RoleCode", SqlDbType.NVarChar);
                RoleCode.Direction = ParameterDirection.Input;
                RoleCode.Value = RequestData.DesignationMasterData.RoleCode;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.DesignationMasterData.Description;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.DesignationMasterData.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.DesignationMasterData.Active;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.DesignationMasterData.SCN;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.DesignationMasterData.UpdateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Designation Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Designation Master");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Designation Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Designation Master");
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
            var DesignationMasterRecord = new DesignationMaster();

            var RequestData = (DeleteDesignationMasterRequest)RequestObj;
            var ResponseData = new DeleteDesignationMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sSql = "delete from  DesignationMaster where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Designation Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Designation Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var DesignationMasterRecord = new DesignationMaster();
            var RequestData = (SelectByIDDesignationMasterRequest)RequestObj;
            var ResponseData = new SelectByIDDesignationMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from DesignationMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objDesignationMaster = new DesignationMaster();
                        objDesignationMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDesignationMaster.DesignationCode = Convert.ToString(objReader["DesignationCode"]);
                        objDesignationMaster.DesignationName = Convert.ToString(objReader["DesignationName"]);
                        objDesignationMaster.Description = Convert.ToString(objReader["Description"]);
                        objDesignationMaster.RoleId = objReader["RoleId"] != DBNull.Value ? Convert.ToInt32(objReader["RoleId"]) : 0;
                        objDesignationMaster.RoleCode = Convert.ToString(objReader["RoleCode"]);
                        objDesignationMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objDesignationMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objDesignationMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objDesignationMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objDesignationMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objDesignationMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objDesignationMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        ResponseData.DesignationMasterRecord = objDesignationMaster;
                        ResponseData.ResponseDynamicData = objDesignationMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Designation Master");
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
            var DesignationMasterList = new List<DesignationMaster>();
            var RequestData = (SelectAllDesignationMasterRequest)RequestObj;
            var ResponseData = new SelectAllDesignationMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("Select *,RM.RoleName,RM.RoleCode from DesignationMaster DM left outer join  RoleMaster RM on DM.RoleId=RM.ID  order by DM.id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);                
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objDesignationMaster = new DesignationMaster();
                        objDesignationMaster.ID =objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) :0;
                        objDesignationMaster.RoleId = objReader["RoleId"] != DBNull.Value ? Convert.ToInt32(objReader["RoleId"]) : 0;                        
                        objDesignationMaster.DesignationCode = Convert.ToString(objReader["DesignationCode"]);
                        objDesignationMaster.DesignationName = Convert.ToString(objReader["DesignationName"]);
                        objDesignationMaster.Description = Convert.ToString(objReader["Description"]);
                        objDesignationMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objDesignationMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objDesignationMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objDesignationMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objDesignationMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objDesignationMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objDesignationMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        objDesignationMaster.RoleCode = Convert.ToString(objReader["RoleCode"]);
                        objDesignationMaster.RoleName = Convert.ToString(objReader["RoleName"]);
                        DesignationMasterList.Add(objDesignationMaster);                       
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignationMasterList = DesignationMasterList;
                    ResponseData.ResponseDynamicData = DesignationMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Designation Master");
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

        public override SelectDesignationMasterLookUpResponse SelectDesignationMasterLookUp(SelectDesignationMasterLookUpRequest RequestObj)
        {
            var DesignationMasterList = new List<DesignationMaster>();


            SelectDesignationMasterLookUpRequest RequestData = new SelectDesignationMasterLookUpRequest();

            SelectDesignationMasterLookUpResponse ResponseData = new SelectDesignationMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,DesignationName from DesignationMaster with(NoLock) where Active='True' ";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objDesignationMaster = new DesignationMaster();
                        objDesignationMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDesignationMaster.DesignationName = Convert.ToString(objReader["DesignationName"]);
                        DesignationMasterList.Add(objDesignationMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignationMasterList = DesignationMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Designation Master");
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

        public override SelectAllDesignationMasterResponse API_SelectAll(SelectAllDesignationMasterRequest RequestObj)
        {
            var DesignationMasterList = new List<DesignationMaster>();
            var RequestData = (SelectAllDesignationMasterRequest)RequestObj;
            var ResponseData = new SelectAllDesignationMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

               // var sSql = new StringBuilder();
                //sSql.Append("Select DM.ID,DM.DesignationCode,DM.DesignationName,DM.Description,DM.Remarks,DM.Active,RM.RoleName,RM.RoleCode from DesignationMaster DM left outer join  RoleMaster RM on DM.RoleId=RM.ID  order by DM.id  asc");

                string sSql = "Select DM.ID,DM.DesignationCode,DM.DesignationName,DM.Description,DM.Remarks,DM.Active,RM.RoleName,RM.RoleCode, RC.TOTAL_CNT [RecordCount] " +
                "from DesignationMaster DM left outer join  RoleMaster RM on DM.RoleId=RM.ID " +
                "LEFT JOIN(Select  count(DM1.ID) As TOTAL_CNT From DesignationMaster DM1 with(NoLock) " +
                "left outer join  RoleMaster RM1 on DM1.RoleId=RM1.ID " +
                "where DM1.Active = " + RequestData.IsActive + " " +
                    "and (isnull('" + RequestData.SearchString + "','') = '' " +
                        "or DM1.DesignationCode like isnull('%" + RequestData.SearchString + "%','') " +
                        "or DM1.DesignationName like isnull('%" + RequestData.SearchString + "%','') " +
                         "or DM1.Description like isnull('%" + RequestData.SearchString + "%','') " +
                          "or RM1.RoleCode like isnull('%" + RequestData.SearchString + "%','') " +
                          "or DM1.Remarks like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +
                "where DM.Active = " + RequestData.IsActive + " " +
                    "and (isnull('" + RequestData.SearchString + "','') = '' " +
                        "or DM.DesignationCode like isnull('%" + RequestData.SearchString + "%','') " +
                        "or DM.DesignationName like isnull('%" + RequestData.SearchString + "%','') " +
                         "or DM.Description like isnull('%" + RequestData.SearchString + "%','') " +
                          "or RM.RoleCode like isnull('%" + RequestData.SearchString + "%','') " +
                          "or DM.Remarks like isnull('%" + RequestData.SearchString + "%','')) " +
                "order by ID asc " +
                "offset " + RequestData.Offset + " rows " +
                "fetch first " + RequestData.Limit + " rows only";
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objDesignationMaster = new DesignationMaster();
                        objDesignationMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objDesignationMaster.RoleId = objReader["RoleId"] != DBNull.Value ? Convert.ToInt32(objReader["RoleId"]) : 0;
                        objDesignationMaster.DesignationCode = Convert.ToString(objReader["DesignationCode"]);
                        objDesignationMaster.DesignationName = Convert.ToString(objReader["DesignationName"]);
                        objDesignationMaster.Description = Convert.ToString(objReader["Description"]);
                        //objDesignationMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objDesignationMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objDesignationMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objDesignationMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objDesignationMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objDesignationMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objDesignationMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        objDesignationMaster.RoleCode = Convert.ToString(objReader["RoleCode"]);
                        objDesignationMaster.RoleName = Convert.ToString(objReader["RoleName"]);
                        DesignationMasterList.Add(objDesignationMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignationMasterList = DesignationMasterList;
                    //ResponseData.ResponseDynamicData = DesignationMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Designation Master");
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
