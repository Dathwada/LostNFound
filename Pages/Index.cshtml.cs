using AspNetCoreHero.ToastNotification.Abstractions;
using LostNFound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LostNFound.Pages {
	public class IndexModel(ILogger<IndexModel> logger, INotyfService notifyService) : PageModel {
		private readonly ILogger<IndexModel> _logger = logger;
		public INotyfService Toast { get; } = notifyService;
		public List<SelectListItem?> Categories = new();
		public ItemModelList? Items = new();

		public async Task<IActionResult> OnGetAsync() {
			ApiService apiClient = new();

			// Fetch category data
			CategoryModelList? categoriesData = await apiClient.GetDataObject<CategoryModelList>("categories");
			if (categoriesData?.Categories != null) {
				// Add categories to SelectListItem list
				Categories.AddRange(
					categoriesData.Categories.Select(category => new SelectListItem { Text = category.Name })
				);
			}

			// Fetch items
			Items = await apiClient.GetDataObject<ItemModelList>("items");

			return Page();
		}
	}
}
