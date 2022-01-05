using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.PaymentDetails;
using EasyBizRequest.Transactions.POSOperations;
using EasyBizResponse.Transactions.PaymentDetails.CashInCashOut;
using EasyBizResponse.Transactions.POSOperations.CashInCashOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.PaymentDetails
{
    public abstract class BaseCashInCashOutDAL : BaseDAL
    {
        public abstract SelectCashInCashOutDetailsResponse SelectCashInCashOutDetails(SelectCashInCashOutDetailsRequest ObjRequest);
        public abstract SelectAllCashInCashoutReportResponse SelectCashInCashOutReportDetails(SelectAllCashInCashoutReportRequest ObjRequest);
        public abstract SelectAllCashInCashOutDateWiseReponse SelectCashInCashOutRecord(SelectAllCashInCashOutDateWiseRequest ObjRequest);
    }
}
