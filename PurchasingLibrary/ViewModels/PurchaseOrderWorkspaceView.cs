using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingSystem.ViewModels
{
    public class PurchaseOrderWorkspaceView
    {
        // Info of vendorr
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ProvinceID { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }

        // Purchase Order Info
        public int PurchaseOrderID { get; set; }
        public string EmployeeID { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }

        // Child Lists
        public List<PurchaseOrderItemView> CurrentOrderItems { get; set; }
        public List<AvailablePurchaseItemView> AvailableParts { get; set; }
    }
}
   