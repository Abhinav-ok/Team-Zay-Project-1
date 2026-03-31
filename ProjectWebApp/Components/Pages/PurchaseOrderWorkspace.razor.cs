using Microsoft.AspNetCore.Components;
using PurchasingSystem.BLL;
using PurchasingSystem.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

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

            var existingPOID = PurchasingService.GetExistingPurchaseOrderID(VendorID);

            if (existingPOID != null)
            {
                workspace.PurchaseOrderID = existingPOID.Value;
                workspace.CurrentOrderItems = PurchasingService.GetCurrentOrderItems(existingPOID.Value);
            }
            else
            {
                workspace.CurrentOrderItems = PurchasingService.GetSuggestedOrderItems(VendorID);
            }

            workspace.AvailableParts = PurchasingService.GetAvailableParts(VendorID, workspace.CurrentOrderItems);
            CalculateTotals();
        }
        protected void AddPart(int partID)
        {
            var selectedPart = workspace.AvailableParts
                .Where(x => x.PartID == partID)
                .FirstOrDefault();

            if (selectedPart != null)
            {
                workspace.CurrentOrderItems.Add(new PurchaseOrderItemView
                {
                    PartID = selectedPart.PartID,
                    Description = selectedPart.Description,
                    QOH = selectedPart.QOH,
                    QOO = selectedPart.QOO,
                    ROL = selectedPart.ROL,
                    QTO = 0,
                    PurchasePrice = selectedPart.PurchasePrice,
                    VendorPartNumber = string.Empty
                });

                workspace.CurrentOrderItems = workspace.CurrentOrderItems.OrderBy(x => x.Description).ToList();

                workspace.AvailableParts.Remove(selectedPart);

                workspace.AvailableParts = workspace.AvailableParts.OrderBy(x => x.Description).ToList();

            }
            CalculateTotals();
        }

        protected void RemovePart(int partID)
        {
            var selectedPart = workspace.CurrentOrderItems.Where(x => x.PartID == partID).FirstOrDefault();

            if (selectedPart != null)
            {
                workspace.AvailableParts.Add(new AvailablePurchaseItemView
                {
                    PartID = selectedPart.PartID,
                    Description = selectedPart.Description,
                    QOH = selectedPart.QOH,
                    QOO = selectedPart.QOO,
                    ROL = selectedPart.ROL,
                    Buffer = 0,
                    PurchasePrice = selectedPart.PurchasePrice
                });

                workspace.AvailableParts = workspace.AvailableParts
                    .OrderBy(x => x.Description) .ToList();

                workspace.CurrentOrderItems.Remove(selectedPart);

                workspace.CurrentOrderItems = workspace.CurrentOrderItems
                    .OrderBy(x => x.Description).ToList();
            }
            CalculateTotals();
        }
        protected void CalculateTotals()
        {
            workspace.SubTotal = workspace.CurrentOrderItems.Sum(x => x.QTO * x.PurchasePrice);
            workspace.TaxAmount = workspace.SubTotal * 0.05m;

            workspace.Total = workspace.SubTotal + workspace.TaxAmount;
        }
        protected void UpdateQuantity(PurchaseOrderItemView item, int quantity)
        {
            item.QTO = quantity;
            CalculateTotals();
        }

        protected void UpdatePrice(PurchaseOrderItemView item, decimal price)
        {
            item.PurchasePrice = price;
            CalculateTotals();
        }

        #endregion
    }
}