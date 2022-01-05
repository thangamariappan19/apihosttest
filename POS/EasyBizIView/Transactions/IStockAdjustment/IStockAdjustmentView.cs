using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.StockStaging;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IStockAdjustment
{
    public interface IStockAdjustmentView : IBaseView
    {
        int ID { get; set; }
        int StyleID { get; set; }
        string DocumentNumber { get; set; }
        DateTime DocumentDate { get; set; }
        //List<StyleMaster> StyleMasterLookUp { set; }
        List<ScaleDetailMaster> StyleWithScaleDetailList { get; set; }
        List<ColorMaster> StyleWithColorList { get; set; }
        StockAdjustmentHeader StockAdjustmentRecord { get; set; }     

        List<TransactionLog> StockList { get; set; }

        List<TransactionLog> TransactionLogList { get; set; }
        string StyleCode { get; }
        UsersSettings UserInformation { get; }


        string HeaderID { get; set; }

        int CountryID { get; }
        int StateID { get; }
        int StoreID { get; }
        string StoreCode { get; }
    }
}
