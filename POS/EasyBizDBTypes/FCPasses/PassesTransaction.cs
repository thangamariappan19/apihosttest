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
    public class PassesTransaction : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int PassID { get; set; }
        [DataMember]
        public string PassCode { get; set; }
        [DataMember]
        public string PassName { get; set; }       
        [DataMember]
        public DateTime ValidFrom { get; set; }
        [DataMember]
        public DateTime ValidTo { get; set; }
        [DataMember]
        public string TransferStatus { get; set; }
        [DataMember]
        public bool EmailSend { get; set; }
        [DataMember]
        public bool WhatsappSend { get; set; }
        [DataMember]
        public bool TextSend { get; set; }
        [DataMember]
        public bool IsFCSync { get; set; }
        [DataMember]
        public List<PassesTransactionDetails> PassesTransactionDetailsList { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
    }
}
