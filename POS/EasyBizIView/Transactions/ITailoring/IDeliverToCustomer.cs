using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Tailoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.ITailoring
{
    public interface IDeliverToCustomer : IBaseView
    {
        List<TailoringOrder> TailoringOrderList { get; set; }
        DateTime ReceivedDate { get; set; }
        int StoreID { get; set; }
        string StoreCode { get; set; }
        string TailoringUnitCode { get; set; }
        List<TailoringMasterTypes> TailoringMasterTypesLookUp { get; set; }

        List<TailoringOrderDetails> TailoringOrderDetailsList { get; set; }
    }
}
