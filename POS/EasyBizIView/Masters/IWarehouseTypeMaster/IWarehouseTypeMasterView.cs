using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IWarehouseTypeMaster
{

    public interface IWarehouseTypeMasterView : IBaseView
    {
       
         int ID { get; set; }       
         string WarehouseTypeCode { get; set; }       
         string WarehouseTypeName { get; set; }       
         string Description { get; set; }
         string Remarks { get; set; }
         bool Active { get; set; }
    }
}
