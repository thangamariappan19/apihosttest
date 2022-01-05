using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ITill
{
    public interface ITillSettingsView : IBaseView
    {

        int ID { get; set; }
        int StoreID { get; set; }
        int CountryID { get; set; }
        int PosID { get; set; }
        int UserTeamID { get; set; }
        Decimal FloatingAmount { get; set; }
        bool CountRequired { get; set; }
        int CountType { get; set; }
        bool TillCountOnAssign { get; set; }
        bool TillCountOnClose { get; set; }
        bool TillCountOnFinalize { get; set; }

        string Remarks { get; set; }
        bool Active { get; set; }
        List<StoreMaster> StoreMasterLookUp {set; }
        List<CountryMaster> CountryMasterLookUp { set; }
        List<PosMaster> PosMasterLookUp { set; }
        List<RoleMaster> RoleMasterLookUp {set; }

        List<CountTypeMaster> CountTypeMasterLookUp { set; }

    }
}
