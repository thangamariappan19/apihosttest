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
    public class TillSettings : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
  
        public int StoreID { get; set; }
        [DataMember]
        public int PosID { get; set; }

        [DataMember]
        public int UserTeamID { get; set; }

        [DataMember]
        public Decimal FloatingAmount { get; set; }

        [DataMember]
        public bool CountRequired { get; set; }

        [DataMember]
        public int CountType { get; set; }

        [DataMember]
        public bool TillCountOnAssign { get; set; }

        [DataMember]
        public bool TillCountOnClose { get; set; }

        [DataMember]
        public bool TillCountOnFinalize { get; set; }

        [DataMember]
        public string CountTypeName { get; set; }


        [DataMember]
        public string StoreName { get; set; }


        [DataMember]
        public string PosName { get; set; }
        [DataMember]
        public string CountryName { get; set; }


        [DataMember]
        public string RoleName { get; set; }


        [DataMember]
        public string Remarks { get; set; }


    }
}
