using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.RetailSettingsLookUpResponse
{

    [DataContract]
    [Serializable]
    public class SelectRetailSettingsLookUpResponse:BaseResponseType
    {
       [DataMember]
        public List<RetailSettingsType> RetailSettingsList { get; set; }
    }
}
