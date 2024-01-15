using AspNetCoreHero.ToastNotification.Abstractions;
using LostNFound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LostNFound.Pages {

		public int? Id { get; set; }
		public int CategoryId { get; set; }
		public string? Description { get; set; }
		public string ImagePath { get; set; }
		public DateTime DateFound { get; set; }
		public string LocationFound { get; set; }

		public IActionResult OnGet(int id) {
	public class ItemModel(INotyfService notifyService) : PageModel {
		public INotyfService Toast { get; } = notifyService;
		public int ItemId { get; set; }
		public Models.ContactModel Contact { get; set; } = new();

			// ONLY FOR TESTING
			Id = id;
			CategoryId = 1;
			Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
			ImagePath = "https://xn--freundeskreis-flchtlinge-lahr-0bd.de/wp-content/uploads/2020/02/Gegenst%C3%A4nde-Schl%C3%BCsselbund.jpg";
			DateFound = DateTime.Now;
			LocationFound = "Raum 42";

			if (Id == null) {
				return RedirectToPage("NotFound");
			ItemId = item - 1;

		public IActionResult OnPostContactForm(ContactModel Contact) {
			if (ModelState.IsValid) {
				try {
					// Call API to insert data
				} catch (Exception) {
					Toast.Error("Leider ist ein Fehler aufgetretten.");
				}

				Toast.Success("Ihre Anfrage wurde gesendet.");
				return RedirectToPage($"Item", new { item = Contact.ItemId });
			}

			return Page();
		}
	}
}
