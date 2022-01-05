using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Vendor
{
    public interface IVendorView : IBaseView
    {
        int ID { get; set; }

        string VendorCode { get; set; }

        string VendorName { get; set; }
        string ShortName { get; set; }
        long PhoneNumber { get; set; }
        int CountryID { get; set; }
        long CompanyID { get; set; }
        int VendorGroupID { get; set; }
        string VendorGroupName { get; set; }
        string Address { get; set; }
        string EmailID { get; set; }

        string Remarks { get; set; }

        bool Active { get; set; }
        List<VendorMaster> VendorList { get; set; }

        List<VendorGroupMaster> VendorGroupLookUp { set; }
        List<CountryMaster> CountryLookUp { set; }
        List<CompanySettings> CompanyLookUp { set; }
    }
}
