using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ISubCollectionMaster
{
    public interface ISubCollectionViewList:IBaseView
    {

        List<SubCollectionMaster> SubCollectionMasterList { get; set; }

        List<CollectionMasterTypes> CollectionMasterTypesList { get; set; }
    }
}
