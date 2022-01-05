using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IInventoryCounting
{
    public interface IInventoryCountingCollectionView : IBaseView
    {
        List<InventoryCountingHeader> InventoryCountingHeaderList { get; set; }
        List<InventoryInit> InventoryInitList { get; set; }
    }
}
