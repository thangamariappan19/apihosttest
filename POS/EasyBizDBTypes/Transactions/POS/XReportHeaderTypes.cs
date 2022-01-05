using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS
{
    [DataContract]
    [Serializable]
    public class XReportHeaderTypes
    {
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public String Cashier { get; set; }
        [DataMember]
        public String ShopName { get; set; }
        [DataMember]
        public String PosName { get; set; }
        [DataMember]
        public String ShiftName { get; set; }
        [DataMember]
        public byte[] StoreImage { get; set; }
        [DataMember]
        public string Time { get; set; }
    }
}
