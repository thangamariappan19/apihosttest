using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ITill
{
   public interface ITillSettingsList:IBaseView
    {
        List<TillSettings> TillSettingsList { get; set; }
    }
}
