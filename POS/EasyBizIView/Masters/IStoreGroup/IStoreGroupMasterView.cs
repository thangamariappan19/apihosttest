using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IStoreGroup
{
    public  interface IStoreGroupMasterView:IBaseView
    {
        int ID { get; set; }
        string StoreGroupCode { get; set; }
        string StoreGroupName { get; set; }
        string Description { get; set; }
        bool Active { get; set; }
        
        List<StoreGroupDetails>  StoreGroupList { get; set; }
    }
}
