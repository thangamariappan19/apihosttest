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
    public class CompanySettings :  BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public int CountrySettingID { get; set; }

        [DataMember]
        public int RetailSettingID { get; set; }
        [DataMember]
        public string Remarks { get; set; }

          [DataMember]
        public string CompanyLogo { get; set; }

          [DataMember]
          public string CountryName { get; set; }

          [DataMember]
          public string RetailName { get; set; }
          [DataMember]
          public string RetailSettingCode { get; set; }
          [DataMember]
          public string CountrySettingCode { get; set; }
        
        

    }
}
