using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICompanySettingMaster
{
    public interface ICompanySettingMasterView:IBaseView
    {

              
          int ID { get; set; }


          string CompanyCode { get; set; }


          string CompanyName { get; set; }


          string Address { get; set; }


          int CountrySettingID { get; set; }
          string CountrySettingCode { get; }
        

          //int RetailSettingID { get; set; }

          
          string Remarks { get; set; }
          Boolean Active { get; set; }

          string CompanyLogo { get; set; }

          List<CountryMaster> CountryMasterLookUp { set; }

          //List<RetailSettingsType> RetailSettingsLookUp { set; }        
    }
}
