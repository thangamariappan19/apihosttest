using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.State
{
   public interface IStateMasterView : IBaseView
    {
        int ID { get; set; }

        string StateCode { get; set; }

        string StateName { get; set; }

        string Country { get; set; }
        int CountryID { get; set; }

        string Remarks { get; set; }

        bool Active { get; set; }

        List<StateMaster> StateList { get; set; }
        List<CountryMaster> CountryLookUp { set; }
    }
}
