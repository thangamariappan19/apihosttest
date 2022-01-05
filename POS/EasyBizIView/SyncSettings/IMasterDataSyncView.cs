using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.SyncSettings;
using System.Data;

namespace EasyBizIView.SyncSettings
{
    public interface IMasterDataSyncView : IBaseView
    {
        List<BrandMaster> BrandList { get; set; }

        string Status { get; set; }

        string Mode { get; set; }

        int BrandID { get; set; }

        string Skucode { get; set; }

        string Barcode { get; set; }

        string INVOICE { get; set; }

        string UserName { get; set; }

        int StoreID { get; }
        
    }
}
