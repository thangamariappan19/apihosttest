using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IStyleStatusMaster
{
    public interface IStyleStatusViewList:IBaseView
    {
        List<StyleStatusMasterType> StyleStatusMasterTypeList { get; set; }
    }
}
