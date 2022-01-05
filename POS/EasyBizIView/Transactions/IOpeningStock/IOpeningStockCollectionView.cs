using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IOpeningStock
{
    public interface IOpeningStockCollectionView : IBaseView
    {
        List<OpeningStockHeader> OpeningStockHeaderList { get; set; }
    }
}
