using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest;
using EasyBizResponse.Transactions.POS.InvoiceCashDetailsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.POS
{
   public abstract class BaseInvoiceCashDetailsDAL:BaseDAL
    {
       public abstract SelectByInvoiceNoCashDetailsResponse SelectByInvoiceNoCashDetails(SelectByInvoiceNoCashDetailsRequest ReqObj);
    }
}
