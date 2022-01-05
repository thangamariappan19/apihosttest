using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesOrder;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.IPaymentView
{
    public interface IPaymentDetailsHome : IBaseView
    {
        InVoiceCashDetails SaveInvoiceCashDetailsRecord { get; set; }
        InvoiceCardDetails SaveInvoiceCreditCardRecord { get; set; }
        Enums.PaymentProcessMode PaymentProcessMode { get; set; }
        InvoiceHeader InvoiceRecord { get; }
        List<TransactionLog> TransactionLogList { get; }
        Enums.OpStatusCode InvoiceStatus { get; set; }
        List<InvoiceCardDetails> InvoiceCardList { get; set; }
        List<InVoiceCashDetails> InvoiceCashList { get; set; }
        List<PaymentDetail> PayList { get; set; }
        
         int ShiftID { get; set; }
         int CashierID { get; set; }

         Decimal ReceivedAmount { get; set; }
         Decimal ReturnAmount { get; set; }

        UsersSettings UserInfo { get; }

        SalesOrderHeader SalesOrderRecord { get;  }

        string SalesOrderPaymentStatus { get; set; }

        Enums.ProcessMode ProcessMode { get; }

        string PrintInvoiceNo { set; }
        //PaymentTypeMasterType PaymentProcess { get; set; }
        Boolean IsPaymentProcesser { get; }
        List<PaymentProcessor> PaymentProcessorList { get; set; }
    }
}
