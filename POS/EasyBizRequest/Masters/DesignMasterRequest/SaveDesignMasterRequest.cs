using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DesignMasterRequest
{

    [DataContract]
    [Serializable]
    public class SaveDesignMasterRequest : BaseRequestType
    {
        [DataMember]
        public DesignMasterTypes DesignMasterData { get; set; }
          [DataMember]
        public List<ItemImageMaster> DesignWithItemImageList { get; set; }
          [DataMember]
          public List<DesignMasterTypes> ImportExcelList { get; set; }
    }

  
}
