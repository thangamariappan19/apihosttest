using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;

using EasyBizResponse;
using EasyBizRequest.Masters.WarehouseTypeMasterRequest;
using EasyBizResponse.Masters.WarehouseMasterResponse;
using EasyBizResponse.Masters.WarehouseTypeMasterResponse;
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
    public class WarehouseTypeMasterDAL : BaseWarehouseTypeMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;


        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveWarehouseTypeMasterRequest)RequestObj;
            var ResponseData = new SaveWarehouseTypeMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertWarehouseTypeMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter WareHouseTypeID = _CommandObj.Parameters.Add("@WareHouseTypeID", SqlDbType.Int);
                WareHouseTypeID.Direction = ParameterDirection.Input;
                WareHouseTypeID.Value = RequestData.WarehouseTypMasterData.ID;

                var WarehouseTypeCode = _CommandObj.Parameters.Add("@WarehouseTypeCode", SqlDbType.NVarChar);
                WarehouseTypeCode.Direction = ParameterDirection.Input;
                WarehouseTypeCode.Value = RequestData.WarehouseTypMasterData.WarehouseTypeCode;

                var WarehouseTypeName = _CommandObj.Parameters.Add("@WarehouseTypeName", SqlDbType.NVarChar);
                WarehouseTypeName.Direction = ParameterDirection.Input;
                WarehouseTypeName.Value = RequestData.WarehouseTypMasterData.WarehouseTypeName;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.WarehouseTypMasterData.Description;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.WarehouseTypMasterData.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.WarehouseTypMasterData.Active;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.WarehouseTypMasterData.CreateBy;

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "WarehouseType Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "WarehouseType Master");
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "WarehouseType Master");
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
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "WarehouseType Master");
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
            var RequestData = (UpdateWarehouseTypeMasterRequest)RequestObj;
            var ResponseData = new UpdateWarehouseTypeMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateWarehouseTypeMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.WarehouseTypeMasterData.ID;

                var WarehouseTypeCode = _CommandObj.Parameters.Add("@WarehouseTypeCode", SqlDbType.NVarChar);
                WarehouseTypeCode.Direction = ParameterDirection.Input;
                WarehouseTypeCode.Value = RequestData.WarehouseTypeMasterData.WarehouseTypeCode;

                var WarehouseTypeName = _CommandObj.Parameters.Add("@WarehouseTypeName", SqlDbType.NVarChar);
                WarehouseTypeName.Direction = ParameterDirection.Input;
                WarehouseTypeName.Value = RequestData.WarehouseTypeMasterData.WarehouseTypeName;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.WarehouseTypeMasterData.Description;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.WarehouseTypeMasterData.SCN;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.WarehouseTypeMasterData.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.WarehouseTypeMasterData.Active;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.WarehouseTypeMasterData.UpdateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "WarehouseType Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "WarehouseType Master");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "WarehouseType Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "WarehouseType Master");
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
            var WarehouseTypeMasterRecord = new WarehouseTypeMaster();

            var RequestData = (DeleteWarehouseTypeMasterRequest)RequestObj;
            var ResponseData = new DeleteWarehouseTypeMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                
                string sSql = "Delete from WarehouseTypeMaster where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);       

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "WarehouseType Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "WarehouseType Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var WarehouseTypeMasterRecord = new WarehouseTypeMaster();
            var RequestData = (SelectByIDWarehouseTypeMasterRequest)RequestObj;
            var ResponseData = new SelectByIDWarehouseTypeMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);                
                string sSql = "Select * from WarehouseTypeMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);   
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWarehouseTypeMaster = new WarehouseTypeMaster();
                        objWarehouseTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWarehouseTypeMaster.WarehouseTypeCode = Convert.ToString(objReader["WarehouseTypeCode"]);
                        objWarehouseTypeMaster.WarehouseTypeName = Convert.ToString(objReader["WarehouseTypeName"]);
                        objWarehouseTypeMaster.Description = Convert.ToString(objReader["Description"]);                                                
                        objWarehouseTypeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objWarehouseTypeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objWarehouseTypeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objWarehouseTypeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objWarehouseTypeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objWarehouseTypeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objWarehouseTypeMaster.Remarks = Convert.ToString(objReader["Remarks"]); 
                        ResponseData.WarehouseTypeMasterRecord = objWarehouseTypeMaster;
                        ResponseData.ResponseDynamicData = objWarehouseTypeMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "WarehouseType Master");
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
            var WarehouseTypeMasterList = new List<WarehouseTypeMaster>();
            var RequestData = (SelectAllWarehouseTypeMasterRequest)RequestObj;
            var ResponseData = new SelectAllWarehouseTypeMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = ("Select * from WarehouseTypeMaster with(NoLock)");
               
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWarehouseTypeMaster = new WarehouseTypeMaster();
                        objWarehouseTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWarehouseTypeMaster.WarehouseTypeCode = Convert.ToString(objReader["WarehouseTypeCode"]);
                        objWarehouseTypeMaster.WarehouseTypeName = Convert.ToString(objReader["WarehouseTypeName"]);
                        objWarehouseTypeMaster.Description = Convert.ToString(objReader["Description"]);                                                
                        objWarehouseTypeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objWarehouseTypeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objWarehouseTypeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objWarehouseTypeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objWarehouseTypeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objWarehouseTypeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objWarehouseTypeMaster.Remarks = Convert.ToString(objReader["Remarks"]); 
                        WarehouseTypeMasterList.Add(objWarehouseTypeMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WarehouseTypeMasterList = WarehouseTypeMasterList;
                    ResponseData.ResponseDynamicData = WarehouseTypeMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "WarehouseType Master");
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

        public override SelectWarehouseTypeMasterLookUpResponse SelectWarehouseTypeMasterLookUp(SelectWarehouseTypeMasterLookUpRequest RequestObj)
        {
            var WarehouseTypeMasterList = new List<WarehouseTypeMaster>();


            SelectWarehouseTypeMasterLookUpRequest RequestData = new SelectWarehouseTypeMasterLookUpRequest();

            SelectWarehouseTypeMasterLookUpResponse ResponseData = new SelectWarehouseTypeMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,WarehouseTypeName from WarehouseTypeMaster with(NoLock)  where Active='True'";
               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWarehouseTypeMaster = new WarehouseTypeMaster();
                        objWarehouseTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWarehouseTypeMaster.WarehouseTypeName = Convert.ToString(objReader["WarehouseTypeName"]);
                        WarehouseTypeMasterList.Add(objWarehouseTypeMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WarehouseTypeMasterList = WarehouseTypeMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "WarehouseType Master");
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

        public override SelectAllWarehouseTypeMasterResponse API_SelectAll(SelectAllWarehouseTypeMasterRequest objRequest)
        {
            var WarehouseTypeMasterList = new List<WarehouseTypeMaster>();
            var RequestData = (SelectAllWarehouseTypeMasterRequest)objRequest;
            var ResponseData = new SelectAllWarehouseTypeMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
              //  string sSql = ("Select * from WarehouseTypeMaster with(NoLock)");
                string sSql = "select ID,WarehouseTypeCode,WarehouseTypeName,Description,Active,Remarks, RC.TOTAL_CNT [RecordCount] " +
                  "from WarehouseTypeMaster with(NoLock) " +
                  "LEFT JOIN(Select  count(WT.ID) As TOTAL_CNT From WarehouseTypeMaster WT with(NoLock) " +
                   "where WT.Active = " + RequestData.IsActive + " " +
                      "and (isnull('" + RequestData.SearchString + "','') = '' " +
                          "or WT.WarehouseTypeCode like isnull('%" + RequestData.SearchString + "%','') " +
                          "or WT.WarehouseTypeName like isnull('%" + RequestData.SearchString + "%','') " +
                           "or WT.Description like isnull('%" + RequestData.SearchString + "%','') " +
                            "or WT.Remarks like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                  "where Active = " + RequestData.IsActive + " " +
                      "and (isnull('" + RequestData.SearchString + "','') = '' " +
                          "or WarehouseTypeCode like isnull('%" + RequestData.SearchString + "%','') " +
                          "or WarehouseTypeName like isnull('%" + RequestData.SearchString + "%','') " +
                           "or Description like isnull('%" + RequestData.SearchString + "%','') " +
                            "or Remarks like isnull('%" + RequestData.SearchString + "%','')) " +
                  "order by ID asc " +
                  "offset " + RequestData.Offset + " rows " +
                  "fetch first " + RequestData.Limit + " rows only";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWarehouseTypeMaster = new WarehouseTypeMaster();
                        objWarehouseTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWarehouseTypeMaster.WarehouseTypeCode = Convert.ToString(objReader["WarehouseTypeCode"]);
                        objWarehouseTypeMaster.WarehouseTypeName = Convert.ToString(objReader["WarehouseTypeName"]);
                        objWarehouseTypeMaster.Description = Convert.ToString(objReader["Description"]);
                        //objWarehouseTypeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objWarehouseTypeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objWarehouseTypeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objWarehouseTypeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objWarehouseTypeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objWarehouseTypeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objWarehouseTypeMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        WarehouseTypeMasterList.Add(objWarehouseTypeMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WarehouseTypeMasterList = WarehouseTypeMasterList;
                    //ResponseData.ResponseDynamicData = WarehouseTypeMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "WarehouseType Master");
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

        public override SelectAllWarehouseTypeMasterResponse SelectAllWarehouseTypeMasterLookUp(SelectAllWarehouseTypeMasterRequest RequestObj)
        {
            var WarehouseTypeMasterList = new List<WarehouseTypeMaster>();
            var RequestData = (SelectAllWarehouseTypeMasterRequest)RequestObj;
            var ResponseData = new SelectAllWarehouseTypeMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = ("Select ID ,WarehouseTypeCode,WarehouseTypeName,Active from WarehouseTypeMaster with(NoLock) WHERE Active = 'True'");

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWarehouseTypeMaster = new WarehouseTypeMaster();
                        objWarehouseTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWarehouseTypeMaster.WarehouseTypeCode = Convert.ToString(objReader["WarehouseTypeCode"]);
                        objWarehouseTypeMaster.WarehouseTypeName = Convert.ToString(objReader["WarehouseTypeName"]);
                        //objWarehouseTypeMaster.Description = Convert.ToString(objReader["Description"]);
                        //objWarehouseTypeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objWarehouseTypeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objWarehouseTypeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objWarehouseTypeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                       // objWarehouseTypeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objWarehouseTypeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                       //objWarehouseTypeMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        WarehouseTypeMasterList.Add(objWarehouseTypeMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WarehouseTypeMasterList = WarehouseTypeMasterList;
                   // ResponseData.ResponseDynamicData = WarehouseTypeMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "WarehouseType Master");
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
