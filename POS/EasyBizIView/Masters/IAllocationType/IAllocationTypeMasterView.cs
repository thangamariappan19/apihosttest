using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IAllocationType
{
  
    public interface IAllocationTypeMasterView : IBaseView
    {
        int ID { get; set; }
        string AllocationTypeCode { get; set; }
        string AllocationTypeName { get; set; }
        string Description { get; set; }
        string Remarks { get; set; }
        Boolean Active { get; set; }
    }
}
