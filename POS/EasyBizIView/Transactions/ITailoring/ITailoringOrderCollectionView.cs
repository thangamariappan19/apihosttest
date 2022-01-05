using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Tailoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.ITailoring
{
	public interface ITailoringOrderCollectionView: IBaseView
	{
		string StoreCode { get; set; }
		List<TailoringOrder> TailoringOrderList { get; set; }
	}
}
