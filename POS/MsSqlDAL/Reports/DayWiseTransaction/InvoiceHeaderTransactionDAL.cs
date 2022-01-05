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
    public class InvoiceHeaderTransactionDAL : BaseInvoiceTransactionDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;   
        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {

            var InvoiceHeaderList = new List<InvoiceHeaderTransaction>();
            var RequestData = (InvoiceHeaderTransactionRequest)RequestObj;
            var ResponseData = new InvoiceHeaderTransactionReponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            String StoreName = "";
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = string.Empty;


                sSql = "SELECT ih.ID,cm.countryname,RIGHT(CONVERT(VARCHAR, ih.DocumentDate, 100), 7) as invoicetime,sm.storename,ih.InvoiceNo,ih.businessdate, case when empc.EmployeeName is null then emps.EmployeeName else empc.EmployeeName end EmployeeName,ih.SubTotalAmount,ih.TotalDiscountAmount,ih.TaxAmount,ih.NetAmount,(ih.ReceivedAmount - ih.ReturnAmount) as ReceivedAmount,";
                sSql = sSql + " um.CustomerName,knet.ReceivedCardAmount as knetAmount,visa.ReceivedCardAmount as visaAmount,Master.ReceivedCardAmount as MasterAmount,Mastero.ReceivedCardAmount as MasteroAmount,Amex.ReceivedCardAmount as AmexAmount,cash.ReceivedAmount as CashAmount FROM InvoiceHeader ih ";
                sSql = sSql + " left join EmployeeMaster empc on ih.CreateBy = empc.ID left join EmployeeMaster emps on ih.SalesEmployeeID = emps.ID left join Customermaster um on ih.CustomerID=um.ID left join countrymaster cm on ih.countryid=cm.id left join storemaster sm on ih.storeid=sm.id ";
                sSql = sSql + " left join InvoiceCardDetails knet on ih.InvoiceNo = knet.InvoiceNumber and knet.CardType = 'K-Net' left join InvoiceCardDetails Visa on ih.InvoiceNo = Visa.InvoiceNumber and Visa.CardType = 'Visa' left join InvoiceCardDetails MASTER on ih.InvoiceNo = MASTER.InvoiceNumber and MASTER.CardType = 'Master'  left join InvoiceCardDetails MASTERO on ih.InvoiceNo = MASTERO.InvoiceNumber and MASTERO.CardType = 'Mastero'  ";
                sSql = sSql + " left join InvoiceCardDetails AMEX on ih.InvoiceNo = AMEX.InvoiceNumber  and AMEX.CardType = 'Amex' left join InvoiceCashDetails Cash on ih.InvoiceNo = Cash.InvoiceNumber  ";
                sSql = sSql + " left join posmaster pm on ih.posid=pm.id Where ih.Businessdate between'" + RequestData.FromDate.ToString("yyyy/MM/dd") + "' and '" + RequestData.ToDate.ToString("yyyy/MM/dd") + "' and ih.storeid ='" + RequestData.StoreID + "' order by ih.countryid,ih.storeid,ih.invoiceno,ih.DocumentDate";

                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                
              

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceHeaderTypes = new InvoiceHeaderTransaction();

                        objInvoiceHeaderTypes.ID = Convert.ToInt64(objReader["ID"]);
                        objInvoiceHeaderTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? (Convert.ToDateTime(objReader["BusinessDate"])).ToString("dd/MM/yyyy") : "";
                       
                        objInvoiceHeaderTypes.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        objInvoiceHeaderTypes.InvoiceTime = objReader["InvoiceTime"] != DBNull.Value ? Convert.ToString(objReader["InvoiceTime"]) : string.Empty;
                        objInvoiceHeaderTypes.EmployeeName = objReader["EmployeeName"] != DBNull.Value ? Convert.ToString(objReader["EmployeeName"]) : String.Empty;
                        objInvoiceHeaderTypes.SubTotalAmount = objReader["SubTotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalAmount"]) : 0;                       
                        objInvoiceHeaderTypes.TotalDiscountAmount = objReader["TotalDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscountAmount"]) : 0;
                  
                        objInvoiceHeaderTypes.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                    
                        objInvoiceHeaderTypes.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceHeaderTypes.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objInvoiceHeaderTypes.KnetAmount = objReader["knetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["knetAmount"]) : 0;
                        objInvoiceHeaderTypes.VisaAmount = objReader["visaAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["visaAmount"]) : 0;
                        objInvoiceHeaderTypes.MasterAmount = objReader["MasterAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["MasterAmount"]) : 0;
                        objInvoiceHeaderTypes.MasteroAmount = objReader["MasteroAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["MasteroAmount"]) : 0;
                        objInvoiceHeaderTypes.AmexAmount = objReader["AmexAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["AmexAmount"]) : 0;
                        objInvoiceHeaderTypes.CashAmount = objReader["CashAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashAmount"]) : 0;
                        objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                        
                        if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        {                
                           
                            StoreName = objReader["storename"] != DBNull.Value ? Convert.ToString(objReader["storename"]) : string.Empty;                    
                        }
                        InvoiceHeaderList.Add(objInvoiceHeaderTypes);

                    }

                    ResponseData.InvStoreName = StoreName;
                    ResponseData.InvFromDate = RequestData.FromDate.ToString("dd/MMM/yyyy");
                    ResponseData.InvToDate = RequestData.ToDate.ToString("dd/MMM/yyyy");
                    ResponseData.InvoiceHeaderTransactionList = InvoiceHeaderList;
                    ResponseData.ResponseDynamicData = InvoiceHeaderList;           
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Transaction");
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
            var InvoiceDetailMasterList = new List<InvoiceDetailTransaction>();
             var InvoicePaymentMasterList = new List<InvoicePaymentTransaction>();
            var RequestData = (InvoiceHeaderTransactionRequest)RequestObj;
            var ResponseData = new InvoiceHeaderTransactionReponse();
            SqlDataReader objReader;
            SqlDataReader objReader_payment;
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
                sSql.Append("select a.*,c.ID,c.CustomerName,SM.StoreName,b.InvoiceNo,b.DocumentDate,sku.StyleCode,brand.BrandName,Color.Description as ColorName,Scale.ScaleCode from InvoiceDetail a ");
                sSql.Append("join InvoiceHeader b on b.ID = a.InvoiceHeaderID ");
                sSql.Append("join SKUMaster sku on a.SKUCode = sku.SKUCode ");
                sSql.Append("join BrandMaster brand on a.BrandID = brand.id ");
                sSql.Append("join ColorMaster Color on sku.ColorID = Color.id ");
                sSql.Append("join ScaleMaster Scale on sku.ScaleID = Scale.id ");
                sSql.Append("join StoreMaster SM on SM.ID = b.StoreID ");
                sSql.Append("join CustomerMaster c on c.CustomerCode = b.CustomerCode ");
                sSql.Append("where InvoiceHeaderID = '" + RequestData.ID + "'");

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceDetailMaster = new InvoiceDetailTransaction();
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
                        objInvoiceDetailMaster.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        objInvoiceDetailMaster.DiscountAmount = objReader["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountAmount"]) : 0;
                        objInvoiceDetailMaster.SellingPrice = objReader["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingPrice"]) : 0;
                        objInvoiceDetailMaster.SellingLineTotal = objReader["SellingLineTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingLineTotal"]) : 0;
                        objInvoiceDetailMaster.Brand = objReader["BrandName"] != DBNull.Value ? Convert.ToString(objReader["BrandName"]) : "";
                        objInvoiceDetailMaster.Color = objReader["ColorName"] != DBNull.Value ? Convert.ToString(objReader["ColorName"]) : "";
                        objInvoiceDetailMaster.Qty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0;
                        objInvoiceDetailMaster.Size = objReader["ScaleCode"] != DBNull.Value ? Convert.ToString(objReader["ScaleCode"]) : "";
                        Storename = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : "";
                        invno = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : "";
                        InvoiceDetailMasterList.Add(objInvoiceDetailMaster);
                    }
                    ResponseData.InvDInvNumber = invno;
                    ResponseData.InvDStoreName = Storename;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InvoiceDetailsTransactionList = InvoiceDetailMasterList;

                    sSql.Clear();
                    sSql.Append("Select 'cash' as paymenttype,isnull(b.ReceivedAmount,0) - isnull(b.ReturnAmount,0) as amount, ");
                    sSql.Append("'' as approvalnumber from InvoiceHeader as a ");
                    sSql.Append("left join invoicecashdetails as b on a.InvoiceNo = b.invoiceNumber ");
                    sSql.Append("where a.ID = '" + RequestData.ID + "' and b.ReceivedAmount > 0 ");
                    sSql.Append("Union ");
                    sSql.Append("Select cardtype as paymenttype,isnull(c.receivedcardamount,0) as amount, ");
                    sSql.Append(" isnull(approvalnumber,'') as approvalnumber from InvoiceHeader as a ");
                    sSql.Append("left join invoicecarddetails as c on a.invoiceno = c.invoicenumber ");
                    sSql.Append("where a.ID = '" + RequestData.ID + "'");

                    
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader_payment = _CommandObj.ExecuteReader();
                    if (objReader_payment.HasRows)
                    {
                        while (objReader_payment.Read())
                        {
                            var objInvoicePaymentMaster = new InvoicePaymentTransaction();
                            objInvoicePaymentMaster.PaymentType = objReader_payment["paymenttype"] != DBNull.Value ? Convert.ToString(objReader_payment["paymenttype"]) : "";
                            objInvoicePaymentMaster.Amount = objReader_payment["amount"] != DBNull.Value ? Convert.ToDecimal(objReader_payment["amount"]) : 0;
                            objInvoicePaymentMaster.ApprovalNumber = objReader_payment["approvalnumber"] != DBNull.Value ? Convert.ToString(objReader_payment["approvalnumber"]) : "";

                            InvoicePaymentMasterList.Add(objInvoicePaymentMaster);
                        }
                        ResponseData.InvoicePaymentTransactionList = InvoicePaymentMasterList;

                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Transaction");
                    }
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

        public override InvoiceHeaderTransactionReponse SelectAllReport(InvoiceHeaderTransactionRequest RequestObj)
        {

            var InvoiceHeaderList = new List<InvoiceHeaderTransaction>();
            var RequestData = (InvoiceHeaderTransactionRequest)RequestObj;
            var ResponseData = new InvoiceHeaderTransactionReponse();
            DataTable objDataTable = new DataTable();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InvoiceHeaderTransaction", _ConnectionObj);
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
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Transaction");
                }

            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Invoice Transaction");
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
