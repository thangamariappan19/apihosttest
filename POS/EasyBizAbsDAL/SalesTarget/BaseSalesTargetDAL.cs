using EasyBizAbsDAL.Common;
using EasyBizRequest.SalesTargetRequest;
using EasyBizResponse.SalesTargetResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.SalesTarget
{
    public abstract class BaseSalesTargetDAL : BaseDAL
    {
        public abstract SalestargetHistoryResponse HistorySalesTarget(SalestargetHistoryRequest ObjRequest);
        public abstract SelectSalesTargetDetailsResponse SelectSalesTargetDetails(SelectSalesTargetDetailsRequest ObjRequest);
    }
}
