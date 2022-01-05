using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.IInvoice
{
    public interface IDenominationView : IBaseView
    {
        ReceivedDenomination ReceivedDenominationRecord { get; set; }
    }
}
