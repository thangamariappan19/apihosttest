using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizRequest.Masters.CustomerGroupMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.CompanySettingResponse;
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
    public class CompanySettingDAL : BaseCompanySettingDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveCompanySettingRequest)RequestObj;
            var ResponseData = new SaveCompanySettingResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertCompanySetting", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@CompanyID", RequestData.CompanySettingData.ID); 
                _CommandObj.Parameters.AddWithValue("@CompanyCode", RequestData.CompanySettingData.CompanyCode);
                _CommandObj.Parameters.AddWithValue("@CompanyName", RequestData.CompanySettingData.CompanyName);
                _CommandObj.Parameters.AddWithValue("@Address", RequestData.CompanySettingData.Address);
                _CommandObj.Parameters.AddWithValue("@CountrySettingID", RequestData.CompanySettingData.CountrySettingID);
                _CommandObj.Parameters.AddWithValue("@RetailSettingID", RequestData.CompanySettingData.RetailSettingID);
                _CommandObj.Parameters.AddWithValue("@CountrySettingCode", RequestData.CompanySettingData.CountrySettingCode);               
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.CompanySettingData.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.CompanySettingData.Active);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.CompanySettingData.CreateBy);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Company Settings");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Company Settings");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Company Settings");
                }

            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var RequestData = (UpdateCompanySettingRequest)RequestObj;
            var ResponseData = new UpdateCompanySettingResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("UpdateCompanySetting", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.CompanySettingData.ID);
                _CommandObj.Parameters.AddWithValue("@CompanyCode", RequestData.CompanySettingData.CompanyCode);
                _CommandObj.Parameters.AddWithValue("@CompanyName", RequestData.CompanySettingData.CompanyName);
                _CommandObj.Parameters.AddWithValue("@Address", RequestData.CompanySettingData.Address);
                _CommandObj.Parameters.AddWithValue("@CountrySettingID", RequestData.CompanySettingData.CountrySettingID);
                _CommandObj.Parameters.AddWithValue("@RetailSettingID", RequestData.CompanySettingData.RetailSettingID);
                _CommandObj.Parameters.AddWithValue("@CountrySettingCode", RequestData.CompanySettingData.CountrySettingCode);               
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.CompanySettingData.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.CompanySettingData.Active);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.CompanySettingData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@SCN", RequestData.CompanySettingData.SCN);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Company Settings");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Company Settings");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Company Settings");
                }


            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var CompanySettings = new CompanySettings();
            var RequestData = (DeleteCompanySettingRequest)RequestObj;
            var ResponseData = new DeleteCompanySettingResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from CompanySettings where ID='{0}'";
                sSql = string.Format(sSql, RequestData.CompanySettingData.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Company Settings");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Company Settings");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var CompanySettings = new CompanySettings();
            var RequestData = (SelectByIDCompanySettingRequest)RequestObj;
            var ResponseData = new SelectByIDCompanySettingResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from CompanySettings with(NoLock) where  ID='{0}'";
               // string sSql = "Select * from CompanySettings";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);



                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCompanySettings = new CompanySettings();
                        objCompanySettings.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCompanySettings.CompanyCode = objReader["CompanyCode"].ToString();
                        objCompanySettings.CompanyName = objReader["CompanyName"].ToString();
                        objCompanySettings.Address = objReader["Address"].ToString();
                        objCompanySettings.CountrySettingID = objReader["CountrySettingID"] != DBNull.Value ? Convert.ToInt32(objReader["CountrySettingID"]) : 0;
                        objCompanySettings.RetailSettingID = objReader["RetailSettingID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailSettingID"]) : 0;
                        objCompanySettings.RetailSettingCode = objReader["RetailSettingCode"].ToString();
                        objCompanySettings.CountrySettingCode = objReader["CountrySettingCode"].ToString();
                        objCompanySettings.Remarks = objReader["Remarks"].ToString();
                        objCompanySettings.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCompanySettings.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCompanySettings.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCompanySettings.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCompanySettings.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCompanySettings.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCompanySettings.Remarks = objReader["Remarks"].ToString();
                        ResponseData.CompanySettings = objCompanySettings;
                        ResponseData.ResponseDynamicData = objCompanySettings;

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Company Settings");
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
            var CompanySettingsList = new List<CompanySettings>();

            var RequestData = new SelectAllCompanySettingRequest();
            var ResponseData = new SelectAllCompanySettingResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                RequestData.ShowInActiveRecords = true;
                var sSql = new StringBuilder();
                //sSql.Append("Select  ROW_NUMBER() OVER(ORDER BY CustomerName asc) AS RowNumber, ");
                sSql.Append("Select CS.*, RS.RetailName,CM.CountryName from CompanySettings CS with(NoLock) ");
                sSql.Append("left join CountryMaster CM ON CS.CountrySettingID=CM.ID   ");
                sSql.Append("left join RetailSettings RS ON CS.RetailSettingID=RS.ID  ");
                sSql.Append("where  CM.Active='True' order by id  asc  ");
               _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objCompanySettings = new CompanySettings();

                        objCompanySettings.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCompanySettings.CompanyCode = objReader["CompanyCode"].ToString();
                        objCompanySettings.CompanyName = objReader["CompanyName"].ToString();
                        objCompanySettings.Address = objReader["Address"].ToString();
                        objCompanySettings.CountrySettingID = objReader["CountrySettingID"] != DBNull.Value ? Convert.ToInt32(objReader["CountrySettingID"]) : 0;
                        objCompanySettings.RetailSettingID = objReader["RetailSettingID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailSettingID"]) : 0;
                        objCompanySettings.RetailSettingCode = objReader["RetailSettingCode"].ToString();
                        objCompanySettings.CountrySettingCode = objReader["CountrySettingCode"].ToString();
                        objCompanySettings.Remarks = objReader["Remarks"].ToString();
                        objCompanySettings.CountryName = objReader["CountryName"].ToString();
                        objCompanySettings.RetailName = objReader["RetailName"].ToString();


                        objCompanySettings.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCompanySettings.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCompanySettings.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCompanySettings.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCompanySettings.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCompanySettings.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        CompanySettingsList.Add(objCompanySettings);                       

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CompanySettingList = CompanySettingsList;
                    ResponseData.ResponseDynamicData = CompanySettingsList;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Company Settings");
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

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override SelectCompanySettingsLookUpResponse SelectCompanySettingsLookUp(SelectCompanySettingsLookUpRequest RequestObj)
        {
            var CompanySettingsList = new List<CompanySettings>();

            var RequestData = (SelectCompanySettingsLookUpRequest)RequestObj; ;
            var ResponseData = new SelectCompanySettingsLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                sQuery = "Select ID,CompanyName,CompanyCode from CompanySettings with(NoLock) where Active='True' and CountrySettingID = '" + RequestData.CountryID + "'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCompanySettings = new CompanySettings();
                        objCompanySettings.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCompanySettings.CompanyName = Convert.ToString(objReader["CompanyName"]);
                        objCompanySettings.CompanyCode = Convert.ToString(objReader["CompanyCode"]);
                        CompanySettingsList.Add(objCompanySettings);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CompanySettingsList = CompanySettingsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Company Settings");
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

        public override SelectAllCompanySettingResponse API_SelectAll(SelectAllCompanySettingRequest objRequest)
        {
            var CompanySettingsList = new List<CompanySettings>();

            var RequestData = (SelectAllCompanySettingRequest)objRequest;
            var ResponseData = new SelectAllCompanySettingResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                RequestData.ShowInActiveRecords = true;
                //var sSql = new StringBuilder();
                ////sSql.Append("Select  ROW_NUMBER() OVER(ORDER BY CustomerName asc) AS RowNumber, ");
                //sSql.Append("Select CS.*, RS.RetailName,CM.CountryName from CompanySettings CS with(NoLock) ");
                //sSql.Append("left join CountryMaster CM ON CS.CountrySettingID=CM.ID   ");
                //sSql.Append("left join RetailSettings RS ON CS.RetailSettingID=RS.ID  ");
                //sSql.Append("where  CM.Active='True' order by id  asc  ");

                string sSql = "select CS.ID,CS.CompanyCode,CS.CompanyName,CM.CountryName,CS.Active,CS.Address, RC.TOTAL_CNT [RecordCount] " +
                  "from CompanySettings CS with(NoLock) left join CountryMaster CM ON CS.CountrySettingID=CM.ID " +
                  "LEFT JOIN(Select count(CS1.ID) As TOTAL_CNT From CompanySettings CS1 " +
                      " left join CountryMaster CM1 ON CS1.CountrySettingID = CM1.ID " +
                      "where CS1.Active = " + RequestData.IsActive + " and (isnull('" + RequestData.SearchString + "', '') = '' " +
                      "or CS1.CompanyCode = isnull('%" + RequestData.SearchString + "%', '') " +
                     "or CS1.CompanyName like isnull('%" + RequestData.SearchString + "%', '') " +
                     "or CS1.Address like isnull('%" + RequestData.SearchString + "%', '') " +
                     " or CM1.CountryName like isnull('%" + RequestData.SearchString + "%', ''))) As RC ON 1 = 1 " +

                  "where CS.Active = " + RequestData.IsActive + " " +
                      "and (isnull('" + RequestData.SearchString + "','') = '' " +
                          "or CS.CompanyCode like isnull('%" + RequestData.SearchString + "%','') " +
                          "or CS.CompanyName like isnull('%" + RequestData.SearchString + "%','') " +
                          "or CS.Address like isnull('%" + RequestData.SearchString + "%','') " +
                            "or CM.CountryName like isnull('%" + RequestData.SearchString + "%','')) " +
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

                        var objCompanySettings = new CompanySettings();

                        objCompanySettings.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCompanySettings.CompanyCode = objReader["CompanyCode"].ToString();
                        objCompanySettings.CompanyName = objReader["CompanyName"].ToString();
                        objCompanySettings.Address = objReader["Address"].ToString();
                        //objCompanySettings.CountrySettingID = objReader["CountrySettingID"] != DBNull.Value ? Convert.ToInt32(objReader["CountrySettingID"]) : 0;
                        //objCompanySettings.RetailSettingID = objReader["RetailSettingID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailSettingID"]) : 0;
                        //objCompanySettings.RetailSettingCode = objReader["RetailSettingCode"].ToString();
                        //objCompanySettings.CountrySettingCode = objReader["CountrySettingCode"].ToString();
                       // objCompanySettings.Remarks = objReader["Remarks"].ToString();
                        objCompanySettings.CountryName = objReader["CountryName"].ToString();
                        //objCompanySettings.RetailName = objReader["RetailName"].ToString();


                        //objCompanySettings.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objCompanySettings.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objCompanySettings.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objCompanySettings.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objCompanySettings.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCompanySettings.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        CompanySettingsList.Add(objCompanySettings);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CompanySettingList = CompanySettingsList;
                    ResponseData.ResponseDynamicData = CompanySettingsList;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Company Settings");
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

        public override SelectAllCompanySettingResponse API_SelectCompanySettingLookUp(object objRequest)
        {
            var CompanySettingsList = new List<CompanySettings>();

            var RequestData = new SelectAllCompanySettingRequest();
            var ResponseData = new SelectAllCompanySettingResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                RequestData.ShowInActiveRecords = true;
                //var sSql = new StringBuilder();
                //sSql.Append("Select  ROW_NUMBER() OVER(ORDER BY CustomerName asc) AS RowNumber, ");
                string sSql = "Select  ID , CompanyCode,CompanyName,Active from CompanySettings with(NoLock) where  Active='True'";
               
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objCompanySettings = new CompanySettings();

                        objCompanySettings.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCompanySettings.CompanyCode = objReader["CompanyCode"].ToString();
                        objCompanySettings.CompanyName = objReader["CompanyName"].ToString();                                                               
                        objCompanySettings.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        CompanySettingsList.Add(objCompanySettings);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CompanySettingList = CompanySettingsList;
                   // ResponseData.ResponseDynamicData = CompanySettingsList;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Company Settings");
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
