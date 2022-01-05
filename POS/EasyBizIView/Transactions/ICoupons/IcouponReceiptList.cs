using EasyBizDBTypes.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.ICoupons
{
    public interface IcouponReceiptList : IBaseView
    {
        List<CouponReceiptHeader> CouponReceiptHeaderList { get; set; }
    }
}
