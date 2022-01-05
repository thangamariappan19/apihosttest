using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EasyBizRequest.Masters.CompanySettingRequest
{

    [DataContract]
    [Serializable]
    public class SelectCompanySettingsLookUpRequest:BaseRequestType
    {     

        public int CountryID { get; set; }

        public string CountrySettingCode { get; set; }

    }
}
