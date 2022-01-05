using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.CouponReceipt;
using EasyBizResponse.Transactions.CouponReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.CouponReceipt
{
    public abstract class BaseCouponReceiptDAL : BaseDAL
    {
        public abstract SelectByCouponReceiptDetailsResponse SelectByCouponReceiptDetails(SelectByCouponReceiptDetailsRequest ObjRequest);
        public abstract GetSerialNumberResponse SelectByCouponReceiptDetails(GetSerialNumberRequest ObjRequest);
        public abstract SelectAllCouponReceiptResponse API_SelectALLCouponReceipt(SelectAllCouponReceiptRequest RequestObj);
        public abstract GetSerialNumberResponse API_GetSerialNumberDetails(GetSerialNumberRequest ObjRequest);
    }
}
