using EasyBizDBTypes.Transactions.POSOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.SalesTargetRequest
{
   [Serializable]
    [DataContract]
    public class SaveSalesTargetRequest : BaseRequestType
    {
       [DataMember]
       public int CountryID { get; set; }     
       public SalesTargetHeader SalesTargetHeaderRecord { get; set; }      
       public List<SalestargetDetails> SalestargetDetailsList { get; set; } 
       public int DocumentTypeID { get; set; }

      
    }
}
