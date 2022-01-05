using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
   public class StoreBrandMapping : BaseType
    {

       [DataMember]
       public int ID { get; set; }
       [DataMember]
       public int CountryID { get; set; }
       [DataMember]
       public int StoreID { get; set; }
       [DataMember]
       public int BrandID { get; set; }
       [DataMember]
       public string StoreCode { get; set; }
       [DataMember]
       public string CountryCode { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public string BrandName { get; set; }     

        [DataMember]
        public Decimal DiscountValue { get; set; }
        [DataMember]
        public string FranchiseCode { get; set; }
        [DataMember]
        public int FranchiseID { get; set; }
      
    }
}
