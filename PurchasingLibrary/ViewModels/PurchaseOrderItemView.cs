using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingSystem.ViewModels
{
    public class PurchaseOrderItemView
    {
        public int PartID { get; set; }
        public string Description { get; set; }
        public int QOH { get; set; }
        public int QOO { get; set; }
        public int ROL { get; set; }
        public int QTO { get; set; }
        public decimal PurchasePrice { get; set; }
        public string VendorPartNumber { get; set; }
    }
}
