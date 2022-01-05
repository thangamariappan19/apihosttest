using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [Serializable]
    [DataContract]
    public class RetailSettingsType:BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string RetailCode { get; set; }

        [DataMember]
        public string RetailName { get; set; }

        [DataMember]
        public Decimal PriceLowerLimit { get; set; }

        [DataMember]
        public Decimal PriceUpperLimit { get; set; }

        [DataMember]
        public bool RowforScan { get; set; }

        [DataMember]
        public bool RefundWOReceipt { get; set; }

        [DataMember]
        public bool ExchangeWOReceipt { get; set; }

        [DataMember]
        public bool AutoUpdateAmt { get; set; }

        [DataMember]
        public bool RefundPromotinal { get; set; }

        [DataMember]
        public bool PrintParked { get; set; }

        [DataMember]
        public bool DeleteParkedDayEnd { get; set; }

        [DataMember]
        public bool MandatorySalePerson { get; set; }

        [DataMember]
        public bool QuickComplete { get; set; }

        [DataMember]
        public bool RestrictOtherPos { get; set; }

        [DataMember]
        public bool MultipleTransRefundInSaingle { get; set; }

        [DataMember]
        public bool LoginDiffDate { get; set; }

        [DataMember]
        public bool ProductSearch { get; set; }

        [DataMember]
        public bool StyleSearch { get; set; }

        [DataMember]
        public bool CustomerSearch { get; set; }

        [DataMember]
        public bool TransactionSearch { get; set; }

        [DataMember]
        public bool CreditLimit { get; set; }

        [DataMember]
        public bool ReceiptReprint { get; set; }

        [DataMember]
        public bool DeletingSuspended { get; set; }

        [DataMember]
        public bool VoidSale { get; set; }

        [DataMember]
        public bool VoidItem { get; set; }

        [DataMember]
        public bool Suspend { get; set; }

        [DataMember]
        public bool PaymentCancel { get; set; }

         [DataMember]
        public bool RefundTransaction { get; set; }

        [DataMember]
        public Decimal MaxDiscountPercentage { get; set; }

        [DataMember]
        public Decimal MaxDiscountAmt { get; set; }

        [DataMember]
        public Decimal MaxLineDiscountPercentage { get; set; }

        [DataMember]
        public Decimal MaxLieDiscountAmt { get; set; }

        [DataMember]
        public int ChangeAmountCurrency { get; set; }
        [DataMember]
        public string DefaultTransMode { get; set; }
        [DataMember]
        public bool ChangeSaleEmployee { get; set; }
        [DataMember]
        public bool LogVoidedTransaction { get; set; }
        [DataMember]
        public Decimal MaxLinesPerTransaction { get; set; }
        public object Remarks { get; set; }
        [DataMember]
        public bool AllowRefundToExchangedItems { get; set; }
        [DataMember]
        public bool AllowSalesForNegativeStock { get; set; }
        [DataMember]
        public bool AllowSalesForZeroPrice { get; set; }
        [DataMember]
        public bool IsCreditCardDetailsMandatory { get; set; }
        [DataMember]
        public bool AllowMultiplePromotions { get; set; }
        [DataMember]
        public bool AllowWNPromotions { get; set; }

        [DataMember]
        public bool EnableFingerPrint { get; set; }
    }
}
