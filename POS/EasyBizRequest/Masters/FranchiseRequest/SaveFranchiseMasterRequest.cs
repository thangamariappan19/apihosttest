using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.FranchiseRequest
{
    [DataContract]
    [Serializable]
    public class SaveFranchiseMasterRequest : BaseRequestType
    {
        [DataMember]
        public FranchiseType FranchiseTypeData { get; set; }       
    }
}
