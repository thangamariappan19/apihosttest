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
   public class VendorMaster : BaseType
    {
          [DataMember]
         public int ID { get; set; }
         [DataMember]
         public string VendorCode { get; set; }
         [DataMember]
         public string VendorName { get; set; }
         [DataMember]
         public string ShortName { get; set; }
         [DataMember]
         public long PhoneNumber { get; set; }
         [DataMember]
         public int CountryID { get; set; }
         [DataMember]
         public long CompanyID { get; set; }
         [DataMember]
         public int VendorGroupID { get; set; }
         [DataMember]
         public string Address { get; set; }
         [DataMember]
         public string EmailID { get; set; }
         [DataMember]
         public string CountryName { get; set; }
         [DataMember]
         public string CompanyName { get; set; }
         [DataMember]
         public string VendorGroupName { get; set; }

         [DataMember]
         public string Remarks { get; set; }

    }
}
