using EasyBizAbsDAL.Reports;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports;
using EasyBizRequest.Reports.CurrentStockReport;
using EasyBizResponse.Reports.CurrentStockReportResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Reports
{
    public class CurrentStockReportDAL : BaseCurrentStockReportDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.Reports.CurrentStockReportResponse.CurrentStockReportResponse CurrentStockReport(EasyBizRequest.Reports.CurrentStockReport.CurrentStockReportRequest ObjRequest)
        {
            var CurrentStockReportList = new List<CurrentStockReport>();
            var RequestData = (CurrentStockReportRequest)ObjRequest;
            var ResponseData = new CurrentStockReportResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                var sSql = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sSql.Append("select CM.CountryName,SM.StoreName,TL.SKUCode,TL.StyleCode,SKU.SKUName,(TL.InQty - TL.OutQty) as StockQuantity from TransactionLog TL with(NoLock) ");
                sSql.Append("left join CountryMaster CM on TL.CountryID = CM.ID ");
                sSql.Append("left join StoreMaster SM on  TL.StoreID =SM.ID ");
                sSql.Append("left join SKUMaster SKU on TL.SKUCode = SKU.SKUCode ");
                sSql.Append("where TL.StyleCode= '" + RequestData.StyleCode + "'");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                    
                        var objCurrentStockReport = new CurrentStockReport();
                        objCurrentStockReport.CountryName = Convert.ToString(objReader["CountryName"]);
                        objCurrentStockReport.StoreName = Convert.ToString(objReader["StoreName"]);
                        objCurrentStockReport.BrandName = Convert.ToString(objReader["BrandName"]);
                        objCurrentStockReport.Stylecode = Convert.ToString(objReader["Stylecode"]);
                        objCurrentStockReport.ScaleID = Convert.ToInt32(objReader["ScaleID"]);
                        objCurrentStockReport.ColorID = Convert.ToInt32(objReader["ColorID"]);
                        objCurrentStockReport.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objCurrentStockReport.SKUName = Convert.ToString(objReader["SKUName"]);
                        objCurrentStockReport.Qty = Convert.ToInt32(objReader["Qty"]);
                        CurrentStockReportList.Add(objCurrentStockReport);

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CurrentStockReportList = CurrentStockReportList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Current Stock Report ");
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

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
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
