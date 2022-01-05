using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Cardex.CardexLocation
{
    [Serializable]
    [DataContract]
    public class CardexLocationTotalDetails : BaseType
    {
        [DataMember]
        public String TotalInQty { get; set; }

        [DataMember]
        public String TotalOutQty { get; set; }

        [DataMember]
        public String TotalBalance { get; set; }
    }
}
