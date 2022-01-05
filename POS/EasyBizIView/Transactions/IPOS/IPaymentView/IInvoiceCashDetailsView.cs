using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.IPaymentView
{
   public interface IInvoiceCashDetailsView:IBaseView
    {
       int ID { get; set; }
       string InvoiceBillNo { get; }
       Decimal InvoiceTotalAmount { get; set; }
       Decimal ReceivedCashAmount { get; set; }
       int PayCurrencyID { get; set; }         
       int ChangeCurrencyID { get; set; }       
       InVoiceCashDetails InvoiceCashDetailsRecord { get; set; }
       List<CurrencyMaster> CurrencyLookup { set; }   
       UsersSettings UserInformation { get; }
       List<ExchangeRates> CurrencyExchangeList { get; set; }
       DateTime BusinessDate { get; }
    }
}
