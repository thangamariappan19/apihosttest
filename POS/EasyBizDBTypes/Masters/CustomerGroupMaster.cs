using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{

    [Serializable]
    [DataContract]
   public class CustomerGroupMaster:BaseType
    {
   [DataMember]
   public int ID { get; set; }

   [DataMember]
   public string GroupCode { get; set; }

   [DataMember]
   public string GroupName { get; set; }

   [DataMember]
   public Decimal DiscountPercentage { get; set; }

   [DataMember]
   public int PriceListID { get; set; }

   [DataMember]
   public string PriceListName { get; set; }

   [DataMember]
   public string Remarks { get; set; }



    }
}
