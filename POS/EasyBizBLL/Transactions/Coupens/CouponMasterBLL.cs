using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Transactions.Coupons;
using EasyBizResponse.Masters.CouponMasterResponse;
using EasyBizResponse.Transactions.Coupons;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Coupens
{
    public class CouponMasterBLL
    {
        public SaveCouponMasterResponse SaveCouponMaster(SaveCouponMasterRequest objRequest)
        {
            SaveCouponMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCouponMaster = new CouponMaster();
                    objCouponMaster = (CouponMaster)objRequest.RequestDynamicData;
                    objRequest.CouponMasterData = objCouponMaster;
                    objRequest.StoreCommonUtilData = objCouponMaster.StoreCommonUtilData;
                    objRequest.CustomerCommonUtilData = objCouponMaster.CustomerCommonUtilData;
                    objRequest.TotalMasterCommonUtilData = objCouponMaster.TotalMasterCommonUtilData;
                }
                objResponse = (SaveCouponMasterResponse)objBaseCouponMasterDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.COUPON;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;
                //    //EasyBizBLL.Transactions.Coupens.CouponMasterBLL
                //    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CouponMasterBLL", "SaveCouponMaster");
                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Coupens.CouponMasterBLL", "SaveCouponMaster");
                //}
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
                if(objRequest.ID == 0)
                {
                    int doc_id;
                    int.TryParse(objRequest.DocumentIDs, out doc_id);
                    objRequest.ID = doc_id;
                }
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
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (UpdateCouponMasterResponse)objBaseCouponMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COUPON;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CouponMasterBLL", "UpdateCouponMaster");
                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Coupens.CouponMasterBLL", "UpdateCouponMaster");
                }
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
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (DeleteCouponMasterResponse)objBaseCouponMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COUPON;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CouponMasterBLL", "DeleteCouponMaster");
                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Coupens.CouponMasterBLL", "DeleteCouponMaster");
                }
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

        public SelectAllCouponMasterResponse API_SelectAllCouponRecords(SelectAllCouponMasterRequest objRequest)
        {
            SelectAllCouponMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetBaseCouponMasterDAL();
                objResponse = (SelectAllCouponMasterResponse)objBaseCouponMasterDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCouponMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        #region "Redeem_Coupon"

        public SelectCouponDataOnCouponCodeResponse SelectCouponDataOnCouponCode(SelectCouponDataOnCouponCodeRequest objRequest)
        {
            SelectCouponDataOnCouponCodeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (SelectCouponDataOnCouponCodeResponse)objBaseCouponMasterDAL.SelectCouponDataOnCouponCode(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCouponDataOnCouponCodeResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateCouponDetailsListResponse GetDeActiveCouponOnReturn(SelectCouponDataOnCouponCodeRequest objRequest)
        {
            UpdateCouponDetailsListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (UpdateCouponDetailsListResponse)objBaseCouponMasterDAL.GetDeActiveCouponOnReturn(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COUPON;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCouponDetailsListResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon List Details");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateCouponDetailsListResponse UpdateCouponListDetails(UpdateCouponDetailsListRequest objRequest)
        {
            UpdateCouponDetailsListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (UpdateCouponDetailsListResponse)objBaseCouponMasterDAL.UpdateCouponListDetails(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COUPON;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CouponMasterBLL", "UpdateCouponMaster");
                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Coupens.CouponMasterBLL", "UpdateCouponListDetails");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCouponDetailsListResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon List Details");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateCouponDetailsListResponse InsertCouponListDetails(UpdateCouponDetailsListRequest objRequest)
        {
            UpdateCouponDetailsListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCouponMasterDAL = objFactory.GetDALRepository().GetCouponMasterDAL();
                objResponse = (UpdateCouponDetailsListResponse)objBaseCouponMasterDAL.InsertCouponListDetails(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COUPON;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CouponMasterBLL", "UpdateCouponMaster");
                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Coupens.CouponMasterBLL", "UpdateCouponListDetails");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCouponDetailsListResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon List Details");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        #endregion

    }
}
