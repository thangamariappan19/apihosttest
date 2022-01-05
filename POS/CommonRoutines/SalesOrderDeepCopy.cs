using EasyBizDBTypes.Transactions.POS.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class DeepCopyCreator
    {
        public static SalesOrderDetail SalesOrderDetailDeepCopy(SalesOrderDetail objSalesOrderDetail)
        {
            SalesOrderDetail TempSalesOrderDetail = new SalesOrderDetail();
            TempSalesOrderDetail.Active = objSalesOrderDetail.Active;
            TempSalesOrderDetail.AppVersion = objSalesOrderDetail.AppVersion;
            TempSalesOrderDetail.CreateBy = objSalesOrderDetail.CreateBy;
            TempSalesOrderDetail.CreatedByUserName = objSalesOrderDetail.CreatedByUserName;
            TempSalesOrderDetail.CreateOn = objSalesOrderDetail.CreateOn;
            TempSalesOrderDetail.ID = objSalesOrderDetail.ID;
            TempSalesOrderDetail.IsCountrySync = objSalesOrderDetail.IsCountrySync;
            TempSalesOrderDetail.IsDeleted = objSalesOrderDetail.IsDeleted;
            TempSalesOrderDetail.IsServerSync = objSalesOrderDetail.IsServerSync;
            TempSalesOrderDetail.IsStoreSync = objSalesOrderDetail.IsStoreSync;
            TempSalesOrderDetail.Price = objSalesOrderDetail.Price;
            TempSalesOrderDetail.Qty = objSalesOrderDetail.Qty;
            TempSalesOrderDetail.Remarks = objSalesOrderDetail.Remarks;
            TempSalesOrderDetail.SalesOrderDocumentNo = objSalesOrderDetail.SalesOrderDocumentNo;
            TempSalesOrderDetail.SalesOrderID = objSalesOrderDetail.SalesOrderID;
            TempSalesOrderDetail.SCN = objSalesOrderDetail.SCN;
            TempSalesOrderDetail.SellingLineTotal = objSalesOrderDetail.SellingLineTotal;
            TempSalesOrderDetail.SerialNo = objSalesOrderDetail.SerialNo;
            TempSalesOrderDetail.SKUCode = objSalesOrderDetail.SKUCode;
            TempSalesOrderDetail.Status = objSalesOrderDetail.Status;
            TempSalesOrderDetail.StyleCode = objSalesOrderDetail.StyleCode;
            TempSalesOrderDetail.UpdateBy = objSalesOrderDetail.UpdateBy;
            TempSalesOrderDetail.UpdatedByUserName = objSalesOrderDetail.UpdatedByUserName;
            TempSalesOrderDetail.UpdateOn = objSalesOrderDetail.UpdateOn;
            return TempSalesOrderDetail;
        }

        public static List<SalesOrderDetail> SalesOrderDetailListDeepCopy(List<SalesOrderDetail> objSalesOrderDetailList)
        {
            var TempSalesOrderDetailList = new List<SalesOrderDetail>();
            foreach (SalesOrderDetail objSalesOrderDetail in objSalesOrderDetailList)
            {
                SalesOrderDetail TempSalesOrderDetail = new SalesOrderDetail();

                TempSalesOrderDetail.Active = objSalesOrderDetail.Active;
                TempSalesOrderDetail.AppVersion = objSalesOrderDetail.AppVersion;
                TempSalesOrderDetail.CreateBy = objSalesOrderDetail.CreateBy;
                TempSalesOrderDetail.CreatedByUserName = objSalesOrderDetail.CreatedByUserName;
                TempSalesOrderDetail.CreateOn = objSalesOrderDetail.CreateOn;
                TempSalesOrderDetail.ID = objSalesOrderDetail.ID;
                TempSalesOrderDetail.IsCountrySync = objSalesOrderDetail.IsCountrySync;
                TempSalesOrderDetail.IsDeleted = objSalesOrderDetail.IsDeleted;
                TempSalesOrderDetail.IsServerSync = objSalesOrderDetail.IsServerSync;
                TempSalesOrderDetail.IsStoreSync = objSalesOrderDetail.IsStoreSync;
                TempSalesOrderDetail.Price = objSalesOrderDetail.Price;
                TempSalesOrderDetail.Qty = objSalesOrderDetail.Qty;
                TempSalesOrderDetail.Remarks = objSalesOrderDetail.Remarks;
                TempSalesOrderDetail.SalesOrderDocumentNo = objSalesOrderDetail.SalesOrderDocumentNo;
                TempSalesOrderDetail.SalesOrderID = objSalesOrderDetail.SalesOrderID;
                TempSalesOrderDetail.SCN = objSalesOrderDetail.SCN;
                TempSalesOrderDetail.SellingLineTotal = objSalesOrderDetail.SellingLineTotal;
                TempSalesOrderDetail.SerialNo = objSalesOrderDetail.SerialNo;
                TempSalesOrderDetail.SKUCode = objSalesOrderDetail.SKUCode;
                TempSalesOrderDetail.Status = objSalesOrderDetail.Status;
                TempSalesOrderDetail.StyleCode = objSalesOrderDetail.StyleCode;
                TempSalesOrderDetail.UpdateBy = objSalesOrderDetail.UpdateBy;
                TempSalesOrderDetail.UpdatedByUserName = objSalesOrderDetail.UpdatedByUserName;
                TempSalesOrderDetail.UpdateOn = objSalesOrderDetail.UpdateOn;

                TempSalesOrderDetailList.Add(TempSalesOrderDetail);
            }
            return TempSalesOrderDetailList;
        }
    }
}
