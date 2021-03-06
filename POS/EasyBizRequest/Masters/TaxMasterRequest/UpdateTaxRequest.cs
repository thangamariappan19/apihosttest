using EasyBizDBTypes.Masters;
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
    public class UpdateTaxRequest : BaseRequestType
    {
        [DataMember]
        public TaxMaster TaxRecord { get; set; }
    }
}
