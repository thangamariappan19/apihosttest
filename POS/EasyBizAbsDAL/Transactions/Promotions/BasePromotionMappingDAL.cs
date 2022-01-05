using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Promotions.PromotionMappingRequest;
using EasyBizResponse.Transactions.Promotions.PromotionMappingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.Promotions
{
    public abstract class BasePromotionMappingDAL : BaseDAL
    {
        public abstract SelectPromotionMappingLookUpResponse SelectPromotionMappingLookUp(SelectPromotionMappingLookUpRequest ObjRequest);

        public abstract SelectPromotionMapListForCategoryResponse SelectPromotionMappingListByPromotion(SelectPromotionMapListForCategoryRequest RequestObj);
    }
}
