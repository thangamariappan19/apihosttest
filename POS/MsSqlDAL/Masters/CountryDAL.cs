using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.CountryResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MsSqlDAL.Masters
{

    public class CountryDAL : BaseCountryDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        SqlTransaction transaction = null;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveCountryRequest)RequestObj;
            var ResponseData = new SaveCountryResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertCountryMaster", _ConnectionObj, transaction);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.CountryMasterData.ID;

                SqlParameter CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.CountryMasterData.CountryCode;

                SqlParameter CountryName = _CommandObj.Parameters.Add("@CountryName", SqlDbType.NVarChar);
                CountryName.Direction = ParameterDirection.Input;
                CountryName.Value = RequestData.CountryMasterData.CountryName;

                SqlParameter LanguageName = _CommandObj.Parameters.Add("@LanguageName", SqlDbType.NVarChar);
                LanguageName.Direction = ParameterDirection.Input;
                LanguageName.Value = RequestData.CountryMasterData.LanguageName;

                SqlParameter DecimalDigit = _CommandObj.Parameters.Add("@DecimalDigit", SqlDbType.NVarChar);
                DecimalDigit.Direction = ParameterDirection.Input;
                DecimalDigit.Value = RequestData.CountryMasterData.DecimalDigit;

                SqlParameter DecimalPlaces = _CommandObj.Parameters.Add("@DecimalPlaces", SqlDbType.NVarChar);
                DecimalPlaces.Direction = ParameterDirection.Input;
                DecimalPlaces.Value = RequestData.CountryMasterData.DecimalPlaces;

                SqlParameter DateFormat = _CommandObj.Parameters.Add("@DateFormat", SqlDbType.NVarChar);
                DateFormat.Direction = ParameterDirection.Input;
                DateFormat.Value = RequestData.CountryMasterData.DateFormat;

                SqlParameter DateSeparator = _CommandObj.Parameters.Add("@DateSeparator", SqlDbType.NVarChar);
                DateSeparator.Direction = ParameterDirection.Input;
                DateSeparator.Value = RequestData.CountryMasterData.DateSeparator;

                SqlParameter NegativeSign = _CommandObj.Parameters.Add("@NegativeSign", SqlDbType.NVarChar);
                NegativeSign.Direction = ParameterDirection.Input;
                NegativeSign.Value = RequestData.CountryMasterData.NegativeSign;

                SqlParameter CurrencySeparator = _CommandObj.Parameters.Add("@CurrencySeparator", SqlDbType.NVarChar);
                CurrencySeparator.Direction = ParameterDirection.Input;
                CurrencySeparator.Value = RequestData.CountryMasterData.CurrencySeparator;

                SqlParameter CurrencyCode = _CommandObj.Parameters.Add("@CurrencyCode", SqlDbType.NVarChar);
                CurrencyCode.Direction = ParameterDirection.Input;
                CurrencyCode.Value = RequestData.CountryMasterData.CurrencyCode;

                SqlParameter TaxCode = _CommandObj.Parameters.Add("@TaxCode", SqlDbType.NVarChar);
                TaxCode.Direction = ParameterDirection.Input;
                TaxCode.Value = RequestData.CountryMasterData.TaxCode;

                SqlParameter CurrencyID = _CommandObj.Parameters.Add("@CurrencyID", SqlDbType.Int);
                CurrencyID.Direction = ParameterDirection.Input;
                CurrencyID.Value = RequestData.CountryMasterData.CurrencyID;

                SqlParameter NearByRoundOff = _CommandObj.Parameters.Add("@NearByRoundOff", SqlDbType.Decimal);
                NearByRoundOff.Direction = ParameterDirection.Input;
                NearByRoundOff.Value = RequestData.CountryMasterData.NearByRoundOff;

                SqlParameter TaxID = _CommandObj.Parameters.Add("@TaxID", SqlDbType.Int);
                TaxID.Direction = ParameterDirection.Input;
                TaxID.Value = RequestData.CountryMasterData.TaxID;

                SqlParameter EmailID = _CommandObj.Parameters.Add("@EmailID", SqlDbType.NVarChar);
                EmailID.Direction = ParameterDirection.Input;
                EmailID.Value = RequestData.CountryMasterData.EmailID;

                SqlParameter CreditLimitCheck = _CommandObj.Parameters.Add("@CreditLimitCheck", SqlDbType.Bit);
                CreditLimitCheck.Direction = ParameterDirection.Input;
                CreditLimitCheck.Value = RequestData.CountryMasterData.CreditLimitCheck;

                SqlParameter AllowMultipleTransaction = _CommandObj.Parameters.Add("@AllowMultipleTransaction", SqlDbType.Bit);
                AllowMultipleTransaction.Direction = ParameterDirection.Input;
                AllowMultipleTransaction.Value = RequestData.CountryMasterData.AllowMultipleTransaction;

                SqlParameter AllowPartialReceiving = _CommandObj.Parameters.Add("@AllowPartialReceiving", SqlDbType.Bit);
                AllowPartialReceiving.Direction = ParameterDirection.Input;
                AllowPartialReceiving.Value = RequestData.CountryMasterData.AllowPartialReceiving;

                SqlParameter AllowSaleAndRedemption = _CommandObj.Parameters.Add("@AllowSaleAndRedemption", SqlDbType.Bit);
                AllowSaleAndRedemption.Direction = ParameterDirection.Input;
                AllowSaleAndRedemption.Value = RequestData.CountryMasterData.AllowSaleAndRedemption;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.CountryMasterData.Active;

                SqlParameter OrginCountry = _CommandObj.Parameters.Add("@OrginCountry", SqlDbType.Bit);
                OrginCountry.Direction = ParameterDirection.Input;
                OrginCountry.Value = RequestData.CountryMasterData.OrginCountry;

                SqlParameter POSTitle = _CommandObj.Parameters.Add("@POSTitle", SqlDbType.NVarChar);
                POSTitle.Direction = ParameterDirection.Input;
                POSTitle.Value = RequestData.CountryMasterData.POSTitle;

                SqlParameter PromotionRoundOff = _CommandObj.Parameters.Add("@PromotionRoundOff", SqlDbType.Decimal);
                PromotionRoundOff.Direction = ParameterDirection.Input;
                PromotionRoundOff.Value = RequestData.CountryMasterData.PromotionRoundOff;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.CountryMasterData.CreateBy;

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Country Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Country Master");

                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Country Master");

                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Country Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
                transaction.Rollback();
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateCountryRequest)RequestObj;

            var ResponseData = new UpdateCountryResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("UpdateCountryMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.CountryMasterData.ID;

                //SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                //CountryID.Direction = ParameterDirection.Input;
                //CountryID.Value = RequestData.CountryMasterData.ID;

                SqlParameter CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.CountryMasterData.CountryCode;

                SqlParameter CountryName = _CommandObj.Parameters.Add("@CountryName", SqlDbType.NVarChar);
                CountryName.Direction = ParameterDirection.Input;
                CountryName.Value = RequestData.CountryMasterData.CountryName;

                SqlParameter LanguageName = _CommandObj.Parameters.Add("@LanguageName", SqlDbType.NVarChar);
                LanguageName.Direction = ParameterDirection.Input;
                LanguageName.Value = RequestData.CountryMasterData.LanguageName;

                SqlParameter DecimalDigit = _CommandObj.Parameters.Add("@DecimalDigit", SqlDbType.NVarChar);
                DecimalDigit.Direction = ParameterDirection.Input;
                DecimalDigit.Value = RequestData.CountryMasterData.DecimalDigit;

                SqlParameter DecimalPlaces = _CommandObj.Parameters.Add("@DecimalPlaces", SqlDbType.Int);
                DecimalPlaces.Direction = ParameterDirection.Input;
                DecimalPlaces.Value = RequestData.CountryMasterData.DecimalPlaces;

                SqlParameter DateFormat = _CommandObj.Parameters.Add("@DateFormat", SqlDbType.NVarChar);
                DateFormat.Direction = ParameterDirection.Input;
                DateFormat.Value = RequestData.CountryMasterData.DateFormat;

                SqlParameter DateSeparator = _CommandObj.Parameters.Add("@DateSeparator", SqlDbType.NVarChar);
                DateSeparator.Direction = ParameterDirection.Input;
                DateSeparator.Value = RequestData.CountryMasterData.DateSeparator;

                SqlParameter CurrencyCode = _CommandObj.Parameters.Add("@CurrencyCode", SqlDbType.NVarChar);
                CurrencyCode.Direction = ParameterDirection.Input;
                CurrencyCode.Value = RequestData.CountryMasterData.CurrencyCode;

                SqlParameter TaxCode = _CommandObj.Parameters.Add("@TaxCode", SqlDbType.NVarChar);
                TaxCode.Direction = ParameterDirection.Input;
                TaxCode.Value = RequestData.CountryMasterData.TaxCode;

                SqlParameter NegativeSign = _CommandObj.Parameters.Add("@NegativeSign", SqlDbType.NVarChar);
                NegativeSign.Direction = ParameterDirection.Input;
                NegativeSign.Value = RequestData.CountryMasterData.NegativeSign;

                SqlParameter NearByRoundOff = _CommandObj.Parameters.Add("@NearByRoundOff", SqlDbType.Decimal);
                NearByRoundOff.Direction = ParameterDirection.Input;
                NearByRoundOff.Value = RequestData.CountryMasterData.NearByRoundOff;

                SqlParameter CurrencySeparator = _CommandObj.Parameters.Add("@CurrencySeparator", SqlDbType.NVarChar);
                CurrencySeparator.Direction = ParameterDirection.Input;
                CurrencySeparator.Value = RequestData.CountryMasterData.CurrencySeparator;

                SqlParameter CurrencyID = _CommandObj.Parameters.Add("@CurrencyID", SqlDbType.Int);
                CurrencyID.Direction = ParameterDirection.Input;
                CurrencyID.Value = RequestData.CountryMasterData.CurrencyID;

                SqlParameter TaxID = _CommandObj.Parameters.Add("@TaxID", SqlDbType.Int);
                TaxID.Direction = ParameterDirection.Input;
                TaxID.Value = RequestData.CountryMasterData.TaxID;

                SqlParameter EmailID = _CommandObj.Parameters.Add("@EmailID", SqlDbType.NVarChar);
                EmailID.Direction = ParameterDirection.Input;
                EmailID.Value = RequestData.CountryMasterData.EmailID;

                SqlParameter CreditLimitCheck = _CommandObj.Parameters.Add("@CreditLimitCheck", SqlDbType.Bit);
                CreditLimitCheck.Direction = ParameterDirection.Input;
                CreditLimitCheck.Value = RequestData.CountryMasterData.CreditLimitCheck;

                SqlParameter AllowMultipleTransaction = _CommandObj.Parameters.Add("@AllowMultipleTransaction", SqlDbType.Bit);
                AllowMultipleTransaction.Direction = ParameterDirection.Input;
                AllowMultipleTransaction.Value = RequestData.CountryMasterData.AllowMultipleTransaction;

                SqlParameter AllowPartialReceiving = _CommandObj.Parameters.Add("@AllowPartialReceiving", SqlDbType.Bit);
                AllowPartialReceiving.Direction = ParameterDirection.Input;
                AllowPartialReceiving.Value = RequestData.CountryMasterData.AllowPartialReceiving;

                SqlParameter AllowSaleAndRedemption = _CommandObj.Parameters.Add("@AllowSaleAndRedemption", SqlDbType.Bit);
                AllowSaleAndRedemption.Direction = ParameterDirection.Input;
                AllowSaleAndRedemption.Value = RequestData.CountryMasterData.AllowSaleAndRedemption;

                SqlParameter OrginCountry = _CommandObj.Parameters.Add("@OrginCountry", SqlDbType.Bit);
                OrginCountry.Direction = ParameterDirection.Input;
                OrginCountry.Value = RequestData.CountryMasterData.OrginCountry;

                SqlParameter POSTitle = _CommandObj.Parameters.Add("@POSTitle", SqlDbType.NVarChar);
                POSTitle.Direction = ParameterDirection.Input;
                POSTitle.Value = RequestData.CountryMasterData.POSTitle;

                SqlParameter PromotionRoundOff = _CommandObj.Parameters.Add("@PromotionRoundOff", SqlDbType.Decimal);
                PromotionRoundOff.Direction = ParameterDirection.Input;
                PromotionRoundOff.Value = RequestData.CountryMasterData.PromotionRoundOff;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.CountryMasterData.Active;

                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.CountryMasterData.UpdateBy;

                //SqlParameter UpdateOn = _CommandObj.Parameters.Add("@UpdateOn", SqlDbType.DateTime);
                //UpdateOn.Direction = ParameterDirection.Input;
                //UpdateOn.Value = RequestData.CountryMasterData.UpdateOn;             

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Country Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Country Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Country Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Country Master");
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
            var CountryMasterRecord = new CountryMaster();
            var RequestData = (DeleteCountryRequest)RequestObj;
            var ResponseData = new DeleteCountryResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Delete from CountryMaster where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Country Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Country Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var CountryMasterRecord = new CountryMaster();
            var RequestData = (SelectByIDCountryRequest)RequestObj;
            var ResponseData = new SelectByIDCountryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select CM.*,CUM.CurrencyCode from CountryMaster CM with(NoLock) ");
                sSql.Append("Inner Join CurrencyMaster CUM on CM.CurrencyID = CUM.ID ");
                sSql.Append("where CM.ID='" + RequestData.ID + "'");
                //_CommandObj = new SqlCommand("Select * from countryMaster with(NoLock) where ID='" + RequestData.ID + "'", _ConnectionObj);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCountryMaster = new CountryMaster();

                        objCountryMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCountryMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objCountryMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objCountryMaster.LanguageName = Convert.ToString(objReader["LanguageName"]);
                        objCountryMaster.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
                        objCountryMaster.TaxCode = Convert.ToString(objReader["TaxCode"]);

                        objCountryMaster.DecimalDigit = objReader["DecimalDigit"] != DBNull.Value ? Convert.ToDecimal(objReader["DecimalDigit"]) : 0;
                        objCountryMaster.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        objCountryMaster.NearByRoundOff = objReader["NearByRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["NearByRoundOff"]) : 0;
                        objCountryMaster.DateFormat = Convert.ToString(objReader["DateFormat"]);
                        objCountryMaster.DateSeparator = Convert.ToString(objReader["DateSeparator"]);
                        objCountryMaster.NegativeSign = Convert.ToString(objReader["NegativeSign"]);
                        objCountryMaster.CurrencySeparator = Convert.ToString(objReader["CurrencySeparator"]);
                        objCountryMaster.CurrencyID = Convert.ToInt32(objReader["CurrencyID"]);
                        objCountryMaster.TaxID = Convert.ToInt32(objReader["TaxID"]);
                        objCountryMaster.EmailID = Convert.ToString(objReader["EmailID"]);
                        objCountryMaster.CreditLimitCheck = objReader["CreditLimitCheck"] != DBNull.Value ? Convert.ToBoolean(objReader["CreditLimitCheck"]) : true;
                        objCountryMaster.AllowMultipleTransaction = objReader["AllowMultipleTransaction"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultipleTransaction"]) : true;
                        objCountryMaster.AllowPartialReceiving = objReader["AllowPartialReceiving"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowPartialReceiving"]) : true;
                        objCountryMaster.AllowSaleAndRedemption = objReader["AllowSaleAndRedemption"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowSaleAndRedemption"]) : true;
                        objCountryMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCountryMaster.OrginCountry = objReader["OrginCountry"] != DBNull.Value ? Convert.ToBoolean(objReader["OrginCountry"]) : true;
                        objCountryMaster.POSTitle = Convert.ToString(objReader["POSTitle"]);
                        objCountryMaster.PromotionRoundOff = objReader["PromotionRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["PromotionRoundOff"]) : 0;
                        objCountryMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCountryMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCountryMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCountryMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; 
                        objCountryMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objCountryMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //objCountryMaster.Currency = Convert.ToString(objReader["CurrencyCode"]);

                        ResponseData.CountryMasterRecord = objCountryMaster;
                        ResponseData.ResponseDynamicData = objCountryMaster;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Country Master");
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
            var CountryMasterList = new List<CountryMaster>();
            var RequestData = (SelectAllCountryRequest)RequestObj;
            var ResponseData = new SelectAllCountryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select CM.*,CUM.CurrencyCode, CUM.CurrencyName from CountryMaster CM with(NoLock) ");
                sSql.Append("Inner Join CurrencyMaster CUM on CM.CurrencyID = CUM.ID ");
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCountryMaster = new CountryMaster();


                        objCountryMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCountryMaster.NearByRoundOff = objReader["NearByRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["NearByRoundOff"]) : 0;
                        objCountryMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objCountryMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objCountryMaster.LanguageName = Convert.ToString(objReader["LanguageName"]);
                        objCountryMaster.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
                        objCountryMaster.TaxCode = Convert.ToString(objReader["TaxCode"]);
                        objCountryMaster.Currency = Convert.ToString(objReader["CurrencyName"]);
                        objCountryMaster.DecimalDigit = objReader["DecimalDigit"] != DBNull.Value ? Convert.ToDecimal(objReader["DecimalDigit"]) : 0;
                        objCountryMaster.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        objCountryMaster.DateFormat = Convert.ToString(objReader["DateFormat"]);
                        objCountryMaster.DateSeparator = Convert.ToString(objReader["DateSeparator"]);
                        objCountryMaster.NegativeSign = Convert.ToString(objReader["NegativeSign"]);
                        objCountryMaster.CurrencySeparator = Convert.ToString(objReader["CurrencySeparator"]);
                        //objCountryMaster.TaxID = Convert.ToInt32(objReader["TaxID"]);
                        objCountryMaster.EmailID = Convert.ToString(objReader["EmailID"]);
                        objCountryMaster.CreditLimitCheck = objReader["CreditLimitCheck"] != DBNull.Value ? Convert.ToBoolean(objReader["CreditLimitCheck"]) : true;
                        objCountryMaster.AllowMultipleTransaction = objReader["AllowMultipleTransaction"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultipleTransaction"]) : true;
                        objCountryMaster.AllowPartialReceiving = objReader["AllowPartialReceiving"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowPartialReceiving"]) : true;
                        objCountryMaster.AllowSaleAndRedemption = objReader["AllowSaleAndRedemption"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowSaleAndRedemption"]) : true;
                        objCountryMaster.OrginCountry = objReader["OrginCountry"] != DBNull.Value ? Convert.ToBoolean(objReader["OrginCountry"]) : true;
                        objCountryMaster.POSTitle = Convert.ToString(objReader["POSTitle"]);
                        objCountryMaster.PromotionRoundOff = objReader["PromotionRoundOff"] != DBNull.Value ? Convert.ToDecimal(objReader["PromotionRoundOff"]) : 0;
                        objCountryMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCountryMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCountryMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCountryMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCountryMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCountryMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objCountryMaster.Currency = Convert.ToString(objReader["CurrencyCode"]);
                        CountryMasterList.Add(objCountryMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CountryMasterList = CountryMasterList;
                    ResponseData.ResponseDynamicData = CountryMasterList;
                    //ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Country  Master");
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Country  Master");
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
        public override SelectCountryLookUpResponse SelectCountryLookUp(SelectCountryLookUpRequest ObjRequest)
        {
            var CountryLookUpList = new List<CountryMaster>();
            var RequestData = (SelectCountryLookUpRequest)ObjRequest;
            var ResponseData = new SelectCountryLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                // This CurrencyID, CurrencyCode required for PriceChange
                sQuery = "Select ID,CountryName,CountryCode,CurrencyID, CurrencyCode,Active from CountryMaster with(NoLock) where Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCountryMaster = new CountryMaster();
                        objCountryMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCountryMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objCountryMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objCountryMaster.CurrencyID = Convert.ToInt32(objReader["CurrencyID"]);
                        objCountryMaster.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
                        objCountryMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        CountryLookUpList.Add(objCountryMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CountryMasterList = CountryLookUpList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Country Master");
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

        public override GetCurrencyCodeForCountryResponse GetCurrencyCodeForCountry(GetCurrencyCodeForCountryRequest ObjRequest)
        {
            var CurrencyCodeForCountry = new CountryMaster();
            var RequestData = (GetCurrencyCodeForCountryRequest)ObjRequest;
            var ResponseData = new GetCurrencyCodeForCountryResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "select b.CurrencySymbol from CountryMaster a Inner Join CurrencyMaster b on b.ID = a.CurrencyID where a.ID = '" + RequestData.CountryID + "'  and a.Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCountryMaster = new CountryMaster();
                        //objCountryMaster.ID =objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) :0;
                        //objCountryMaster.CountryName = Convert.ToString(objReader["CountryName"]);

                        objCountryMaster.Currency = Convert.ToString(objReader["CurrencySymbol"]);
                        //CurrencyCodeForCountry.Add(objCountryMaster);

                        ResponseData.CurrencyCodeForCountry = objCountryMaster;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Country Master");
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

        public override GetCurrencyByStoreResponse GetCurencyByStore(GetCurrencyByStoreRequest ObjRequest)
        {
            var CurrencyByStore = new CountryMaster();
            var RequestData = (GetCurrencyByStoreRequest)ObjRequest;
            var ResponseData = new GetCurrencyByStoreResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select CurrencyCode from CountryMaster CM join StoreMaster SM on SM.CountryID = CM.ID where SM.ID =  '" + RequestData.ID + "' ";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCountryMaster = new CountryMaster();
                        //objCountryMaster.ID =objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) :0;
                        //objCountryMaster.CountryName = Convert.ToString(objReader["CountryName"]);

                        objCountryMaster.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
                        //CurrencyCodeForCountry.Add(objCountryMaster);

                        ResponseData.CurrencyByStore = objCountryMaster;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Country Master");
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

        public override SelectAllCountryResponse API_SelectAll(SelectAllCountryRequest objRequest)
        {
            var CountryMasterList = new List<CountryMaster>();
            var RequestData = (SelectAllCountryRequest)objRequest;
            var ResponseData = new SelectAllCountryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string IsActive = RequestData.IsActive == null ? "1 "
                //    : RequestData.IsActive.ToLower() == "1" || RequestData.IsActive.ToLower() == "true" ? "1" : "0";
                //string SearchString = RequestData.SearchString == null ? "" : RequestData.SearchString

                string sSql = "select ID, CountryCode, CountryName, Active, RC.TOTAL_CNT [RecordCount] " +
                    "from CountryMaster " +
                    "LEFT JOIN (Select count(CM2.ID) As TOTAL_CNT From CountryMaster CM2 "+
                    "where CM2.Active = " + RequestData.IsActive + " " +
                        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                            "or CM2.CountryCode like isnull('%" + RequestData.SearchString + "%','') " +
                            "or CM2.CountryName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                            "where Active = " + RequestData.IsActive + " " +
                            "and (isnull('" + RequestData.SearchString + "','') = '' " +
                            "or CountryCode like isnull('%" + RequestData.SearchString + "%','') " +
                            "or CountryName like isnull('%" + RequestData.SearchString + "%','')) " + 
                    "order by ID asc " +
                    "offset " + RequestData.Offset + " rows " +
                    "fetch first " + RequestData.Limit + " rows only";
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();     
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCountryMaster = new CountryMaster();
                        objCountryMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCountryMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objCountryMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objCountryMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        CountryMasterList.Add(objCountryMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CountryMasterList = CountryMasterList;
                    //ResponseData.ResponseDynamicData = CountryMasterList;
                    //ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Country  Master");
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Country  Master");
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
