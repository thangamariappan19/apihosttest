using EasyBizDBTypes.Transactions.POS.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.SalesOrder
{
    [DataContract]
    [Serializable]
    public class SelectBySalesOrderIDResponse : BaseResponseType
    {
        public SalesOrderHeader SalesOrderMasterRecord { get; set; }
    }
}
