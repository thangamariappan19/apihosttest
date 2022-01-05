using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters
{
    public interface IProductLineMasterView : IBaseView
    {

         int ID { get; set; }        
         string ProductLineCode { get; set; }        
         string ProductLineName { get; set; }        
         string Description { get; set; }
         List<ProductLineMaster> ProductLineMasterList { get; set; }
         bool Active { get; set; }
    }
}
