using EasyBizDBTypes.Transactions.POSOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOSOperations.ISalesTarget
{
    public interface ItargetCollection : IBaseView
    {
        List<SalesTargetHeader> SalesTargetHeaderList { get; set; }
    }
}
