using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.SyncSettings
{
    [DataContract]
    [Serializable]

   public class MasterDataSyncDBType
    {
         [DataMember]
         public int BrandID { get; set; }

         [DataMember]
         public int StoreID { get; set; }

         [DataMember]
          public string SkuCode { get; set; }

        [DataMember]
           public string Mode { get; set; }

        [DataMember]
          public string INVOICE { get; set; }

        [DataMember]
           public string UserName { get; set; }

        [DataMember]
         public string Barcode { get; set; }

    }
}
