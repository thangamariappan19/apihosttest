using EasyBizDBTypes.Transactions.StockReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IStockReturn
{
    public interface IStockReturnCollectionView : IBaseView
    {
        List<StockReturnHeader> StockReturnHeaderList { get; set; }
        int StoreID { get; }
        string StoreCode { get; }
    }
}
