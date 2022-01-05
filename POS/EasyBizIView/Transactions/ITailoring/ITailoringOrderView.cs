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
	public interface ITailoringOrderView: IBaseView
	{
		int ID { get; set; }
		string DocumentNo { get; set; }
		DateTime DocumentDate { get; set; }
		string StoreCode { get; set; }
		//string StoreCode { get; }
		int CustomerID { get; set; }
		string CustomerCode { get; set; }
		List<CustomerMaster> CustomerMasterList { get; set; }

		DateTime ExpectedDeliveryDate { get; set; }
		List<TailoringOrderDetails> TailoringOrderDetailsList { get; set; }

		//int StoreID { get; }
		int StoreID { get; }
		string SKUSearchString { get; }

		string CustomerSearchString { get; }
		List<SKUMasterTypes> SKUMasterTypesList { get; set; }

		List<CustomerMaster> CustomerMasterLookUp { set; }


		ManagerOverride DefaultManagerOverrideSetting { get; set; }
		ManagerOverride ManagerOverrideSetting { get; set; }
		long ManagerOverrideID { get; set; }

       
        int RetailID { get; }

        RetailSettingsType RetailSetting { get; set; }
        
    }
}
