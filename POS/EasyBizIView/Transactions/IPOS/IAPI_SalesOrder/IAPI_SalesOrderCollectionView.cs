using EasyBizDBTypes.Transactions.POS.API_SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.IAPI_SalesOrder
{
    public interface IAPI_SalesOrderCollectionView : IBaseView
    {
        List<API_SalesOrderHeader> SalesOrderMasterList { get; set; }
        string DataMode { get; }
    }
}
