using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IPrevileges
{
    public interface IPrevilegesView : IBaseView
    {
        List<POSScreenTypes> POSScreenList { get; set; }

        List<RoleMaster> RoleListLookup { set; }

        List<UserPrivilagesTypes> UserPrivilagesTypesList { get; set; }

        long RoleId { get; set; }
        string ScreenName { get; set; }
    }
}
