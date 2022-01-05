using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.BarcodeSettingsRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.BarcodeSettingsResponse;
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
    public class BarcodeSettingsDAL: BaseBarcodeSettingsDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveBarcodeSettingsRequest)RequestObj;
            var ResponseData = new SaveBarcodeSettingsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertOrUpdateBarcodeSettings", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var BarcodeSettings = _CommandObj.Parameters.Add("@BarcodeSettings", SqlDbType.Xml);
                BarcodeSettings.Direction = ParameterDirection.Input;
                BarcodeSettings.Value = BarcodeSettingsListXml(RequestData.BarcodeSettingsList);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                //SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                //ID.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Barcode Settings");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    // ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Barcode Settings");
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Barcode Settings");
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
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Barcode Settings");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        private string BarcodeSettingsListXml(List<BarcodeSettings> BarcodeSettingsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (BarcodeSettings objBarcodeSettings in BarcodeSettingsList)
            {
                sSql.Append("<BarcodeSettings>");
                sSql.Append("<ID>" + (objBarcodeSettings.ID) + "</ID>");
                sSql.Append("<DocNumID>" + (objBarcodeSettings.DocNumID) + "</DocNumID>");
                sSql.Append("<Prefix>" + (objBarcodeSettings.Prefix) + "</Prefix>");
                sSql.Append("<Suffix>" + objBarcodeSettings.Suffix + "</Suffix>");
                sSql.Append("<StartNumber>" + (objBarcodeSettings.StartNumber) + "</StartNumber>");
                sSql.Append("<EndNumber>" + objBarcodeSettings.EndNumber + "</EndNumber>");
                sSql.Append("<NumberOfDigit>" + objBarcodeSettings.NumberOfDigit + "</NumberOfDigit>");
                sSql.Append("<StartDate>" + sqlCommon.GetSQLServerDateString(objBarcodeSettings.StartDate) + "</StartDate>");
                sSql.Append("<EndDate>" + sqlCommon.GetSQLServerDateString(objBarcodeSettings.EndDate) + "</EndDate>");
                sSql.Append("<RunningNo>" + objBarcodeSettings.RunningNo + "</RunningNo>");
                //sSql.Append("<SCN>" + objBarcodeSettings.SCN + "</SCN>");
                sSql.Append("<Active>" + objBarcodeSettings.Active + "</Active>");
                sSql.Append("<CreateBy>" + objBarcodeSettings.CreateBy + "</CreateBy>");
                sSql.Append("</BarcodeSettings>");
            }
            return sSql.ToString();
        }
        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var RequestData = (UpdateBarcodeSettingsRequest)RequestObj;
            var ResponseData = new UpdateBarcodeSettingsResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateBarcodeSettings", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.BarcodeSettingsData.ID;

                //var CouponCode = _CommandObj.Parameters.Add("@StartDigit", SqlDbType.NVarChar);
                //CouponCode.Direction = ParameterDirection.Input;
                //CouponCode.Value = RequestData.BarcodeSettingsData.StartDigit;

                //var CouponName = _CommandObj.Parameters.Add("@IncrementBy", SqlDbType.NVarChar);
                //CouponName.Direction = ParameterDirection.Input;
                //CouponName.Value = RequestData.BarcodeSettingsData.IncrementBy;

                //var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                //Description.Direction = ParameterDirection.Input;
                //Description.Value = RequestData.BarcodeSettingsData.Description;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.BarcodeSettingsData.Active;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.BarcodeSettingsData.SCN;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.BarcodeSettingsData.UpdateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Barcode Settings");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Barcode Settings");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Barcode Settings");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Barcode Settings");
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
            var BarcodeSettingsRecord = new BarcodeSettings();

            var RequestData = (DeleteBarcodeSettingsRequest)RequestObj;
            var ResponseData = new DeleteBarcodeSettingsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sSql = "Update BarcodeSettings set Active='false' where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Barcode Settings");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Barcode Settings");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var BarcodeSettingsRecord = new BarcodeSettings();
            var RequestData = (SelectByIDBarcodeSettingsRequest)RequestObj;
            var ResponseData = new SelectByIDBarcodeSettingsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from BarcodeSettings with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBarcodeSettings = new BarcodeSettings();
                        //objBarcodeSettings.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        ////objBarcodeSettings.StartDigit = Convert.ToString(objReader["StartDigit"]);
                        ////objBarcodeSettings.IncrementBy = objReader["IncrementBy"] != DBNull.Value ? Convert.ToInt32(objReader["IncrementBy"]) : 0;
                        ////objBarcodeSettings.Description = Convert.ToString(objReader["Description"]);
                        //objBarcodeSettings.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objBarcodeSettings.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objBarcodeSettings.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objBarcodeSettings.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objBarcodeSettings.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objBarcodeSettings.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objBarcodeSettings.ID = Convert.ToInt32(objReader["ID"]);
                        //objBarcodeSettings.DocNumID = Convert.ToInt32(objReader["DocNumID"]);
                        //objBarcodeSettings.DocumentName = Convert.ToString(objReader["DocumentName"]);
                        objBarcodeSettings.Prefix = Convert.ToString(objReader["Prefix"]);
                        objBarcodeSettings.Suffix = Convert.ToString(objReader["Suffix"]);
                        objBarcodeSettings.EndNumber = objReader["EndNumber"] != DBNull.Value ? Convert.ToInt64(objReader["EndNumber"]) : 0;
                        objBarcodeSettings.StartNumber = objReader["StartNumber"] != DBNull.Value ? Convert.ToInt64(objReader["StartNumber"]) : 0;
                        objBarcodeSettings.NumberOfDigit = objReader["NumberOfDigit"] != DBNull.Value ? Convert.ToInt32(objReader["NumberOfDigit"]) : 0;
                        objBarcodeSettings.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objBarcodeSettings.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objBarcodeSettings.RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
                        objBarcodeSettings.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.BarcodeSettingsRecord = objBarcodeSettings;

                        ResponseData.ResponseDynamicData = objBarcodeSettings;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Barcode Settings");
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
            var BarcodeSettingsList = new List<BarcodeSettings>();
            var RequestData = (SelectAllBarcodeSettingsRequest)RequestObj;
            var ResponseData = new SelectAllBarcodeSettingsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select *from BarcodeSettings");             
                              
             
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBarcodeSettings = new BarcodeSettings();
                        objBarcodeSettings.ID = Convert.ToInt32(objReader["ID"]);
                        //objBarcodeSettings.DocNumID = Convert.ToInt32(objReader["DocNumID"]);
                        //objBarcodeSettings.DocumentName = Convert.ToString(objReader["DocumentName"]);
                        objBarcodeSettings.Prefix = Convert.ToString(objReader["Prefix"]);
                        objBarcodeSettings.Suffix = Convert.ToString(objReader["Suffix"]);
                        objBarcodeSettings.EndNumber = objReader["EndNumber"] != DBNull.Value ? Convert.ToInt64(objReader["EndNumber"]) : 0;
                        objBarcodeSettings.StartNumber = objReader["StartNumber"] != DBNull.Value ? Convert.ToInt64(objReader["StartNumber"]) : 0;
                        objBarcodeSettings.NumberOfDigit = objReader["NumberOfDigit"] != DBNull.Value ? Convert.ToInt32(objReader["NumberOfDigit"]) : 0;
                        objBarcodeSettings.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objBarcodeSettings.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objBarcodeSettings.RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
                        objBarcodeSettings.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        BarcodeSettingsList.Add(objBarcodeSettings);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.BarcodeSettingsList = BarcodeSettingsList;
                    ResponseData.ResponseDynamicData = BarcodeSettingsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "BarCode Master");
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

        public override SelectBarcodeSettingsLookUpResponse SelectBarcodeSettingsLookUp(SelectBarcodeSettingsLookUpRequest RequestObj)
        {
            var BarcodeSettingsList = new List<BarcodeSettings>();


            SelectBarcodeSettingsLookUpRequest RequestData = new SelectBarcodeSettingsLookUpRequest();

            SelectBarcodeSettingsLookUpResponse ResponseData = new SelectBarcodeSettingsLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,StartDigit from BarcodeSettings with(NoLock)  where Active='True' ";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBarcodeSettings = new BarcodeSettings();
                        objBarcodeSettings.ID = Convert.ToInt32(objReader["ID"]);
                        //objBarcodeSettings.StartDigit = Convert.ToString(objReader["StartDigit"]);
                        BarcodeSettingsList.Add(objBarcodeSettings);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.BarcodeSettingsList = BarcodeSettingsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Barcode Settings");
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

        public override SelectBarcodeGenerateBySKUResponse SelectBarcodeGenerateBySKU(SelectBarcodeGenerateBySKURequest RequestObj)
        {
            var BarcodeGenerateBySKU = new BarcodeSettings();
            var RequestData = (SelectBarcodeGenerateBySKURequest)RequestObj;
            var ResponseData = new SelectBarcodeGenerateBySKUResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "select *from BarcodeSettings where StartDate < SYSDATETIME() and EndDate > SYSDATETIME()";                
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    int DetailID = 0;
                    int RunningNo = 0;
                    while (objReader.Read())
                    {
                        BarcodeSettings objBarcodeGenerateBySKU = new BarcodeSettings();
                        objBarcodeGenerateBySKU.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBarcodeGenerateBySKU.Prefix = Convert.ToString(objReader["Prefix"]);
                        objBarcodeGenerateBySKU.Suffix = Convert.ToString(objReader["Suffix"]);
                        objBarcodeGenerateBySKU.StartNumber = objReader["StartNumber"] != DBNull.Value ? Convert.ToInt64(objReader["StartNumber"]) : 0;
                        objBarcodeGenerateBySKU.EndNumber = objReader["EndNumber"] != DBNull.Value ? Convert.ToInt64(objReader["EndNumber"]) : 0;
                        objBarcodeGenerateBySKU.NumberOfDigit = objReader["NumberOfDigit"] != DBNull.Value ? Convert.ToInt32(objReader["NumberOfDigit"]) : 0;
                        objBarcodeGenerateBySKU.RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
                       
                        ResponseData.BarcodeGenerateBySKURecord = objBarcodeGenerateBySKU;
                    }
                    objReader.Close();
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;                   
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Document Numbering");
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

        public override SelectAllBarcodeSettingsResponse API_SelectALL(SelectAllBarcodeSettingsRequest requestData)
        {
            var BarcodeSettingsList = new List<BarcodeSettings>();
            var RequestData = (SelectAllBarcodeSettingsRequest)requestData;
            var ResponseData = new SelectAllBarcodeSettingsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select ID,StartNumber,EndNumber,NumberOfDigit,StartDate,EndDate,RunningNo,Active, RC.TOTAL_CNT [RecordCount] from BarcodeSettings with(NoLock) ");
                sSql.Append("LEFT JOIN(Select  count(BS.ID) As TOTAL_CNT From BarcodeSettings BS with(NoLock) ");
                sSql.Append("where BS.Active = " + RequestData.IsActive + " ");
                sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                if (RequestData.SearchString.Contains("-"))
                {
                    sSql.Append("or BS.StartDate like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or BS.EndDate like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");
                }
                else
                {
                    sSql.Append("or BS.StartNumber like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or BS.EndNumber like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or BS.NumberOfDigit like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or BS.RunningNo like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");
                }
                sSql.Append("where Active = " + RequestData.IsActive + " ");
                sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                if (RequestData.SearchString.Contains("-"))
                {
                    sSql.Append("or StartDate like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or EndDate like isnull('%" + RequestData.SearchString + "%','')) ");
                }
                else
                {
                    sSql.Append("or StartNumber like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or EndNumber like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or NumberOfDigit like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or RunningNo like isnull('%" + RequestData.SearchString + "%','')) ");
                }
                sSql.Append("order by ID asc ");
                sSql.Append("offset " + RequestData.Offset + " rows ");
                sSql.Append("fetch first " + RequestData.Limit + " rows only");

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBarcodeSettings = new BarcodeSettings();
                        objBarcodeSettings.ID = Convert.ToInt32(objReader["ID"]);
                        //objBarcodeSettings.DocNumID = Convert.ToInt32(objReader["DocNumID"]);
                        //objBarcodeSettings.DocumentName = Convert.ToString(objReader["DocumentName"]);
                        /*objBarcodeSettings.Prefix = Convert.ToString(objReader["Prefix"]);
                        objBarcodeSettings.Suffix = Convert.ToString(objReader["Suffix"]);*/
                        objBarcodeSettings.EndNumber = objReader["EndNumber"] != DBNull.Value ? Convert.ToInt64(objReader["EndNumber"]) : 0;
                        objBarcodeSettings.StartNumber = objReader["StartNumber"] != DBNull.Value ? Convert.ToInt64(objReader["StartNumber"]) : 0;
                        objBarcodeSettings.NumberOfDigit = objReader["NumberOfDigit"] != DBNull.Value ? Convert.ToInt32(objReader["NumberOfDigit"]) : 0;
                        objBarcodeSettings.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objBarcodeSettings.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objBarcodeSettings.RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
                        objBarcodeSettings.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        BarcodeSettingsList.Add(objBarcodeSettings);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.BarcodeSettingsList = BarcodeSettingsList;
                    ResponseData.ResponseDynamicData = BarcodeSettingsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "BarCode Master");
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
