using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Tailoring;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.ITailoring
{
	public interface IDespatchToTailoringUnit : IBaseView
	{
		string TailoringUnitCode { get; set; }
		DateTime TailorDeliveryDate { get; set; }
		List<TailoringOrder> TailoringOrderList { get; set; }
		List<TailoringMasterTypes> TailoringMasterTypesLookUp { get; set; }
		string StoreCode { get; set; }
		int StoreID { get; set; }
	}
}
