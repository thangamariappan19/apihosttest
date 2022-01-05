using EasyBizDBTypes.Masters;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICustomerMaster
{
    public interface ICustomerMasterView:IBaseView
    {

            
         int ID { get; set; }
         string DocumentNo { get; set; }
    
         string CustomerCode { get; set; }

        
         string CustomerName { get; set; }

   
         string PhoneNumber { get; set; }

     
         string AlterPhoneNumber { get; set; }

       
         int CustomerGroupID { get; set; }

     
         string BuildingAndBlockNo { get; set; }

      
         string StreetName { get; set; }

      
         string AreaName1 { get; set; }

       
         string AreaName2 { get; set; }

      
         string City { get; set; }

       
         int StateID { get; set; }

        
         int CountryID { get; set; }

      
         string Email { get; set; }


      
         DateTime DOB { get; set; }

       
         string Gender { get; set; }

         List<CustomerGroupMaster> CustomerGroupNameLookUp {  set; }


         List<StateMaster> StateMasterLookUp { set; }

         List<CountryMaster> CountryMasterLookUp { set; }

         string Remarks { get; set; }

         bool Active { get; set; }
         long BaseID { get; set; }

         Decimal CreditAmount { get; set; }

         int StoreID { get; }
         int DocumentTypeID { get; }

         int UserCountryID { get; }
         int UserStateID { get; }
         int UserStoreID { get; }

         int DetailID { get; set; }
         int RunningNum { get; set; }
         UsersSettings UserInformation { get; }
         DocumentNumberingDetails DocumentNumberingBillNoDetailsRecord { get; set; }
         string CustomerGroupCode { get; }         
         string StateCode { get; }         
         string CountryCode { get; }

         bool OnAccountApplicable { get; set; }
        
    }
}
