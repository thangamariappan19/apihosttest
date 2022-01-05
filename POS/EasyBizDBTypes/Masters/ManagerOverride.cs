using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizTypes.Masters
{
    [DataContract]
    [Serializable]
   public class ManagerOverride : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string CreditLimitOverride { get; set; }

        [DataMember]
        public Boolean ReprintTransReceipt { get; set; }

        [DataMember]
        public Boolean changeSalesPersoninSOE { get; set; }
        [DataMember]
        public Boolean ChangeSalesPersonRefund { get; set; }
        [DataMember]
        public Boolean DelSuspendedTransaction { get; set; }
        [DataMember]
        public Boolean VoidSale { get; set; }
        [DataMember]
        public Boolean voidItem { get; set; }
        [DataMember]
        public Boolean TransModeChange { get; set; }
        [DataMember]
        public Boolean CustomerSearch { get; set; }
        [DataMember]
        public Boolean ProductSearch { get; set; }
        [DataMember]
        public Boolean SaleInfoEdit { get; set; }
        [DataMember]
        public Boolean ItemInfoEdit { get; set; }
        [DataMember]
        public Boolean TransactionSearch { get; set; }
        [DataMember]
        public Boolean SuspendRecall { get; set; }
        [DataMember]
        public Boolean CashOut { get; set; }
        [DataMember]
        public Boolean CashIn { get; set; }
        [DataMember]
        public Boolean TransactionRefund { get; set; }
        [DataMember]
        public string returnIDS { get; set; }
        [DataMember]
        public Boolean TotalDiscount { get; set; }
        [DataMember]
        public Boolean DayInDayOut { get; set; }
        [DataMember]
        public Boolean AllowEditcustomer { get; set; }
    }
}
