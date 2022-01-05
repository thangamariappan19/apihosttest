using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ISubCollectionMaster
{
    public interface ISubCollectionView:IBaseView
    {

        int ID { get; set; }
        int CollectionID { get; set; }
        string CollectionName { get; set; }

        List<SubCollectionMaster> SubCollectionMasterList { get; set; }
        List<SubCollectionMaster> NewSubCollectionMasterList { get; set; }
        List<CollectionMasterTypes> CollectionMasterTypesLookUp { set; }
        List<SubCollectionMaster> AllSubCollectionDetailsList { get; set; }
    }
}
