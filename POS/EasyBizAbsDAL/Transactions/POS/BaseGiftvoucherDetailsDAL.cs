using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.GiftvoucherDetailsRequest;
using EasyBizResponse.Transactions.POS.GiftvoucherDetailsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.POS
{
   public abstract class BaseGiftvoucherDetailsDAL:BaseDAL
    {
       public abstract SelectGiftvoucherDetailByInvoiceNoResponse SelectGiftvoucherDetailByInvoiceNo(SelectGiftvoucherDetailByInvoiceNoRequest ObjReq);
    }
}
