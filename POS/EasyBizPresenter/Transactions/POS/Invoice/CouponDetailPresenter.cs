using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Transactions.IPOS.IPaymentView;
using EasyBizRequest.Transactions.POS.CouponDetailRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS.Invoice
{
   public class CouponDetailPresenter
    {
       CouponDetailView _CouponDetailView;
       CouponDetailBLL _CouponDetailBLL = new CouponDetailBLL();
       public CouponDetailPresenter(CouponDetailView ViewObj)
       {
           _CouponDetailView = ViewObj;
       }

       public void SaveCouponDetail()
       {
           var RequestData = new SaveCouponDetailRequest();
           RequestData.CouponDetailDataforPayment = new CouponDetail();
           RequestData.CouponDetailDataforPayment.ID = _CouponDetailView.ID;
           RequestData.CouponDetailDataforPayment.ApplicationDate = DateTime.Now;
           //RequestData.CouponDetailDataforPayment.InvoiceHeaderID = _CouponDetailView.InvoiceHeaderID;
           RequestData.CouponDetailDataforPayment.InvoiceNumber = _CouponDetailView.InvoiceNumber;
           RequestData.CouponDetailDataforPayment.PayMentMode = _CouponDetailView.PayMentMode;
           RequestData.CouponDetailDataforPayment.CouponCode = _CouponDetailView.CouponCode;
           RequestData.CouponDetailDataforPayment.StoreGroupCode = _CouponDetailView.StoreGroupCode;
           RequestData.CouponDetailDataforPayment.Customer = _CouponDetailView.Customer;
           RequestData.CouponDetailDataforPayment.ValidityStartDate = _CouponDetailView.ValidityStartDate;
           RequestData.CouponDetailDataforPayment.ValidityEndDate = _CouponDetailView.ValidityEndDate;
           RequestData.CouponDetailDataforPayment.DiscountType = _CouponDetailView.DiscountType;
           RequestData.CouponDetailDataforPayment.Discountvalue = _CouponDetailView.Discountvalue;
           RequestData.CouponDetailDataforPayment.CreateBy = _CouponDetailView.UserID;
           RequestData.CouponDetailDataforPayment.CreateOn = DateTime.Now;
           RequestData.CouponDetailDataforPayment.SCN = _CouponDetailView.SCN;

           var ResponseData = _CouponDetailBLL.SavePaymentCouponDetail(RequestData);
           _CouponDetailView.Message = ResponseData.DisplayMessage;
       }

       public void SelectCouponDetailByInvoiceNo()
       {
           var RequestData = new SelectCouponDetailByInvoiceNoRequest();
           RequestData.InvoiceNumber = _CouponDetailView.InvoiceNumber;
           var ResponseData = _CouponDetailBLL.SelectCouponDetailByInvoiceNo(RequestData);

           if(ResponseData.CouponDetailData != null)
           {
               _CouponDetailView.ID = ResponseData.CouponDetailData.ID;
               _CouponDetailView.InvoiceNumber = ResponseData.CouponDetailData.InvoiceNumber;
               //_CouponDetailView.InvoiceHeaderID = ResponseData.CouponDetailData.InvoiceHeaderID;
               _CouponDetailView.CouponCode = ResponseData.CouponDetailData.CouponCode;
               _CouponDetailView.StoreGroupCode = ResponseData.CouponDetailData.StoreGroupCode;
               _CouponDetailView.Customer = ResponseData.CouponDetailData.Customer;
               _CouponDetailView.ValidityStartDate = ResponseData.CouponDetailData.ValidityStartDate;
               _CouponDetailView.ValidityEndDate = ResponseData.CouponDetailData.ValidityEndDate;
               _CouponDetailView.DiscountType = ResponseData.CouponDetailData.DiscountType;
               _CouponDetailView.Discountvalue = ResponseData.CouponDetailData.Discountvalue;
           }
           _CouponDetailView.ProcessStatus = Enums.OpStatusCode.Success;
       }

       public void GetCouponMasterByCustomerList()
       {
           var RequestData = new GetCouponMasterByCustomerListRequest();
           RequestData.CouponCode = _CouponDetailView.CouponCode;
           var ResponseData = _CouponDetailBLL.GetCouponMasterByCustomerList(RequestData);

           if (ResponseData.CouponMasterByCustomerData != null)
           {
               _CouponDetailView.Customer = ResponseData.CouponMasterByCustomerData.Customer;
               _CouponDetailView.StoreGroupCode = ResponseData.CouponMasterByCustomerData.StoreGroupCode;
               _CouponDetailView.ValidityStartDate = ResponseData.CouponMasterByCustomerData.ValidityStartDate;
               _CouponDetailView.ValidityEndDate = ResponseData.CouponMasterByCustomerData.ValidityEndDate;
               _CouponDetailView.DiscountType = ResponseData.CouponMasterByCustomerData.DiscountType;
               _CouponDetailView.Discountvalue = ResponseData.CouponMasterByCustomerData.Discountvalue;
           }
           //else 
           //{
           //    //_CouponDetailView.Customer = "";
           //    //_CouponDetailView.StoreGroupCode = "";
           //    //_CouponDetailView.ValidityStartDate = Convert.ToDateTime(null);
           //    //_CouponDetailView.ValidityEndDate = Convert.ToDateTime(null);
           //    //_CouponDetailView.DiscountType = "";
           //    //_CouponDetailView.Discountvalue = Convert.ToInt32(null);
           //    _CouponDetailView.Message = "This Coupon does not have Correct Customer Deails";
           //}
       }

    }
}
