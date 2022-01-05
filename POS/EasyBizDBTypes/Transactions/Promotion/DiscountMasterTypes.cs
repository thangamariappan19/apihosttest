using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Promotion
{
    [DataContract]
    [Serializable]
   public class DiscountMasterTypes : BaseType
    {
        [DataMember]
       public int ID { get; set; }
        [DataMember]
        public int CustomerGroupID { get; set; }
        [DataMember]
        public string CustomerGroupCode { get; set; }
        [DataMember]
        public string CountryIDs { get; set; }
        [DataMember]
        public string CountryCodes { get; set; }
        [DataMember]
        public string StoreIDs { get; set; }
        [DataMember]
        public string StoreCodes { get; set; }
        [DataMember]
        public string DiscountType { get; set; }
        public List<EmployeDiscountDetailTypes> EmployeeDiscountDetails { get; set; }
        public List<FamilyDiscountDetailTypes> FamilyDiscountDetails { get; set; }
    }
}
