using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IAllocationType
{
  
    public interface IAllocationTypeMasterList : IBaseView
    {
        List<AllocationTypeMaster> AllocationTypeMasterList { get; set; }
    }
}
