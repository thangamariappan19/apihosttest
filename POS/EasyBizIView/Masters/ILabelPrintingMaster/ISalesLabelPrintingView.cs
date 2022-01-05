using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ILabelPrintingMaster
{
   public interface ISalesLabelPrintingView : IBaseView
    {
        string SalesName { get; set; }
        int StoreID { get; set; }
        string Department { get; set; }
        string ProductCode { get; set; }
        string Currrency { get; set; }
        int NoOfLabel { get; set; }     
        int SelectedStoreId { get; set; }
        StoreMaster StoreMasterRecord { get; set; }
        //TransactionLog QuantityBySKURecord { get; set; }
        Boolean PrintProductCode { get; set; }
        Boolean PrintPriceWAS { get; set; }
        //StylePricing PriceRecord { get; set; }

        //StylePricing Price { get; set; }

        // DataTable LabelPrintingReportTable { set; }
        //List<TransactionLog> QuantityBySKUList { get; set; }        
        //string PriceListIDs { get; set; }
        //List<PriceListType> PriceListType { get; set; }

        CountryMaster CurrencyRecord { get; set; }
        //decimal WasPrice { get; set; }
        //decimal NowPrice { get; set; }

        WNPromotionDetails WNPriceRecord { get; set; }

        List<StoreMaster> StoreList { set; }
    }
}
