using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IDropMaster
{
    public interface  IDropMasterViewList:IBaseView
    {
        List<DropMasterTypes> DropMasterTypesList { get; set; }
    }
}
