using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IStoreMaster
{
    public interface IStoreMasterList:IBaseView
    {

        List<StoreMaster> StoreMasterList { get; set; }

        List<StoreBrandMapping> SelectALLStoreBrandMappingList { get; set; }
    }
}
