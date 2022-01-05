using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.CouponDetailRequest;
using EasyBizResponse.Transactions.POS.CouponDetailResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
   public class CouponDetailBLL
    {
       public SaveCouponDetailResponse SavePaymentCouponDetail(SaveCouponDetailRequest objRequest)
       {
           SaveCouponDetailResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var objBaseCouponDetailDAL = objFactory.GetDALRepository().GetBaseCouponDetailDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var ObjCouponDetail = new CouponDetail();
                   ObjCouponDetail = (CouponDetail)objRequest.RequestDynamicData;
                   objRequest.CouponDetailDataforPayment = ObjCouponDetail;
               }
               objResponse = (SaveCouponDetailResponse)objBaseCouponDetailDAL.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.COUPON;
                   objRequest.ProcessMode = Enums.ProcessMode.New;

                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.CouponDetailBLL", "SavePaymentCouponDetail");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveCouponDetailResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectCouponDetailByInvoiceNoResponse SelectCouponDetailByInvoiceNo(SelectCouponDetailByInvoiceNoRequest objRequest)
       {
           SelectCouponDetailByInvoiceNoResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseCouponDetailDAL = objFactory.GetDALRepository().GetBaseCouponDetailDAL();
               objResponse = (SelectCouponDetailByInvoiceNoResponse)objBaseCouponDetailDAL.SelectCouponDetailByInvoiceNo(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectCouponDetailByInvoiceNoResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public GetCouponMasterByCustomerListResponse GetCouponMasterByCustomerList(GetCouponMasterByCustomerListRequest objRequest)
       {
           GetCouponMasterByCustomerListResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseCouponDetailDAL = objFactory.GetDALRepository().GetBaseCouponDetailDAL();
               objResponse = (GetCouponMasterByCustomerListResponse)objBaseCouponDetailDAL.GetCouponMasterByCustomerList(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new GetCouponMasterByCustomerListResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

    }
}
