using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LostNFound.Pages {
	public class IndexModel(ILogger<IndexModel> logger, INotyfService notifyService) : PageModel {
		private readonly ILogger<IndexModel> _logger = logger;
		public INotyfService Toast { get; } = notifyService;

		public void OnGet() {

		}
	}
}
