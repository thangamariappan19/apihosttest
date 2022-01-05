using EasyBizDBTypes.Transactions.POS.SalesReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.ISalesReturns
{
   
    public interface ISalesReturnCollectionView : IBaseView
    {
        List<SalesReturnHeader> SalesReturnHeaderList { get; set; }
    }
}
