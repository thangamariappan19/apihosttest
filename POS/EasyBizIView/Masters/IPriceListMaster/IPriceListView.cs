using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IPriceListMaster
{
   public interface IPriceListView:IBaseView
    {
         int ID { get; set; }       
         string PriceListCode { get; set; }    
         string PriceListName { get; set; } 
         string Remarks { get; set; }    
         int PriceListCurrencyType { get; set; }
         string PriceCategory { get; set; }
         int BasePriceListID { get; set; }

         Decimal ConversionFactore { get; set; }
         bool Active { get; set; }
         string PriceListType { get; set; }    
         List<CurrencyMaster> SalesCurrencyLookUp { set; }

         List<PriceListType> PriceListTypeLookUp { set; }      
    }
}
