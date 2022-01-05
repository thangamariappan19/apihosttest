using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.FCPasses
{
    [DataContract]
    [Serializable]
    public class PassesTransactionDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int HeaderID { get; set; }

        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }


        [DataMember]
        public string PassRefNo { get; set; }
        [DataMember]      
        public bool IsSent { get; set; }
        [DataMember]
        public bool IsUsed { get; set; }
        [DataMember]
        public bool IsFCSync { get; set; }
    }
}
