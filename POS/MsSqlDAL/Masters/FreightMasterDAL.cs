using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using System.Data.SqlClient;
using EasyBizResponse;
using EasyBizRequest.Masters.FreightMasterRequest;
using EasyBizResponse.Masters.FreightMasterResponse;
using System.Data;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Masters
{
    public class FreightMasterDAL: BaseFreightMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveFreightMasterRequest)RequestObj;
            var ResponseData = new SaveFreightMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertFreightMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var FreightID = _CommandObj.Parameters.Add("@FreightID", SqlDbType.Int);
                FreightID.Direction = ParameterDirection.Input;
                FreightID.Value = RequestData.FreightMasterData.ID;

                var FreightCode = _CommandObj.Parameters.Add("@FreightCode", SqlDbType.NVarChar);
                FreightCode.Direction = ParameterDirection.Input;
                FreightCode.Value = RequestData.FreightMasterData.FreightCode;

                var FreightName = _CommandObj.Parameters.Add("@FreightName", SqlDbType.NVarChar);
                FreightName.Direction = ParameterDirection.Input;
                FreightName.Value = RequestData.FreightMasterData.FreightName;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.FreightMasterData.Description;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.FreightMasterData.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.NVarChar);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.FreightMasterData.Active;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.FreightMasterData.CreateBy;

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Freight Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();         
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Freight Master");
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Freight Master");
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
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Freight Master");
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
            var RequestData = (UpdateFreightMasterRequest)RequestObj;
            var ResponseData = new UpdateFreightMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateFreightMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.FreightMasterData.ID;

                var FreightCode = _CommandObj.Parameters.Add("@FreightCode", SqlDbType.NVarChar);
                FreightCode.Direction = ParameterDirection.Input;
                FreightCode.Value = RequestData.FreightMasterData.FreightCode;

                var FreightName = _CommandObj.Parameters.Add("@FreightName", SqlDbType.NVarChar);
                FreightName.Direction = ParameterDirection.Input;
                FreightName.Value = RequestData.FreightMasterData.FreightName;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.FreightMasterData.Description;


                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.FreightMasterData.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.NVarChar);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.FreightMasterData.Active;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.FreightMasterData.SCN;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.FreightMasterData.UpdateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Freight Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Freight Master");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Freight Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Freight Master");
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
            var FreightMasterRecord = new FreightMaster();

            var RequestData = (DeleteFreightMasterRequest)RequestObj;
            var ResponseData = new DeleteFreightMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sSql = "delete from FreightMaster where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Freight Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Freight Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var FreightMasterRecord = new FreightMaster();
            var RequestData = (SelectByIDFreightMasterRequest)RequestObj;
            var ResponseData = new SelectByIDFreightMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from FreightMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objFreightMaster = new FreightMaster();
                        objFreightMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objFreightMaster.FreightCode = Convert.ToString(objReader["FreightCode"]);
                        objFreightMaster.FreightName = Convert.ToString(objReader["FreightName"]);
                        objFreightMaster.Description = Convert.ToString(objReader["Description"]);
                        objFreightMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objFreightMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objFreightMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objFreightMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objFreightMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objFreightMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objFreightMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        ResponseData.FreightMasterRecord = objFreightMaster;
                        ResponseData.ResponseDynamicData = objFreightMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Freight Master");
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
            var FreightMasterList = new List<FreightMaster>();
            var RequestData = (SelectAllFreightMasterRequest)RequestObj;
            var ResponseData = new SelectAllFreightMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from FreightMaster with(NoLock) ";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objFreightMaster = new FreightMaster();
                        objFreightMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objFreightMaster.FreightCode = Convert.ToString(objReader["FreightCode"]);
                        objFreightMaster.FreightName = Convert.ToString(objReader["FreightName"]);
                        objFreightMaster.Description = Convert.ToString(objReader["Description"]);
                        objFreightMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objFreightMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objFreightMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objFreightMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objFreightMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objFreightMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objFreightMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        FreightMasterList.Add(objFreightMaster);                       
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.FreightMasterList = FreightMasterList;
                    ResponseData.ResponseDynamicData = FreightMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Freight Master");
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

        public override SelectFreightMasterLookUpResponse SelectFreightMasterLookUp(SelectFreightMasterLookUpRequest RequestObj)
        {
            var FreightMasterList = new List<FreightMaster>();


            SelectFreightMasterLookUpRequest RequestData = new SelectFreightMasterLookUpRequest();

            SelectFreightMasterLookUpResponse ResponseData = new SelectFreightMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,FreightName from FreightMaster with(NoLock) where Active='True' ";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objFreightMaster = new FreightMaster();
                        objFreightMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objFreightMaster.FreightName = Convert.ToString(objReader["FreightName"]);
                        FreightMasterList.Add(objFreightMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.FreightMasterList = FreightMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Freight Master");
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
    }
    
}
