using EasyBizDBTypes.Transactions.POS.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.ISalesOrder
{
   public interface ISalesOrderCollectionView : IBaseView
    {
        List<SalesOrderHeader> SalesOrderMasterList { get; set; }

        string DataMode { get; }
    }
}
