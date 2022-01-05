using EasyBizDBTypes.Transactions.POS.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.SalesOrder
{
    [DataContract]
    [Serializable]
    public class SelectBySalesOrderIDRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
       
        [DataMember]
        public string DocumentNo { get; set; }
    }
}
