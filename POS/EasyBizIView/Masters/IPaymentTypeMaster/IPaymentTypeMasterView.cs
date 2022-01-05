using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.PaymentTypeMaster
{

   
   public interface IPaymentTypeMasterView:IBaseView
    {
       
         int ID { get; set; }
         int CountryID { get; set; }
      
         string PaymentCode { get; set; }
       
         string PaymentName { get; set; }
    
         string PaymentType { get; set; }
  
         bool CountRequired { get; set; }
       
         string CountType { get; set; }
      
         bool Refundable { get; set; }
     
         bool RequiredManageApproval { get; set; }
       
         bool OpenCashDraw { get; set; }
      
         bool AllowOverTender { get; set; }
     
         bool AllowPartialTender { get; set; }

         string Remarks { get; set; } 
         bool PaymentProcesser { get; set; }
         bool Active { get; set; }
         bool IsCountryNeed { get; set; }
         List<CountryMaster> CountryLookUp { set; }
         string CountryName { get; set; }
         string CountryCode { get; }
         byte[] PaymentImage { get; set; }
         List<PaymentTypeMasterType> PaymentImageList { get; set; }
        string SortOrder { get; set; }
        string PaymentReceivedType { get; set; }
    }
}
