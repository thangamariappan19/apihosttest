using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CompanySettingResponse
{

    [DataContract]
    [Serializable]
    public class SelectAllCompanySettingResponse:BaseResponseType
    {
        [DataMember]
        public List<CompanySettings> CompanySettingList { get; set; }
    }
}
