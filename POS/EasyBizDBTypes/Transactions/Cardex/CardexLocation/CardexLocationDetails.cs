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
    public class CardexLocationDetails : BaseType
    {

        [DataMember]
        public String TransactionDate { get; set; }

        [DataMember]
        public string DocumentNumber { get; set; }

        [DataMember]
        public string TransactionType { get; set; }

        [DataMember]
        public string QuantityIn { get; set; }

        [DataMember]
        public string QuantityOut { get; set; }
        
        [DataMember]
        public string LocationCode { get; set; }

        [DataMember]
        public string StyleCode { get; set; }

        [DataMember]
        public string SKUCode { get; set; }

        [DataMember]
        public Double Balance { get; set; }



    }
}
