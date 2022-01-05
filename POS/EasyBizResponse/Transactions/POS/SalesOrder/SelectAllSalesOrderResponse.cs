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
    public class SelectAllSalesOrderResponse : BaseResponseType
    {
        [DataMember]
        public List<SalesOrderHeader> SalesOrderHeaderList { get; set; }
    }

    [DataContract]
    [Serializable]
    public class SelectSalesOrderDetailResponse :BaseResponseType
    {
        [DataMember]
        public List<SalesOrderDetail> SalesOrderDetailList { get; set; }
    }
}
