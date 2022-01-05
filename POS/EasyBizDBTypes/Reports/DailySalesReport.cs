using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Reports
{
    [DataContract]
    [Serializable]
    public class DailySalesReport
    {
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public string PosNo { get; set; }
        [DataMember]
        public string Stylecode { get; set; }
        [DataMember]
        public string StyleName { get; set; }
        [DataMember]
        public int ScaleID { get; set; }
        [DataMember]
        public int ColorID { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string SKUName { get; set; }
        [DataMember]
        public String InvoiceNo { get; set; }
        [DataMember]
        public int Qty { get; set; }
        [DataMember]
        public Decimal DiscountAmount { get; set; }
        [DataMember]
        public Decimal Price { get; set; }
        [DataMember]
        public Decimal LineTotal { get; set; }
        [DataMember]
        public Decimal TotalPrice { get; set; }
        

    }
}
