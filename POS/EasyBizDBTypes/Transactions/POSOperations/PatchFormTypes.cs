using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POSOperations
{
    [DataContract]
    [Serializable]
    public class PatchFormTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public String ApplicationType { get; set; }
         [DataMember]
        public String ApplicationVersion { get; set; }
         [DataMember]
         public String DBVersion { get; set; }
         [DataMember]
         public Byte[] AppPatchFile { get; set; }
         [DataMember]
         public int DocumentTypeID { get; set; }
        [DataMember]
        public String Extension { get; set; }





    }
}
