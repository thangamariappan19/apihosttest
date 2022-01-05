using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICityMaster
{
    public interface ICityMasterView : IBaseView
    {
        int ID { get; set; }

        string CityCode { get; set; }

        string CityName { get; set; }

        string State { get; set; }
        int StateID { get; set; }

        string Remarks { get; set; }

        bool Active { get; set; }

        List<StateMaster> StateMasterLookup { set; }

    }
}
