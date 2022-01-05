using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
    public interface IZReport : IBaseView
    {
        UsersSettings UserInformation { get; }
        List<ZReport> ZReportList { get; set; }
        List<ZSubReport> ZReportList1 { get; set; }
        List<Zreport2> ZReportList2 { get; set; }
        DateTime BusinessDate { get; set; }
        int StoreID { get; set; }
        StoreMaster StoreMasterRecord { get; set; }
    }
}
