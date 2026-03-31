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
    }
}
