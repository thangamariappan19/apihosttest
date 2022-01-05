using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.IPaymentView
{
   public interface InvoiceCashDetailsView:IBaseView
    {
       int ID { get; set; }
       string InvoiceBillNo { get; set; }
       Double InvoiceTotalAmount { get; set; }
       Double ReceivedCash { get; set; }
       Double BalanceCash { get; set; }
       //float DenominationValue { get; set; }
       //float DenominationNumber { get;set; }
       InVoiceCashDetails InvoiceCashDetailsRecord { get; set; }
       string PaymentMode { get; set; }
       string PaymentType { get; set; }
       Double Amounttobebepay { get; set; }
       string PaymentCurrency { get; set; }
       string ChangeCurrency { get; set; }
       List<CurrencyMaster> CurrencyLookup { set; }
       int CashPaymentCurrencyID { get; set; }
       int PaymentCurrencyID { get; set; }
       int ChangeCurrencyID { get; set; }
        
    }
}
