using EasyBizDBTypes.Transactions.POSOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.PatchFormRequest
{
    [Serializable]
    [DataContract]
    public class SavePatchFormRequest : BaseRequestType
    {
        public int DocumentTypeID { get; set; }
        public PatchFormTypes PatchFormTypesRecord { get; set; }  
    }
}
