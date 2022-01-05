using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class DeepCopyCreator
    {
        public static InventoryCountingHeader InventoryCountingHeaderDeepCopy(InventoryCountingHeader objInventoryCountingHeader)
        {
            var _InventoryCountingHeader = new InventoryCountingHeader();
            _InventoryCountingHeader.ID = objInventoryCountingHeader.ID;
            _InventoryCountingHeader.DocumentNumber = objInventoryCountingHeader.DocumentNumber;
            _InventoryCountingHeader.DocumentDate = objInventoryCountingHeader.DocumentDate;
            _InventoryCountingHeader.StoreID = objInventoryCountingHeader.StoreID;
            _InventoryCountingHeader.PostingDone = objInventoryCountingHeader.PostingDone;
            _InventoryCountingHeader.PostingDate = objInventoryCountingHeader.PostingDate;
            _InventoryCountingHeader.InventoryCountingDetailList = objInventoryCountingHeader.InventoryCountingDetailList;
            return _InventoryCountingHeader;
        }
        public static InventoryCountingDetails InventoryCountingDetailsDeepCopy(InventoryCountingDetails objInventoryCountingDetails)
        {
            var _InventoryCountingDetails = new InventoryCountingDetails();
            _InventoryCountingDetails.ID = objInventoryCountingDetails.ID;
            _InventoryCountingDetails.InventoryCountingID = objInventoryCountingDetails.InventoryCountingID;
            _InventoryCountingDetails.SKUCode = objInventoryCountingDetails.SKUCode;
            _InventoryCountingDetails.BarCode = objInventoryCountingDetails.BarCode;
            _InventoryCountingDetails.StyleCode = objInventoryCountingDetails.StyleCode;
            _InventoryCountingDetails.BrandCode = objInventoryCountingDetails.BrandCode;
            _InventoryCountingDetails.ColorCode = objInventoryCountingDetails.ColorCode;
            _InventoryCountingDetails.SizeCode = objInventoryCountingDetails.SizeCode;
            _InventoryCountingDetails.StoreID = objInventoryCountingDetails.StoreID;
            _InventoryCountingDetails.StoreName = objInventoryCountingDetails.StoreName;
            _InventoryCountingDetails.SystemQuantity = objInventoryCountingDetails.SystemQuantity;
            _InventoryCountingDetails.PhysicalQuantity = objInventoryCountingDetails.PhysicalQuantity;
            _InventoryCountingDetails.DifferenceQuantity = objInventoryCountingDetails.DifferenceQuantity;
            _InventoryCountingDetails.CreateBy = objInventoryCountingDetails.CreateBy;
            _InventoryCountingDetails.CreateOn = objInventoryCountingDetails.CreateOn;
            _InventoryCountingDetails.UpdateBy = objInventoryCountingDetails.UpdateBy;
            _InventoryCountingDetails.UpdateOn = objInventoryCountingDetails.UpdateOn;
            _InventoryCountingDetails.SCN = objInventoryCountingDetails.SCN;
            _InventoryCountingDetails.Active = objInventoryCountingDetails.Active;
            return _InventoryCountingDetails;
        }
        public static List<InventoryCountingDetails> InventoryCountingDetailListDeepCopy(List<InventoryCountingDetails> InventoryCountingDetailsList)
        {
            var _InventoryCountingDetailsList = new List<InventoryCountingDetails>();
            foreach (InventoryCountingDetails objInventoryCountingDetails in InventoryCountingDetailsList)
            {
                var _InventoryCountingDetails = new InventoryCountingDetails();
                _InventoryCountingDetails.ID = objInventoryCountingDetails.ID;
                _InventoryCountingDetails.InventoryCountingID = objInventoryCountingDetails.InventoryCountingID;
                _InventoryCountingDetails.SKUCode = objInventoryCountingDetails.SKUCode;
                _InventoryCountingDetails.BarCode = objInventoryCountingDetails.BarCode;
                _InventoryCountingDetails.StyleCode = objInventoryCountingDetails.StyleCode;
                _InventoryCountingDetails.BrandCode = objInventoryCountingDetails.BrandCode;
                _InventoryCountingDetails.ColorCode = objInventoryCountingDetails.ColorCode;
                _InventoryCountingDetails.SizeCode = objInventoryCountingDetails.SizeCode;
                _InventoryCountingDetails.StoreID = objInventoryCountingDetails.StoreID;
                _InventoryCountingDetails.StoreName = objInventoryCountingDetails.StoreName;
                _InventoryCountingDetails.SystemQuantity = objInventoryCountingDetails.SystemQuantity;
                _InventoryCountingDetails.PhysicalQuantity = objInventoryCountingDetails.PhysicalQuantity;
                _InventoryCountingDetails.DifferenceQuantity = objInventoryCountingDetails.DifferenceQuantity;
                _InventoryCountingDetails.CreateBy = objInventoryCountingDetails.CreateBy;
                _InventoryCountingDetails.CreateOn = objInventoryCountingDetails.CreateOn;
                _InventoryCountingDetails.UpdateBy = objInventoryCountingDetails.UpdateBy;
                _InventoryCountingDetails.UpdateOn = objInventoryCountingDetails.UpdateOn;
                _InventoryCountingDetails.SCN = objInventoryCountingDetails.SCN;
                _InventoryCountingDetails.Active = objInventoryCountingDetails.Active;

                _InventoryCountingDetailsList.Add(_InventoryCountingDetails);
            }
            return _InventoryCountingDetailsList;
        }
        public static InventoryManualCountDetail InventoryManualCountDetailDeepCopy(InventoryManualCountDetail ObjInventoryManualCountDetail)
        {
            var _InventoryManualCountDetail = new InventoryManualCountDetail();
            _InventoryManualCountDetail.ID = ObjInventoryManualCountDetail.ID;
            _InventoryManualCountDetail.InventoryManualCountID = ObjInventoryManualCountDetail.InventoryManualCountID;
            _InventoryManualCountDetail.LocationID = ObjInventoryManualCountDetail.LocationID;
            _InventoryManualCountDetail.SheetName = ObjInventoryManualCountDetail.SheetName;
            _InventoryManualCountDetail.BarCode = ObjInventoryManualCountDetail.BarCode;
            _InventoryManualCountDetail.SKUCode = ObjInventoryManualCountDetail.SKUCode;
            _InventoryManualCountDetail.StockQty = ObjInventoryManualCountDetail.StockQty;
            _InventoryManualCountDetail.StoreID = ObjInventoryManualCountDetail.StoreID;
            return _InventoryManualCountDetail;
        }
    }
}
