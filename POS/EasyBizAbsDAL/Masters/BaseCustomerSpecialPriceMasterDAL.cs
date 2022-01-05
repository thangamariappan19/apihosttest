using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CustomerSpecialPriceMasterRequest;
using EasyBizResponse.Masters.CustomerSpecialPriceMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseCustomerSpecialPriceMasterDAL : BaseDAL

    {
        public abstract SelectByCustomerSpecialPriceDetailsResponse SelectByCustomerSpecialPriceDetails(SelectByCustomerSpecialPriceDetailsRequest ObjRequest);
        //public abstract SelectByIDCustomerSpecialStoreDetailsResponse SelectByIDCustomerSpecialStoreDetails(SelectByIDCustomerSpecialStoreDetailsRequest ObjRequest);
       public abstract SelectByIDCustomerSpecialCategoryResponse SelectByIDCustomerSpecialCategoryDetails(SelectByIDCustomerSpecialCategoryRequest ObjRequest);
    }
}
