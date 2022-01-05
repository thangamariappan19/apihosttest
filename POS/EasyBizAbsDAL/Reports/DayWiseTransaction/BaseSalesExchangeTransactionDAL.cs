using EasyBizAbsDAL.Common;
using EasyBizRequest.Reports.DayWiseTransactionRequest;
using EasyBizResponse.Reports.DayWiseTransactionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Reports.DayWiseTransaction
{
    public abstract class BaseSalesExchangeTransactionDAL : BaseDAL
    {
        public abstract SalesExchangeTransactionResponse SelectAllSalesExchangeDetailList(SalesExchangeTransactionRequest RequestObj);
        public abstract SalesExchangeTransactionResponse SelectAllSalesExchangeTransactionReportList(SalesExchangeTransactionRequest RequestObj);
        public abstract SalesExchangeTransactionResponse SelectRecordByID(SalesExchangeTransactionRequest RequestObj);
    }
}
