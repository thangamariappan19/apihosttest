using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.ILabelPrintingMaster.Masters
{
    public interface IlabelprinitngView : IBaseView
    {
        int StoreID { get; set; }
        string Department { get; set; }
        string ProductCode { get; set; }
        string ColorCode { get; set; }
        string SizeCode { get; set; }
        int NoOfLabel { get; set; }
        List<SKUMasterTypes> SKUMasterColorList { get; set; }
        List<SKUMasterTypes> SKUMasterSizeList { get; set; }
        int SelectedStoreId { get; set; }
        StoreMaster StoreMasterRecord { get; set; }
        TransactionLog QuantityBySKURecord { get; set; }        

        Boolean PrintPrice { get; set; }      
        StylePricing PriceRecord { get; set; }

        StylePricing Price { get; set; }
        CountryMaster CurrencyRecord { get; set; }

        // DataTable LabelPrintingReportTable { set; }
        //List<TransactionLog> QuantityBySKUList { get; set; }        
        //string PriceListIDs { get; set; }
        //List<PriceListType> PriceListType { get; set; }

        SKUMasterTypes BarCodeRecord { get; set; }

        StyleMaster StyleRecord { get; set; }

        List<StoreMaster> StoreList { set; }
        List<TransactionLog> QuantityBySKUList { get; set; }
        Boolean ChkSupplierBarcode { get; set; }
        List<PriceListType> PriceListTypeLookUP { get; set; }
        int PriceListID { get; set; }
    }
}
