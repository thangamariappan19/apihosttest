using EasyBizDBTypes.Transactions.StockStaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IStockAdjustment
{
    public interface IStockAdjustmentCollectionView:IBaseView
    {
        List<StockAdjustmentHeader> StockAdjustmentHeaderList { get; set; }
    }
}
