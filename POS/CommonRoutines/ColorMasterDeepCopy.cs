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
        public static ColorMaster ColorMasterDeepCopy(ColorMaster objColorMaster)
        {
             //var objColorMasterListDeepCopy = new List<ColorMaster>();
            ColorMaster TempColorMaster = new ColorMaster();
            TempColorMaster.Active = objColorMaster.Active;
            TempColorMaster.Attribute1 = objColorMaster.Attribute1;
            TempColorMaster.Attribute2 = objColorMaster.Attribute2;
            TempColorMaster.ColorCode = objColorMaster.ColorCode;
            TempColorMaster.Colors = objColorMaster.Colors;
            TempColorMaster.CreateBy = objColorMaster.CreateBy;
            TempColorMaster.CreatedByUserName = objColorMaster.CreatedByUserName;
            TempColorMaster.CreateOn = objColorMaster.CreateOn;
            TempColorMaster.Description = objColorMaster.Description;
            TempColorMaster.ID = objColorMaster.ID;
            TempColorMaster.InternalCode = objColorMaster.InternalCode;
            TempColorMaster.IsDeleted = objColorMaster.IsDeleted;
            TempColorMaster.NRFCode = objColorMaster.NRFCode;
            TempColorMaster.SCN = objColorMaster.SCN;
            TempColorMaster.Shade = objColorMaster.Shade;
            TempColorMaster.UpdateBy = objColorMaster.UpdateBy;
            TempColorMaster.UpdatedByUserName = objColorMaster.UpdatedByUserName;
            TempColorMaster.UpdateOn = objColorMaster.UpdateOn;
             //objColorMasterListDeepCopy.Add(TempColorMaster);
            return TempColorMaster;
         }
            
    }
}
