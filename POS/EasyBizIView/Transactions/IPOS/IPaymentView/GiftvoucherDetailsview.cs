using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.IPaymentView
{
   public interface GiftvoucherDetailsview:IBaseView
    {
       int ID { get; set; }
       string InvoiceNumber { get; set; }
       int InvoiceHeaderID { get; set; }
       string GiftvoucherCode { get; set; }
       Decimal Amount { get; set; }
       string PayMentMode { get; set; }
    }
}
