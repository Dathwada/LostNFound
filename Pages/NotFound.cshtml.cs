using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LostNFound.Pages {
	public class NotFoundModel(INotyfService notifyService) : PageModel {
		public INotyfService Toast { get; } = notifyService;

		public void OnGet() {
		}
	}
}
