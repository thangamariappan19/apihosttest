using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizResponse.Transactions.POS.CardDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.POS
{
   public abstract class BaseCardDetailsDAL : BaseDAL
    {
       public abstract SelectByIDCardDetailsResponse SelectByIDInvoiceDetails(SelectByIDCardDetailsRequest ObjRequest);
       public abstract SelectCreditCardDetailsByInvoiceNoResponse SelectCreditCardDetailsByInvoiceNo(SelectCreditCardDetailsByInvoiceNoRequest ObjRequest);

        //public abstract SaveInvoiceResponse SavePaymentProcesor(SaveInvoiceRequest ObjRequest);
      
    }
}
