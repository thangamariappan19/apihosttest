using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PosAPI.DTOs
{
    public class LoginDTO
    {
        public int ID { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int EmployeeID { get; set; }
        public int RoleID { get; set; }
        public string EmployeeCode { get; set; }
        public string RoleCode { get; set; }
    }
}