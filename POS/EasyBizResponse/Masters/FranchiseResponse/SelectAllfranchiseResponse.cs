using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.FranchiseResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllfranchiseResponse : BaseResponseType
    {
        public List<FranchiseType> FranchiseTypeList { get; set; }
    }
}
