using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class ExpenseMasterTypes:BaseType
    {

        [DataMember]

        public int ID { get; set; }

        [DataMember]

        public string ExpenseCode { get; set; }

        [DataMember]

        public string ExpenseName { get; set; }


        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public List<ExpenseMasterTypes> ExpenseMasterTypesData { get; set; }


    }
}
