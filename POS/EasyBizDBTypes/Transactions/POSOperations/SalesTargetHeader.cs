using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POSOperations
{
    [DataContract]
    [Serializable]
    public class SalesTargetHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public String Brand { get; set; }
        [DataMember]
        public String StoreIDs { get; set; }
        [DataMember]
        public String Year { get; set; }
        [DataMember]
        public int DocumentTypeID { get; set; }
        public List<SalestargetDetails> SalestargetDetails { get; set; }

    }
}
