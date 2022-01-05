using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS
{
    public interface ISalesTiles : IBaseView
    {
        long CountryID { get; set; }
        List<ShiftMaster> ShiftList { get; set; }
        ShiftMaster ShiftRecodrd { get; set; }
        int shiftID { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
