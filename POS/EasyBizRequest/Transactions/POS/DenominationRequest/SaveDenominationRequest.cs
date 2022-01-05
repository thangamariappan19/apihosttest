using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.DenominationRequest
{

    [Serializable]
    [DataContract]
    public class SaveDenominationRequest : BaseRequestType
    {
        [DataMember]
        public List<DenominationForShiftOutType> DenominationForShiftOutTypeList { get; set; }
        [DataMember]
        public DenominationForShiftoutTypeHeader DenominationForShiftoutTypeHeader { get; set; }      
        [DataMember]
        public ReceivedDenomination ReceivedDenominationData { get; set; }

        [DataMember]
        public List<PaymentTypeMasterType> PaymentTypeMasterTypeList { get; set; }
    }
}
