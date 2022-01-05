using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class DeepCopyCreator
    {
        public static ShiftMaster ShiftMasterDeepCopy(ShiftMaster objShiftMaster)
        {
            var TempShiftMaster = new ShiftMaster();
            if (objShiftMaster != null)
            {                
                TempShiftMaster.ShiftID = objShiftMaster.ShiftID;
                TempShiftMaster.ID = objShiftMaster.ID;
                TempShiftMaster.ShiftCode = objShiftMaster.ShiftCode;
                TempShiftMaster.CountryID = objShiftMaster.CountryID;
                TempShiftMaster.ShiftInAmount = objShiftMaster.ShiftInAmount;
                TempShiftMaster.POSID = objShiftMaster.POSID;
                TempShiftMaster.BusinessDate = objShiftMaster.BusinessDate;
                TempShiftMaster.StoreID = objShiftMaster.StoreID;
                TempShiftMaster.OriginalDayInStatus = objShiftMaster.OriginalDayInStatus;
                TempShiftMaster.CountryName = objShiftMaster.CountryName;
                TempShiftMaster.ShiftInUserID = objShiftMaster.ShiftInUserID;
                TempShiftMaster.Status = objShiftMaster.Status;
                TempShiftMaster.ShiftStatus = objShiftMaster.ShiftStatus;
                TempShiftMaster.OriginalShiftInStatus = objShiftMaster.OriginalShiftInStatus;
                TempShiftMaster.ShiftName = objShiftMaster.ShiftName;
                TempShiftMaster.SortOrder = objShiftMaster.SortOrder;
                TempShiftMaster.ShiftOutUserID = objShiftMaster.ShiftOutUserID;
                TempShiftMaster.ShiftInDateTime = objShiftMaster.ShiftInDateTime;
                TempShiftMaster.ShiftOutDateTime = objShiftMaster.ShiftOutDateTime;
                TempShiftMaster.ShiftLogID = objShiftMaster.ShiftLogID;
            }            
            return TempShiftMaster;
        }
        public static List<ShiftMaster> ShiftMasterListDeepCopy(List<ShiftMaster> objShiftMaster)
        {
            var tempShiftDetailsList = new List<ShiftMaster>();
            foreach (ShiftMaster objShift in objShiftMaster)
            {
                var TempShiftList = new ShiftMaster();

                TempShiftList.ShiftID = objShift.ShiftID;
                TempShiftList.ID = objShift.ID;
                TempShiftList.ShiftCode = objShift.ShiftCode;
                TempShiftList.CountryID = objShift.CountryID;
                TempShiftList.ShiftInAmount = objShift.ShiftInAmount;
                TempShiftList.POSID = objShift.POSID;
                TempShiftList.BusinessDate = objShift.BusinessDate;
                TempShiftList.StoreID = objShift.StoreID;
                TempShiftList.OriginalDayInStatus = objShift.OriginalDayInStatus;
                TempShiftList.CountryName = objShift.CountryName;
                TempShiftList.ShiftInUserID = objShift.ShiftInUserID;
                TempShiftList.Status = objShift.Status;
                TempShiftList.ShiftStatus = objShift.ShiftStatus;
                TempShiftList.OriginalShiftInStatus = objShift.OriginalShiftInStatus;
                TempShiftList.ShiftName = objShift.ShiftName;
                TempShiftList.SortOrder = objShift.SortOrder;
                TempShiftList.ShiftOutUserID = objShift.ShiftOutUserID;
                TempShiftList.ShiftInDateTime = objShift.ShiftInDateTime;
                TempShiftList.ShiftOutDateTime = objShift.ShiftOutDateTime;
                TempShiftList.ShiftLogID = objShift.ShiftLogID;
                tempShiftDetailsList.Add(TempShiftList);
            }
            return tempShiftDetailsList;
        }
      
    }
}
