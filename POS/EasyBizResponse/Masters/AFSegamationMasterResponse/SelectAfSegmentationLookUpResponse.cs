using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.AFSegamationMasterResponse
{
    public class SelectAfSegmentationLookUpResponse : BaseResponseType
    {
         [DataMember]
        public List<AFSegamationMasterTypes> AFSegmentationMaster { get; set; }
    }
}
