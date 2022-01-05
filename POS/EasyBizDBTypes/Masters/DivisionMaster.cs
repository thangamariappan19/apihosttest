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
   public class DivisionMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DivisionCode { get; set; }
        [DataMember]
        public string DivisionName { get; set; }

        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public long BrandID { get; set; }
        [DataMember]
        public int UpdateFlag { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
        [DataMember]
        public int UpdateBy { get; set; }
    }
}
