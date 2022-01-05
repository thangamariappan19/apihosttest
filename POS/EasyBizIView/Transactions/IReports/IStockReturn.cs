using EasyBizDBTypes.Transactions.StockReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IReports
{
   public interface IStockReturn:IBaseView
    {
        List<StockReturnHeader> StockReturnHeaderList { set; }      
        DateTime BusinessDate { get; }
        int StoreID { get; }
        int ID { get; set; } 
        string StoreCode { get;}
    }
   public interface IStockReturnDetailsView
   {
       int ID { get; set; }
       List<StockReturnDetails> StockReturnDetailsList { set; }
   }
}
