using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.PaymentDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPaymentDetails.ICashInCashOut
{
    public interface ICashInCashOutView : IBaseView
    {
        int ID { get; set; }
        DateTime DocumentDate { get; set; }
        Decimal Total { get; set; }

        int StoreID { get; set; }
        int POSID { get; set; }
        int ShiftID { get; set; }
        string StoreCode { get; set; }
        string POSCode { get; set; }
        string ShiftCode { get; set; }


        List<CashInCashOutDetails> CashInCashOutDetailsList { get; set; }
        List<ReasonMaster> ReasonMasterList { get; set; }
        UsersSettings UserInformation { get; }
    }
}
