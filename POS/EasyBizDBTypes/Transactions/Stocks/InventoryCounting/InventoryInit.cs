using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Stocks.InventoryCounting
{
    [DataContract]
    [Serializable]
    public class InventoryInit : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public string DocumentNo { get; set; }

        [DataMember]
        public DateTime DocumentDate { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public bool PostingDone { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int ApprovedBy { get; set; }

        [DataMember]
        public Nullable<DateTime> PostingDate { get; set; }

        [DataMember]
        public List<InventorySysCount> InventorySysCountList { get; set; }
    }

    [DataContract]
    [Serializable]
    public class InventorySysCount : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int InventoryInitID { get; set; }

        [DataMember]
        public string SKUCode { get; set; }

        [DataMember]
        public string SKUName { get; set; }

        [DataMember]
        public string BarCode { get; set; }

        [DataMember]
        public string SupplierBarCode { get; set; }

        [DataMember]
        public string StyleCode { get; set; }

        [DataMember]
        public string StyleName { get; set; }

        [DataMember]
        public string BrandCode { get; set; }

        [DataMember]
        public string ColorCode { get; set; }

        [DataMember]
        public string SizeCode { get; set; }

        [DataMember]
        public int StockQty { get; set; }

        [DataMember]
        public decimal RRPPrice { get; set; }

        [DataMember]
        public decimal SalesPrice { get; set; }
    }
    [DataContract]
    [Serializable]
    public class InventoryManualCount : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public int InventoryInitID { get; set; }

        [DataMember]
        public string DocumentNo { get; set; }

        [DataMember]
        public DateTime DocumentDate { get; set; }

        [DataMember]
        public string CountingType { get; set; }

        [DataMember]
        public List<InventoryManualCountDetail> InventoryManualCountDetailList { get; set; }

    }
    [DataContract]
    [Serializable]
    public class InventoryManualCountDetail
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int InventoryManualCountID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public int LocationID { get; set; }

        [DataMember]
        public string SheetName { get; set; }

        [DataMember]
        public string BarCode { get; set; }

        [DataMember]
        public string SKUCode { get; set; }

        [DataMember]
        public string StyleCode { get; set; }

        [DataMember]
        public int StockQty { get; set; }

        //[DataMember]
        //public string SystemStock { get; set; }

        //[DataMember]
        //public int SystemStockQty { get; set; }

        //[DataMember]
        //public decimal RRPPrice { get; set; }

        //[DataMember]
        //public decimal SalesPrice { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ExcelSKU
    {
        [DataMember]
        public string BarCode { get; set; }

        [DataMember]
        public int StockQty { get; set; }

        [DataMember]
        public string SKUCode { get; set; }
        //[DataMember]
        //public string Remarks { get; set; }
    }

    [DataContract]
    [Serializable]
    public class StockCount
    {
        public string StoreName { get; set; }
        public string Department { get; set; }
        public string SKUCode { get; set; }
        public string SKUName { get; set; }
        public string StyleCode { get; set; }
        public string StyleDescription { get; set; }
        public int SystemQty { get; set; }
        public int PhysicalQty { get; set; }
        public int DiffQty { get; set; }

        public decimal RRPPrice { get; set; }
        public decimal SalesPrice { get; set; }

        public decimal PhysicalRRPPriceTotal { get; set; }
        public decimal PhysicalSalesPriceTotal { get; set; }

        public decimal SystemRRPPriceTotal { get; set; }
        public decimal SystemSalesPriceTotal { get; set; }

        public decimal TotalRRPDifferencePrice { get; set; }
        public decimal TotalSalesDifferencePrice { get; set; }
    }
}
