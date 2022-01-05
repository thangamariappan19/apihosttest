using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.IInvoice
{
    public interface IInvoiceCardDetails : IBaseView
    {
        int ID { get; set; }
        string InvoiceBillNo { get; }
        Decimal InvoiceTotalAmount { get; set; }        
        int PayCurrencyID { get; set; }       
        List<CurrencyMaster> CurrencyLookup { set; }
        UsersSettings UserInformation { get; }
        List<ExchangeRates> CurrencyExchangeList { get; set; }
        DateTime BusinessDate { get; }      
        string CardNumber { get; set; }
        string CardHolderName { get; set; }        
        string CardType { get; set; }          
        string ApprovalNumber { get;set ;}
        Decimal ReceivedCardAmount { get; set; }        
        InvoiceCardDetails InvoiceCardRecord { get; set; }
        List<PaymentTypeMasterType> PaymentTypeLookUp { set; }
        string CountryCode { get; }
        string CardType2 { get; set; }       
        List<PaymentTypeMasterType> PaymentTypeList { get; set; }
    }
}
