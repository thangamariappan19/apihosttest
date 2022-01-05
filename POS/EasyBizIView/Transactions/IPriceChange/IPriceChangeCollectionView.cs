using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPriceChange
{
	public interface IPriceChangeCollectionView : IBaseView
	{
		List<EasyBizDBTypes.Transactions.PriceChange.PriceChange> PriceChangeList { get; set; }
	}
}
