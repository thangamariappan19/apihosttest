using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StyleMasterRequest
{
    [DataContract]
    [Serializable]
    public class SaveStyleRequest : BaseRequestType
    {
        [DataMember]
        public StyleMaster StyleRecord { get; set; }

        [DataMember]
        public List<ScaleDetailMaster> StyleWithScaleDetailsList { get; set; }
        [DataMember]
        public List<ColorMaster> StyleWithColorDetailsList { get; set; }
        [DataMember]
        public List<ItemImageMaster> ItemImageMasterDetailsList { get; set; }
       
        [DataMember]
        public List<StyleMaster> ImportExcelList { get; set; }
        [DataMember]
        public List<StyleMaster> ImportcolorExcelList { get; set; }
        [DataMember]
        public List<StyleMaster> ImportScaleExcelList { get; set; }
        [DataMember]
        public StyleMaster StyleMasterData { get; set; }
    }
}
