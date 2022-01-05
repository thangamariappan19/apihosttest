using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICouponMaster
{
    public interface ICouponMasterList:IBaseView
    {
        List<CouponMaster> CouponMasterList { get; set; }
    }
}
