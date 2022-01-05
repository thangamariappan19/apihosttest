using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ISubSeason
{
    public interface ISubSeasonMasterCollectionView : IBaseView
    {
        List<SubSeasonMaster> SubSeasonList { get; set; }

        List<SeasonMaster> SeasonList { get; set; }
    }
}
