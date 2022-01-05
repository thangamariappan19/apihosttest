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
    public class StateMaster : BaseType

    {
         [DataMember]
         public int ID { get; set; }
         [DataMember]
         public string StateCode { get; set; }
         [DataMember]
         public string StateName { get; set; }
         [DataMember]
         public String CountryName { get; set; }
        [DataMember]
         public int CountryID { get; set; }
        [DataMember]
         public string Remarks { get; set; }
        [DataMember]
        public Boolean Active { get; set; }


    }
}
