using EasyBizAbsDAL.Reports;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports;
using EasyBizRequest.Reports.StockMovementReport;
using EasyBizResponse.Reports.StockMovementReportResponse;
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
    public class StockMovementReportDAL : BaseStockMovementReportDAL 
       {
                SqlConnection _ConnectionObj;
                SqlCommand _CommandObj;
                string _ConnectionString;
                Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.Reports.StockMovementReportResponse.StockMovementReportResponse StockMovementReport(EasyBizRequest.Reports.StockMovementReport.StockMovementReportRequest ObjRequest)
        {
            var StockMovementList = new List<StockMovementReport>();
            var RequestData = (StockMovementReportRequest)ObjRequest;
            var ResponseData = new StockMovementReportResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                var sSql = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sSql.Append("select  CAST(TL.BusinessDate AS DATE) as BusinessDate,CM.CountryName,SM.StoreName,TL.SKUCode,SKU.SKUName,DT.DocumentCode,DT.DocumentName,TL.StyleCode,(TL.InQty - TL.OutQty) as StockQuantity from TransactionLog TL with(NoLock) ");
                sSql.Append("join CountryMaster CM on CM.ID=TL.CountryID ");
                sSql.Append("join StoreMaster SM on SM.ID= TL.StoreID ");
                sSql.Append("join SKUMaster SKU on SKU.SKUCode=TL.SKUCode ");
                sSql.Append("join DocumentType DT on DT.ID=TL.DocumentID ");
                sSql.Append("where TL.StyleCode='" + RequestData.StyleCode + "'and CAST( TL.BusinessDate AS DATE) between '" + RequestData.FromDate + "' and  '" + RequestData.ToDate + "'");
                
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockMovementReport = new StockMovementReport();

                        objStockMovementReport.DocumentDate = Convert.ToDateTime(objReader["DocumentDate"]);
                        objStockMovementReport.CountryName = Convert.ToString(objReader["CountryName"]);
                        objStockMovementReport.StoreName = Convert.ToString(objReader["StoreName"]);
                        objStockMovementReport.PosNo = Convert.ToString(objReader["PosNo"]);
                        objStockMovementReport.Stylecode = Convert.ToString(objReader["Stylecode"]);
                        objStockMovementReport.ScaleID = Convert.ToInt32(objReader["ScaleID"]);
                        objStockMovementReport.ColorID = Convert.ToInt32(objReader["ColorID"]);
                        objStockMovementReport.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objStockMovementReport.SKUName = Convert.ToString(objReader["SKUName"]);
                        objStockMovementReport.InvoiceNo = Convert.ToString(objReader["InvoiceNo"]);
                        objStockMovementReport.InQty = Convert.ToInt32(objReader["InQty"]);
                        objStockMovementReport.OutQty = Convert.ToInt32(objReader["OutQty"]);
                        objStockMovementReport.DocumentType = Convert.ToString(objReader["DocumentType"]);
                        objStockMovementReport.DocumentNo = Convert.ToInt32(objReader["DocumentNo"]);


                        StockMovementList.Add(objStockMovementReport);

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockMovementReportList = StockMovementList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Movement Report");
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
