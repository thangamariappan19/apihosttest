using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.CouponTransfer;
using EasyBizResponse.Transactions.CouponTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.CouponTransfer
{
    public abstract class BaseCouponTransferDAL : BaseDAL
    {
        public abstract SelectCouponTransferLookUpResponse SelectCouponMasterLookUp(SelectCouponTransferLookUpRequest RequestObj);
        public abstract SelectByCouponTransferDetailsResponse SelectByCouponTransferDetails(SelectByCouponTransferDetailsRequest RequestObj);
        public abstract SaveCouponTransactionResponse SaveCouponTransactionDetails(SaveCouponTransactionRequest RequestObj);
        public abstract SelectAllCouponTransferResponse API_SelectALL(SelectAllCouponTransferRequest RequestObj);
    }
}
