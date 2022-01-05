using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICollectionMaster
{
    public interface ICollectionMasterViewList : IBaseView
    {
        List<CollectionMasterTypes> CollectionMasterTypesList { get; set; }
    }
}
