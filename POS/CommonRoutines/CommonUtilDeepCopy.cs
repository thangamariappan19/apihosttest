using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class DeepCopyCreator
    {
        public static CommonUtil CommonUtilDeepCopy(CommonUtil objCommonUtil)
        {
            var tempCommonUtil = new CommonUtil();
            tempCommonUtil.ID = objCommonUtil.ID;
            tempCommonUtil.DocumentCode = objCommonUtil.DocumentCode;
            tempCommonUtil.DocumentName = objCommonUtil.DocumentName;
            tempCommonUtil.DocumentID = objCommonUtil.DocumentID;
            tempCommonUtil.StyleCode = objCommonUtil.StyleCode;
            tempCommonUtil.Prompt = objCommonUtil.Prompt;
            tempCommonUtil.Quantity = objCommonUtil.Quantity;
            tempCommonUtil.TypeName = objCommonUtil.TypeName;
            tempCommonUtil.TypeID = objCommonUtil.TypeID;
            tempCommonUtil.BuyQuantity = objCommonUtil.BuyQuantity;
            tempCommonUtil.GetQuantity = tempCommonUtil.GetQuantity;
            tempCommonUtil.Amount = objCommonUtil.Amount;
            tempCommonUtil.DiscountValue = objCommonUtil.DiscountValue;
            tempCommonUtil.Active = objCommonUtil.Active;
            tempCommonUtil.UpdateFlag = objCommonUtil.UpdateFlag;
            tempCommonUtil.DiscountType = objCommonUtil.DiscountType;
            tempCommonUtil.IsMandatory = objCommonUtil.IsMandatory;
            return tempCommonUtil;
        }
        public static List<CommonUtil> CommonUtilListDeepCopy(List<CommonUtil> objCommonUtilList)
        {

            var tempCommonUtilList = new List<CommonUtil>();
            foreach(CommonUtil objCommonUtil in objCommonUtilList )
            {
                var tempCommonUtil = new CommonUtil();
                tempCommonUtil.ID = objCommonUtil.ID;
                tempCommonUtil.DocumentCode = objCommonUtil.DocumentCode;
                tempCommonUtil.DocumentName = objCommonUtil.DocumentName;
                tempCommonUtil.DocumentID = objCommonUtil.DocumentID;
                tempCommonUtil.Prompt = objCommonUtil.Prompt;
                tempCommonUtil.Quantity = objCommonUtil.Quantity;
                tempCommonUtil.TypeName = objCommonUtil.TypeName;
                tempCommonUtil.Amount = objCommonUtil.Amount;
                tempCommonUtil.DiscountValue = objCommonUtil.DiscountValue;
                tempCommonUtil.Active = objCommonUtil.Active;
                tempCommonUtil.IsMandatory = objCommonUtil.IsMandatory;
                tempCommonUtilList.Add(tempCommonUtil);
            }
            return tempCommonUtilList;
        }
    }

    public static partial class DeepCopyCreator
    {

    }
}
