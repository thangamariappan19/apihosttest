using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IOpeningStock
{
    public interface IOpeningStockView : IBaseView
    {
        int ID { get; set; }
        string DocumentNo { get; set; }
        DateTime DocumentDate { get; set; }
        int TotalQuantity { get; set; }       
        List<OpeningStockDetails> OpeningStockDetailsList { get; set; }
        List<SKUMasterTypes> SKUMasterList { get; set; }
        int CountryID { get; }
        int StateID { get; }
        int StoreID { get; }
        int DocumentTypeID { get; }      
        string SKUSearchString { get; }
        List<SKUMasterTypes> SKUMasterTypesList { get; set; }     
        StoreMaster StoreMasterRecord { get; set; }
        string Remarks { get; set; }
        string StoreCode { get; }
        int HeaderID { get; set; }
        List<TransactionLog> TransactionLogList { get; set; }
        int OpeningHeaderID { get; set; }  
    }
}
