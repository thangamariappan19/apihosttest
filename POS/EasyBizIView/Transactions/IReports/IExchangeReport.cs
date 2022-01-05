using EasyBizDBTypes.Transactions.POS.SalesExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IReports
{
    public interface IExchangeReport : IBaseView
    {
        List<SalesExchangeHeader> SalesExchangeHeaderList { set; }       
        DateTime BusinessDate { get; }
        int StoreID { get; }  
    }

    public interface IExchangeDetailsView
    {
        int ID { get; set; }
        List<SalesExchangeDetail> SalesExchangeDetailList { set; }
    }
}
