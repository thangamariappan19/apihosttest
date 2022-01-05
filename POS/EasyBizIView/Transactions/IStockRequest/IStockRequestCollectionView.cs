using EasyBizDBTypes.Transactions.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IStockRequest
{
    public interface IStockRequestCollectionView : IBaseView
    {
        List<StockRequestHeader> StockRequestHeaderList { get; set; }
    }
}
