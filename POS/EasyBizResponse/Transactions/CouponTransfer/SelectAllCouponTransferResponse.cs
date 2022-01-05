using EasyBizDBTypes.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.CouponTransfer
{
    [Serializable]
    [DataContract]
    public class SelectAllCouponTransferResponse : BaseResponseType
    {
        public List<CouponTransferMaster> CouponTransferMasterList { get; set; }
    }
}
