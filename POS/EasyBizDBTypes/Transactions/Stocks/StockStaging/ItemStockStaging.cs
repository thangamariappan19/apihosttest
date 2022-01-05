using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.StockStaging
{
    [DataContract]
    [Serializable]
    public class ItemStockStaging : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public DateTime DocumentDate { get; set; }

        [DataMember]
        public string StyleCode { get; set; }

        [DataMember]
        public string SKUCode { get; set; }

        [DataMember]
        public int InQty { get; set; }

        [DataMember]
        public int OutQty { get; set; }

        [DataMember]
        public int StockQty { get; set; }
    }

}
