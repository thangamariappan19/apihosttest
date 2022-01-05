using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICollectionMaster
{
    public interface ICollectionMasterView : IBaseView
    {
        int ID { get; set; }

        string CollectionCode { get; set; }

        string CollectionName { get; set; }
        string Remarks { get; set; }

        Boolean Active { get; set; }
    }
}
