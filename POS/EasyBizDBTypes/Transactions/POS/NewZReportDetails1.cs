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
    public   class NewZReportDetails1
    {
        [DataMember]
        public byte[] StoreImage { get; set; }
        [DataMember]
        public String ShopCode { get; set; }
        [DataMember]
        public String Country { get; set; }
        [DataMember]
        public String ShopName { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public String Time { get; set; }

    }
}
