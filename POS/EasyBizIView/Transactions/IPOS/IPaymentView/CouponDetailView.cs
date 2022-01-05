using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.IPaymentView
{
   public interface CouponDetailView:IBaseView
    {
       int ID { get; set; }
       string InvoiceNumber { get; set; }
       int InvoiceHeaderID { get; set; }
       string CouponCode { get; set; }
       string Customer { get; set; }
       string StoreGroupCode { get; set; }
       string DiscountType { get; set; }
       Decimal Discountvalue { get; set; }
       DateTime ValidityStartDate { get; set; }
       DateTime ValidityEndDate { get; set; }
       string PayMentMode { get; set; }
    }
}
