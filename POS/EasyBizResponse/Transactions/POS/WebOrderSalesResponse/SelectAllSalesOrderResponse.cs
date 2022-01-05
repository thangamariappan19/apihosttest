using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.WebOrderSalesResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllSalesOrderResponse : BaseResponseType
    {
        //public List<OrderSalesHeader> OrderSalesHeader { get; set; }
        //public List<OrderSalesDetails> OrderSalesDetails { get;set;}
    }
}
