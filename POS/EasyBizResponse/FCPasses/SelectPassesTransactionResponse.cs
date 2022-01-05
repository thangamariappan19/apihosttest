using EasyBizDBTypes.FCPasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.FCPasses
{
    [DataContract]
    [Serializable]
    public class SelectPassesTransactionResponse : BaseResponseType
    {
        [DataMember]
        public PassesTransaction PassesTransactionHeaderData { get; set; }
        [DataMember]
        public List<PassesTransactionDetails> PassesTransactionDetailsList { get; set; }
    }
}
