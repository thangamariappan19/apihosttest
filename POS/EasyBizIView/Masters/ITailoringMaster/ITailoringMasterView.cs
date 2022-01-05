using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ITailoringMaster
{
    public interface ITailoringMasterView : IBaseView
    {
        int ID { get; set; }
        int CountryID { get; set; }
        int StoreID { get; set; }       
        String tailoringunitcode { get; set; }
        String tailoringunitName { get; set; }
        bool Active { get; set; }
        string CountryCode { get; }       
        string StoreCode { get; }       
        List<CountryMaster> CountryMasterLookUp { set; }

        List<StoreMaster> StoreMasterLookUp { set; }

        List<TailoringMasterTypes> TailoringMasterList { get; set; }
    }
}
