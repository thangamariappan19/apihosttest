using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Promotions.DiscountMasterResponse
{
    [DataContract]
    [Serializable]
   public class SelectAllDiscountMasterResponse : BaseResponseType
    {

        public DiscountMasterTypes DiscountMasterRecord { get; set; }
        [DataMember]
        public List<EmployeDiscountDetailTypes> EmployeeDiscountDetailList { get; set; }
        [DataMember]
        public List<FamilyDiscountDetailTypes> FamilyDiscountDetailList { get; set; }
    }
}
