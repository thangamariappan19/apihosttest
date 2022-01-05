using EasyBizAbsDAL.Reports.DayWiseTransaction;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports.DayWiseTransaction;
using EasyBizRequest.Reports.DayWiseTransactionRequest;
using EasyBizResponse.Reports.DayWiseTransactionResponse;
using EasyBizRequest;
using EasyBizResponse;
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
    public class SalesReturnTransactionDAL : BaseSalesReturnTransactionDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {

            var SalesReturnHeaderList = new List<SalesReturnHeaderTransaction>();
            var RequestData = (SalesReturnTransactionRequest)RequestObj;
            var ResponseData = new SalesReturnTransactionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            String StoreName = "";
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = string.Empty;


                sSql = "SELECT sr.ID,cm.countryname,sm.storename,sr.DocumentNo,sr.DocumentDate, case when empc.EmployeeName is null then emps.EmployeeName else empc.EmployeeName end EmployeeName,sr.TotalReturnAmount,";
                sSql = sSql + " um.CustomerName, invh.invoiceNo FROM SalesReturnHeader sr left join EmployeeMaster empc on sr.CreateBy = empc.ID  ";
                sSql = sSql + " left join EmployeeMaster emps on sr.Createby = emps.ID left join invoiceheader invh on invh.invoiceNo =  sr.SalesInvoiceNumber left join Customermaster um on invh.CustomerID=um.ID left join countrymaster cm on sr.countryid=cm.id left join storemaster sm on sr.storeid=sm.id ";
                sSql = sSql + " left join posmaster pm on sr.posid=pm.id Where sr.DocumentDate between'" + RequestData.FromDate.ToString("yyyy/MM/dd") + "' and '" + RequestData.ToDate.ToString("yyyy/MM/dd") + "' and sr.storeid ='" + RequestData.StoreID + "' order by sr.countryid,sr.storeid,sr.DocumentNo,sr.DocumentDate";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;



                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesReturnHeaderTypes = new SalesReturnHeaderTransaction();

                        objSalesReturnHeaderTypes.ID = Convert.ToInt64(objReader["ID"]);
                        objSalesReturnHeaderTypes.BusinessDate = objReader["DocumentDate"] != DBNull.Value ? (Convert.ToDateTime(objReader["DocumentDate"])).ToString("dd/MM/yyyy") :"";
                        objSalesReturnHeaderTypes.SalesReturnNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : string.Empty;
                        objSalesReturnHeaderTypes.EmployeeName = objReader["EmployeeName"] != DBNull.Value ? Convert.ToString(objReader["EmployeeName"]) : String.Empty;
                        objSalesReturnHeaderTypes.ReturnAmount = objReader["TotalReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalReturnAmount"]) : 0;
                        objSalesReturnHeaderTypes.InvNo = objReader["invoiceNo"] != DBNull.Value ? Convert.ToString(objReader["invoiceNo"]) : string.Empty;
                        objSalesReturnHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;

                        if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        {
                            objSalesReturnHeaderTypes.CountryName = objReader["countryname"] != DBNull.Value ? Convert.ToString(objReader["countryname"]) : string.Empty;
                            StoreName = objReader["storename"] != DBNull.Value ? Convert.ToString(objReader["storename"]) : string.Empty;
                        }
                        SalesReturnHeaderList.Add(objSalesReturnHeaderTypes);

                    }

                    ResponseData.InvStoreName = StoreName;
                    ResponseData.InvFromDate = RequestData.FromDate.ToString("dd/MMM/yyyy");
                    ResponseData.InvToDate = RequestData.ToDate.ToString("dd/MMM/yyyy");
                    ResponseData.SalesReturnHeaderTransactionList = SalesReturnHeaderList;
                    ResponseData.ResponseDynamicData = SalesReturnHeaderList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Return");
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
        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SalesReturnDetailMasterList = new List<SalesReturnDetailTransaction>();
            var RequestData = (SalesReturnTransactionRequest)RequestObj;
            var ResponseData = new SalesReturnTransactionResponse();
            SqlDataReader objReader;
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
                sSql.Append("select a.*,SM.StoreName,b.DocumentNo,b.DocumentDate,sku.StyleCode,Color.Description as ColorName,Scale.ScaleCode from salesreturnDetail a ");
                sSql.Append("join SalesReturnHeader b on b.ID = a.SalesReturnID ");
                sSql.Append("join SKUMaster sku on a.itemcode = sku.SKUCode ");
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
                        var objInvoiceDetailMaster = new SalesReturnDetailTransaction();
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
                        objInvoiceDetailMaster.Department = department;
                        objInvoiceDetailMaster.ItemCode = itemcode;
                        objInvoiceDetailMaster.Price = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                        objInvoiceDetailMaster.Color = objReader["ColorName"] != DBNull.Value ? Convert.ToString(objReader["ColorName"]) : "";
                        objInvoiceDetailMaster.Qty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;
                        objInvoiceDetailMaster.Size = objReader["ScaleCode"] != DBNull.Value ? Convert.ToString(objReader["ScaleCode"]) : "";
                        objInvoiceDetailMaster.SKUCode = objReader["ItemCode"] != DBNull.Value ? Convert.ToString(objReader["ItemCode"]) : "";
                        Storename = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : "";
                        invno = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
                        SalesReturnDetailMasterList.Add(objInvoiceDetailMaster);
                    }
                    ResponseData.InvDInvNumber = invno;
                    ResponseData.InvDStoreName = Storename;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesReturnDetailsTransactionList  = SalesReturnDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Return");
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

        public override SalesReturnTransactionResponse SelectAllReport(SalesReturnTransactionRequest RequestObj)
        {

            var InvoiceHeaderList = new List<SalesReturnHeaderTransaction>();
            var RequestData = (SalesReturnTransactionRequest)RequestObj;
            var ResponseData = new SalesReturnTransactionResponse();
            DataTable objDataTable = new DataTable();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("SalesReturnHeaderTransaction", _ConnectionObj);
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
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Return");
                }

            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Sales Return");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            return null;
        }
        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            return null;
        }
        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            return null;
        }

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}