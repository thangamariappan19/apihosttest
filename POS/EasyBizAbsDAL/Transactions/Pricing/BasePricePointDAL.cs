using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizResponse.Transactions.Pricing.PricePointResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.Pricing
{
    public abstract class BasePricePointDAL : BaseDAL
    {
        public abstract GetPricePointRangeListResponse GetPricePointRangeList(GetPricePointRangeListRequest RequestObj);

        public abstract SelectAllPricePointResponse API_SelectALL(SelectAllPricePointRequest requestData);
    }
}
