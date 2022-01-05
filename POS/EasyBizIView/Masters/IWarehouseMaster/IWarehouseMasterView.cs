using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IWarehouseMaster
{
   public interface IWarehouseMasterView : IBaseView
    {
        
        int ID { get; set; }        
        string WarehouseCode { get; set; }        
        string WarehouseName { get; set; }        
        int CountryID { get; set; }        
        int CompanyID { get; set; }        
        int WarehouseTypeID { get; set; }
        string Remarks { get; set; }
        bool Active { get; set; }
        List<CountryMaster> CountryMasterLookUp { set; }

        List<CompanySettings> CompanySettingsLookUp { set; }

        List<WarehouseTypeMaster> WarehouseTypeMasterLookUp { set; }
    }
}
