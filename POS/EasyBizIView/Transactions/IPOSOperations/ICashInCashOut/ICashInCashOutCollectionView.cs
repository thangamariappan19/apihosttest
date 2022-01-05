using EasyBizDBTypes.Transactions.PaymentDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPaymentDetails.ICashInCashOut
{
    public interface ICashInCashOutCollectionView : IBaseView
    {
        List<CashInCashOutMaster> CashInCashOutMasterList { get; set; }
    }
}
