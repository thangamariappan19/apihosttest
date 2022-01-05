using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Reports
{
    [DataContract]
    [Serializable]
    public class SelectAllResponse : BaseResponseType
    {
        public List<InvoiceHeader> InvoiceHeaderList { get; set; }
    }
}
