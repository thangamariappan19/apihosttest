using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters
{
   public interface IRoleList:IBaseView
    {
       List<RoleMaster> IRoleMasterList { get; set; }
    }
}
