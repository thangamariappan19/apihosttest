using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Reports.DayWiseTransaction
{
    [DataContract]
    [Serializable]
    public class SalesExchangeDetailTransaction : BaseType
    {
        [DataMember]
        public string SKUCode { get; set; }

        [DataMember]
        public int Qty { get; set; }

        [DataMember]
        public Decimal Price { get; set; }

        [DataMember]
        public Decimal SellingPrice { get; set; }

        [DataMember]
        public Decimal SellingLineTotal { get; set; }

        [DataMember]
        public Decimal DiscountAmount { get; set; }

        [DataMember]
        public String Department { get; set; }

        [DataMember]
        public String ItemCode { get; set; }

        [DataMember]
        public String Brand { get; set; }

        [DataMember]
        public String Size { get; set; }

        [DataMember]
        public String Color { get; set; }
    }
}
