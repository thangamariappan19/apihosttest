using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters
{
    public interface ICustomerGroupMasterView : IBaseView
    {
        int ID { get; set; }      
        string GroupCode { get; set; }     
        string GroupName { get; set; }      
        Decimal DiscountPercentage { get; set; }    
        int PriceListID { get; set; }

        List<PriceListType> PriceListTypeLookUP {  set; }

        string Remarks { get; set; }

        bool Active { get; set; }

       
    }
}
