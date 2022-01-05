using EasyBizDBTypes.Transactions.PriceChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.PriceChange
{
	public class ValidatePriceChangeResponse : BaseResponseType
	{
		public List<PriceChangeDetails> ValidatingPriceChangeDetailsList { get; set; }
	}
}
