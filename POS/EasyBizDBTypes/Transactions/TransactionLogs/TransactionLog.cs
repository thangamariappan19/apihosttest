using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EasyBizDBTypes.Transactions.TransactionLogs
{
    [DataContract]
    [Serializable]
    public class TransactionLog : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string TransactionType { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public DateTime ActualDateTime { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public int DocumentLineID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string SKUName { get; set; }
        [DataMember]
        public int InQty { get; set; }
        [DataMember]
        public int OutQty { get; set; }
        [DataMember]
        public Decimal TransactionPrice { get; set; }
        [DataMember]
        public Decimal Currency { get; set; }
        [DataMember]
        public Decimal ExchangeRate { get; set; }
        [DataMember]
        public Decimal DocumentPrice { get; set; }
        [DataMember]
        public long UserID { get; set; }

        [DataMember]
        public int StockQty { get; set; }  //Read only field

        [DataMember]
        public string ColorCode { get; set; }  //Read only field
        [DataMember]
        public string ColorName { get; set; }  //Read only field


        [DataMember]
        public string SizeCode { get; set; }  //Read only field
        [DataMember]
        public string BarCode { get; set; }  //Read only field

        public string SizeName { get; set; }

        public string VisualOrder { get; set; }

        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        [DataMember]
        //public ImageSource SKUImageSource { get; set; }
        public dynamic SKUImageSource { get; set; }

        public List<TransactionLog> TransactionLogList { get; set; }
       
        [DataMember]
        public string StoreCode { get; set; }

        [DataMember]
        public string StoreName { get; set; }

        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]

        public string POSCode { get; set; }
        [DataMember]
        public string DocumentNo { get;set; }

        [DataMember]
        public string SupplierBarCode { get; set; }

        [DataMember]
        public string Tag_Id { get; set; }
        [DataMember]
        public string BinCode { get; set; }
        [DataMember]
        public int BinID { get; set; }
        [DataMember]
        public string BinSubLevelCode { get; set; }
        [DataMember]
        public Decimal Price { get; set; }

    }

    [DataContract]
    [Serializable]
    public class StockData
    {
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public string SupplierBarCode { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string SizeCode { get; set; }
        [DataMember]
        public int StockQty { get; set; }
        
    }
    [DataContract]
    [Serializable]
    public class ImageData
    {
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public decimal SellingPrice { get; set; }
        [DataMember]
        public string ItemImage { get; set; }
    }
}
