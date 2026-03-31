using Microsoft.AspNetCore.Components;
using PurchasingSystem.BLL;
using PurchasingSystem.ViewModels;
using System.Threading.Tasks;

namespace ProjectWebApp.Components.Pages
{
    public partial class PurchaseOrderWorkspace
    {
        #region Fields

        protected PurchaseOrderWorkspaceView workspace = new();

        #endregion

        #region Properties

        [Parameter]
        public int VendorID { get; set; }

        [Inject]
        protected PurchasingService PurchasingService { get; set; }

        #endregion

        #region Methods

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            workspace = PurchasingService.GetVendorHeader(VendorID);
        }

        #endregion
    }
}