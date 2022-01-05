using EasyBizDBTypes.Transactions.Tailoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Tailoring
{
	[DataContract]
	[Serializable]
	public class SelectAllOPENTailoringOrderResponse : BaseResponseType
	{
		public List<TailoringOrder> TailoringOrderList { get; set; }
	}
}
