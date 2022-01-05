using EasyBizDBTypes.Transactions.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPricePoint
{
    public interface IPricePointListView :IBaseView
    {
        List<PricePoint> PricePointList { get; set; }
    }
}
