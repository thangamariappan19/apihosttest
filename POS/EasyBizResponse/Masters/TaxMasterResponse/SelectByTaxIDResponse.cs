using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.TaxMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectByTaxIDResponse : BaseResponseType
    {
        [DataMember]
        public TaxMaster TaxRecord { get; set; }
        [DataMember]
        public List<TaxMaster> TaxList = new List<TaxMaster>();
    }
}
