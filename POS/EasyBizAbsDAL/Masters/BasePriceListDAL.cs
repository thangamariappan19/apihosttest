using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizResponse.Masters.PriceListResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BasePriceListDAL:BaseDAL
    {
        public abstract SelectPriceListLookUPResponse SelectPriceListLookUPResponse(SelectPriceListLookUPRequest ObjRequest);

        public abstract SelectSalePriceListLookupResponse SelectSalePriceListLookUP(SelectSalePriceListLookupRequest ObjRequest);

        public abstract SelectPriceListLookUPResponse API_SelectPriceListMasterLookUp(SelectPriceListLookUPRequest requestData);
        public abstract SelectAllPriceListResponse API_SelectALL(SelectAllPriceListRequest objRequest);

    }
}
