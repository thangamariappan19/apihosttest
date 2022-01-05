using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SearchEngineResponse
{
    public class SearchCustomerResponse:BaseResponseType
    {
        [DataMember]
        public List<SearchEngine> SearchEngineDataList = new List<SearchEngine>();
    }
}
