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
  public class CurrentStockReport
    {
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public string BrandName { get; set; }
        [DataMember]
        public bool ZeroStock { get; set; }
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
        public int Qty { get; set; }
       
    }
}
