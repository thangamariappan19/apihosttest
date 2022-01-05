using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.ICoupons
{
    public interface ICouponTransferView : IBaseView
    {
        int ID { get; set; }
        List<CountryMaster> CountryLookUp { set; }
        int FromCountryID { get; set; }
        List<StoreMaster> StoreMasterLookUp { set; }
        int ToStoreID { get; set; }
        int CouponID { get; set; }
        UsersSettings UserInformation { get; }
        string Fromloaction { get; set; }
        List<CouponMaster> CouponLookUp { set; }
        string CouponCode { get; }
        string FromCountryCode { get; }
        string ToStoreCode { get; }
        string FromSerialNum { get; set; }
        string ToSerialNum { get; set; }
        bool Active { get; set; }

        List<CouponReceiptDetails> CouponReceiptDetailsRecord { get; set; }
        List<CouponTransaction> CouponTransactionList { get; set; }
        
    }
}
