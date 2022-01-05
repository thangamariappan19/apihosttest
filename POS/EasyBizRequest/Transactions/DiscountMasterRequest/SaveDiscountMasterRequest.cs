using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.DiscountMasterRequest
{
    [Serializable]
    [DataContract]
   public class SaveDiscountMasterRequest : BaseRequestType
    {
        [DataMember]
        public int CustomerGroupID { get; set; }
        [DataMember]
        public string CountryIDs { get; set; }
        [DataMember]
        public string StoreIDs { get; set; }
        [DataMember]
        public string DiscountType { get; set; }
        [DataMember]
        public DiscountMasterTypes DiscountMasterRecord { get; set; }
        [DataMember]
        public List<EmployeDiscountDetailTypes> EmployeeDiscountDetailList { get; set; }
        [DataMember]
        public List<FamilyDiscountDetailTypes> FamilyDiscountDetailList { get; set; }
    }
}
