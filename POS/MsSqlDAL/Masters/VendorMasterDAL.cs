using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.VendorMasterRequest;
using EasyBizResponse.Masters.VendorMasterResponse;
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
     
   public class VendorMasterDAL : BaseVendorMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveVendorRequest)RequestObj;
            var ResponseData = new SaveVendorResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertVendorMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter VendorID = _CommandObj.Parameters.Add("@VendorID", SqlDbType.Int);
                VendorID.Direction = ParameterDirection.Input;
                VendorID.Value = RequestData.VendorRecord.ID;
                
                SqlParameter VendorCode = _CommandObj.Parameters.Add("@VendorCode", SqlDbType.NVarChar);
                VendorCode.Direction = ParameterDirection.Input;
                VendorCode.Value = RequestData.VendorRecord.VendorCode;

                SqlParameter VendorName = _CommandObj.Parameters.Add("@VendorName", SqlDbType.NVarChar);
                VendorName.Direction = ParameterDirection.Input;
                VendorName.Value = RequestData.VendorRecord.VendorName;

                SqlParameter ShortName = _CommandObj.Parameters.Add("@ShortName", SqlDbType.NVarChar);
                ShortName.Direction = ParameterDirection.Input;
                ShortName.Value = RequestData.VendorRecord.ShortName;

                SqlParameter PhoneNumber = _CommandObj.Parameters.Add("@PhoneNumber", SqlDbType.BigInt);
                PhoneNumber.Direction = ParameterDirection.Input;
                PhoneNumber.Value = RequestData.VendorRecord.PhoneNumber;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.VendorRecord.CountryID;
                
                SqlParameter CompanyID = _CommandObj.Parameters.Add("@CompanyID", SqlDbType.Int);
                CompanyID.Direction = ParameterDirection.Input;
                CompanyID.Value = RequestData.VendorRecord.CompanyID;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.VendorRecord.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.VendorRecord.Active;

                SqlParameter VendorGroupMasterID = _CommandObj.Parameters.Add("@VendorGroupMasterID", SqlDbType.Int);
                VendorGroupMasterID.Direction = ParameterDirection.Input;
                VendorGroupMasterID.Value = RequestData.VendorRecord.VendorGroupID;

                SqlParameter Address = _CommandObj.Parameters.Add("@Address", SqlDbType.NVarChar);
                Address.Direction = ParameterDirection.Input;
                Address.Value = RequestData.VendorRecord.Address;

                SqlParameter EmailID = _CommandObj.Parameters.Add("@EmailID", SqlDbType.NVarChar);
                EmailID.Direction = ParameterDirection.Input;
                EmailID.Value = RequestData.VendorRecord.EmailID;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.VendorRecord.CreateBy;

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Vendor Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Vendor Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Vendor Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Vendor Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateVendorRequest)RequestObj;
            var ResponseData = new UpdateVendorResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("UpdateVendorMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.VendorRecord.ID;

                SqlParameter VendorCode = _CommandObj.Parameters.Add("@VendorCode", SqlDbType.NVarChar);
                VendorCode.Direction = ParameterDirection.Input;
                VendorCode.Value = RequestData.VendorRecord.VendorCode;

                SqlParameter VendorName = _CommandObj.Parameters.Add("@VendorName", SqlDbType.NVarChar);
                VendorName.Direction = ParameterDirection.Input;
                VendorName.Value = RequestData.VendorRecord.VendorName;

                SqlParameter ShortName = _CommandObj.Parameters.Add("@ShortName", SqlDbType.NVarChar);
                ShortName.Direction = ParameterDirection.Input;
                ShortName.Value = RequestData.VendorRecord.ShortName;

                SqlParameter PhoneNumber = _CommandObj.Parameters.Add("@PhoneNumber", SqlDbType.BigInt);
                PhoneNumber.Direction = ParameterDirection.Input;
                PhoneNumber.Value = RequestData.VendorRecord.PhoneNumber;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.VendorRecord.CountryID;

                SqlParameter CompanyID = _CommandObj.Parameters.Add("@CompanyID", SqlDbType.Int);
                CompanyID.Direction = ParameterDirection.Input;
                CompanyID.Value = RequestData.VendorRecord.CompanyID;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.VendorRecord.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.VendorRecord.Active;

                SqlParameter VendorGroupMasterID = _CommandObj.Parameters.Add("@VendorGroupMasterID", SqlDbType.Int);
                VendorGroupMasterID.Direction = ParameterDirection.Input;
                VendorGroupMasterID.Value = RequestData.VendorRecord.VendorGroupID;

                SqlParameter Address = _CommandObj.Parameters.Add("@Address", SqlDbType.NVarChar);
                Address.Direction = ParameterDirection.Input;
                Address.Value = RequestData.VendorRecord.Address;

                SqlParameter EmailID = _CommandObj.Parameters.Add("@EmailID", SqlDbType.NVarChar);
                EmailID.Direction = ParameterDirection.Input;
                EmailID.Value = RequestData.VendorRecord.EmailID;

                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.VendorRecord.UpdateBy;

                SqlParameter SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.VendorRecord.SCN;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;
                
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Vendor Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Vendor Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Vendor Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Vendor Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var VendorRecord = new VendorMaster();
            var RequestData = (DeleteVendorRequest)RequestObj;
            var ResponseData = new DeleteVendorResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from VendorMaster where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Vendor Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Vendor Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var VendorRecord = new VendorMaster();
            var RequestData = (SelectByVendorIDRequest)RequestObj;
            var ResponseData = new SelectByVendorIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from VendorMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        VendorMaster objVendor = new VendorMaster();
                        objVendor.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objVendor.VendorCode = Convert.ToString(objReader["VendorCode"]);
                        objVendor.VendorName = Convert.ToString(objReader["VendorName"]);
                        objVendor.ShortName = Convert.ToString(objReader["ShortName"]);
                        objVendor.PhoneNumber = objReader["PhoneNumber"] != DBNull.Value ? Convert.ToInt64(objReader["PhoneNumber"]) : 0;
                        objVendor.CountryID =objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) :0;
                        objVendor.CompanyID =objReader["CompanyID"] != DBNull.Value ? Convert.ToInt32(objReader["CompanyID"]) :0;
                        objVendor.VendorGroupID = objReader["VendorGroupMasterID"] != DBNull.Value ? Convert.ToInt32(objReader["VendorGroupMasterID"]) : 0;
                        objVendor.Address = Convert.ToString(objReader["Address"]);
                        objVendor.EmailID = Convert.ToString(objReader["EmailID"]);
                        objVendor.Remarks = Convert.ToString(objReader["Remarks"]);
                        objVendor.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objVendor.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objVendor.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objVendor.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objVendor.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objVendor.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ResponseData.VendorRecord = objVendor;
                        ResponseData.ResponseDynamicData = objVendor;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Vendor Master");
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

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var VendorList = new List<VendorMaster>();
            var RequestData = (SelectAllVendorRequest)RequestObj;
            var ResponseData = new SelectAllVendorResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select VM.*,VGM.VendorGroupName,CS.CompanyName,CM.CountryName from VendorMaster VM  ");
                sSql.Append("left outer join VendorGroupMaster VGM on VM.VendorGroupMasterID=VGM.ID    ");
                sSql.Append("left outer join CompanySettings CS on VM.CompanyID=CS.ID   ");
                sSql.Append("left outer join CountryMaster CM on VM.CountryID=CM.ID  ");
               
                 sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objVendor = new VendorMaster();
                        objVendor.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objVendor.VendorCode = Convert.ToString(objReader["VendorCode"]);
                        objVendor.VendorName = Convert.ToString(objReader["VendorName"]);
                        objVendor.ShortName = Convert.ToString(objReader["ShortName"]);
                        objVendor.PhoneNumber =objReader["PhoneNumber"] != DBNull.Value ? Convert.ToInt64(objReader["PhoneNumber"]) :0;
                        objVendor.CountryID =objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) :0;
                        objVendor.CompanyID =objReader["CompanyID"] != DBNull.Value ? Convert.ToInt32(objReader["CompanyID"]) :0;
                        objVendor.VendorGroupID = objReader["VendorGroupMasterID"] != DBNull.Value ? Convert.ToInt32(objReader["VendorGroupMasterID"]) : 0;
                        objVendor.Address = Convert.ToString(objReader["Address"]);
                        objVendor.EmailID = Convert.ToString(objReader["EmailID"]);
                        objVendor.Remarks = Convert.ToString(objReader["Remarks"]);
                        objVendor.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objVendor.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objVendor.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objVendor.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objVendor.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objVendor.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objVendor.CompanyName = Convert.ToString(objReader["CompanyName"]);
                        objVendor.CountryName = Convert.ToString(objReader["CountryName"]);
                        objVendor.VendorGroupName = Convert.ToString(objReader["VendorGroupName"]);
                        VendorList.Add(objVendor);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.VendorList = VendorList;
                    ResponseData.ResponseDynamicData = VendorList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Vendor Master");
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
