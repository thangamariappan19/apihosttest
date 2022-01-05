using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.PriceTypeMasterResponse;
using EasyBizRequest.Masters.PriceTypeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BasePriceTypeDAL : BaseDAL
    {
        public abstract SelectAllPriceTypeResponse API_SelectALL(SelectAllPriceTypeRequest requestData);
    }
}
