using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class LabelPrintingTypes
    {
        [DataMember]
        public string SalesName { get; set; }
         [DataMember]
        public string Location { get; set; }
        [DataMember]
        public string Department { get; set; }
         [DataMember]
         public string ProductCode { get; set; }
         [DataMember]
         public string ColorCode { get; set; }
         [DataMember]
         public string SizeCode { get; set; }
         [DataMember]
         public string CurrencyCode { get; set; }
         [DataMember]
         public int NoOfLabel { get; set; }
         [DataMember]
         public Boolean PrintPrice { get; set; }
         [DataMember]
         public Boolean PrintProductCode { get; set; }
         [DataMember]
         public Boolean PrintPriceWAS { get; set; }
         [DataMember]
         public decimal Price { get; set; }
         [DataMember]
         public string Barcode { get; set; }
         [DataMember]
         public string StyleName { get; set; }
         [DataMember]
         public decimal WasPrice { get; set; }
         [DataMember]
         public decimal NowPrice { get; set; }
    }
}
