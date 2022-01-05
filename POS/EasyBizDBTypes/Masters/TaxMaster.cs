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
   public class TaxMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string TaxCode { get; set; }
        [DataMember]
        public string TaxPercentage { get; set; }
         [DataMember]
        public bool Sales { get; set; }
         [DataMember]
        public bool Purchase { get; set; }
        [DataMember]
         public bool inclusivetax { get; set; }
        [DataMember]
        public List<TaxMaster> Taxlist { get; set; }
    }
}
