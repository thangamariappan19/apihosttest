using System;
using EasyBizDBTypes.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ISegmentMaster
{
  public  interface ISegmentMasterView : IBaseView
    {
      int ID { get; set; }
        string SegmentName { get; set; }

        int MaxLength { get; set; }
        string Remarks { get; set; }

        Boolean Active { get; set; }
        

        List<SegmentMaster> SegmentMasterTypesList { get; set; }
    }
}
