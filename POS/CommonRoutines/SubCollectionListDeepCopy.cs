using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class DeepCopyCreator
    {
        public static List<SubCollectionMaster> SubCollectionMasterListDeepCopy(List<SubCollectionMaster> objSubCollectionListDeepCopy)
        {
            var objSubCollectionMasterListDeepCopy = new List<SubCollectionMaster>();
            foreach(SubCollectionMaster objSubCollectionMaster in objSubCollectionListDeepCopy)
            {
                var tempSubCollectionMaster = new SubCollectionMaster();
                //tempSubCollectionMaster.Active = objSubCollectionMaster.Active;
                tempSubCollectionMaster.CollectionID = objSubCollectionMaster.CollectionID;
                tempSubCollectionMaster.CollectionName = objSubCollectionMaster.CollectionName;
                tempSubCollectionMaster.ID = objSubCollectionMaster.ID;
                tempSubCollectionMaster.SubCollectionCode = objSubCollectionMaster.SubCollectionCode;
                tempSubCollectionMaster.SubCollectionName = objSubCollectionMaster.SubCollectionName;
                //tempSubCollectionMaster.CreateBy = objSubCollectionMaster.CreateBy;
                //tempSubCollectionMaster.CreatedByUserName = objSubCollectionMaster.CreatedByUserName;
                //tempSubCollectionMaster.CreateOn = objSubCollectionMaster.CreateOn;
                //tempSubCollectionMaster.IsDeleted = objSubCollectionMaster.IsDeleted;
                //tempSubCollectionMaster.SCN = objSubCollectionMaster.SCN;
                //tempSubCollectionMaster.UpdateBy = objSubCollectionMaster.UpdateBy;
                //tempSubCollectionMaster.UpdatedByUserName = objSubCollectionMaster.UpdatedByUserName;
                //tempSubCollectionMaster.UpdateOn = objSubCollectionMaster.UpdateOn;           

                objSubCollectionMasterListDeepCopy.Add(tempSubCollectionMaster);
            }
            return objSubCollectionMasterListDeepCopy;
        }
    }
}
