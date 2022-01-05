using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
   public class CustomerSpecialPriceMasterTypes:BaseType
    {[DataMember]
        public int ID { get; set; }
         [DataMember]
        public int ApplicablePriceList { get; set; }
       [DataMember]
        public DateTime DateFrom { get; set; }
         [DataMember]
        public DateTime DateTo { get; set; }
         [DataMember]
        public int CustomerGroup { get; set; }
        [DataMember]
        public string DiscountType { get; set; }
       [DataMember]
        public int DiscountValue { get; set; }
       public bool CustomerGroupUsed { get; set; }
       public bool CustomerMasterUsed { get; set; }
       [DataMember]
       public string ApplicablePriceListName { get; set; }
        [DataMember]
       public List<CommonUtil> StoreList { get; set; }
        [DataMember]
        public List<CommonUtil> GetItemTypeList { get; set; }


        [DataMember]
        public List<CommonUtil> GetCustomerList { get; set; }
        public List<CustomerMaster> CustomerMasterSpecialPriceMasterList { get; set; }

        public List<CommonUtil> CategoryCommonUtil { get; set; }
        public List<CommonUtil> StoreCommonUtil { get; set; }
        public string ApplicablePriceListCode { get; set; }
        public string CustomerGroupCode { get; set; }       
       
    }
}
