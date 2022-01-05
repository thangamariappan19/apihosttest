using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.WarehouseMasterRequest;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizResponse.Masters.WarehouseMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseWarehouseMasterDAL: BaseDAL
    {
        public abstract SelectWhareouseLookUpResponse SelectWhareHouseLookUp(SelectWhareHouseLookUpRequest RequestObj);
        public BaseWarehouseMasterDAL()
        {
             
            // TODO: Complete member initialization
        }

        public abstract SelectAllWarehouseMasterResponse API_SelectAll(SelectAllWarehouseMasterRequest objRequest);
    }
}
