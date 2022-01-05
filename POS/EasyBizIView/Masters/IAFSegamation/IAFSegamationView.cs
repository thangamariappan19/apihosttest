using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IAFSegamation
{

    public interface IAFSegamationView:IBaseView
    {

        
         int ID { get; set; }
      
         int AFSegamationCode { get; set; }
     
         string AFSegamationName { get; set; }
         List<SegmentMaster> SegmentMasterList { get; set; }
         
         Boolean Active { get; set; }
         string Remarks { get; set; }
         int CodeLength { get; set; }
         string UseSeperator { get; set; }
         int MaxLengthTotal { get; set; }
    }
}
