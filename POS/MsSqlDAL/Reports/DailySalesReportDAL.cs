using EasyBizAbsDAL.Masters;
using EasyBizAbsDAL.Reports;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest.Reports.DailySalesReport;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Reports.DailySalesReportResponse;
using EasyBizResponse.Transactions.POS.Invoice;
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
    public class DailySalesReportDAL : BaseDailySalesReportDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
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
        public override DailySalesReportResponse DailySalesReport(DailySalesReportRequest ObjRequest)
        {
            var SalesReportList = new List<DailySalesReport>();
            var RequestData = (DailySalesReportRequest)ObjRequest;
            var ResponseData = new DailySalesReportResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                var sSql = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                if (RequestData.FromDate != null && RequestData.ToDate != null && RequestData.CountryID == 0 && RequestData.StoreGroupID == 0 && RequestData.StoreID == 0 && RequestData.PosID == 0 && RequestData.StyleID == 0)
                {
                    //sSql.Append("select CAST( IHR.DocumentDate AS DATE) as BusinessDate,IHR.DocumentDate,CMR.CountryName,SMR.StoreName,SMR.StoreGroupID,POS.ID as PosID,IHR.PosNo,SYR.StyleCode,SYR.ID as StyleId,SYR.StyleName,SKU.ScaleID,SKU.ColorID,SKU.SKUCode,SKU.SKUName,IHR.InvoiceNo,IDL.Qty,IHR.DiscountAmount,IDL.Price,IDL.LineTotal,IHR.TotalPrice from InvoiceDetail IDL with(NoLock)");
                    sSql.Append("select CAST( IHR.DocumentDate AS DATE) as BusinessDate,IHR.DocumentDate,CMR.CountryName,SMR.StoreName,SGM.StoreGroupName,POS.PosName as PosID,SYR.StyleCode,SKU.ScaleID,SKU.ColorID,SKU.SKUCode,SKU.SKUName,IHR.InvoiceNo,IDL.Qty,IHR.DiscountAmount,convert(Decimal(18,3),IDL.Price) as Price,convert(Decimal(18,3),IDL.LineTotal) as LineTotal,convert(Decimal(18,3),IHR.TotalPrice) as TotalPrice from InvoiceDetail IDL with(NoLock)");
                    sSql.Append("left join InvoiceHeader IHR on IDL.InvoiceHeaderID = IHR.ID ");
                    sSql.Append("left join CountryMaster CMR on IDL.CountryID = CMR.ID ");
                    sSql.Append("left join StoreMaster SMR on IDL.StoreID = SMR.ID ");
                    sSql.Append("left join StoreGroupMaster SGM on  SMR.StoreGroupID=SGM.ID ");
                    sSql.Append("left join PosMaster POS on POS.PosName=IHR.PosNo ");
                    sSql.Append("left join SKUMaster SKU on IDL.SKUID = SKU.ID ");
                    sSql.Append("left join StyleMaster SYR on SKU.StyleCode = SYR.StyleCode ");
                    sSql.Append("where CAST( IHR.DocumentDate AS DATE) between '" + RequestData.FromDate + "' and  '" + RequestData.ToDate + "'");
                }
                else if (RequestData.FromDate != null && RequestData.ToDate != null && RequestData.CountryID != 0 && RequestData.StoreGroupID == 0 && RequestData.StoreID == 0 && RequestData.PosID == 0 && RequestData.StyleID == 0)
                {
                    sSql.Append("select CAST( IHR.DocumentDate AS DATE) as BusinessDate,IHR.DocumentDate,CMR.CountryName,SMR.StoreName,SGM.StoreGroupName,POS.PosName as PosID,SYR.StyleCode,SKU.ScaleID,SKU.ColorID,SKU.SKUCode,SKU.SKUName,IHR.InvoiceNo,IDL.Qty,IHR.DiscountAmount,convert(Decimal(18,3),IDL.Price) as Price,convert(Decimal(18,3),IDL.LineTotal) as LineTotal,convert(Decimal(18,3),IHR.TotalPrice) as TotalPrice from InvoiceDetail IDL with(NoLock)");
                    sSql.Append("left join InvoiceHeader IHR on IDL.InvoiceHeaderID = IHR.ID ");
                    sSql.Append("left join CountryMaster CMR on IDL.CountryID = CMR.ID ");
                    sSql.Append("left join StoreMaster SMR on IDL.StoreID = SMR.ID ");
                    sSql.Append("left join StoreGroupMaster SGM on  SMR.StoreGroupID=SGM.ID ");
                    sSql.Append("left join PosMaster POS on POS.PosName=IHR.PosNo ");
                    sSql.Append("left join SKUMaster SKU on IDL.SKUID = SKU.ID ");
                    sSql.Append("left join StyleMaster SYR on SKU.StyleCode = SYR.StyleCode ");
                    sSql.Append("where IHR.CountryID=" + RequestData.CountryID + " and CAST( IHR.DocumentDate AS DATE) between '" + RequestData.FromDate + "' and  '" + RequestData.ToDate + "'");
                }
                else if (RequestData.FromDate != null && RequestData.ToDate != null && RequestData.CountryID != 0 && RequestData.StoreGroupID != 0 && RequestData.StoreID == 0 && RequestData.PosID == 0 && RequestData.StyleID == 0)
                {
                    sSql.Append("select CAST( IHR.DocumentDate AS DATE) as BusinessDate,IHR.DocumentDate,CMR.CountryName,SMR.StoreName,SGM.StoreGroupName,POS.PosName as PosID,SYR.StyleCode,SKU.ScaleID,SKU.ColorID,SKU.SKUCode,SKU.SKUName,IHR.InvoiceNo,IDL.Qty,IHR.DiscountAmount,convert(Decimal(18,3),IDL.Price) as Price,convert(Decimal(18,3),IDL.LineTotal) as LineTotal,convert(Decimal(18,3),IHR.TotalPrice) as TotalPrice from InvoiceDetail IDL with(NoLock)");
                    sSql.Append("left join InvoiceHeader IHR on IDL.InvoiceHeaderID = IHR.ID ");
                    sSql.Append("left join CountryMaster CMR on IDL.CountryID = CMR.ID ");
                    sSql.Append("left join StoreMaster SMR on IDL.StoreID = SMR.ID ");
                    sSql.Append("left join StoreGroupMaster SGM on  SMR.StoreGroupID=SGM.ID ");
                    sSql.Append("left join PosMaster POS on POS.PosName=IHR.PosNo ");
                    sSql.Append("left join SKUMaster SKU on IDL.SKUID = SKU.ID ");
                    sSql.Append("left join StyleMaster SYR on SKU.StyleCode = SYR.StyleCode ");
                    sSql.Append("where IHR.CountryID=" + RequestData.CountryID + "  and SMR.StoreGroupID=" + RequestData.StoreGroupID + " and CAST( IHR.DocumentDate AS DATE) between '" + RequestData.FromDate + "' and  '" + RequestData.ToDate + "'");
                }
                else if (RequestData.FromDate != null && RequestData.ToDate != null && RequestData.CountryID != 0 && RequestData.StoreGroupID != 0 && RequestData.StoreID != 0 && RequestData.PosID == 0 && RequestData.StyleID == 0)
                {
                    sSql.Append("select CAST( IHR.DocumentDate AS DATE) as BusinessDate,IHR.DocumentDate,CMR.CountryName,SMR.StoreName,SGM.StoreGroupName,POS.PosName as PosID,SYR.StyleCode,SKU.ScaleID,SKU.ColorID,SKU.SKUCode,SKU.SKUName,IHR.InvoiceNo,IDL.Qty,IHR.DiscountAmount,convert(Decimal(18,3),IDL.Price) as Price,convert(Decimal(18,3),IDL.LineTotal) as LineTotal,convert(Decimal(18,3),IHR.TotalPrice) as TotalPrice from InvoiceDetail IDL with(NoLock)");
                    sSql.Append("left join InvoiceHeader IHR on IDL.InvoiceHeaderID = IHR.ID ");
                    sSql.Append("left join CountryMaster CMR on IDL.CountryID = CMR.ID ");
                    sSql.Append("left join StoreMaster SMR on IDL.StoreID = SMR.ID ");
                    sSql.Append("left join StoreGroupMaster SGM on  SMR.StoreGroupID=SGM.ID ");
                    sSql.Append("left join PosMaster POS on POS.PosName=IHR.PosNo ");
                    sSql.Append("left join SKUMaster SKU on IDL.SKUID = SKU.ID ");
                    sSql.Append("left join StyleMaster SYR on SKU.StyleCode = SYR.StyleCode ");
                    sSql.Append("where IHR.CountryID=" + RequestData.CountryID + "  and SMR.StoreGroupID=" + RequestData.StoreGroupID + " and IDL.StoreID =" + RequestData.StoreID + " and CAST( IHR.DocumentDate AS DATE) between '" + RequestData.FromDate + "' and  '" + RequestData.ToDate + "'");
                }
                else if (RequestData.FromDate != null && RequestData.ToDate != null && RequestData.CountryID != 0 && RequestData.StoreGroupID != 0 && RequestData.StoreID != 0 && RequestData.PosID != 0 && RequestData.StyleID == 0)
                {
                    sSql.Append("select CAST( IHR.DocumentDate AS DATE) as BusinessDate,IHR.DocumentDate,CMR.CountryName,SMR.StoreName,SGM.StoreGroupName,POS.PosName as PosID,SYR.StyleCode,SKU.ScaleID,SKU.ColorID,SKU.SKUCode,SKU.SKUName,IHR.InvoiceNo,IDL.Qty,IHR.DiscountAmount,convert(Decimal(18,3),IDL.Price) as Price,convert(Decimal(18,3),IDL.LineTotal) as LineTotal,convert(Decimal(18,3),IHR.TotalPrice) as TotalPrice from InvoiceDetail IDL with(NoLock)");
                    sSql.Append("left join InvoiceHeader IHR on IDL.InvoiceHeaderID = IHR.ID ");
                    sSql.Append("left join CountryMaster CMR on IDL.CountryID = CMR.ID ");
                    sSql.Append("left join StoreMaster SMR on IDL.StoreID = SMR.ID ");
                    sSql.Append("left join StoreGroupMaster SGM on  SMR.StoreGroupID=SGM.ID ");
                    sSql.Append("left join PosMaster POS on POS.PosName=IHR.PosNo ");
                    sSql.Append("left join SKUMaster SKU on IDL.SKUID = SKU.ID ");
                    sSql.Append("left join StyleMaster SYR on SKU.StyleCode = SYR.StyleCode ");
                    sSql.Append("where IHR.CountryID=" + RequestData.CountryID + "  and SMR.StoreGroupID=" + RequestData.StoreGroupID + " and IDL.StoreID =" + RequestData.StoreID + " and POS.ID =" + RequestData.PosID + " and CAST( IHR.DocumentDate AS DATE) between '" + RequestData.FromDate + "' and  '" + RequestData.ToDate + "'");

                }
                else if (RequestData.FromDate != null && RequestData.ToDate != null && RequestData.CountryID != 0 && RequestData.StoreGroupID != 0 && RequestData.StoreID != 0 && RequestData.PosID != 0 && RequestData.StyleCode != null)
                {
                    sSql.Append("select CAST( IHR.DocumentDate AS DATE) as BusinessDate,IHR.DocumentDate,CMR.CountryName,SMR.StoreName,SGM.StoreGroupName,POS.PosName as PosID,SYR.StyleCode,SKU.ScaleID,SKU.ColorID,SKU.SKUCode,SKU.SKUName,IHR.InvoiceNo,IDL.Qty,IHR.DiscountAmount,convert(Decimal(18,3),IDL.Price) as Price,convert(Decimal(18,3),IDL.LineTotal) as LineTotal,convert(Decimal(18,3),IHR.TotalPrice) as TotalPrice from InvoiceDetail IDL with(NoLock)");
                    sSql.Append("left join InvoiceHeader IHR on IDL.InvoiceHeaderID = IHR.ID ");
                    sSql.Append("left join CountryMaster CMR on IDL.CountryID = CMR.ID ");
                    sSql.Append("left join StoreMaster SMR on IDL.StoreID = SMR.ID ");
                    sSql.Append("left join StoreGroupMaster SGM on  SMR.StoreGroupID=SGM.ID ");
                    sSql.Append("left join PosMaster POS on POS.PosName=IHR.PosNo ");
                    sSql.Append("left join SKUMaster SKU on IDL.SKUID = SKU.ID ");
                    sSql.Append("left join StyleMaster SYR on SKU.StyleCode = SYR.StyleCode ");
                    sSql.Append("where IHR.CountryID=" + RequestData.CountryID + " and SMR.StoreGroupID =" + RequestData.StoreGroupID + " and  IDL.StoreID =" + RequestData.StoreID + " and POS.ID  =" + RequestData.PosID + " and SYR.StyleCode='" + RequestData.StyleCode + "' and CAST( IHR.DocumentDate AS DATE) between '" + RequestData.FromDate + "' and  '" + RequestData.ToDate + "'");

                }
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objDailySalesReport = new DailySalesReport();
                        objDailySalesReport.BusinessDate = Convert.ToDateTime(objReader["BusinessDate"]);
                        objDailySalesReport.DocumentDate = Convert.ToDateTime(objReader["DocumentDate"]);
                        objDailySalesReport.CountryName = Convert.ToString(objReader["CountryName"]);
                        objDailySalesReport.StoreName = Convert.ToString(objReader["StoreName"]);
                        objDailySalesReport.PosNo = Convert.ToString(objReader["PosID"]);
                        objDailySalesReport.Stylecode = Convert.ToString(objReader["Stylecode"]);
                        objDailySalesReport.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"]) : 0;
                        objDailySalesReport.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objDailySalesReport.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : "";
                        objDailySalesReport.SKUName = objReader["SKUName"] != DBNull.Value ? Convert.ToString(objReader["SKUName"]) : "";
                        //objDailySalesReport.ScaleID = Convert.ToInt32(objReader["ScaleID"]);
                        //objDailySalesReport.ColorID = Convert.ToInt32(objReader["ColorID"]);
                        //objDailySalesReport.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        //objDailySalesReport.SKUName = Convert.ToString(objReader["SKUName"]);
                        objDailySalesReport.InvoiceNo = Convert.ToString(objReader["InvoiceNo"]);
                        objDailySalesReport.Qty = Convert.ToInt32(objReader["Qty"]);
                        objDailySalesReport.DiscountAmount = objReader["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountAmount"]) : 0;
                        objDailySalesReport.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        objDailySalesReport.LineTotal = objReader["LineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["LineTotal"]) : 0;
                        objDailySalesReport.TotalPrice = objReader["TotalPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalPrice"]) : 0;
                        SalesReportList.Add(objDailySalesReport);

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DailySalesReportList = SalesReportList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Daily Sales Report");
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
