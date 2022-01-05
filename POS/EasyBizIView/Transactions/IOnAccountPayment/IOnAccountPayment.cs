using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Common;

namespace EasyBizIView.Transactions.IOnAccountPayment
{
    public interface IOnAccountPayment : IBaseView
    {
        string Type { get; set; }
        string SearchString { get; set; }
        OnAccountPayment OnAccountPaymentRecord { get; set; }        
        List<OnAccountPaymentDetails> OnAccountPaymentDetailsList { get; set; }
        List<OnAcInvoiceWisePayment> OnAcInvoiceWisePaymentList { get; set; }
        List<CustomerMaster> CustomerMasterList { get; set; }
        List<ExchangeRates> CurrencyExchangeList { get; set; }
        List<CurrencyMaster> CurrencyLookup { get; set; }
        UsersSettings UserInfo { get; }
    }
    public interface IOnAccountPaymentCollectionView : IBaseView
    {
        List<OnAccountPayment> OnAccountPaymentList { get; set; }
        Enums.RequestFrom DataRequestFrom { get; set; }
        string SearchString { get; }
    }
}
