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
    public class PassesTransactionResponse : BaseResponseType
    {
        [DataMember]
        public PassesTransaction PassesTransactionResponseData { get; set; }
        [DataMember]
        public List<PassesTransaction> PassesTransactionResponseList { get; set; }

       
    }
}
