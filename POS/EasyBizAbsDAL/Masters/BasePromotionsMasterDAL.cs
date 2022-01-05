using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Transactions.Promotions.PromotionCriteria;
using EasyBizResponse.Masters.PromotionsMasterResponse;
using EasyBizResponse.Transactions.Promotions.PromotionCriteriaResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BasePromotionsMasterDAL : BaseDAL
    {
        public abstract SelectPromotionsLookUpResponse SelectPromotionsLookUp(SelectPromotionsLookUpRequest ObjRequest);

        public abstract SelectByPromotionIDStoreDetailsResponse SelectByPromotionWithStoreDetails(SelectByPromotionIDStoreDetailsRequest ObjRequest);

        public abstract SelectPromotionCriteriaResponse SelectPromotionCriteriaDetails(SelectPromotionCriteriaRequest ObjRequest);

        public abstract SelectAllPromotionsResponse SelectAllStorePromotions(SelectAllPromotionsRequest RequestObj);

        public abstract SelectByPromotionIDStoreDetailsResponse SelectAllStorePromotionDetails(SelectByPromotionIDStoreDetailsRequest ObjRequest);

        public abstract SelectAllPromotionsResponse API_SelectALL(SelectAllPromotionsRequest requestData);
        public abstract SelectAllPromotionsResponse SelectPromotionWithPriorityRecords(SelectAllPromotionsRequest objRequest);
    }
}
