using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ExchangeRatesRequest;
using EasyBizResponse.Masters.ExchangeRatesResponse;
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
    public class ExchangeRatesDAL : BaseExchangeRatesDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveExchangeRatesRequest)RequestObj;
            var ResponseData = new SaveExchangeRatesResponse();
            var ExchangeRateslist = RequestData.ExchangeRateslist;
            var sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertExchangeRates", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.IDs;

                SqlParameter ExchangeRateDate = _CommandObj.Parameters.Add("@ExchangeRateDate", SqlDbType.DateTime);
                ExchangeRateDate.Direction = ParameterDirection.Input;
                ExchangeRateDate.Value = sqlCommon.GetSQLServerDateTimeString(RequestData.ExchangeRatesDate);

                foreach (ExchangeRates objExchangeRates in ExchangeRateslist)
                {
                    sSql.Append("<ExchangeRatesData>");
                    sSql.Append("<ID>" + (objExchangeRates.ID) + "</ID>");
                    sSql.Append("<ExchangeRatesCode>" + (objExchangeRates.ExchangeRatesCode) + "</ExchangeRatesCode>");
                    sSql.Append("<ExchangeRatesName>" + objExchangeRates.ExchangeRatesName + "</ExchangeRatesName>");
                    sSql.Append("<BaseCurrency>" + (objExchangeRates.BaseCurrency) + "</BaseCurrency>");
                    sSql.Append("<BaseCurrencyID>" + (objExchangeRates.BaseCurrencyID) + "</BaseCurrencyID>");
                    sSql.Append("<ExchangeAmount>" + objExchangeRates.ExchangeAmount + "</ExchangeAmount>");
                    sSql.Append("<ExchangeRateDate>" + sqlCommon.GetSQLServerDateTimeString(objExchangeRates.ExchangeRateDate) + "</ExchangeRateDate>");
                    sSql.Append("<TargetCurrency>" + (objExchangeRates.TargetCurrency) + "</TargetCurrency>");
                    sSql.Append("<CreateBy>" + (objExchangeRates.CreateBy) + "</CreateBy>");
                    sSql.Append("<Active>" + (objExchangeRates.Active) + "</Active>");
                    sSql.Append("<TargetCurrencyID>" + (objExchangeRates.TargetCurrencyID) + "</TargetCurrencyID>");
                    sSql.Append("</ExchangeRatesData>");
                }
                var SubBrandData = _CommandObj.Parameters.Add("@ExchangeRatesData", SqlDbType.Xml);
                SubBrandData.Direction = ParameterDirection.Input;
                SubBrandData.Value = sSql.ToString();

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ExchangeRatesIDs", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "ExchangeRates");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "ExchangeRates");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ExchangeRates");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ExchangeRates");
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
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ExchangeRatesList = new List<ExchangeRates>();
            var ExchangeRatesRecord = new ExchangeRates();
            var RequestData = (SelectByIDExchangeRatesRequest)RequestObj;
            var ResponseData = new SelectByIDExchangeRatesResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from ExchangeRates with(NoLock) where ExchangeCode='{0}'";
                sSql = string.Format(sSql, RequestData.ExchangeRatesCode);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objExchangeRates = new ExchangeRates();
                        objExchangeRates.ID = Convert.ToInt32(objReader["ID"]);
                        objExchangeRates.ExchangeRatesCode = Convert.ToString(objReader["ExchangeCode"]);
                        objExchangeRates.ExchangeRatesName = Convert.ToString(objReader["ExchangeName"]);

                        objExchangeRates.BaseCurrency = Convert.ToString(objReader["BaseCurrency"]);
                        objExchangeRates.BaseCurrencyID = Convert.ToInt32(objReader["BaseCurrencyID"]);
                        objExchangeRates.ExchangeAmount = Convert.ToDecimal(objReader["ExchangeAmount"]);
                        objExchangeRates.ExchangeRateDate = Convert.ToDateTime(objReader["ExchangeRateDate"]);
                        objExchangeRates.TargetCurrency = Convert.ToString(objReader["TargetCurrency"]);
                        objExchangeRates.TargetCurrencyID = Convert.ToInt32(objReader["TargetCurrencyID"]);

                        objExchangeRates.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objExchangeRates.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objExchangeRates.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objExchangeRates.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objExchangeRates.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objExchangeRates.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ExchangeRatesList.Add(objExchangeRates);

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ExchangeRatesList = ExchangeRatesList;
                    ResponseData.ResponseDynamicData = ExchangeRatesList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ExchangeRates Master");
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
            var ExchangeRatesList = new List<ExchangeRates>();
            var RequestData = (SelectAllExchangeRatesRequest)RequestObj;
            var ResponseData = new SelectAllExchangeRatesResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select Distinct  ExchangeCode,ExchangeName,BaseCurrency,convert(varchar(7),ExchangeRateDate, 120) as ExchangeRateDate,Active from ExchangeRates with(NoLock)";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objExchangeRates = new ExchangeRates();

                        objExchangeRates.ExchangeRatesCode = Convert.ToString(objReader["ExchangeCode"]);
                        objExchangeRates.ExchangeRatesName = Convert.ToString(objReader["ExchangeName"]);

                        objExchangeRates.BaseCurrency = Convert.ToString(objReader["BaseCurrency"]);
                        //objExchangeRates.BaseCurrencyID = Convert.ToInt32(objReader["BaseCurrencyID"]);
                        //objExchangeRates.ExchangeAmount = Convert.ToDecimal(objReader["ExchangeAmount"]);
                        objExchangeRates.ExchangeRateDate = Convert.ToDateTime(objReader["ExchangeRateDate"]);
                        //objExchangeRates.TargetCurrency = Convert.ToString(objReader["TargetCurrency"]);
                        //objExchangeRates.TargetCurrencyID = Convert.ToInt32(objReader["TargetCurrencyID"]);

                        //objExchangeRates.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objExchangeRates.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objExchangeRates.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objExchangeRates.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objExchangeRates.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objExchangeRates.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ExchangeRatesList.Add(objExchangeRates);
                    }
                    ResponseData.ExchangeRatesList = ExchangeRatesList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = ExchangeRatesList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ExchangeRates Master");
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

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectCurrecnyExchangeRatesResponse SelectCurrecnyExchangeRates(SelectCurrecnyExchangeRatesRequest ObjRequest)
        {
            var CurrencyExchangeRatesList = new List<ExchangeRates>();
            var RequestData = (SelectCurrecnyExchangeRatesRequest)ObjRequest;
            var ResponseData = new SelectCurrecnyExchangeRatesResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from ExchangeRates with(NoLock) where ExchangeRateDate = '" + sqlCommon.GetSQLServerDateString(RequestData.Exchangedate) + "' ";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objExchangeRates = new ExchangeRates();

                        objExchangeRates.ExchangeRatesCode = Convert.ToString(objReader["ExchangeCode"]);
                        objExchangeRates.ExchangeRatesName = Convert.ToString(objReader["ExchangeName"]);

                        objExchangeRates.BaseCurrency = Convert.ToString(objReader["BaseCurrency"]);
                        objExchangeRates.BaseCurrencyID = Convert.ToInt32(objReader["BaseCurrencyID"]);
                        objExchangeRates.ExchangeAmount = Convert.ToDecimal(objReader["ExchangeAmount"]);
                        objExchangeRates.ExchangeRateDate = Convert.ToDateTime(objReader["ExchangeRateDate"]);
                        objExchangeRates.TargetCurrency = Convert.ToString(objReader["TargetCurrency"]);
                        objExchangeRates.TargetCurrencyID = Convert.ToInt32(objReader["TargetCurrencyID"]);

                        objExchangeRates.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objExchangeRates.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objExchangeRates.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objExchangeRates.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objExchangeRates.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objExchangeRates.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        CurrencyExchangeRatesList.Add(objExchangeRates);
                    }
                    ResponseData.CurrencyExchangeRatesList = CurrencyExchangeRatesList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = CurrencyExchangeRatesList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CurrencyExchangeRates");
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

        public override SelectAllExchangeRatesResponse API_SelectALL(SelectAllExchangeRatesRequest requestData)
        {
            var ExchangeRatesList = new List<ExchangeRates>();
            var RequestData = (SelectAllExchangeRatesRequest)requestData;
            var ResponseData = new SelectAllExchangeRatesResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select Distinct  ExchangeCode,ExchangeName,BaseCurrency,convert(varchar(7),ExchangeRateDate, 120) as ExchangeRateDate,Active from ExchangeRates with(NoLock)";

                string sSql = "Select Distinct ExchangeCode, ExchangeName, BaseCurrency,ExchangeRateDate, ID, Active, RC.TOTAL_CNT [RecordCount] " +
                   "from ExchangeRates with(NoLock) " +
                   "LEFT JOIN(Select  count(EM.ID) As TOTAL_CNT From ExchangeRates EM with(NoLock) " +
                   "where EM.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or EM.ExchangeCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM.ExchangeName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM.BaseCurrency like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM.ExchangeRateDate like isnull('%" + RequestData.SearchString + "%','')) )  As RC ON 1 = 1 " +

                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or ExchangeCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or ExchangeName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or BaseCurrency like isnull('%" + RequestData.SearchString + "%','') " +
                       "or ExchangeRateDate like isnull('%" + RequestData.SearchString + "%','')) " +
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
                        var objExchangeRates = new ExchangeRates();

                        objExchangeRates.ExchangeRatesCode = Convert.ToString(objReader["ExchangeCode"]);
                        objExchangeRates.ExchangeRatesName = Convert.ToString(objReader["ExchangeName"]);

                        objExchangeRates.BaseCurrency = Convert.ToString(objReader["BaseCurrency"]);
                        //objExchangeRates.BaseCurrencyID = Convert.ToInt32(objReader["BaseCurrencyID"]);
                        //objExchangeRates.ExchangeAmount = Convert.ToDecimal(objReader["ExchangeAmount"]);
                        objExchangeRates.ExchangeRateDate = Convert.ToDateTime(objReader["ExchangeRateDate"]);
                        //objExchangeRates.TargetCurrency = Convert.ToString(objReader["TargetCurrency"]);
                        //objExchangeRates.TargetCurrencyID = Convert.ToInt32(objReader["TargetCurrencyID"]);

                        //objExchangeRates.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objExchangeRates.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objExchangeRates.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objExchangeRates.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objExchangeRates.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objExchangeRates.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ExchangeRatesList.Add(objExchangeRates);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    //ResponseData.ExchangeRatesList = ExchangeRatesList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = ExchangeRatesList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ExchangeRates Master");
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

        public override SelectCurrecnyExchangeRatesResponse API_SelectCurrencyALL(SelectCurrecnyExchangeRatesRequest requestData)
        {
            var ExchangeRatesList = new List<ExchangeRates>();
            var RequestData = (SelectCurrecnyExchangeRatesRequest)requestData;
            var ResponseData = new SelectCurrecnyExchangeRatesResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select Distinct  ExchangeCode,ExchangeName,BaseCurrency,convert(varchar(7),ExchangeRateDate, 120) as ExchangeRateDate,Active from ExchangeRates with(NoLock)";

                //string sSql = "Select * from ExchangeRates with(NoLock) where ExchangeRateDate = '" + sqlCommon.GetSQLServerDateString(requestData.Exchangedate) + "'";

                string sSql = ";with ex                                                                                         " +
                              " as                                                                                              " +
                              " (                                                                                               " +
                              "     Select[ID], ExchangeCode, ExchangeName, BaseCurrencyID, BaseCurrency                        " +
                              "         , TargetCurrencyID, TargetCurrency, ExchangeHeaderDate                                  " +
                              "         , ExchangeRateDate, ExchangeAmount, Active, CreateBy, CreateOn, UpdateBy, UpdateOn, SCN " +
                              "         , IsStoreSync, IsCountrySync, Remarks                                                   " +
                              "     from ExchangeRates with(NoLock)                                                             " +
                              "     where ExchangeRateDate = '" + sqlCommon.GetSQLServerDateString(requestData.Exchangedate) + "'                                                       " +
                              " )                                                                                               " +
                              " select top 1 0[ID], ExchangeCode, ExchangeName, BaseCurrencyID, BaseCurrency                    " +
                              "     , BaseCurrencyID[TargetCurrencyID], BaseCurrency[TargetCurrency], ExchangeHeaderDate        " +
                              "     , ExchangeRateDate, 1[ExchangeAmount], Active, CreateBy, CreateOn, UpdateBy, UpdateOn, SCN  " +
                              "     , IsStoreSync, IsCountrySync, Remarks                                                       " +
                              " from ex                                                                                         " +
                              " union all                                                                                       " +
                              " select[ID], ExchangeCode, ExchangeName, BaseCurrencyID, BaseCurrency                            " +
                              "     , TargetCurrencyID, TargetCurrency, ExchangeHeaderDate                                      " +
                              "     , ExchangeRateDate, ExchangeAmount, Active, CreateBy, CreateOn, UpdateBy, UpdateOn, SCN     " +
                              "     , IsStoreSync, IsCountrySync, Remarks                                                       " +
                              " from ex ";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objExchangeRates = new ExchangeRates();

                        objExchangeRates.ExchangeRatesCode = Convert.ToString(objReader["ExchangeCode"]);
                        objExchangeRates.ExchangeRatesName = Convert.ToString(objReader["ExchangeName"]);

                        objExchangeRates.BaseCurrency = Convert.ToString(objReader["BaseCurrency"]);
                        objExchangeRates.BaseCurrencyID = Convert.ToInt32(objReader["BaseCurrencyID"]);
                        objExchangeRates.ExchangeAmount = Convert.ToDecimal(objReader["ExchangeAmount"]);
                        objExchangeRates.ExchangeRateDate = Convert.ToDateTime(objReader["ExchangeRateDate"]);
                        objExchangeRates.TargetCurrency = Convert.ToString(objReader["targetcurrency"]);
                        objExchangeRates.TargetCurrencyID = Convert.ToInt32(objReader["TargetCurrencyID"]);

                        //objExchangeRates.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objExchangeRates.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objExchangeRates.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objExchangeRates.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objExchangeRates.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objExchangeRates.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ExchangeRatesList.Add(objExchangeRates);
                        //ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    //ResponseData.ExchangeRatesList = ExchangeRatesList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = ExchangeRatesList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ExchangeRates Master");
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
