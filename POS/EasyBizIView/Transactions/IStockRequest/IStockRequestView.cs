using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizDBTypes.Transactions.Stocks.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IStockRequest
{
    public interface IStockRequestView : IBaseView
    {
        int ID { get; set; }
        string DocumentNo { get; set; }       
        DateTime DocumentDate { get; set; }
        int TotalQuantity { get; set; }
        string Status { get; set; }
        List<StockRequestDetails> StockRequestDetailsList { get; set; }
        List<SKUMasterTypes> SKUMasterList { get; set; }
        int CountryID { get; }
        int StateID { get; }
        int StoreID { get; }       
        int DocumentTypeID { get; }     
        List<WarehouseMaster> WarehouseMasterLookUp { set; }      
        string SKUSearchString { get; }
        List<SKUMasterTypes> SKUMasterTypesList { get; set; }
        int WareHouseID { get; set; }
        StoreMaster StoreMasterRecord { get; set; }
        string Remarks { get; set; }
        string StoreCode { get; }
        string ToWhareHouseCodeCode { get; }
        List<int_stockrequestTypes> int_stockrequestTypesList { get; set; }
        // List<StoreMaster> StoreMasterLookUp { set; }
        // List<StoreMaster> ToStoreMasterLookUp { set; }
        // int FromStoreID { get; set; }
        //  int ToStoreID { get; set; }
    }
}
