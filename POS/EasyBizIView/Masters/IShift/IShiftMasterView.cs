using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IShift
{
    public interface IShiftMasterView: IBaseView
    {
        int ID { get; set; }

        long CountryID { get; set; }

        string CountryName { get; set; }

        List<ShiftMaster> ShiftList { get; set; }

        List<CountryMaster> CountryLookUp { set; }
    }
}
