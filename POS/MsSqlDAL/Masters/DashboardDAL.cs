using EasyBizAbsDAL.DashBoard;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Dashboard;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest;
using EasyBizRequest.DashBoardRequest;
using EasyBizRequest.Masters.DashboardRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.DashboardReponse;
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
    public class DashboardDAL : BaseRegisterDashBoardDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override SelectDashboardResponse API_SelectBetweenDayDetails(SelectDashboardRequest requestData)
        {
            var RequestData = (SelectDashboardRequest)requestData;
            var ResponseData = new SelectDashboardResponse();
            bool Nodata = true;
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                if (requestData.report_type.ToLower() == "all" || requestData.report_type.ToLower() == "card")
                {
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                    _CommandObj = new SqlCommand("DashboardSales", _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.Parameters.AddWithValue("@FromDate", RequestData.FromDate);
                    _CommandObj.Parameters.AddWithValue("@ToDate", RequestData.ToDate);
                    _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.country_id);

                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        Nodata = false;
                        while (objReader.Read())
                        {

                            ResponseData.SalesNetAmount = objReader["SalesAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SalesAmount"]) : 0;
                            ResponseData.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;

                        }

                    }
                }

                if (requestData.report_type.ToLower() == "all" || requestData.report_type.ToLower() == "area")
                {
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                    _CommandObj = new SqlCommand("DashboardGraph", _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.Parameters.AddWithValue("@FromDate", RequestData.FromDate);
                    _CommandObj.Parameters.AddWithValue("@ToDate", RequestData.ToDate);

                    List<Dashboard_AreaChart_AllData> AllData = new List<Dashboard_AreaChart_AllData>();
                    List<Dashboard_AreaChart> result = new List<Dashboard_AreaChart>();
                    objReader = _CommandObj.ExecuteReader();

                    if (objReader.HasRows)
                    {
                        Nodata = false;
                        while (objReader.Read())
                        {
                            var objDashboard = new Dashboard_AreaChart_AllData();
                            objDashboard.CountryCode = Convert.ToString(objReader["CountryCode"]);
                            objDashboard.CountryName = Convert.ToString(objReader["CountryName"]);
                            objDashboard.MonthName = Convert.ToString(objReader["MonthName"]);
                            objDashboard.MonthNo = objReader["MonthNo"] != DBNull.Value ? Convert.ToInt32(objReader["MonthNo"]) : 0;
                            objDashboard.Sales = objReader["Sales"] != DBNull.Value ? Convert.ToDecimal(objReader["Sales"]) : 0;

                            AllData.Add(objDashboard);
                        }

                        if (AllData != null && AllData.Count > 0)
                        {
                            foreach (var data in AllData)
                            {
                                var temp = result.Where(x => x.Name == data.CountryCode).FirstOrDefault();
                                if (temp != null)
                                {
                                    var child = new Dashboard_AreaChart_Child()
                                    {
                                        Name = data.MonthName,
                                        Value = data.Sales
                                    };
                                    temp.Series.Add(child);
                                }
                                else
                                {
                                    var child = new Dashboard_AreaChart_Child()
                                    {
                                        Name = data.MonthName,
                                        Value = data.Sales
                                    };
                                    var parent = new Dashboard_AreaChart()
                                    {
                                        Name = data.CountryCode,
                                        Series = new List<Dashboard_AreaChart_Child>()
                                    };
                                    parent.Series.Add(child);
                                    result.Add(parent);
                                }
                            }
                        }

                        ResponseData.AreaChart = result;
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                }

                if (requestData.report_type.ToLower() == "all" || requestData.report_type.ToLower() == "purchase")
                {
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                    _CommandObj = new SqlCommand("DashboardPurchaseGraph", _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.Parameters.AddWithValue("@FromDate", RequestData.FromDate);
                    _CommandObj.Parameters.AddWithValue("@ToDate", RequestData.ToDate);

                    List<Dashboard_Purchase> Purchase = new List<Dashboard_Purchase>();

                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        Nodata = false;
                        while (objReader.Read())
                        {
                            var objDashboard = new Dashboard_Purchase();
                            objDashboard.Name = Convert.ToString(objReader["CountryCode"]);
                            objDashboard.Value = objReader["TotalReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalReceivedQuantity"]) : 0;
                            Purchase.Add(objDashboard);
                        }

                        ResponseData.PurchaseChart = Purchase;
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                }
                if (requestData.report_type.ToLower() == "all" || requestData.report_type.ToLower() == "piechart")
                {
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                    _CommandObj = new SqlCommand("DashboardPiechartGraph", _ConnectionObj);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.Parameters.AddWithValue("@FromDate", RequestData.FromDate);
                    _CommandObj.Parameters.AddWithValue("@ToDate", RequestData.ToDate);
                    _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.country_id);

                    List<Dashboard_Product> PieChart = new List<Dashboard_Product>();

                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        Nodata = false;
                        while (objReader.Read())
                        {
                            var objDashboard = new Dashboard_Product();
                            objDashboard.Name = Convert.ToString(objReader["SKUCode"]);
                            objDashboard.Value = objReader["SalesCount"] != DBNull.Value ? Convert.ToInt32(objReader["SalesCount"]) : 0;
                            PieChart.Add(objDashboard);
                        }

                        ResponseData.PieChart = PieChart;
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                }


                if (Nodata)
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = "No Data Found";
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
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
        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
