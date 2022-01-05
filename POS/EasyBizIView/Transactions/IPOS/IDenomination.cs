using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Masters.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS
{
    public interface IDenomination : IBaseView
    {
     
         int ID { get; set; }
         int ShiftLogID { get; set; }
        String PayCurrencyCode { get; set; }
        String ShiftCode { get; set; }
        String StoreCode { get; set; }
        String POSCode { get; set; }
        Decimal ShiftInAmount { get; set; }
        Decimal ShiftOutAmount { get; set; }
        UsersSettings UserInformation { get; }
        List<PaymentTypeMasterType> PaymentTypeList { get; set; }
        List<CurrencyDetails> CurrencyDetailsList { get; set; }
        List<DenominationForShiftOutType> DenominationForShiftOutTypeList { get; set; }
        List<DenominationForShiftOutType> SaveDenominationForShiftOutTypeList { get; set; }
        Decimal TotalValueCount { get; set; }
        Decimal TotalCardValue { get; set; }
        Decimal GrandTotalValue { get; set; }
        String Remarks { get; set; }

        int CountyID { get; set; }
        String CountryCode { get; set; }
    }
}
