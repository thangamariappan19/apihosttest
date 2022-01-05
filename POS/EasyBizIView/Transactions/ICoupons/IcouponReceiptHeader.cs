using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.ICoupons
{
    public interface IcouponReceiptHeader : IBaseView
    {
        int ID { get; set; }       
        List<CouponMaster> CouponLookUp { set; }
        string FromSerialNum { get; set; }
        string ToSerialNum { get; set; }
        bool Active { get; set; }
        int CouponID { get; set; }
        string CouponCode { get; }
        string CurrentLocation { get; set; }
        UsersSettings UserInformation { get; }
        List<CouponReceiptDetails> CouponReceiptDetailsList { get; set; }
        List<CouponTransaction> CouponTransactionList { get; set; }
    }
}
