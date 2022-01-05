using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IPosMaster
{
    public interface IPosMasterView: IBaseView
    {
     
         long ID { get; set; }
     
         string PosCode { get; set; }
     
         string PosName { get; set; }

     
         int StoreID { get; set; }
         int CustomerID { get; set; }    
        
     
         int CountryID { get; set; }

     
         int StoreGroupID { get; set; }

         List<CountryMaster> CountryMasterLookUp { set; }

         List<StoreGroupMaster> StoreGroupMasterLookUp { set; }

         List<StoreMaster> StoreMasterLookUp { set; }

         List<StoreMaster> StoreRecord { set; }

         string PrinterDeviceName { get; set; }
         bool Active { get; set; }

         string DiskID { get; set; }
         string CPUID { get; set; }
         List<CustomerMaster> CustomerMasterList { get; set; }
         string PoleDisplayPort { get; set; }
         string DisplayLineMsgOne { get; set; }
         string DisplayLineMsgTwo { get; set; }

         string CountryCode { get;}
         string StoreCode { get;}
         string StoreGroupCode { get;}
         string CustmerCode { get; set; }  
            //List<PosMaster> PosMasterList { get; set; }

         
    }
}
