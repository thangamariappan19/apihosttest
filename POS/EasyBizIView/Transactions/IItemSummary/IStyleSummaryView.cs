using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IItemSummary
{
    public interface IStyleSummaryView : IBaseView
    {
        List<CountryMaster> CountryList { set; }
        byte[] StyleImage { set; }
        DataSet StyleSummaryDataSet { get; set; }
        string StyleCode { get; }
        int CountryID { get; set; }
        long StyleID { get; set; }
        StyleMaster StyleMasterData { get; set; }
    }
}
