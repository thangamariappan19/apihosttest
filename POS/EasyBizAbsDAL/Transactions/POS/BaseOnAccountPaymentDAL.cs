using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.OnAccountPaymentRequest;
using EasyBizResponse.Transactions.POS.OnAccountPaymentResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.POS
{
    public abstract class BaseOnAccountPaymentDAL : BaseDAL
    {
        public abstract GetOnAccountPaymentPendingResponse GetOnAccountPaymentPendingList(GetOnAccountPaymentPendingRequest ObjRequest);
        public abstract GetOnAccountPaymentDetailsResponse GetOnAccountDetails(GetOnAccountPaymentDetailsRequest objRequest);
        public abstract SelectAllOnAccountPaymentResponse API_GetALLOnAccountDetails(SelectAllOnAccountPaymentRequest objRequest);
    }
}
