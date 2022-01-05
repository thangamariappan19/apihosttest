using EasyBizDBTypes.Transactions.Stocks.NonTradingItemStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.INonTradingItem
{
    public interface INonTradingStockCollectionView : IBaseView
    {
        List<NonTradingStockHeaderTypes> NonTradingStockHeaderList { get; set; }
        int StoreID { get; }
        string StoreCode { get; }
    }
}
