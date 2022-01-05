using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Tailoring;
using EasyBizResponse.Transactions.Tailoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.Tailoring
{
	public abstract class BaseTailoringOrderDAL:BaseDAL
	{
		public abstract SelectTailoringOrderDetailsResponse SelectTailoringOrderDetails(SelectTailoringOrderDetailsRequest ObjRequest);
        public abstract SelectTailoringOrderDeliverToCustomerResponse SelectTailoringOrderDeliverToCustomerDetails(SelectTailoringOrderForDeliverToCustomerRequest ObjRequest);
		public abstract SelectAllOPENTailoringOrderResponse SelectAllOPENTailoringOrder(SelectAllOPENTailoringOrderRequest ObjRequest);
		public abstract SaveDespatchToTailorResponse SaveDespatchToTailor(SaveDespatchToTailorRequest ObjRequest);

        public abstract SelectTailoringOrderForReceiveFromTailorResponse SelectTailoringOrderForReceiveFromTailor(SelectTailoringOrderForReceiveFromTailorRequest ObjRequest);
        public abstract SelectTailoringOrderDetailsForReceiveFromTailorResponse SelectTailoringOrderDetailsForReceiveFromTailor(SelectTailoringOrderDetailsForReceiveFromTailorRequest ObjRequest);
        public abstract SaveReceiveFromTailoringOrderResponse SaveReceiveFromTailoring(SaveReceiveFromTailoringOrderRequest ObjRequest);

        public abstract SaveDeliverToCustomerResponse SaveDeliverToCustomer(SaveDeliverToCustomerRequest ObjRequest);
	}
}
