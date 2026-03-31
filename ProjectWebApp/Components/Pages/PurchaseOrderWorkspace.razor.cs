using Microsoft.AspNetCore.Components;

namespace ProjectWebApp.Components.Pages
{
    public partial class PurchaseOrderWorkspace
    {
        #region Properties

        [Parameter]
        public int VendorID { get; set; }

        #endregion
    }
}