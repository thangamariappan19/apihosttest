using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace EasyBizDBTypes.Combo
{
    [DataContract]
    [Serializable]
    public class ComboDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SerialNo { get; set; }
        [DataMember]
        public int ComboID { get; set; }
        [DataMember]
        public int HeaderID { get; set; }
        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public int SKUID { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string SKUName { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public List<SKUMasterTypes> SKUMasterList { get; set; }
        [DataMember]
        public List<ComboDetails> ComboDetailsList { get; set; }
    }
}
