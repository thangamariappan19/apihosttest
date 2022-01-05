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
    public class TailoringMasterTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string tailoringunitcode { get; set; }
        [DataMember]
        public string tailoringunitName { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
