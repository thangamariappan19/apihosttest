using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IInventoryCounting
{
    public interface IInventoryCountingView
    {
        int InventoryCountingID { get; }
        InventoryCountingHeader InventoryCountingRecord { set; }
    }
}
