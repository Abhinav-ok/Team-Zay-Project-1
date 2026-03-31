using Microsoft.AspNetCore.Components;
using PurchasingSystem.BLL;
using PurchasingSystem.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ProjectWebApp.Components.Pages
{
    public partial class VendorSelection
    {
        #region Fields
        protected List<VendorSelectionView> vendors = new();

        #endregion
        #region Properties
        [Inject]
        protected PurchasingService PurchasingService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        #endregion
        #region Methods

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            vendors = PurchasingService.GetActiveVendors();
        }
        protected void SelectVendor(int vendorID)
        {
            NavigationManager.NavigateTo($"/purchaseorderworkspace/{vendorID}");
        }
    }

        #endregion
    
}
