using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IBinLevelDetails
{
    public interface IBinLevelDetailsView : IBaseView
    {
        int ID { get; set; }
        int StoreID { get; set; }
        string StoreCode { get; set; }
        int LevelNo { get; set; }
        string LevelName { get; set; }
        Boolean EnableBin { get; set; }
        List<BinLevelMasterTypes> BinLevelMasterList { get; set; }
    }
}
