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
    public class PaymentModeTypes:BaseType
    {
        [DataMember]
        public int ID { get; set; }
       
        [DataMember]
        public string PaymentModeCode { get; set; }
        [DataMember]
        public string PaymentModeName { get; set; }
        [DataMember]
        public string SortOrder { get; set; }

        [DataMember]
        public string Remarks { get; set; }

    }
}
