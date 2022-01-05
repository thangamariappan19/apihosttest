using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IManagerOverride
{
    public interface IManagerOverrideView : IBaseView
    {
        int ID { get; set; }

        string Code { get; set; }
        string Name { get; set; }
        string CreditLimitOverride { get; set; }
        Boolean ReprintTransReceipt { get; set; }
        Boolean changeSalesPersoninSOE { get; set; }
        Boolean ChangeSalesPersonRefund { get; set; }
        Boolean DelSuspendedTransaction { get; set; }
        Boolean VoidSale { get; set; }
        Boolean voidItem { get; set; }
        Boolean TransModeChange { get; set; }
        Boolean CustomerSearch { get; set; }
        Boolean ProductSearch { get; set; }
        Boolean SaleInfoEdit { get; set; }
        Boolean ItemInfoEdit { get; set; }
        Boolean TransactionSearch { get; set; }
        Boolean SuspendRecall { get; set; }
        Boolean CashOut { get; set; }
        Boolean CashIn { get; set; }
        Boolean TransactionRefund { get; set; }
        Boolean Active { get; set; }
        Boolean TotalDiscount { get; set; }
        Boolean DayInDayOut { get; set; }
        Boolean AllowEditcustomer { get; set; }
    }
}
