using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.EmployeeDiscountInfoRequest;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.EmployeeDiscountInfoResponse;
using EasyBizResponse.Masters.EmployeeMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
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

    public class EmployeeMasterDAL : BaseEmployeeMasterDAL
    {
        //Test
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveEmployeeMasterRequest)RequestObj;
            var ResponseData = new SaveEmployeeMasterResponse();

            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertEmployeeMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter EmployeeID = _CommandObj.Parameters.Add("@EmployeeID", SqlDbType.Int);
                EmployeeID.Direction = ParameterDirection.Input;
                EmployeeID.Value = RequestData.BaseID;

                SqlParameter BaseID = _CommandObj.Parameters.Add("@BaseID", SqlDbType.BigInt);
                BaseID.Direction = ParameterDirection.Input;
                BaseID.Value = RequestData.BaseID;

                SqlParameter EmployeeCode = _CommandObj.Parameters.Add("@EmployeeCode", SqlDbType.NVarChar);
                EmployeeCode.Direction = ParameterDirection.Input;
                EmployeeCode.Value = RequestData.EmployeeMasterRecord.EmployeeCode;

                SqlParameter EmployeeName = _CommandObj.Parameters.Add("@EmployeeName", SqlDbType.NVarChar);
                EmployeeName.Direction = ParameterDirection.Input;
                EmployeeName.Value = RequestData.EmployeeMasterRecord.EmployeeName;

                SqlParameter RoleName = _CommandObj.Parameters.Add("@RoleName", SqlDbType.NVarChar);
                RoleName.Direction = ParameterDirection.Input;
                RoleName.Value = RequestData.EmployeeMasterRecord.RoleName;

                SqlParameter Designation = _CommandObj.Parameters.Add("@Designation", SqlDbType.NVarChar);
                Designation.Direction = ParameterDirection.Input;
                Designation.Value = RequestData.EmployeeMasterRecord.Designation;

                SqlParameter DateofJoining = _CommandObj.Parameters.Add("@DateofJoining", SqlDbType.DateTime);
                DateofJoining.Direction = ParameterDirection.Input;
                DateofJoining.Value = RequestData.EmployeeMasterRecord.DateofJoining;

                SqlParameter EmployeeImage = _CommandObj.Parameters.Add("@EmployeeImage", SqlDbType.VarChar);
                EmployeeImage.Direction = ParameterDirection.Input;
                EmployeeImage.Value = RequestData.EmployeeMasterRecord.EmployeeImage;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.VarChar);
                StoreCode.Direction = ParameterDirection.Input;
                if (RequestData.EmployeeMasterRecord.StoreCode != null)
                    StoreCode.Value = RequestData.EmployeeMasterRecord.StoreCode;
                else
                    StoreCode.Value = 0;
                SqlParameter CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.VarChar);
                CountryCode.Direction = ParameterDirection.Input;
                if (RequestData.EmployeeMasterRecord.CountryCode != null)
                    CountryCode.Value = RequestData.EmployeeMasterRecord.CountryCode;
                else
                    CountryCode.Value = 0;

                SqlParameter Salary = _CommandObj.Parameters.Add("@Salary", SqlDbType.Int);
                Salary.Direction = ParameterDirection.Input;
                Salary.Value = RequestData.EmployeeMasterRecord.Salary;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.EmployeeMasterRecord.CountryID;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.EmployeeMasterRecord.StoreID;

                SqlParameter Address = _CommandObj.Parameters.Add("@Address", SqlDbType.NVarChar);
                Address.Direction = ParameterDirection.Input;
                Address.Value = RequestData.EmployeeMasterRecord.Address;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.EmployeeMasterRecord.Remarks;

                SqlParameter PhoneNo = _CommandObj.Parameters.Add("@PhoneNo", SqlDbType.NVarChar);
                PhoneNo.Direction = ParameterDirection.Input;
                PhoneNo.Value = RequestData.EmployeeMasterRecord.PhoneNo;

                SqlParameter IsSelection = _CommandObj.Parameters.Add("@IsSelection", SqlDbType.Bit);
                IsSelection.Direction = ParameterDirection.Input;
                IsSelection.Value = RequestData.EmployeeMasterRecord.IsSelection;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.EmployeeMasterRecord.CreateBy;



                //SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                //UpdateBy.Direction = ParameterDirection.Input;
                //UpdateBy.Value = RequestData.RoleMasterData.UpdateBy;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Employee");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Employee");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Employee");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Employee");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateEmployeeMasterRequest)RequestObj;
            var ResponseData = new UpdateEmployeeMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_UpdateEmployeeMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                //if (_RequestFrom == Enums.RequestFrom.MainServer)
                //{
                //    if (RequestData.BaseID == 0 && RequestData.EmployeeMasterRecord.BaseID !=0)
                //    {
                //        _CommandObj.Parameters.AddWithValue("@ID", RequestData.EmployeeMasterRecord.BaseID);
                //        _CommandObj.Parameters.AddWithValue("@BaseID", RequestData.EmployeeMasterRecord.BaseID);
                //    }
                //    else
                //    {
                //        _CommandObj.Parameters.AddWithValue("@ID", RequestData.EmployeeMasterRecord.ID);
                //        _CommandObj.Parameters.AddWithValue("@BaseID", RequestData.EmployeeMasterRecord.ID);
                //    }
                //}
                //else if (_RequestFrom == Enums.RequestFrom.StoreServer && RequestData.BaseID != 0)
                //{
                //    _CommandObj.Parameters.AddWithValue("@ID", RequestData.EmployeeMasterRecord.ID);
                //    _CommandObj.Parameters.AddWithValue("@BaseID", RequestData.BaseID);
                //}
                //else if (_RequestFrom == Enums.RequestFrom.StoreServer && RequestData.EmployeeMasterRecord.BaseID != 0)
                //{
                //    _CommandObj.Parameters.AddWithValue("@ID", RequestData.EmployeeMasterRecord.ID);
                //    _CommandObj.Parameters.AddWithValue("@BaseID", RequestData.EmployeeMasterRecord.BaseID);
                //}

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.EmployeeMasterRecord.ID;

                SqlParameter BaseID = _CommandObj.Parameters.Add("@BaseID", SqlDbType.BigInt);
                BaseID.Direction = ParameterDirection.Input;
                BaseID.Value = RequestData.BaseID;

                SqlParameter EmployeeCode = _CommandObj.Parameters.Add("@EmployeeCode", SqlDbType.NVarChar);
                EmployeeCode.Direction = ParameterDirection.Input;
                EmployeeCode.Value = RequestData.EmployeeMasterRecord.EmployeeCode;

                SqlParameter EmployeeName = _CommandObj.Parameters.Add("@EmployeeName", SqlDbType.NVarChar);
                EmployeeName.Direction = ParameterDirection.Input;
                EmployeeName.Value = RequestData.EmployeeMasterRecord.EmployeeName;

                SqlParameter RoleName = _CommandObj.Parameters.Add("@RoleName", SqlDbType.NVarChar);
                RoleName.Direction = ParameterDirection.Input;
                RoleName.Value = RequestData.EmployeeMasterRecord.RoleName;

                SqlParameter Designation = _CommandObj.Parameters.Add("@Designation", SqlDbType.NVarChar);
                Designation.Direction = ParameterDirection.Input;
                Designation.Value = RequestData.EmployeeMasterRecord.Designation;

                SqlParameter EmployeeImage = _CommandObj.Parameters.Add("@EmployeeImage", SqlDbType.VarChar);
                EmployeeImage.Direction = ParameterDirection.Input;
                EmployeeImage.Value = RequestData.EmployeeMasterRecord.EmployeeImage;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.VarChar);

                StoreCode.Direction = ParameterDirection.Input;
                if (RequestData.EmployeeMasterRecord.StoreCode != null)
                    StoreCode.Value = RequestData.EmployeeMasterRecord.StoreCode;
                else
                    StoreCode.Value = 0;


                SqlParameter CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.VarChar);
                CountryCode.Direction = ParameterDirection.Input;
                if (RequestData.EmployeeMasterRecord.CountryCode != null)
                    CountryCode.Value = RequestData.EmployeeMasterRecord.CountryCode;
                else
                    CountryCode.Value = 0;



                SqlParameter DateofJoining = _CommandObj.Parameters.Add("@DateofJoining", SqlDbType.DateTime);
                DateofJoining.Direction = ParameterDirection.Input;
                DateofJoining.Value = RequestData.EmployeeMasterRecord.DateofJoining;

                SqlParameter Salary = _CommandObj.Parameters.Add("@Salary", SqlDbType.Int);
                Salary.Direction = ParameterDirection.Input;
                Salary.Value = RequestData.EmployeeMasterRecord.Salary;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.EmployeeMasterRecord.CountryID;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.EmployeeMasterRecord.StoreID;

                SqlParameter Address = _CommandObj.Parameters.Add("@Address", SqlDbType.NVarChar);
                Address.Direction = ParameterDirection.Input;
                Address.Value = RequestData.EmployeeMasterRecord.Address;

                SqlParameter RequestFrom = _CommandObj.Parameters.Add("@RequestFrom", SqlDbType.NVarChar);
                RequestFrom.Direction = ParameterDirection.Input;
                RequestFrom.Value = (int)RequestData.RequestFrom;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.EmployeeMasterRecord.Remarks;

                SqlParameter PhoneNo = _CommandObj.Parameters.Add("@PhoneNo", SqlDbType.NVarChar);
                PhoneNo.Direction = ParameterDirection.Input;
                PhoneNo.Value = RequestData.EmployeeMasterRecord.PhoneNo;

                SqlParameter IsSelection = _CommandObj.Parameters.Add("@IsSelection", SqlDbType.Bit);
                IsSelection.Direction = ParameterDirection.Input;
                IsSelection.Value = RequestData.EmployeeMasterRecord.IsSelection;

                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.EmployeeMasterRecord.UpdateBy;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Employee");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = RequestData.EmployeeMasterRecord.ID.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Employee");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Employee");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Employee");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var EmployeeRecord = new EmployeeMaster();
            var RequestData = (DeleteEmployeeMasterRequest)RequestObj;
            var ResponseData = new DeleteEmployeeMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("delete from EmployeeMaster where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Employee");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Employee");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var EmployeeRecord = new EmployeeMaster();
            var RequestData = (SelectByIDEmployeeMasterRequest)RequestObj;
            var ResponseData = new GetEmployeeByStoreResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Select *,b.ID'RoleID' from EmployeeMaster a inner join RoleMaster b on a.RoleName=b.RoleName where a.ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objEmployeeMaster = new EmployeeMaster();

                        objEmployeeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objEmployeeMaster.BaseID = objReader["BaseID"] != DBNull.Value ? Convert.ToInt32(objReader["BaseID"]) : 0;
                        objEmployeeMaster.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                        objEmployeeMaster.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        objEmployeeMaster.Designation = Convert.ToString(objReader["Designation"]);
                        objEmployeeMaster.RoleName = Convert.ToString(objReader["RoleName"]);
                        objEmployeeMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objEmployeeMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objEmployeeMaster.RoleID = objReader["RoleID"] != DBNull.Value ? Convert.ToInt32(objReader["RoleID"]) : 0;
                        objEmployeeMaster.DateofJoining = objReader["DateofJoining"] != DBNull.Value ? Convert.ToDateTime(objReader["DateofJoining"]) : DateTime.Now;
                        objEmployeeMaster.Salary = objReader["Salary"] != DBNull.Value ? Convert.ToInt32(objReader["Salary"]) : 0;
                        objEmployeeMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objEmployeeMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objEmployeeMaster.Address = Convert.ToString(objReader["Address"]);
                        objEmployeeMaster.PhoneNo = Convert.ToString(objReader["PhoneNo"]);
                        objEmployeeMaster.IsSelection = objReader["IsSelection"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSelection"]) : true;
                        objEmployeeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objEmployeeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objEmployeeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objEmployeeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objEmployeeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objEmployeeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objEmployeeMaster.EmployeeImage = objReader["EmployeeImage"] != DBNull.Value ? Convert.ToString(objReader["EmployeeImage"]) : string.Empty;
                        objEmployeeMaster.Remarks = Convert.ToString(objReader["Remarks"]);

                        ResponseData.EmployeeMasterRecord = objEmployeeMaster;
                        ResponseData.ResponseDynamicData = objEmployeeMaster;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Employee");
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
            var EmployeeMasterList = new List<EmployeeMaster>();
            var RequestData = (SelectAllEmployeeMasterRequest)RequestObj;
            var ResponseData = new SelectAllEmployeeMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = string.Empty;

                if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    sSql = "Select em.*, sm.StoreName " +
                        "from EmployeeMaster em with(NoLock) " +
                        "left join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode " +
                        "join UserMaster um  with(NoLock) on em.ID = um.EmployeeID where em.StoreID={0} and em.Active='True' " +
                        "ORDER BY em.ID DESC";
                    sSql = string.Format(sSql, RequestData.StoreID);
                }
                else
                {
                    sSql = "Select em.*,sm.StoreName from EmployeeMaster em with(NoLock) ";
                    sSql = sSql + "left join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode ";
                    if (RequestData.ShowInActiveRecords == false)
                    {
                        sSql = sSql + " where em.Active='True'";
                    }
                }

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objEmployeeMaster = new EmployeeMaster();

                        objEmployeeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objEmployeeMaster.BaseID = objReader["BaseID"] != DBNull.Value ? Convert.ToInt32(objReader["BaseID"]) : 0;
                        objEmployeeMaster.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                        objEmployeeMaster.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        objEmployeeMaster.RoleName = Convert.ToString(objReader["RoleName"]);
                        objEmployeeMaster.Designation = Convert.ToString(objReader["Designation"]);
                        objEmployeeMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objEmployeeMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objEmployeeMaster.DateofJoining = objReader["DateofJoining"] != DBNull.Value ? Convert.ToDateTime(objReader["DateofJoining"]) : DateTime.Now;
                        objEmployeeMaster.Salary = Convert.ToInt32(objReader["Salary"] != DBNull.Value ? Convert.ToInt32(objReader["Salary"]) : 0);
                        objEmployeeMaster.CountryID = Convert.ToInt32(objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0);
                        //objEmployeeMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objEmployeeMaster.StoreID = Convert.ToInt32(objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0);
                        objEmployeeMaster.Address = Convert.ToString(objReader["Address"]);
                        objEmployeeMaster.PhoneNo = Convert.ToString(objReader["PhoneNo"]);
                        objEmployeeMaster.IsSelection = objReader["IsSelection"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSelection"]) : false;
                        objEmployeeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objEmployeeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objEmployeeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objEmployeeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objEmployeeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objEmployeeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        objEmployeeMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        objEmployeeMaster.EmployeeImage = objReader["EmployeeImage"] != DBNull.Value ? Convert.ToString(objReader["EmployeeImage"]) : string.Empty;
                        objEmployeeMaster.StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : string.Empty;

                        EmployeeMasterList.Add(objEmployeeMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.EmployeeMasterList = EmployeeMasterList;
                    ResponseData.ResponseDynamicData = EmployeeMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Employee");
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

        public override SelectEmployeeLookUpResponse SelectCountryLookUp(SelectEmployeeLookUpRequest ObjRequest)
        {
            var EmployeeLookUpList = new List<EmployeeMaster>();
            var RequestData = (SelectEmployeeLookUpRequest)ObjRequest;
            var ResponseData = new SelectEmployeeLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select ID,EmployeeName,EmployeeCode,RoleName from EmployeeMaster with(NoLock) where Active='True' order by EmployeeName asc";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objEmployeeMaster = new EmployeeMaster();
                        objEmployeeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objEmployeeMaster.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        objEmployeeMaster.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                        objEmployeeMaster.RoleName = Convert.ToString(objReader["RoleName"]);
                        EmployeeLookUpList.Add(objEmployeeMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.EmployeeList = EmployeeLookUpList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Employee");
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

        public override SelectStoreMasterLookUpResponse SelectStoreMasterLookUp(SelectStoreMasterLookUpRequest RequestObj)
        {
            var StoreMasterList = new List<StoreMaster>();


            SelectStoreMasterLookUpRequest RequestData = (SelectStoreMasterLookUpRequest)RequestObj;

            SelectStoreMasterLookUpResponse ResponseData = new SelectStoreMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,StoreName,StoreCode from StoreMaster with(NoLock) where Active='True' and CountryID='" + RequestData.CountryID + "' ";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreMaster = new StoreMaster();
                        objStoreMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        objStoreMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        StoreMasterList.Add(objStoreMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreMasterList = StoreMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master");
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

        public override SelectEmployeeDiscountInfoResponseByCustCode SelectEmployeediscountInfoByCustCode(SelectEmployeeDiscountInfoByCustCode RequestObj)
        {
            var EmployeeDiscountInfoList = new List<EmployeeDiscountInfo>();
            var RequestData = (SelectEmployeeDiscountInfoByCustCode)RequestObj;
            var ResponseData = new SelectEmployeeDiscountInfoResponseByCustCode();
            SqlDataReader objReader;
            var sqlcommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlcommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = string.Empty;

                sSql = "Select * from EmployeeDiscountInfo where Customercode=" + RequestData.CustomerCode + " ";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objEmployeeDiscountInfo = new EmployeeDiscountInfo();

                        objEmployeeDiscountInfo.EmployeeDiscountID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objEmployeeDiscountInfo.CustomerCode = Convert.ToString(objReader["CustomerCode"]);
                        objEmployeeDiscountInfo.UsedAmount = objReader["UsedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["UsedAmount"]) : 0;
                        objEmployeeDiscountInfo.BalanceAmount = objReader["BalanceAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BalanceAmount"]) : 0;
                        EmployeeDiscountInfoList.Add(objEmployeeDiscountInfo);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.EmployeeDiscountInfoList = EmployeeDiscountInfoList;
                    ResponseData.ResponseDynamicData = EmployeeDiscountInfoList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Employee Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlcommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override SelectEmployeeLookUpResponse GetEmployeeByStore(GetEmployeeByStoreRequest objRequest)
        {
            var EmployeeLookUpList = new List<EmployeeMaster>();
            var RequestData = (GetEmployeeByStoreRequest)objRequest;
            var ResponseData = new SelectEmployeeLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select ID,EmployeeName,EmployeeCode from EmployeeMaster with(NoLock) where Active='True' and StoreID = '" + RequestData.StoreID + "' order by EmployeeName asc";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objEmployeeMaster = new EmployeeMaster();
                        objEmployeeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objEmployeeMaster.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        objEmployeeMaster.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                        objEmployeeMaster.comboempcodename = Convert.ToString(objReader["EmployeeName"]) + '-' + Convert.ToString(objReader["EmployeeCode"]);
                        EmployeeLookUpList.Add(objEmployeeMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.EmployeeList = EmployeeLookUpList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Employee Master");
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

        public override SelectAllEmployeeMasterResponse SelectSalesEmployeeForPOS(SelectAllEmployeeMasterRequest objRequest)
        {
            var EmployeeMasterList = new List<EmployeeMaster>();
            var RequestData = (SelectAllEmployeeMasterRequest)objRequest;
            var ResponseData = new SelectAllEmployeeMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = string.Empty;

                //Old Query Before Optimization
                //sSql = "Select em.*, sm.StoreName " +
                //    "from EmployeeMaster em with(NoLock) " +
                //    "left join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode " +
                //    "join UserMaster um  with(NoLock) on em.ID = um.EmployeeID where em.StoreID={0} and em.Active='True' " +
                //    "ORDER BY em.ID DESC";


                sSql = "Select em.ID,em.EmployeeCode,em.EmployeeName,em.EmployeeImage " +
                    "from EmployeeMaster em with(NoLock) " +
                    "where em.StoreID = 2 and em.Active = 'True' " +
                    "ORDER BY em.ID DESC";
                sSql = string.Format(sSql, RequestData.StoreID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objEmployeeMaster = new EmployeeMaster();
                        objEmployeeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objEmployeeMaster.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                        objEmployeeMaster.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        //objEmployeeMaster.RoleName = Convert.ToString(objReader["RoleName"]);
                        //objEmployeeMaster.Designation = Convert.ToString(objReader["Designation"]);
                        //objEmployeeMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        //objEmployeeMaster.StoreID = Convert.ToInt32(objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0);
                        //objEmployeeMaster.Address = Convert.ToString(objReader["Address"]);
                        //objEmployeeMaster.PhoneNo = Convert.ToString(objReader["PhoneNo"]);

                        //objEmployeeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        //objEmployeeMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        objEmployeeMaster.EmployeeImage = objReader["EmployeeImage"] != DBNull.Value ? Convert.ToString(objReader["EmployeeImage"]) : string.Empty;
                        //objEmployeeMaster.StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : string.Empty;

                        EmployeeMasterList.Add(objEmployeeMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.EmployeeMasterList = EmployeeMasterList;
                    //ResponseData.ResponseDynamicData = EmployeeMasterList;

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

        public override SelectAllEmployeeMasterResponse API_SelectALL(SelectAllEmployeeMasterRequest requestData)
        {
            var EmployeeMasterList = new List<EmployeeMaster>();
            var RequestData = (SelectAllEmployeeMasterRequest)requestData;
            var ResponseData = new SelectAllEmployeeMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = string.Empty;
                if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    //sSql = "Select em.*, sm.StoreName " +
                    //    "from EmployeeMaster em with(NoLock) " +
                    //    "left join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode " +
                    //    "join UserMaster um  with(NoLock) on em.ID = um.EmployeeID where em.StoreID={0} and em.Active='True' " +
                    //    "ORDER BY em.ID DESC";

                    sSql = "Select em.ID, em.EmployeeCode, em.EmployeeName, em.RoleName, em.Designation, em.PhoneNo, sm.StoreName, em.Active,RC.TOTAL_CNT [RecordCount]  " +
                    "from EmployeeMaster em with(NoLock) " +
                    "inner join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode " +
                    "join UserMaster um  with(NoLock) on em.ID = um.EmployeeID" +
                     "LEFT JOIN(Select  count(EM1.ID) As TOTAL_CNT From EmployeeMaster EM1 with(NoLock) " +
                     "inner join StoreMaster sm1 with(NoLock) on EM1.StoreCode = sm1.StoreCode " +
                    "join UserMaster um1  with(NoLock) on EM1.ID = um1.EmployeeID where EM1.StoreID={0}" +
                    "and EM1.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or EM1.EmployeeCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM1.EmployeeName like isnull('%" + RequestData.SearchString + "%','')) " +
                       "or EM1.RoleName like isnull('%" + RequestData.SearchString + "%','')) " +
                       "or EM1.Designation like isnull('%" + RequestData.SearchString + "%','')) " +
                       "or EM1.PhoneNo like isnull('%" + RequestData.SearchString + "%','')) " +
                       "or sm1.StoreName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +

                    "where em.StoreID ={ 0}" +
                    "and em.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or em.EmployeeCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or em.EmployeeName like isnull('%" + RequestData.SearchString + "%','')) " +
                       "or em.RoleName like isnull('%" + RequestData.SearchString + "%','')) " +
                       "or em.Designation like isnull('%" + RequestData.SearchString + "%','')) " +
                       "or em.PhoneNo like isnull('%" + RequestData.SearchString + "%','')) " +
                       "or sm.StoreName like isnull('%" + RequestData.SearchString + "%','')) " +
                       "order by em.ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                    sSql = string.Format(sSql, RequestData.StoreID);
                }
                else
                {
                    //sSql = "Select em.*,sm.StoreName from EmployeeMaster em with(NoLock) ";
                    //sSql = sSql + "left join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode ";

                    //if (RequestData.ShowInActiveRecords == false)
                    //{
                    //    sSql = sSql + " where em.Active='True'";
                    //}

                    sSql = "Select em.ID, em.EmployeeCode, em.EmployeeName, em.RoleName, em.Designation, em.PhoneNo, sm.StoreName, em.Active, RC.TOTAL_CNT [RecordCount]  " +
                    "from EmployeeMaster em with(NoLock) " +
                    "inner join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode " +
                    "LEFT JOIN(Select  count(EM1.ID) As TOTAL_CNT From EmployeeMaster EM1 with(NoLock) " +
                    "inner join StoreMaster sm1 with(NoLock) on EM1.StoreCode = sm1.StoreCode " +
                     "where EM1.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or EM1.EmployeeCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM1.EmployeeName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM1.RoleName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM1.Designation like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM1.PhoneNo like isnull('%" + RequestData.SearchString + "%','') " +
                       "or sm1.StoreName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +
                    "where em.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or em.EmployeeCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or em.EmployeeName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or em.RoleName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or em.Designation like isnull('%" + RequestData.SearchString + "%','') " +
                       "or em.PhoneNo like isnull('%" + RequestData.SearchString + "%','') " +
                       "or sm.StoreName like isnull('%" + RequestData.SearchString + "%','')) " +
                       "order by em.ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                }

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objEmployeeMaster = new EmployeeMaster();

                        objEmployeeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objEmployeeMaster.BaseID = objReader["BaseID"] != DBNull.Value ? Convert.ToInt32(objReader["BaseID"]) : 0;
                        objEmployeeMaster.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                        objEmployeeMaster.EmployeeName = Convert.ToString(objReader["EmployeeName"]);

                        objEmployeeMaster.RoleName = Convert.ToString(objReader["RoleName"]);
                        objEmployeeMaster.Designation = Convert.ToString(objReader["Designation"]);
                        //objEmployeeMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        //objEmployeeMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        //objEmployeeMaster.DateofJoining = objReader["DateofJoining"] != DBNull.Value ? Convert.ToDateTime(objReader["DateofJoining"]) : DateTime.Now;
                        //objEmployeeMaster.Salary = Convert.ToInt32(objReader["Salary"] != DBNull.Value ? Convert.ToInt32(objReader["Salary"]) : 0);
                        //objEmployeeMaster.CountryID = Convert.ToInt32(objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0);

                        ////objEmployeeMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        //objEmployeeMaster.StoreID = Convert.ToInt32(objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0);
                        //objEmployeeMaster.Address = Convert.ToString(objReader["Address"]);
                        objEmployeeMaster.PhoneNo = Convert.ToString(objReader["PhoneNo"]);
                        //objEmployeeMaster.IsSelection = objReader["IsSelection"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSelection"]) : false;
                        //objEmployeeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objEmployeeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objEmployeeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objEmployeeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objEmployeeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

                        objEmployeeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        //objEmployeeMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        //objEmployeeMaster.EmployeeImage = objReader["EmployeeImage"] != DBNull.Value ? Convert.ToString(objReader["EmployeeImage"]) : string.Empty;
                        objEmployeeMaster.StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : string.Empty;

                        EmployeeMasterList.Add(objEmployeeMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.EmployeeMasterList = EmployeeMasterList;
                    ResponseData.ResponseDynamicData = EmployeeMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Employee");
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

        public override SelectAllEmployeeMasterResponse API_SelectFilterData(SelectCountryStoreFilterEmployeeMaster requestData)
        {
            var EmployeeMasterList = new List<EmployeeMaster>();
            var RequestData = (SelectCountryStoreFilterEmployeeMaster)requestData;
            var ResponseData = new SelectAllEmployeeMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = string.Empty;
                if (requestData.CountryID != 0 && requestData.StoreID != 0 && requestData.Designation != null)
                {
                    sSql = "Select em.ID, em.EmployeeCode, em.EmployeeName, em.RoleName, em.Designation, em.PhoneNo, sm.StoreName, em.Active, RecordCount = COUNT(*) OVER() " +
                        "from EmployeeMaster em with(NoLock) " +
                        "inner join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode " +
                        "where  sm.id = " + RequestData.StoreID + " " +
                           "and em.CountryID = " + RequestData.CountryID + " " +
                           "and em.Designation = '" + RequestData.Designation + "' " +
                            "and em.Active = " + RequestData.IsActive + " " +
                            "order by em.ID asc " +
                         "offset " + RequestData.Offset + " rows " +
                        "fetch first " + RequestData.Limit + " rows only";
                }
                else if(requestData.CountryID != 0 && requestData.StoreID == 0 && requestData.Designation == null)
                {
                    sSql = "Select em.ID, em.EmployeeCode, em.EmployeeName, em.RoleName, em.Designation, em.PhoneNo, sm.StoreName, em.Active, RecordCount = COUNT(*) OVER() " +
                      "from EmployeeMaster em with(NoLock) " +
                      "inner join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode " +
                      "where  em.CountryID = " + RequestData.CountryID + " " +
                       "and em.Active = " + RequestData.IsActive + " " +
                         "order by em.ID asc " +
                         "offset " + RequestData.Offset + " rows " +
                        "fetch first " + RequestData.Limit + " rows only";

                }
                else if (requestData.CountryID != 0 && requestData.StoreID != 0 && requestData.Designation == null)
                {
                    sSql = "Select em.ID, em.EmployeeCode, em.EmployeeName, em.RoleName, em.Designation, em.PhoneNo, sm.StoreName, em.Active, RecordCount = COUNT(*) OVER() " +
                      "from EmployeeMaster em with(NoLock) " +
                      "inner join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode " +
                      "where  em.CountryID = " + RequestData.CountryID + " " +
                      "and sm.id = " + RequestData.StoreID + " " +
                       "and em.Active = " + RequestData.IsActive + " " +
                         "order by em.ID asc " +
                         "offset " + RequestData.Offset + " rows " +
                        "fetch first " + RequestData.Limit + " rows only";

                }
                else if (requestData.CountryID == 0 && requestData.StoreID == 0 && requestData.Designation != null)
                {
                    sSql = "Select em.ID, em.EmployeeCode, em.EmployeeName, em.RoleName, em.Designation, em.PhoneNo, sm.StoreName, em.Active, RecordCount = COUNT(*) OVER() " +
                      "from EmployeeMaster em with(NoLock) " +
                      "inner join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode " +
                      "where  em.Designation = '" + RequestData.Designation + "' " +
                       "and em.Active = " + RequestData.IsActive + " " +
                        "order by em.ID asc " +
                         "offset " + RequestData.Offset + " rows " +
                        "fetch first " + RequestData.Limit + " rows only";

                }
                if (requestData.CountryID != 0 && requestData.StoreID == 0 && requestData.Designation != null)
                {
                    sSql = "Select em.ID, em.EmployeeCode, em.EmployeeName, em.RoleName, em.Designation, em.PhoneNo, sm.StoreName, em.Active, RecordCount = COUNT(*) OVER() " +
                        "from EmployeeMaster em with(NoLock) " +
                        "inner join StoreMaster sm with(NoLock) on em.StoreCode = sm.StoreCode " +
                        "where  em.CountryID = " + RequestData.CountryID + " " +
                        "and em.Active = " + RequestData.IsActive + " "+
                           "and em.Designation = '" + RequestData.Designation + "' " +
                           "order by em.ID asc " +
                         "offset " + RequestData.Offset + " rows " +
                        "fetch first " + RequestData.Limit + " rows only";
                }
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objEmployeeMaster = new EmployeeMaster();
                        objEmployeeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objEmployeeMaster.BaseID = objReader["BaseID"] != DBNull.Value ? Convert.ToInt32(objReader["BaseID"]) : 0;
                        objEmployeeMaster.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                        objEmployeeMaster.EmployeeName = Convert.ToString(objReader["EmployeeName"]);

                        objEmployeeMaster.RoleName = Convert.ToString(objReader["RoleName"]);
                        objEmployeeMaster.Designation = Convert.ToString(objReader["Designation"]);
                        //objEmployeeMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        //objEmployeeMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        //objEmployeeMaster.DateofJoining = objReader["DateofJoining"] != DBNull.Value ? Convert.ToDateTime(objReader["DateofJoining"]) : DateTime.Now;
                        //objEmployeeMaster.Salary = Convert.ToInt32(objReader["Salary"] != DBNull.Value ? Convert.ToInt32(objReader["Salary"]) : 0);
                        //objEmployeeMaster.CountryID = Convert.ToInt32(objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0);

                        ////objEmployeeMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        //objEmployeeMaster.StoreID = Convert.ToInt32(objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0);
                        //objEmployeeMaster.Address = Convert.ToString(objReader["Address"]);
                        objEmployeeMaster.PhoneNo = Convert.ToString(objReader["PhoneNo"]);
                        //objEmployeeMaster.IsSelection = objReader["IsSelection"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSelection"]) : false;
                        //objEmployeeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objEmployeeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objEmployeeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objEmployeeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objEmployeeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

                        objEmployeeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        //objEmployeeMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        //objEmployeeMaster.EmployeeImage = objReader["EmployeeImage"] != DBNull.Value ? Convert.ToString(objReader["EmployeeImage"]) : string.Empty;
                        objEmployeeMaster.StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : string.Empty;

                        EmployeeMasterList.Add(objEmployeeMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.EmployeeMasterList = EmployeeMasterList;
                    ResponseData.ResponseDynamicData = EmployeeMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Employee");
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



