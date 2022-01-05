using EasyBizDBTypes.Common;
using System;
using System.Runtime.Serialization;

namespace EasyBizDBTypes.FCPasses
{
    [DataContract]
    [Serializable]
    public class PassMaster: BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string PassCode { get; set; }
        [DataMember]
        public string PassName { get; set; }
        [DataMember]
        public int Points { get; set; }
        [DataMember]
        public string CardType { get; set; }
        [DataMember]
        public DateTime ValidFrom { get; set; }
        [DataMember]
        public DateTime ValidTo { get; set; }
        [DataMember]
        public string ScanMethod { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public bool IsOneTimePass { get; set; }
        [DataMember]
        public bool IsSync { get; set; }

    }
}
