using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchasingSystem.Models;
using PurchasingSystem.ViewModels;


namespace PurchasingSystem.BLL
{
    public class PurchasingService
    {
        private readonly eBike_2026CleanContext _context;

        public PurchasingService(eBike_2026CleanContext context)
        {
            _context = context;
        }

        public List<VendorSelectionView> GetActiveVendors()
        {
            return _context.Vendors.Where(x => x.RemoveFromViewFlag == false).OrderBy(x => x.VendorName).Select(x => new VendorSelectionView
                {   VendorID = x.VendorId,
                    VendorName = x.VendorName,
                    Address = x.Address,
                    City = x.City,
                    ProvinceID = x.ProvinceId,
                    PostalCode = x.PostalCode,
                    Phone = x.Phone }).ToList();
        }
        public PurchaseOrderWorkspaceView GetVendorHeader(int vendorID)
        {
            return _context.Vendors
                .Where(x => x.VendorId == vendorID && x.RemoveFromViewFlag == false)
                .Select(x => new PurchaseOrderWorkspaceView
                {
                    VendorID = x.VendorId,
                    VendorName = x.VendorName,
                    Address = x.Address,
                    City = x.City,
                    ProvinceID = x.ProvinceId,
                    PostalCode = x.PostalCode,
                    Phone = x.Phone,
                    PurchaseOrderID = 0,
                    EmployeeID = string.Empty,
                    SubTotal = 0,
                    TaxAmount = 0,
                    Total = 0,
                    CurrentOrderItems = new List<PurchaseOrderItemView>(),
                    AvailableParts = new List<AvailablePurchaseItemView>()
                })
                .FirstOrDefault();
        }
        public int? GetExistingPurchaseOrderID(int vendorID)
        {
            var result = _context.PurchaseOrders
                .Where(x => x.VendorId == vendorID && x.Closed == false)
                .Select(x => x.PurchaseOrderId)
                .FirstOrDefault();

            if (result == 0)
                return null;

            return result;
        }
        public List<PurchaseOrderItemView> GetSuggestedOrderItems(int vendorID)
        {
            return _context.Parts
                .Where(x => x.VendorId == vendorID
                            && x.RemoveFromViewFlag == false
                            && x.ReorderLevel > (x.QuantityOnHand + x.QuantityOnOrder))
                .OrderBy(x => x.Description)
                .Select(x => new PurchaseOrderItemView
                {
                    PartID = x.PartId,
                    Description = x.Description,
                    QOH = x.QuantityOnHand,
                    QOO = x.QuantityOnOrder,
                    ROL = x.ReorderLevel,
                    QTO = x.ReorderLevel - (x.QuantityOnHand + x.QuantityOnOrder),
                    PurchasePrice = x.PurchasePrice,
                    VendorPartNumber = string.Empty
                })
                .ToList();
        }
        public List<PurchaseOrderItemView> GetCurrentOrderItems(int purchaseOrderID)
        {
            return _context.PurchaseOrderDetails .Where(x => x.PurchaseOrderId == purchaseOrderID&& x.RemoveFromViewFlag == false)
                .OrderBy(x => x.Part.Description)
                .Select(x => new PurchaseOrderItemView
                {
                    PartID = x.PartId,
                    Description = x.Part.Description,
                    QOH = x.Part.QuantityOnHand,
                    QOO = x.Part.QuantityOnOrder,
                    ROL = x.Part.ReorderLevel,
                    QTO = x.Quantity,
                    PurchasePrice = x.PurchasePrice,
                    VendorPartNumber = x.VendorPartNumber
                }).ToList();
        }
        public List<AvailablePurchaseItemView> GetAvailableParts(int vendorID, List<PurchaseOrderItemView> currentOrderItems)
        {
            var currentPartIDs = currentOrderItems.Select(x => x.PartID).ToList();
            return _context.Parts.Where(x => x.VendorId == vendorID && x.RemoveFromViewFlag == false && !currentPartIDs.Contains(x.PartId))
                .OrderBy(x => x.Description).Select(x => new AvailablePurchaseItemView
                {
                    PartID = x.PartId,
                    Description = x.Description,
                    QOH = x.QuantityOnHand,
                    QOO = x.QuantityOnOrder,
                    ROL = x.ReorderLevel,
                    Buffer = 0,
                    PurchasePrice = x.PurchasePrice
                }).ToList();
        }
    }
}
