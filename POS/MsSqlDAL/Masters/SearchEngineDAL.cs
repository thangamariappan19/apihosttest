using System;
using System.Collections.Generic;
using System.Text;
using MsSqlDAL.Common;
using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using ResourceStrings;
using System.Data;
using System.Data.SqlClient;
using EasyBizRequest.Masters.SearchEngineRequest;
using EasyBizResponse.Masters.SearchEngineResponse;
using EasyBizRequest;
using EasyBizResponse;
using System.Linq;
//using MsSqlDAL.Common;


namespace MsSqlDAL.Masters
{
    public class SearchEngineDAL : BaseSearchEngineDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        public bool checknontrade = false;

        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override CustomersSkuResponse GetCustomerSKUSearchPOS(CustomerSkuRequest requestData)
        {
            var SearchEngineList = new List<SearchEngine>();
            var RequestData = new CustomerSkuRequest();
            var ResponseData = new CustomersSkuResponse();

            RequestData = (CustomerSkuRequest)requestData;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                if (RequestData.SearchString != null)
                {
                    _ConnectionString = RequestData.ConnectionString;
                    _RequestFrom = RequestData.RequestFrom;

                    var sSql = new StringBuilder();

                    string sQuery = string.Empty;
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                    _CommandObj = new SqlCommand("API_SKUandCustomerSearch", _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);

                    _CommandObj.CommandType = CommandType.StoredProcedure;

                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objSearchEngine = new SearchEngine();
                            objSearchEngine.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objSearchEngine.Code = objReader["Code"] != DBNull.Value ? Convert.ToString(objReader["Code"]) : string.Empty;
                            objSearchEngine.Name = objReader["Name"] != DBNull.Value ? Convert.ToString(objReader["Name"]) : string.Empty;
                            objSearchEngine.Type = objReader["Type"] != DBNull.Value ? Convert.ToString(objReader["Type"]) : string.Empty;
                            objSearchEngine.Number = objReader["Number"] != DBNull.Value ? Convert.ToString(objReader["Number"]) : string.Empty;



                            SearchEngineList.Add(objSearchEngine);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.SearchEngineDataList = SearchEngineList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Search Engine");
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.InvalidInput;
                    ResponseData.SearchEngineDataList = null;
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

        public override SearchCustomerResponse GetCustomerSearchPOS(SearchCustomerRequest requestData)
        {
            var SearchEngineList = new List<SearchEngine>();
            var RequestData = new SearchCustomerRequest();
            var ResponseData = new SearchCustomerResponse();

            RequestData = (SearchCustomerRequest)requestData;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_CustomerSearch", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);

                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSearchEngine = new SearchEngine();
                        objSearchEngine.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSearchEngine.Code = objReader["Code"] != DBNull.Value ? Convert.ToString(objReader["Code"]) : string.Empty;
                        objSearchEngine.Name = objReader["Name"] != DBNull.Value ? Convert.ToString(objReader["Name"]) : string.Empty;
                        objSearchEngine.Type = objReader["Type"] != DBNull.Value ? Convert.ToString(objReader["Type"]) : string.Empty;
                      
                        objSearchEngine.Number = objReader["Number"] != DBNull.Value ? Convert.ToString(objReader["Number"]) : string.Empty;
                    


                        SearchEngineList.Add(objSearchEngine);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SearchEngineDataList = SearchEngineList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Search Engine");
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

        public override SearchBrandResponse GetBrandSearch(SearchBrandRequest requestData)
        {
            var SearchEngineList = new List<SearchEngine>();
            var RequestData = new SearchBrandRequest();
            var ResponseData = new SearchBrandResponse();

            RequestData = (SearchBrandRequest)requestData;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_BrandSearch", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);

                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSearchEngine = new SearchEngine();
                        objSearchEngine.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSearchEngine.Code = objReader["Code"] != DBNull.Value ? Convert.ToString(objReader["Code"]) : string.Empty;
                        objSearchEngine.Name = objReader["Name"] != DBNull.Value ? Convert.ToString(objReader["Name"]) : string.Empty;
                       



                        SearchEngineList.Add(objSearchEngine);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SearchEngineDataList = SearchEngineList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Search Engine");
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

        public override SearchSalesReturnExchangeResponse GetSalereturnsearch(SearchSalesReturnExchangeRequest requestData)
        {
            var SearchEngineList = new List<SearchEngine>();
            var RequestData = new SearchSalesReturnExchangeRequest();
            var ResponseData = new SearchSalesReturnExchangeResponse();

            RequestData = (SearchSalesReturnExchangeRequest)requestData;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_SalesExchangeandInvoiceSearch", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@storeid", RequestData.StoreID);

                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSearchEngine = new SearchEngine();
                        // objSearchEngine.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSearchEngine.Code = objReader["Code"] != DBNull.Value ? Convert.ToString(objReader["Code"]) : string.Empty;
                        objSearchEngine.Name = objReader["Name"] != DBNull.Value ? Convert.ToString(objReader["Name"]) : string.Empty;
                        objSearchEngine.Type = objReader["Type"] != DBNull.Value ? Convert.ToString(objReader["Type"]) : string.Empty;
                        objSearchEngine.Date = objReader["Date"] != DBNull.Value ? Convert.ToString(objReader["Date"]) : string.Empty;



                        SearchEngineList.Add(objSearchEngine);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SearchEngineDataList = SearchEngineList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Search Engine");
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

        public override SearchExchangeResponse GetExchangeSearch(SearchExchangeRequest requestData)
        {
            var SearchEngineList = new List<SearchEngine>();
            var RequestData = new SearchExchangeRequest();
            var ResponseData = new SearchExchangeResponse();

            RequestData = (SearchExchangeRequest)requestData;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_SalesExchangeandInvoiceSearch", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@storeid", RequestData.StoreID);
                _CommandObj.Parameters.AddWithValue("@Exchange", RequestData.IsActive);

                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSearchEngine = new SearchEngine();
                        // objSearchEngine.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSearchEngine.Code = objReader["Code"] != DBNull.Value ? Convert.ToString(objReader["Code"]) : string.Empty;
                        objSearchEngine.Name = objReader["Name"] != DBNull.Value ? Convert.ToString(objReader["Name"]) : string.Empty;
                        objSearchEngine.Type = objReader["Type"] != DBNull.Value ? Convert.ToString(objReader["Type"]) : string.Empty;
                        objSearchEngine.Date = objReader["Date"] != DBNull.Value ? Convert.ToString(objReader["Date"]) : string.Empty;



                        SearchEngineList.Add(objSearchEngine);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SearchEngineDataList = SearchEngineList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Search Engine");
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
