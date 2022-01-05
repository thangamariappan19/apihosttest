using EasyBizFactory;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizResponse.Masters.CouponMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class CouponMasterBLL
    {
        public SaveCouponMasterResponse SaveCouponMaster(SaveCouponMasterRequest objRequest)
        {
            SaveCouponMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (SaveCouponMasterResponse)objBaseCouponMasterDAL.InsertRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SaveCouponMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllCouponMasterResponse SelectAllCouponMaster(SelectAllCouponMasterRequest objRequest)
        {
            SelectAllCouponMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (SelectAllCouponMasterResponse)objBaseCouponMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCouponMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDCouponMasterResponse SelectCouponMasterRecord(SelectByIDCouponMasterRequest objRequest)
        {
            SelectByIDCouponMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (SelectByIDCouponMasterResponse)objBaseCouponMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDCouponMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateCouponMasterResponse UpdateCouponMaster(UpdateCouponMasterRequest objRequest)
        {
            UpdateCouponMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (UpdateCouponMasterResponse)objBaseCouponMasterDAL.UpdateRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCouponMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteCouponMasterResponse DeleteCouponMaster(DeleteCouponMasterRequest objRequest)
        {
            DeleteCouponMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (DeleteCouponMasterResponse)objBaseCouponMasterDAL.DeleteRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new DeleteCouponMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Coupon Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectCouponMasterLookUpResponse SelectCouponMasterLookUp(SelectCouponMasterLookUpRequest objRequest)
        {
            SelectCouponMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (SelectCouponMasterLookUpResponse)objBaseCouponMasterDAL.SelectCouponMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCouponMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectCouponStoreDetailsResponse SelectCouponStoreDetails(SelectCouponStoreDetailsRequest objRequest)
        {
            SelectCouponStoreDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (SelectCouponStoreDetailsResponse)objBaseCouponMasterDAL.SelectCouponMasterStoreType(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCouponStoreDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }


        public SelectCouponCustomerDetailsResponse SelectCouponCustomerDetails(SelectCouponCustomerDetailsRequest objRequest)
        {
            SelectCouponCustomerDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (SelectCouponCustomerDetailsResponse)objBaseCouponMasterDAL.SelectCouponMasterCustomerType(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCouponCustomerDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectCouponProductCategoryDetailsResponse SelectCouponMasterProductTypeBLL(SelectCouponProductCategoryDetailsRequest objRequest)
        {
            SelectCouponProductCategoryDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (SelectCouponProductCategoryDetailsResponse)objBaseCouponMasterDAL.SelectCouponMasterProductType(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCouponProductCategoryDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectCouponCouponListDetailsResponse SelectCouponMasterList(SelectCouponCouponListDetailsRequest objRequest)
        {
            SelectCouponCouponListDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (SelectCouponCouponListDetailsResponse)objBaseCouponMasterDAL.SelectCouponMasterList(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCouponCouponListDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }



    }
}
