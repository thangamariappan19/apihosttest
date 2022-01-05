using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Transactions.Coupons;
using EasyBizResponse.Masters.CouponMasterResponse;
using EasyBizResponse.Transactions.Coupons;
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

        public abstract SelectAllCouponMasterResponse API_SelectALL(SelectAllCouponMasterRequest requestData);


        #region "Redeem_Coupon"
        public abstract SelectCouponDataOnCouponCodeResponse SelectCouponDataOnCouponCode(SelectCouponDataOnCouponCodeRequest objRequest);

        public abstract SelectByIDCouponMasterResponse SelectCouponDataBasedOnCouponID(SelectByIDCouponMasterRequest RequestObj);
        public abstract UpdateCouponDetailsListResponse InsertCouponListDetails(UpdateCouponDetailsListRequest objRequest);
        public abstract UpdateCouponDetailsListResponse GetDeActiveCouponOnReturn(SelectCouponDataOnCouponCodeRequest objRequest);

        public abstract UpdateCouponDetailsListResponse UpdateCouponListDetails(UpdateCouponDetailsListRequest objRequest);
        #endregion
    }

}
