using EasyBizBLL.Common;
using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesOrder;
using EasyBizIView.Transactions.IPOS.IPaymentView;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest;
using EasyBizRequest.Transactions.POS.SalesOrder;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS.Invoice
{
    public class PaymentHomeInvoicePresenter
    {
        IPaymentDetailsHome _IPaymentDetailsHome;
        InvoiceCashDetailsBLL _InvoiceCashDetailsBLL = new InvoiceCashDetailsBLL();
        InvoiceCardDetailsBLL _InvoiceCardDetailsBLL = new InvoiceCardDetailsBLL();     
        InvoiceBLL _InvoiceBLL = new InvoiceBLL();
        private static string _BillNo = string.Empty;
        private static int _DetailID = 0;
        private static int _RunningNo = 0;
        public PaymentHomeInvoicePresenter(IPaymentDetailsHome ViewObj)
        {
            _IPaymentDetailsHome = ViewObj;
        }
        public void SelectDocumentNumberingRecord()
        {
            try
            {
                var _DocumentNumberingBLL = new DocumentNumberingBLL();

                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.SALES;
                RequestData.CountryID = _IPaymentDetailsHome.UserInfo.CountryID;
                RequestData.StateID = _IPaymentDetailsHome.UserInfo.StateID;
                RequestData.StoreID = _IPaymentDetailsHome.UserInfo.StoreID;
                RequestData.POSID = _IPaymentDetailsHome.UserInfo.POSID;

                RequestData.StoreCode = _IPaymentDetailsHome.UserInfo.StoreCode;
                RequestData.POSCode = _IPaymentDetailsHome.UserInfo.POSCode;

                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string BillNo = string.Empty;

                    BillNo = BillNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);


                    _BillNo = BillNo;
                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SavePaymentProcessor()
        {                   
            try
            {            
                    var RequestData = new SaveInvoiceRequest();
                    RequestData.PaymentProcessorList = _IPaymentDetailsHome.PaymentProcessorList;               

                    var ResponseData = _InvoiceBLL.SavePaymentProcessor(RequestData);              
                    //_IPaymentDetailsHome.Message = ResponseData.DisplayMessage;
                    //_IPaymentDetailsHome.ProcessStatus = ResponseData.StatusCode;               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveInvoice()
        {
            try
            {

                SelectDocumentNumberingRecord();

                var _InvoiceBLL = new InvoiceBLL();
                var RequestData = new SaveInvoiceRequest();

                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.InvoiceHeaderData = new InvoiceHeader();
                RequestData.InvoiceDetailList = new List<InvoiceDetails>();

                //Decimal ReceivedAmount = _IPaymentDetailsHome.PayList.Sum(x => x.Receivedamount);
                //Decimal ReturnAmount = ReceivedAmount - _IPaymentDetailsHome.InvoiceRecord.NetAmount;

                int ShiftID = _IPaymentDetailsHome.ShiftID;
                int CashierID = _IPaymentDetailsHome.CashierID;
                int EmployeeID = _IPaymentDetailsHome.InvoiceRecord.SalesEmployeeID;
                // int CASHIERID = _IInvoiceView.UserInformation.ID;
                _IPaymentDetailsHome.InvoiceRecord.ReceivedAmount = _IPaymentDetailsHome.ReceivedAmount;
                _IPaymentDetailsHome.InvoiceRecord.ReturnAmount = _IPaymentDetailsHome.ReturnAmount;
                _IPaymentDetailsHome.InvoiceRecord.ShiftID = ShiftID;
                _IPaymentDetailsHome.InvoiceRecord.CashierID = CashierID;
                _IPaymentDetailsHome.InvoiceRecord.InvoiceNo = _BillNo;
                _IPaymentDetailsHome.PrintInvoiceNo = _BillNo;
                RequestData.PaymentProcessorList = _IPaymentDetailsHome.PaymentProcessorList;

                if (_IPaymentDetailsHome.SalesOrderRecord != null)
                {
                    RequestData.SalesOrderDocumentNo = _IPaymentDetailsHome.SalesOrderRecord.DocumentNo;
                }
                else
                {
                    RequestData.SalesOrderDocumentNo = string.Empty;
                }

                RequestData.InvoiceHeaderData = _IPaymentDetailsHome.InvoiceRecord;

                if(_IPaymentDetailsHome.InvoiceRecord.InvoiceDetailList != null && _IPaymentDetailsHome.InvoiceRecord.InvoiceDetailList.Count > 0)
                {
                    _IPaymentDetailsHome.InvoiceRecord.InvoiceDetailList.ForEach(x => x.BillNo = _BillNo);
                    _IPaymentDetailsHome.InvoiceRecord.InvoiceDetailList.ForEach(x => x.InvoiceNo = _BillNo);
                }
                RequestData.InvoiceDetailList = _IPaymentDetailsHome.InvoiceRecord.InvoiceDetailList;

                if(_IPaymentDetailsHome.PayList != null && _IPaymentDetailsHome.PayList.Count > 0)
                {
                    _IPaymentDetailsHome.PayList.ForEach(x => x.InvoiceNumber = _BillNo);
                }
                RequestData.PaymentList = _IPaymentDetailsHome.PayList;

                RequestData.BaseIntegrateStoreID = _IPaymentDetailsHome.InvoiceRecord.StoreID;
                RequestData.TransactionLogList = _IPaymentDetailsHome.TransactionLogList;

                RequestData.RunningNo = _RunningNo;
                RequestData.DocumentNumberingID = _DetailID;

                var ResponseData = _InvoiceBLL.SaveInvoice(RequestData);

                _IPaymentDetailsHome.Message = ResponseData.DisplayMessage;
                _IPaymentDetailsHome.InvoiceStatus = ResponseData.StatusCode;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectSalesOrderDocumentNumber()
        {
            try
            {
                var _DocumentNumberingBLL = new DocumentNumberingBLL();
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.SALESORDER;
                RequestData.CountryID = _IPaymentDetailsHome.UserInfo.CountryID;
                RequestData.StateID = _IPaymentDetailsHome.UserInfo.StateID;
                RequestData.StoreID = _IPaymentDetailsHome.UserInfo.StoreID;
                RequestData.POSID = _IPaymentDetailsHome.UserInfo.POSID;

                RequestData.StoreCode = _IPaymentDetailsHome.UserInfo.StoreCode;
                RequestData.POSCode = _IPaymentDetailsHome.UserInfo.POSCode;

                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string BillNo = string.Empty;
                    BillNo = BillNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

                    _BillNo = BillNo;

                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public void SaveSalesOrder()
        {
            try
            {
                if (_IPaymentDetailsHome.SalesOrderRecord != null)
                {
                    var PayList = new List<PaymentDetail>();
                    var _SalesOrderBLL = new SalesOrderBLL();
                    var RequestData = new SaveSalesOrderRequest();
                    RequestData.SalesOrderHeaderRecord = new SalesOrderHeader();

                    var SalesOrderHeaderRecord = _IPaymentDetailsHome.SalesOrderRecord;
                    if (_IPaymentDetailsHome.ProcessMode == Enums.ProcessMode.New)
                    {
                        SelectSalesOrderDocumentNumber();
                        SalesOrderHeaderRecord.DocumentNo = _BillNo; //Re-Assign Document

                        PayList = _IPaymentDetailsHome.PayList;
                        PayList.ForEach(x => x.InvoiceNumber = _BillNo);
                    }
                    else
                    {
                        PayList = _IPaymentDetailsHome.PayList;
                        PayList.ForEach(x => x.InvoiceNumber = SalesOrderHeaderRecord.DocumentNo);
                    }

                    SalesOrderHeaderRecord.PaymentStatus = _IPaymentDetailsHome.SalesOrderPaymentStatus;

                    RequestData.SalesOrderHeaderRecord = SalesOrderHeaderRecord;                    
                    RequestData.SalesOrderDetailsList = SalesOrderHeaderRecord.SalesOrderDetailsList;
                    RequestData.PaymentList = PayList;

                    var ResponseData = _SalesOrderBLL.SaveSalesOrder(RequestData);
                    _IPaymentDetailsHome.InvoiceStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IPaymentDetailsHome.InvoiceStatus = Enums.OpStatusCode.CreateRecordFailed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveTransactionsLog()
        {
            try
            {
                var _TransactionLogBLL = new TransactionLogBLL();
                var RequestData = new SaveTransactionLogRequest();
                RequestData.TransactionLogList = _IPaymentDetailsHome.TransactionLogList;
                var ResponseData = _TransactionLogBLL.SaveTransactionLog(RequestData);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
