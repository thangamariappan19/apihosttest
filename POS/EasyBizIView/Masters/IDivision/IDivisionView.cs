using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IDivision
{
    public interface IDivisionView: IBaseView
    {
        int ID { get; set; }

        string DivisionCode { get; set; }

        string DivisionName { get; set; }

        List<DivisionMaster> DivisionList { get; set; }

        string Remarks { get; set; }

        bool Active { get; set; }
    }
}
