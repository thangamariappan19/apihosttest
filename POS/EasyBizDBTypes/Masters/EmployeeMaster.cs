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
    public class EmployeeMaster:BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public long BaseID { get; set; }
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        public DateTime DateofJoining { get; set; }
        [DataMember]
        public int Salary { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string PhoneNo { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public Boolean IsSelection { get; set; }

        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public String Designation { get; set; }
        [DataMember]
        public string EmployeeImage { get; set; }
        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public long UpdateFlag { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public string comboempcodename { get; set; }
    }
    public class EmployeeDiscountInfo
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public int EmployeeDiscountID { get; set; }
        [DataMember]
        public Decimal UsedAmount { get; set; }
        [DataMember]
        public Decimal BalanceAmount { get; set; }
    }
}
