using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IShift
{
    public interface IShiftMasterCollectionView : IBaseView
    {
        List<ShiftMaster> ShiftList { get; set; }

        List<CountryMaster> CountryList { get; set; }
    }
}
