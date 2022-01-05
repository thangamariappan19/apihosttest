using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IYear
{
    public interface IYearView : IBaseView
    {
        int ID { get; set; }
        string YearCode { get; set; }
        string Year { get; set; }

        List<YearMaster> YearList { get; set; }

        string Remarks { get; set; }

        bool Active { get; set; }
    }
}
