using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ISubSeason
{
    public interface ISubSeasonMasterView : IBaseView
    {
        int ID { get; set; }
        long SeasonID { get; set; }
        string SeasonName { get; set; }
        List<SubSeasonMaster> SubSeasonList { get; set; }
        List<SeasonMaster> SeasonLookUp { set; }
    }
}
