using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Promotion
{
    [DataContract]
    [Serializable]
   public class EmployeDiscountDetailTypes : BaseType
    {
        [DataMember]
       public int ID { get; set; }
        [DataMember]
        public int DiscountHeaderID { get; set; }
        [DataMember]
        public DateTime FromDate { get; set; }
        [DataMember]
        public DateTime ToDate { get; set; }
        [DataMember]
        public Decimal DiscountValue { get; set; }
    }
}
