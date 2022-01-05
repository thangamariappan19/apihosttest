using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizResponse.Masters.CouponMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseCouponMasterDAL : BaseDAL
    {
        public abstract SelectCouponMasterLookUpResponse SelectCouponMasterLookUp(SelectCouponMasterLookUpRequest RequestObj);

        public abstract SelectCouponStoreDetailsResponse SelectCouponMasterStoreType(SelectCouponStoreDetailsRequest RequestObj);

        public abstract SelectCouponCustomerDetailsResponse SelectCouponMasterCustomerType(SelectCouponCustomerDetailsRequest RequestObj);

        public abstract SelectCouponProductCategoryDetailsResponse SelectCouponMasterProductType(SelectCouponProductCategoryDetailsRequest RequestObj);

        public abstract SelectCouponCouponListDetailsResponse SelectCouponMasterList(SelectCouponCouponListDetailsRequest RequestObj);


    }
    
}
