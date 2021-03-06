using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace EasyBizResponse.Masters.BinTransferResponse
{
    [DataContract]
    [Serializable]
    public class SelectBinDetailsBySKUCodeResponse : BaseResponseType
    {
        [DataMember]
        public List<BinLogTypes> BinDetailsList { get; set; }
    }
}
