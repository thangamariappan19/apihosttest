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
    public class EmployeeFingerPrintMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public long EmployeeID { get; set;}
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string FingerPrint { get; set;}
       /* [DataMember]
        public string StoreCode { get; set; }
       */
    }
}
