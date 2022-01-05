using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.ISalesExchange
{
    public interface ISalesExchangeCollectionView : IBaseView
    {
        List<SalesExchangeHeader> SalesExchangeList { get; set; }       
    }
}
