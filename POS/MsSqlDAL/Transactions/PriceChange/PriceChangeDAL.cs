using EasyBizAbsDAL.Transactions.PriceChange;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.PriceChange;
using EasyBizRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.PriceChange;
using EasyBizResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Transactions.PriceChange;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.PriceChange
{
    public class PriceChangeDAL : BasePriceChangeDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        #region "PriceChange"
        public override ValidatePriceChangeResponse ValidatePriceChangeDetails(ValidatePriceChangeRequest ObjRequest)
        {
            List<PriceChangeDetails> PriceChangeDetailsList = new List<PriceChangeDetails>();
            var RequestData = (ValidatePriceChangeRequest)ObjRequest;
            var ResponseData = new ValidatePriceChangeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                string Str_PriceChangeDetails = PriceChangeDetailsXML(RequestData.ValidatingPriceChangeDetailsList, ObjRequest);

                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_ValidatePriceChangeDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.CommandTimeout = 100;

                SqlParameter PriceChangeDetails = _CommandObj.Parameters.Add("@PriceChangeDetails", SqlDbType.Xml);
                PriceChangeDetails.Direction = ParameterDirection.Input;
                PriceChangeDetails.Value = Str_PriceChangeDetails;

                SqlParameter SourceCountryID = _CommandObj.Parameters.Add("@SourceCountryID", SqlDbType.Int);
                SourceCountryID.Direction = ParameterDirection.Input;
                SourceCountryID.Value = RequestData.SourceCountryID;

                SqlParameter PriceChangeType = _CommandObj.Parameters.Add("@PriceChangeType", SqlDbType.VarChar);
                PriceChangeType.Direction = ParameterDirection.Input;
                PriceChangeType.Value = RequestData.PriceChangeType;


                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceChangeDetails = new PriceChangeDetails();
                        objPriceChangeDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceChangeDetails.style_serialNo = objReader["style_serialNo"] != DBNull.Value ? Convert.ToInt32(objReader["style_serialNo"]) : 0;
                        objPriceChangeDetails.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;
                        objPriceChangeDetails.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"]) : 0;
                        objPriceChangeDetails.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : "";
                        objPriceChangeDetails.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objPriceChangeDetails.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : "";

                        objPriceChangeDetails.BrandName = objReader["BrandName"] != DBNull.Value ? Convert.ToString(objReader["BrandName"]) : "";
                        objPriceChangeDetails.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objPriceChangeDetails.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : "";
                        objPriceChangeDetails.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
                        objPriceChangeDetails.CurrencyCode = objReader["CurrencyCode"] != DBNull.Value ? Convert.ToString(objReader["CurrencyCode"]) : "";

                        objPriceChangeDetails.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;

                        objPriceChangeDetails.PriceListCode = objReader["PriceListCode"] != DBNull.Value ? Convert.ToString(objReader["PriceListCode"]) : "";
                        objPriceChangeDetails.PricePointApplicable = objReader["PricePointApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["PricePointApplicable"]) : false;
                        objPriceChangeDetails.OldPrice = objReader["OldPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["OldPrice"]) : 0;
                        objPriceChangeDetails.NewPrice = objReader["NewPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["NewPrice"]) : 0;

                        objPriceChangeDetails.Status = objReader["Status"] != DBNull.Value ? Convert.ToString(objReader["Status"]) : "";
                        objPriceChangeDetails.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : "";

                        objPriceChangeDetails.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPriceChangeDetails.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPriceChangeDetails.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPriceChangeDetails.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;

                        PriceChangeDetailsList.Add(objPriceChangeDetails);
                    }
                    ResponseData.ValidatingPriceChangeDetailsList = PriceChangeDetailsList;
                    ResponseData.ResponseDynamicData = PriceChangeDetailsList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Change");
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
        public string PriceChangeDetailsXML(List<PriceChangeDetails> PriceChangeDetailsList, BaseRequestType RequestObj)
        {
            var RequestData = RequestObj;
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            List<PriceChangeDetails> TempPriceChangeDetailsList = new List<PriceChangeDetails>();
            if (RequestData.BaseIntegrateStoreID == 0)
            {
                TempPriceChangeDetailsList = PriceChangeDetailsList;
            }
            else
            {
                List<string> BrandCodes;
                List<int> BrandIDs;
                int CountryID = GetCountryID(RequestObj, out BrandCodes, out BrandIDs);
                TempPriceChangeDetailsList = PriceChangeDetailsList.Where(x => x.CountryID == CountryID).ToList<PriceChangeDetails>();

                if (TempPriceChangeDetailsList != null && TempPriceChangeDetailsList.Count > 0 && BrandCodes != null && BrandCodes.Count > 0)
                {
                    TempPriceChangeDetailsList = TempPriceChangeDetailsList.Where(x => BrandIDs.Contains(x.BrandID)).ToList<PriceChangeDetails>();
                }
            }

            foreach (PriceChangeDetails objPriceChangeDetails in TempPriceChangeDetailsList)
            {
                sSql.Append("<PriceChangeDetailsData>");
                sSql.Append("<ID>" + objPriceChangeDetails.ID + "</ID>");
                sSql.Append("<style_serialNo>" + objPriceChangeDetails.style_serialNo + "</style_serialNo>");
                sSql.Append("<HeaderID>" + objPriceChangeDetails.HeaderID + "</HeaderID>");
                sSql.Append("<StyleID>" + objPriceChangeDetails.StyleID + "</StyleID>");
                sSql.Append("<StyleCode>" + (objPriceChangeDetails.StyleCode) + "</StyleCode>");
                sSql.Append("<BrandID>" + objPriceChangeDetails.BrandID + "</BrandID>");
                sSql.Append("<BrandCode>" + objPriceChangeDetails.BrandCode + "</BrandCode>");
                sSql.Append("<BrandName>" + objPriceChangeDetails.BrandName + "</BrandName>");
                sSql.Append("<CountryID>" + (objPriceChangeDetails.CountryID) + "</CountryID>");
                sSql.Append("<CountryCode>" + objPriceChangeDetails.CountryCode + "</CountryCode>");
                sSql.Append("<CurrencyID>" + objPriceChangeDetails.CurrencyID + "</CurrencyID>");
                sSql.Append("<CurrencyCode>" + objPriceChangeDetails.CurrencyCode + "</CurrencyCode>");
                sSql.Append("<PriceListID>" + (objPriceChangeDetails.PriceListID) + "</PriceListID>");
                sSql.Append("<PriceListCode>" + (objPriceChangeDetails.PriceListCode) + "</PriceListCode>");
                sSql.Append("<PricePointApplicable>" + objPriceChangeDetails.PricePointApplicable + "</PricePointApplicable>");
                sSql.Append("<OldPrice>" + objPriceChangeDetails.OldPrice + "</OldPrice>");
                sSql.Append("<NewPrice>" + objPriceChangeDetails.NewPrice + "</NewPrice>");
                sSql.Append("<Status>" + objPriceChangeDetails.Status + "</Status>");
                sSql.Append("<Remarks>" + objPriceChangeDetails.Remarks + "</Remarks>");
                sSql.Append("<CreateBy>" + (objPriceChangeDetails.CreateBy) + "</CreateBy>");
                sSql.Append("<UpdateBy>" + (objPriceChangeDetails.UpdateBy) + "</UpdateBy>");
                sSql.Append("</PriceChangeDetailsData>");

            }
            //return sSql.ToString().Replace("&", "&#38;");
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
            //return sSql.ToString();
        }
        public int GetCountryID(EasyBizRequest.BaseRequestType RequestObj, out List<string> BrandCodes, out List<int> BrandIDs)
        {
            var RequestData = RequestObj;
            int CountryID = 0;
            SqlDataReader objReader;
            BrandCodes = new List<string>();
            BrandIDs = new List<int>();
            string BrandCode = "";
            int BrandID = 0;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "SELECT DISTINCT BrandID, BrandCode, CountryID FROM StoreBrandMapping WHERE StoreID = " + RequestData.BaseIntegrateStoreID.ToString();

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : "";
                        BrandIDs.Add(BrandID);
                        BrandCodes.Add(BrandCode);
                    }
                }
            }
            catch (Exception ex)
            {
                CountryID = 0;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return CountryID;
        }
        public string PriceChangeConutriesXML(List<PriceChangeCountries> PriceChangeCountriesList, BaseRequestType RequestObj)
        {
            string StrVal = "";
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            if (RequestObj.BaseIntegrateStoreID == 0)
            {
                foreach (PriceChangeCountries objPriceChangeCountries in PriceChangeCountriesList)
                {
                    sSql.Append("<PriceChangeCountriesData>");
                    sSql.Append("<ID>" + objPriceChangeCountries.ID + "</ID>");
                    sSql.Append("<HeaderID>" + objPriceChangeCountries.HeaderID + "</HeaderID>");
                    sSql.Append("<Select>" + objPriceChangeCountries.Select + "</Select>");
                    sSql.Append("<CountryID>" + (objPriceChangeCountries.CountryID) + "</CountryID>");
                    sSql.Append("<CountryCode>" + objPriceChangeCountries.CountryCode + "</CountryCode>");
                    sSql.Append("<CountryName>" + objPriceChangeCountries.CountryName + "</CountryName>");
                    sSql.Append("<PricePointApplicable>" + objPriceChangeCountries.PricePointApplicable + "</PricePointApplicable>");
                    sSql.Append("<CurrencyID>" + (objPriceChangeCountries.CurrencyID) + "</CurrencyID>");
                    sSql.Append("<CurrencyCode>" + objPriceChangeCountries.CurrencyCode + "</CurrencyCode>");
                    sSql.Append("</PriceChangeCountriesData>");

                }
                StrVal = sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
            }

            //return sSql.ToString().Replace("&", "&#38;");
            //return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
            //return sSql.ToString();
            return StrVal;
        }
        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SavePriceChangeRequest)RequestObj;
            var ResponseData = new SavePriceChangeResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                string Str_PriceChangeDetails = PriceChangeDetailsXML(RequestData.PriceChangeDetailsList, RequestObj);
                string Str_PriceChangeCountries = PriceChangeConutriesXML(RequestData.PriceChangeCountriesList, RequestObj);

                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdatePriceChange", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.PriceChangeRecord.ID;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.NVarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.PriceChangeRecord.DocumentNo;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.PriceChangeRecord.DocumentDate);

                SqlParameter PriceChangeDate = _CommandObj.Parameters.Add("@PriceChangeDate", SqlDbType.VarChar);
                PriceChangeDate.Direction = ParameterDirection.Input;
                PriceChangeDate.Value = sqlCommon.GetSQLServerDateString(RequestData.PriceChangeRecord.PriceChangeDate);

                SqlParameter PriceChangeType = _CommandObj.Parameters.Add("@PriceChangeType", SqlDbType.NVarChar);
                PriceChangeType.Direction = ParameterDirection.Input;
                PriceChangeType.Value = RequestData.PriceChangeRecord.PriceChangeType;

                SqlParameter MultipleCountry = _CommandObj.Parameters.Add("@MultipleCountry", SqlDbType.Bit);
                MultipleCountry.Direction = ParameterDirection.Input;
                MultipleCountry.Value = RequestData.PriceChangeRecord.MultipleCountry;

                SqlParameter SourceCountryID = _CommandObj.Parameters.Add("@SourceCountryID", SqlDbType.Int);
                SourceCountryID.Direction = ParameterDirection.Input;
                SourceCountryID.Value = RequestData.PriceChangeRecord.SourceCountryID;

                SqlParameter SourceCountryCode = _CommandObj.Parameters.Add("@SourceCountryCode", SqlDbType.NVarChar);
                SourceCountryCode.Direction = ParameterDirection.Input;
                SourceCountryCode.Value = RequestData.PriceChangeRecord.SourceCountryCode;

                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.VarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = RequestData.PriceChangeRecord.Status;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.VarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.PriceChangeRecord.Remarks;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.PriceChangeRecord.CreateBy;

                SqlParameter SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.PriceChangeRecord.SCN;

                SqlParameter PriceChangeDetails = _CommandObj.Parameters.Add("@PriceChangeDetails", SqlDbType.Xml);
                PriceChangeDetails.Direction = ParameterDirection.Input;
                PriceChangeDetails.Value = Str_PriceChangeDetails;

                SqlParameter PriceChangeCountries = _CommandObj.Parameters.Add("@PriceChangeCountries", SqlDbType.Xml);
                PriceChangeCountries.Direction = ParameterDirection.Input;
                PriceChangeCountries.Value = Str_PriceChangeCountries;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.VarChar, 10);
                ID2.Direction = ParameterDirection.Output;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    if (RequestData.PriceChangeRecord.ID == 0)
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Price Change");
                    }
                    else
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Price Change");
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Price Change");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Change");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Change");
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
            throw new NotImplementedException();
        }
        public override BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SelectPriceChangeRecordRequest)RequestObj;
            var ResponseData = new SelectPriceChangeRecordResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "SELECT * FROM PriceChange WHERE ID = {0}";
                sQuery = string.Format(sQuery, RequestData.ID);

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceChange = new EasyBizDBTypes.Transactions.PriceChange.PriceChange();
                        objPriceChange.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceChange.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
                        objPriceChange.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objPriceChange.PriceChangeDate = objReader["PriceChangeDate"] != DBNull.Value ? Convert.ToDateTime(objReader["PriceChangeDate"]) : DateTime.Now;
                        objPriceChange.PriceChangeType = objReader["PriceChangeType"] != DBNull.Value ? Convert.ToString(objReader["PriceChangeType"]) : "";
                        objPriceChange.MultipleCountry = objReader["MultipleCountry"] != DBNull.Value ? Convert.ToBoolean(objReader["MultipleCountry"]) : false;
                        objPriceChange.SourceCountryID = objReader["SourceCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["SourceCountryID"]) : 0;
                        objPriceChange.SourceCountryCode = objReader["SourceCountryCode"] != DBNull.Value ? Convert.ToString(objReader["SourceCountryCode"]) : "";
                        objPriceChange.Status = objReader["Status"] != DBNull.Value ? Convert.ToString(objReader["Status"]) : "";
                        objPriceChange.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : "";
                        objPriceChange.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPriceChange.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

                        objPriceChange.PriceChangeDetailsList = new List<PriceChangeDetails>();
                        var objSelectPriceChangeDetailsRequest = new SelectPriceChangeDetailsRequest();
                        objSelectPriceChangeDetailsRequest.ID = objPriceChange.ID;
                        objSelectPriceChangeDetailsRequest.ConnectionString = RequestData.ConnectionString;
                        var objSelectPriceChangeDetailsResponse = new SelectPriceChangeDetailsResponse();
                        objSelectPriceChangeDetailsResponse = GetPriceChangeDetails(objSelectPriceChangeDetailsRequest);
                        if (objSelectPriceChangeDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPriceChange.PriceChangeDetailsList = objSelectPriceChangeDetailsResponse.PriceChangeDetailsList;
                        }

                        objPriceChange.PriceChangeCountriesList = new List<PriceChangeCountries>();
                        var objSelectPriceChangeCountriesRequest = new SelectPriceChangeCountriesRequest();
                        objSelectPriceChangeCountriesRequest.ID = objPriceChange.ID;
                        objSelectPriceChangeCountriesRequest.ConnectionString = RequestData.ConnectionString;
                        var objSelectPriceChangeCountriesResponse = new SelectPriceChangeCountriesResponse();
                        objSelectPriceChangeCountriesResponse = GetPriceChangeCountries(objSelectPriceChangeCountriesRequest);
                        if (objSelectPriceChangeCountriesResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPriceChange.PriceChangeCountriesList = objSelectPriceChangeCountriesResponse.PriceChangeCountriesList;
                        }



                        ResponseData.PriceChangeRecord = objPriceChange;
                        ResponseData.PriceChangeDetailsList = objPriceChange.PriceChangeDetailsList;
                        ResponseData.PriceChangeCountriesList = objPriceChange.PriceChangeCountriesList;
                        ResponseData.ResponseDynamicData = objPriceChange;
                        ResponseData.ResponseDynamicData.PriceChangeDetailsList = objPriceChange.PriceChangeDetailsList;
                        ResponseData.ResponseDynamicData.PriceChangeCountriesList = objPriceChange.PriceChangeCountriesList;
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Change");
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
        public override BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var PriceChangeList = new List<EasyBizDBTypes.Transactions.PriceChange.PriceChange>();
            var RequestData = (SelectAllPriceChangeRequest)RequestObj;
            var ResponseData = new SelectAllPriceChangeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "SELECT * FROM PriceChange";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceChange = new EasyBizDBTypes.Transactions.PriceChange.PriceChange();
                        objPriceChange.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceChange.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
                        objPriceChange.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objPriceChange.PriceChangeDate = objReader["PriceChangeDate"] != DBNull.Value ? Convert.ToDateTime(objReader["PriceChangeDate"]) : DateTime.Now;
                        objPriceChange.PriceChangeType = objReader["PriceChangeType"] != DBNull.Value ? Convert.ToString(objReader["PriceChangeType"]) : "";
                        objPriceChange.MultipleCountry = objReader["MultipleCountry"] != DBNull.Value ? Convert.ToBoolean(objReader["MultipleCountry"]) : false;
                        objPriceChange.SourceCountryID = objReader["SourceCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["SourceCountryID"]) : 0;
                        objPriceChange.SourceCountryCode = objReader["SourceCountryCode"] != DBNull.Value ? Convert.ToString(objReader["SourceCountryCode"]) : "";
                        objPriceChange.Status = objReader["Status"] != DBNull.Value ? Convert.ToString(objReader["Status"]) : "";
                        objPriceChange.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : "";


                        objPriceChange.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPriceChange.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objPriceChange.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objPriceChange.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPriceChange.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        PriceChangeList.Add(objPriceChange);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PriceChangeList = PriceChangeList;
                    ResponseData.ResponseDynamicData = PriceChangeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Change");
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
            var RequestData = (SelectByIDPriceChangeRequest)RequestObj;
            var ResponseData = new SelectByIDPriceChangeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "SELECT * FROM PriceChange WHERE ID = {0}";
                sQuery = string.Format(sQuery, RequestData.ID);

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceChange = new EasyBizDBTypes.Transactions.PriceChange.PriceChange();
                        objPriceChange.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceChange.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
                        objPriceChange.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objPriceChange.PriceChangeDate = objReader["PriceChangeDate"] != DBNull.Value ? Convert.ToDateTime(objReader["PriceChangeDate"]) : DateTime.Now;
                        objPriceChange.PriceChangeType = objReader["PriceChangeType"] != DBNull.Value ? Convert.ToString(objReader["PriceChangeType"]) : "";
                        objPriceChange.MultipleCountry = objReader["MultipleCountry"] != DBNull.Value ? Convert.ToBoolean(objReader["MultipleCountry"]) : false;
                        objPriceChange.SourceCountryID = objReader["SourceCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["SourceCountryID"]) : 0;
                        objPriceChange.SourceCountryCode = objReader["SourceCountryCode"] != DBNull.Value ? Convert.ToString(objReader["SourceCountryCode"]) : "";
                        objPriceChange.Status = objReader["Status"] != DBNull.Value ? Convert.ToString(objReader["Status"]) : "";
                        objPriceChange.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : "";
                        //objPriceChange.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objPriceChange.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objPriceChange.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objPriceChange.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPriceChange.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

                        objPriceChange.PriceChangeDetailsList = new List<PriceChangeDetails>();

                        SelectPriceChangeDetailsRequest objSelectPriceChangeDetailsRequest = new SelectPriceChangeDetailsRequest();
                        SelectPriceChangeDetailsResponse objSelectPriceChangeDetailsResponse = new SelectPriceChangeDetailsResponse();
                        objSelectPriceChangeDetailsRequest.ID = Convert.ToInt32(objReader["ID"]);
                        objSelectPriceChangeDetailsResponse = GetPriceChangeDetails(objSelectPriceChangeDetailsRequest);
                        if (objSelectPriceChangeDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPriceChange.PriceChangeDetailsList = objSelectPriceChangeDetailsResponse.PriceChangeDetailsList;
                        }

                        objPriceChange.PriceChangeCountriesList = new List<PriceChangeCountries>();

                        SelectPriceChangeCountriesRequest objSelectPriceCountryListRequest = new SelectPriceChangeCountriesRequest();
                        SelectPriceChangeCountriesResponse objSelectPriceCountryListResponse = new SelectPriceChangeCountriesResponse();
                        objSelectPriceCountryListRequest.ID = Convert.ToInt32(objReader["ID"]);
                        objSelectPriceCountryListResponse = GetPriceChangeCountries(objSelectPriceCountryListRequest);
                        if (objSelectPriceCountryListResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPriceChange.PriceChangeCountriesList = objSelectPriceCountryListResponse.PriceChangeCountriesList;
                        }

                        ResponseData.PriceChangeRecord = objPriceChange;
                        ResponseData.ResponseDynamicData = objPriceChange;
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Change");
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
        public override BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override SelectPriceChangeDetailsResponse GetPriceChangeDetails(SelectPriceChangeDetailsRequest ObjRequest)
        {
            List<PriceChangeDetails> PriceChangeDetailsList = new List<PriceChangeDetails>();
            var RequestData = (SelectPriceChangeDetailsRequest)ObjRequest;
            var ResponseData = new SelectPriceChangeDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sQuery = string.Empty;
                sQuery = "SELECT * FROM PriceChangeDetails WHERE HeaderID = {0}";
                sQuery = string.Format(sQuery, RequestData.ID);


                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceChangeDetails = new PriceChangeDetails();
                        objPriceChangeDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceChangeDetails.style_serialNo = objReader["style_serialNo"] != DBNull.Value ? Convert.ToInt32(objReader["style_serialNo"]) : 0;
                        objPriceChangeDetails.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;
                        objPriceChangeDetails.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"]) : 0;
                        objPriceChangeDetails.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : "";
                        objPriceChangeDetails.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objPriceChangeDetails.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : "";
                        objPriceChangeDetails.BrandName = objReader["BrandName"] != DBNull.Value ? Convert.ToString(objReader["BrandName"]) : "";
                        objPriceChangeDetails.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objPriceChangeDetails.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : "";
                        objPriceChangeDetails.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
                        objPriceChangeDetails.CurrencyCode = objReader["CurrencyCode"] != DBNull.Value ? Convert.ToString(objReader["CurrencyCode"]) : "";
                        objPriceChangeDetails.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        objPriceChangeDetails.PriceListCode = objReader["PriceListCode"] != DBNull.Value ? Convert.ToString(objReader["PriceListCode"]) : "";
                        objPriceChangeDetails.PricePointApplicable = objReader["PricePointApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["PricePointApplicable"]) : false;
                        objPriceChangeDetails.OldPrice = objReader["OldPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["OldPrice"]) : 0;
                        objPriceChangeDetails.NewPrice = objReader["NewPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["NewPrice"]) : 0;
                        objPriceChangeDetails.Status = objReader["Status"] != DBNull.Value ? Convert.ToString(objReader["Status"]) : "";
                        objPriceChangeDetails.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : "";
                        objPriceChangeDetails.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPriceChangeDetails.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPriceChangeDetails.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPriceChangeDetails.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;

                        PriceChangeDetailsList.Add(objPriceChangeDetails);
                    }
                    ResponseData.PriceChangeDetailsList = PriceChangeDetailsList;
                    ResponseData.ResponseDynamicData = PriceChangeDetailsList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Change");
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
        public override SelectPriceChangeCountriesResponse GetPriceChangeCountries(SelectPriceChangeCountriesRequest ObjRequest)
        {
            List<PriceChangeCountries> PriceChangeCountriesList = new List<PriceChangeCountries>();
            var RequestData = (SelectPriceChangeCountriesRequest)ObjRequest;
            var ResponseData = new SelectPriceChangeCountriesResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sQuery = string.Empty;
                /*sQuery = "SELECT ISNULL(P.ID,0) AS ID, ISNULL(P.HeaderID,0) AS HeaderID, CASE WHEN P.ID IS NULL THEN 0 ELSE P.[Select] END [Select]"
						+ ", C.ID AS CountryID, C.CountryCode, C.CountryName, CASE WHEN P.ID IS NULL THEN 0 ELSE P.PricePointApplicable END PricePointApplicable" 
						+ ", C.CurrencyID, C.CurrencyCode FROM CountryMaster C LEFT JOIN PriceChangeCountries P ON C.ID = P.CountryID AND P.HeaderID = {0}";
				sQuery =  string.Format(sQuery, RequestData.ID);*/
                sQuery = "SELECT * From PriceChangeCountries Where HeaderID = {0}";
                sQuery = string.Format(sQuery, RequestData.ID);

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjPriceChangeCountries = new PriceChangeCountries();
                        ObjPriceChangeCountries.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        ObjPriceChangeCountries.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;
                        ObjPriceChangeCountries.Select = objReader["Select"] != DBNull.Value ? Convert.ToBoolean(objReader["Select"]) : false;
                        ObjPriceChangeCountries.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        ObjPriceChangeCountries.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : "";
                        ObjPriceChangeCountries.CountryName = objReader["CountryName"] != DBNull.Value ? Convert.ToString(objReader["CountryName"]) : "";
                        ObjPriceChangeCountries.PricePointApplicable = objReader["PricePointApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["PricePointApplicable"]) : false;
                        ObjPriceChangeCountries.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
                        ObjPriceChangeCountries.CurrencyCode = objReader["CurrencyCode"] != DBNull.Value ? Convert.ToString(objReader["CurrencyCode"]) : "";
                        PriceChangeCountriesList.Add(ObjPriceChangeCountries);
                    }
                    ResponseData.PriceChangeCountriesList = PriceChangeCountriesList;
                    ResponseData.ResponseDynamicData = PriceChangeCountriesList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Change");
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
        public override SelectPriceChangeStatusResponse SelectPriceChangeStatus(SelectPriceChangeStatusRequest ObjRequest)
        {
            var RequestData = (SelectPriceChangeStatusRequest)ObjRequest;
            var ResponseData = new SelectPriceChangeStatusResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            int IsInProgressCount = 0, PriceUpdateCount = 0;
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "SELECT ISNULL((SELECT COUNT(ID) FROM PriceChange WHERE IsInProgress = 1),0) AS IsInProgressCount, ISNULL((SELECT COUNT(ID) FROM PriceChange WHERE IsPriceUpdated = 0),0) AS PriceUpdateCount";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        IsInProgressCount = objReader["IsInProgressCount"] != DBNull.Value ? Convert.ToInt32(objReader["IsInProgressCount"]) : 0;
                        PriceUpdateCount = objReader["PriceUpdateCount"] != DBNull.Value ? Convert.ToInt32(objReader["PriceUpdateCount"]) : 0;

                        ResponseData.IsInProgressCount = IsInProgressCount;
                        ResponseData.PriceUpdateCount = PriceUpdateCount;
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Change");
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
        public override PriceUpdateResponse UpdateStylePrice(PriceUpdateRequest ObjRequest)
        {
            var RequestData = (PriceUpdateRequest)ObjRequest;
            var ResponseData = new PriceUpdateResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("PriceChange_UpdatePrice", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StoreID;

                SqlParameter PriceChangeDate = _CommandObj.Parameters.Add("@PriceChangeDate", SqlDbType.DateTime);
                PriceChangeDate.Direction = ParameterDirection.Input;
                PriceChangeDate.Value = sqlCommon.GetSQLServerDateString(RequestData.PriceChangeDate);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Price Change");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Price Change");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Change");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Change");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        #endregion
        #region "Price Change Log"
        public override SelectPriceChangeLogResponse SelectPriceChangeLog(SelectPriceChangeLogRequest ObjRequest)
        {
            var RequestData = (SelectPriceChangeLogRequest)ObjRequest;
            var ResponseData = new SelectPriceChangeLogResponse();
            SqlDataAdapter objAdapter;
            DataTable DT = new DataTable();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("PriceChangeLogReport", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.CommandTimeout = 100;

                SqlParameter FromStyleCode = _CommandObj.Parameters.Add("@FromStyleCode", SqlDbType.VarChar);
                FromStyleCode.Direction = ParameterDirection.Input;
                FromStyleCode.Value = RequestData.FromStyleCode;

                SqlParameter ToStyleCode = _CommandObj.Parameters.Add("@ToStyleCode", SqlDbType.VarChar);
                ToStyleCode.Direction = ParameterDirection.Input;
                ToStyleCode.Value = RequestData.ToStyleCode;

                ResponseData.DT_PriceChange = new DataTable();

                using (objAdapter = new SqlDataAdapter(_CommandObj))
                {
                    objAdapter.Fill(DT);

                }

                if (DT != null && DT.Rows.Count > 0)
                {
                    ResponseData.DT_PriceChange = DT.Select("", "").CopyToDataTable();
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Change Log Report");
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

        public override SelectAllPriceChangeResponse API_SelectALL(SelectAllPriceChangeRequest requestData)
        {
            var PriceChangeList = new List<EasyBizDBTypes.Transactions.PriceChange.PriceChange>();
            var RequestData = (SelectAllPriceChangeRequest)requestData;
            var ResponseData = new SelectAllPriceChangeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sQuery = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //sQuery = "SELECT * FROM PriceChange";

                sQuery.Append("SELECT ID, DocumentNo, DocumentDate, PriceChangeDate, PriceChangeType, SourceCountryCode, Status, Remarks, Active, RC.TOTAL_CNT [RecordCount] ");
                sQuery.Append("FROM PriceChange with(NoLock) ");
                sQuery.Append(" LEFT JOIN(Select  count(PC.ID) As TOTAL_CNT From PriceChange PC with(NoLock) ");
                sQuery.Append("where PC.Active = " + RequestData.IsActive + " ");
                sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sQuery.Append("or PC.DocumentNo like isnull('%" + RequestData.SearchString + "%','') ");
                if (RequestData.SearchString.Contains('-'))
                {
                    sQuery.Append("or PC.DocumentDate like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or PC.PriceChangeDate like isnull('%" + RequestData.SearchString + "%','') ");
                }
                sQuery.Append("or PC.PriceChangeType like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or PC.SourceCountryCode like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or PC.Status like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or PC.Remarks like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");

                sQuery.Append("where Active = " + RequestData.IsActive + " ");
                sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sQuery.Append("or DocumentNo like isnull('%" + RequestData.SearchString + "%','') ");
                if (RequestData.SearchString.Contains('-'))
                        {
                        sQuery.Append("or DocumentDate like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or PriceChangeDate like isnull('%" + RequestData.SearchString + "%','') ");
                }
                sQuery.Append("or PriceChangeType like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or SourceCountryCode like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or Status like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or Remarks like isnull('%" + RequestData.SearchString + "%','')) ");
                sQuery.Append("order by ID asc ");
                sQuery.Append("offset " + RequestData.Offset + " rows ");
                sQuery.Append("fetch first " + RequestData.Limit + " rows only");

                _CommandObj = new SqlCommand(sQuery.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceChange = new EasyBizDBTypes.Transactions.PriceChange.PriceChange();
                        objPriceChange.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceChange.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
                        objPriceChange.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objPriceChange.PriceChangeDate = objReader["PriceChangeDate"] != DBNull.Value ? Convert.ToDateTime(objReader["PriceChangeDate"]) : DateTime.Now;
                        objPriceChange.PriceChangeType = objReader["PriceChangeType"] != DBNull.Value ? Convert.ToString(objReader["PriceChangeType"]) : "";
                        //objPriceChange.MultipleCountry = objReader["MultipleCountry"] != DBNull.Value ? Convert.ToBoolean(objReader["MultipleCountry"]) : false;
                        //objPriceChange.SourceCountryID = objReader["SourceCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["SourceCountryID"]) : 0;
                        objPriceChange.SourceCountryCode = objReader["SourceCountryCode"] != DBNull.Value ? Convert.ToString(objReader["SourceCountryCode"]) : "";
                        objPriceChange.Status = objReader["Status"] != DBNull.Value ? Convert.ToString(objReader["Status"]) : "";
                        objPriceChange.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : "";
                        objPriceChange.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        //objPriceChange.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objPriceChange.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objPriceChange.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objPriceChange.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objPriceChange.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        PriceChangeList.Add(objPriceChange);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.PriceChangeList = PriceChangeList;
                    ResponseData.ResponseDynamicData = PriceChangeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Change");
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

        //public override BaseResponseType InsertPaymentProcessorRecord(BaseRequestType RequestObj)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion


    }
}
