using EasyBizDBTypes.Transactions.Cardex.CardexLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.Icardex.IcardexLocation
{
     public interface IcardexLocationView : IBaseView
    {
         List<CardexLocationDetails> _CardexLocationViewList { get; set; }
        string SearchString { get; set; }
        String TotalInQty { get; set; }
        String TotalOutQty { get; set; }
        String TotalBalance { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        int StoreID { get;  }
    }
}
