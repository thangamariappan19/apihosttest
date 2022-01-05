using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace EasyBizDBTypes.Masters
{
    [Serializable]
    [DataContract]
    public class SearchEngine : BaseType
    {

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string BarCode { get; set; }

        [DataMember]
        public string Number { get; set; }

        [DataMember]
        public string Date { get; set; }





    }
}
