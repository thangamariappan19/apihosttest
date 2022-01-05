using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.TaxMasterRequest
{
    [DataContract]
    [Serializable]
    public class SelectTaxDetailsByStoreIDRequest : BaseRequestType
    {
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public bool Type { get; set; }
    }
}
