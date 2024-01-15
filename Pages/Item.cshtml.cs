using AspNetCoreHero.ToastNotification.Abstractions;
using LostNFound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.Net;

namespace LostNFound.Pages {
	public class ItemModel(INotyfService notifyService) : PageModel {
		public INotyfService Toast { get; } = notifyService;
		public int ItemId { get; set; }
		public Models.ItemModel Item { get; set; } = new();
		public ContactModel Contact { get; set; } = new();

		public async Task<IActionResult> OnGetAsync(int item) {
			// ONLY FOR TESTING
			ItemId = item - 1;

			ApiService apiClient = new();
			var ItemList = await apiClient.GetDataObject<ItemModelList>("items");

			if (ItemList == null || ItemList.Items.Count <= ItemId) {
				Toast.Error("Es konnten leider keine Daten für den gesuchten Gegenstand gefunden werden.");
				return RedirectToPage("Index");
			}

			Item = ItemList.Items[ItemId];

			return Page();
		}

		public async Task<IActionResult> OnPostContactForm(ContactModel Contact) {
			if (ModelState.IsValid) {
				try {
					ApiService apiClient = new();
					HttpStatusCode statusCode = await apiClient.PostDataObject("addClaimant", Contact);

					if (statusCode == HttpStatusCode.OK) {
						Toast.Success("Ihre Anfrage wurde gesendet.");
					} else {
						throw new Exception($"StatusCode war nicht OK. StatusCode: {statusCode}");
					}

				} catch (Exception ex) {
					Log.Error($"Message: {ex.Message}\n    StackTrace: {ex.StackTrace}");
					Toast.Error("Beim Senden Ihrer Anfrage ist ein Fehler aufgetreten.");
				}

				return RedirectToPage($"Item", new { item = Contact.ItemId });
			}

			return Page();
		}
	}
}
