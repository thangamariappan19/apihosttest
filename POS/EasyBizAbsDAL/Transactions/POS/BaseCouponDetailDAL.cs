using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.CouponDetailRequest;
using EasyBizResponse.Transactions.POS.CouponDetailResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.POS
{
  public abstract  class BaseCouponDetailDAL:BaseDAL
    {
        public abstract SelectCouponDetailByInvoiceNoResponse SelectCouponDetailByInvoiceNo(SelectCouponDetailByInvoiceNoRequest ReqObj);
        public abstract GetCouponMasterByCustomerListResponse GetCouponMasterByCustomerList(GetCouponMasterByCustomerListRequest ReqObj);
    }
}
