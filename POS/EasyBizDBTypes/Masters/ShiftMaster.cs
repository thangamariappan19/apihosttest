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
   public class ShiftMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public String ShiftCode { get; set; }

        [DataMember]
        public String ShiftName { get; set; }

        [DataMember]
        public long CountryID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }

        [DataMember]
        public int SortOrder { get; set; }
         [DataMember]
        public string CountryName { get; set; }
         [DataMember]
         public int ShiftID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int POSID { get; set; }
        [DataMember]
        public String POSCode { get; set; }
        [DataMember]
        public long ShiftInUserID { get; set; }
        [DataMember]
        public long ShiftOutUserID { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public DateTime ShiftInDateTime { get; set; }
        [DataMember]
        public DateTime ShiftOutDateTime { get; set; }
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ShiftStatus { get; set; }

        [DataMember]
        public string OriginalDayInStatus { get; set; }

        [DataMember]
        public string OriginalShiftInStatus { get; set; }
        [DataMember]
        public List<ShiftMaster> Shiftlist { get; set; }
        [DataMember]
        public int ShiftLogID { get; set; }
        [DataMember]
        public Decimal ShiftInAmount { get; set; }
        [DataMember]
        public string POSName { get; set; }
        [DataMember]
        public Boolean Dayin { get; set; }
        [DataMember]
        public Boolean Shiftin { get; set; }

        // This fields is for getting default customer details in POS.
        [DataMember]
        public int DefaultCustomerID { get; set; }
        [DataMember]
        public string DefaultCustomerCode { get; set; }
        [DataMember]
        public string DefaultCustomerName { get; set; }
        [DataMember]
        public string DefaultPhoneNumber { get; set; }
        [DataMember]
        public int DefaultCustomerGroupID { get; set; }
        [DataMember]
        public string DefaultCustomerGroupCode { get; set; }
        [DataMember]
        public string PrinterDeviceName { get; set; }
    }
}
