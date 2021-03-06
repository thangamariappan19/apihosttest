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
    public class SelectAllTaxResponse : BaseResponseType
    {
        public List<TaxMaster> TaxList { get; set; }
    }
}
