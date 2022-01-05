using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.SalesExchangeRequest;
using EasyBizResponse.Transactions.POS.SalesExchangeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.POS
{
   public abstract class BaseSalesExchangeDAL:BaseDAL
    {
       public abstract SelectAllSalesExchangeDetailResponse SelectAllSalesExchangeDetailList(SelectAllSalesExchangeDetailRequest RequestObj);
       public abstract SelectExchangeByInvoiceNumResponse GetExchangeReceipt(SelectExchangeByInvoiceNumRequest RequestObj);
        public abstract GetExchangeOrSalesResponse GetSalesOrExchangeList(SelectAllInvoiceRequest objRequest);
       
    }
}
