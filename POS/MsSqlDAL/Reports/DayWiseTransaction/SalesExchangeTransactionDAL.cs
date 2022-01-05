using EasyBizAbsDAL.Reports.DayWiseTransaction;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports.DayWiseTransaction;
using EasyBizRequest.Reports.DayWiseTransactionRequest;
using EasyBizResponse.Reports.DayWiseTransactionResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Reports.DayWiseTransaction
{
    public class SalesExchangeTransactionDAL : BaseSalesExchangeTransactionDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;

        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        public override SalesExchangeTransactionResponse SelectAllSalesExchangeDetailList(SalesExchangeTransactionRequest RequestObj)
        {
            var SalesExchangeList = new List<SalesExchangeHeaderTransaction>();
            var RequestData = (SalesExchangeTransactionRequest)RequestObj;
            var ResponseData = new SalesExchangeTransactionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = string.Empty;
                String StoreName = String.Empty;
                sSql = @"Select se.*,um.CustomerName,ih.DocumentDate as SalesDate,ih.InvoiceNo as SalesInvoiceNumber,SM.Storename,
                        case when empc.EmployeeName is null then emps.EmployeeName else empc.EmployeeName end EmployeeName 
                        from SalesExchangeHeader se
                        left join InvoiceHeader ih on se.SalesInvoiceNumber = ih.InvoiceNo 
                        left join EmployeeMaster empc on ih.CreateBy = empc.ID
                        left join EmployeeMaster emps on ih.SalesEmployeeID = emps.ID 
                        left join StoreMaster SM on se.StoreID = SM.ID
                        left join Customermaster um on ih.CustomerID=um.ID where se.DocumentDate between '" + RequestData.FromDate.ToString("yyyy/MM/dd") + "' and  '" + RequestData.ToDate.ToString("yyyy/MM/dd") + "' and se.StoreID = "  + RequestData.StoreID + "  order by se.Documentno,se.DocumentDate" ;

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesExchangeHeader = new SalesExchangeHeaderTransaction();
                        objSalesExchangeHeader.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSalesExchangeHeader.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objSalesExchangeHeader.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? (Convert.ToDateTime(objReader["DocumentDate"])).ToString("dd/MM/yyyy") : "";
                        objSalesExchangeHeader.SalesInvoiceNumber = objReader["SalesInvoiceNumber"] != DBNull.Value ? Convert.ToString(objReader["SalesInvoiceNumber"]) : "";
                        objSalesExchangeHeader.SalesDate = objReader["SalesDate"] != DBNull.Value ? Convert.ToDateTime(objReader["SalesDate"]).ToString("dd/MM/yyyy") : "";
                        objSalesExchangeHeader.TotalExchangeQty = objReader["TotalExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalExchangeQty"]) : 0;
                        objSalesExchangeHeader.ExchangeWithOutInvoiceNo = objReader["ExchangeWithOutInvoiceNo"] != DBNull.Value ? Convert.ToBoolean(objReader["InvoiceHeaderID"]) : false;
                        objSalesExchangeHeader.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : "";
                        objSalesExchangeHeader.SalesManName = objReader["EmployeeName"] != DBNull.Value ? Convert.ToString(objReader["EmployeeName"]) : "";
                        StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : "";

                        SalesExchangeList.Add(objSalesExchangeHeader);
                    }
                  
                    ResponseData.StoreName = StoreName;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesExchangeHeaderList = SalesExchangeList;
                    ResponseData.ResponseDynamicData = SalesExchangeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Exchange");
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

        public override SalesExchangeTransactionResponse SelectRecordByID(SalesExchangeTransactionRequest RequestObj)
        {
            var SalesExchangeDetailMasterList = new List<SalesExchangeDetailTransaction>();
            var RequestData = (SalesExchangeTransactionRequest)RequestObj;
            var ResponseData = new SalesExchangeTransactionResponse();
            SqlDataReader objReader;
            SqlDataReader objReaderwith;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                string skucode = "";
                string department = "";
                string itemcode = "";
                String invno = "";
                String Storename = "";
                int i = 0;
                sSql.Append("select a.*,SM.StoreName,b.DocumentNo,b.DocumentDate,sku.StyleCode,Color.Description as ColorName,Scale.ScaleCode from SalesExchangeDetail a ");
                sSql.Append("join SalesExchangeHeader b on b.ID = a.SalesExchangeID ");
                sSql.Append("join SKUMaster sku on a.SKUCode = sku.SKUCode ");
                sSql.Append("join ColorMaster Color on sku.ColorID = Color.id ");
                sSql.Append("join ScaleMaster Scale on sku.ScaleID = Scale.id ");
                sSql.Append("join StoreMaster SM on SM.ID = b.StoreID ");
                sSql.Append("where b.id = '" + RequestData.ID + "'");

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesExchangeDetail = new SalesExchangeDetailTransaction();
                        skucode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : "";
                        string[] words = skucode.Split('-');
                        foreach (string word in words)
                        {
                            if (i == 0)
                            {
                                i = 1;
                                department = word;
                            }
                            else
                            {
                                itemcode = word;
                            }
                        }
                        objSalesExchangeDetail.Department = department;
                        objSalesExchangeDetail.ItemCode = itemcode;
                        objSalesExchangeDetail.Price = objReader["SellingPricePerQty"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingPricePerQty"]) : 0;
                        objSalesExchangeDetail.Color = objReader["ColorName"] != DBNull.Value ? Convert.ToString(objReader["ColorName"]) : "";
                        objSalesExchangeDetail.Qty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0;
                        objSalesExchangeDetail.Size = objReader["ScaleCode"] != DBNull.Value ? Convert.ToString(objReader["ScaleCode"]) : "";
                        objSalesExchangeDetail.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : "";
                        Storename = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : "";
                        invno = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
                        SalesExchangeDetailMasterList.Add(objSalesExchangeDetail);
                    }
                    ResponseData.ExchangeNumber = invno;
                    ResponseData.StoreName = Storename;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesExchangeDetailsTransactionList = SalesExchangeDetailMasterList;
                    objReader.Close();
                    sSql.Clear();
                    var SalesExchangewithMasterList = new List<SalesExchangeWithTransaction>();

                    sSql.Append("select a.*,SM.StoreName,b.InQty as Qty,b.SKUCode,b.DocumentNo,sku.StyleCode,Color.Description as ColorName,Scale.ScaleCode from  SalesExchangeHeader as a ");
                    sSql.Append(" inner join TransactionLog b on b.DocumentNo = a.DocumentNo and b.transactiontype = 'SalesExchange' and b.InQty > 0 ");
                    sSql.Append(" join SKUMaster sku on b.SKUCode = sku.SKUCode ");
                    sSql.Append(" join ColorMaster Color on sku.ColorID = Color.id ");
                    sSql.Append(" join ScaleMaster Scale on sku.ScaleID = Scale.id ");
                    sSql.Append(" join StoreMaster SM on SM.ID = b.StoreID ");
                    sSql.Append(" where a.id = '" + RequestData.ID + "'");

                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReaderwith = _CommandObj.ExecuteReader();
                    if (objReaderwith.HasRows)
                    {
                        while (objReaderwith.Read())
                        {
                            var objSalesExchangeWith = new SalesExchangeWithTransaction();
                            skucode = objReaderwith["StyleCode"] != DBNull.Value ? Convert.ToString(objReaderwith["StyleCode"]) : "";
                            string[] words = skucode.Split('-');
                            foreach (string word in words)
                            {
                                if (i == 0)
                                {
                                    i = 1;
                                    department = word;
                                }
                                else
                                {
                                    itemcode = word;
                                }
                            }
                            objSalesExchangeWith.Department = department;
                            objSalesExchangeWith.ItemCode = itemcode;                       
                            objSalesExchangeWith.Color = objReaderwith["ColorName"] != DBNull.Value ? Convert.ToString(objReaderwith["ColorName"]) : "";
                            objSalesExchangeWith.Qty = objReaderwith["Qty"] != DBNull.Value ? Convert.ToInt32(objReaderwith["Qty"]) : 0;
                            objSalesExchangeWith.Size = objReaderwith["ScaleCode"] != DBNull.Value ? Convert.ToString(objReaderwith["ScaleCode"]) : "";
                            objSalesExchangeWith.SKUCode = objReaderwith["SKUCode"] != DBNull.Value ? Convert.ToString(objReaderwith["SKUCode"]) : "";                        
                            SalesExchangewithMasterList.Add(objSalesExchangeWith);
                        } 
                        ResponseData.SalesExchangeWithTransactionList = SalesExchangewithMasterList;
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Exchange");
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

        public override SalesExchangeTransactionResponse SelectAllSalesExchangeTransactionReportList(SalesExchangeTransactionRequest RequestObj)
        {

            var SalesExchangeDetailMasterList = new List<SalesExchangeDetailTransaction>();
            var RequestData = (SalesExchangeTransactionRequest)RequestObj;
            var ResponseData = new SalesExchangeTransactionResponse();
            DataTable objDataTable = new DataTable();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("SalesExchangeHeaderTransaction", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter FromDate = _CommandObj.Parameters.Add("@FromDate", SqlDbType.DateTime);
                FromDate.Direction = ParameterDirection.Input;
                FromDate.Value = sqlCommon.GetSQLServerDateString(RequestData.FromDate);

                SqlParameter ToDate = _CommandObj.Parameters.Add("@ToDate", SqlDbType.DateTime);
                ToDate.Direction = ParameterDirection.Input;
                ToDate.Value = sqlCommon.GetSQLServerDateString(RequestData.ToDate);

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StoreID;


                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlDataReader objReader;

                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    objDataTable.Load(objReader);
                }

                if (objDataTable.Rows.Count > 0)
                {
                    ResponseData.ReportDataTable = objDataTable;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.ReportDataTable = new DataTable();
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Exchange");
                }

            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Sales Exchange");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            return null;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            return null;
        }
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            return null;
        }
        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            return null;
        }
        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            return null;
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
