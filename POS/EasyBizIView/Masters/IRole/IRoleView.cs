using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters
{
  public  interface IRoleView: IBaseView
    {
      int ID { get; set; }
      string RoleCode { get; set; }
      string RoleName { get; set; }
      List<RoleMaster> RoleMasterList { get; set; }

      string Remarks { get; set; }

      bool Active { get; set; }
    }
}
