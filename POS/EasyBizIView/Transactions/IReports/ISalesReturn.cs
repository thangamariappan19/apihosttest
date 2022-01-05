using EasyBizDBTypes.Transactions.POS.SalesReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IReports
{
    public interface ISalesReturn : IBaseView
    {
        List<SalesReturnHeader> SalesReturnList { set; }      
        DateTime BusinessDate { get; }
        int StoreID { get; }
        int ID { get; set; } 
    }
    public interface ISalesReturnDetailsView
    {
        int ID { get; set; }
        List<SalesReturnDetail> SalesReturnDetailList { set; }
    }
}
