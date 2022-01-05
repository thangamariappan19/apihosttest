using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IRetailSettings
{
    public interface IRetailView : IBaseView
    {
        int ID { get; set; }
        string RetailCode { get; set; }
        string RetailName { get; set; }
        Decimal PriceLowerLimit { get; set; }
        Decimal PriceUpperLimit { get; set; }
        bool RowforScan { get; set; }
        bool RefundWOReceipt { get; set; }
        bool ExchangeWOReceipt { get; set; }
        bool AutoUpdateAmt { get; set; }
        bool RefundPromotinal { get; set; }
        bool PrintParked { get; set; }
        bool DeleteParkedDayEnd { get; set; }
        bool MandatorySalePerson { get; set; }
        bool QuickComplete { get; set; }
        bool RestrictOtherPos { get; set; }
        bool MultipleTransRefundInSaingle { get; set; }
        bool LoginDiffDate { get; set; }
        bool ProductSearch { get; set; }
        bool StyleSearch { get; set; }
        bool CustomerSearch { get; set; }
        bool TransactionSearch { get; set; }
        bool CreditLimit { get; set; }
        bool ReceiptReprint { get; set; }
        bool DeletingSuspended { get; set; }
        bool VoidSale { get; set; }
        bool VoidItem { get; set; }
        bool Suspend { get; set; }
        bool PaymentCancel { get; set; }
        bool RefundTransaction { get; set; }
        Decimal MaxDiscountPercentage { get; set; }
        bool AllowRefundToExchanged { get; set; }
        Decimal MaxDiscountAmt { get; set; }
        //Decimal MaxLineDiscountPercentage { get; set; }
        //Decimal MaxLieDiscountAmt { get; set; }
        int ChangeAmountCurrency { get; set; }
        string DefaultTransMode { get; set; }
        bool ChangeSaleEmployee { get; set; }
        bool LogVoidedTransaction { get; set; }
        Decimal MaxLinesPerTransaction { get; set; }
        List<CurrencyMaster> CurrencyLookup { get; set; }

        bool Active { get; set; }

        bool AllowSalesForNegativeStock { get; set; }
        bool AllowSalesForZeroPrice { get; set; }
        bool IsCreditCardDetailsMandatory { get; set; }
        bool AllowMultiplePromotions { get; set; }
        bool AllowWNPromotions { get; set; }
        bool EnableFingerPrint { get; set; }
    }
}
