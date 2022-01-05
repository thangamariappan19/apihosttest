using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Promotions.WNPromotionRequest;
using EasyBizResponse.Transactions.Promotions.WNPromotionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.Promotions
{
    public abstract class BaseWNPromotionDAL : BaseDAL
    {
        public abstract SelectWNPromotionDetailsResponse SelectWNPromotionDetailsList(SelectWNPromotionDetailsRequest RequestObj);

        public abstract SelectWNPromotionLookUpResponse SelectWNPromotionLookUp(SelectWNPromotionLookUpRequest objRequest);

        public abstract SelectWNPromotionDetailsResponse GetWNPrice(SelectWNPromotionDetailsRequest objRequest);
        public abstract SelectWNPromotionLookUpResponse API_SelectALL(SelectWNPromotionLookUpRequest requestData);
        public abstract SelectAllWNPromotionResponse API_SelectALLWN(SelectAllWNPromotionRequest requestData);
    }
}
