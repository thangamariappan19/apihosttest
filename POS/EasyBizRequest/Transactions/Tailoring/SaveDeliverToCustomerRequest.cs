using EasyBizDBTypes.Transactions.Tailoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Tailoring
{
    public class SaveDeliverToCustomerRequest : BaseRequestType
    {
        public List<TailoringOrder> TailoringOrderList { get; set; }
        public DateTime ReceivedDate { get; set; }
    }
}
