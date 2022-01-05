using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.RetailSettingsResponse
{
    [DataContract]
    [Serializable]
   public class SelectAllRetailResponse : BaseResponseType
    {
        public List<RetailSettingsType> RetailList { get; set; }
    }
}
