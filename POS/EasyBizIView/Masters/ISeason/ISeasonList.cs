using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ISeason
{
   public interface ISeasonList:IBaseView
    {
       List<SeasonMaster> SeasonMasterList { get; set; }
    }
}
