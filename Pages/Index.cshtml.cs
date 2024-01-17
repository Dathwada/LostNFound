using AspNetCoreHero.ToastNotification.Abstractions;
using LostNFound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LostNFound.Pages {
	public class IndexModel(INotyfService notifyService) : PageModel {
		public INotyfService Toast { get; } = notifyService;
		public List<SelectListItem?> Categories = new();
		public List<Models.ItemModel>? Items = new();

		public async Task<IActionResult> OnGetAsync() {
			ApiService apiClient = new();

			// Fetch category data
			CategoryModelList? categoriesData = await apiClient.GetDataObject<CategoryModelList>("getCategories");
			if (categoriesData?.Categories != null) {
				// Add categories to SelectListItem list
				Categories.AddRange(
					categoriesData.Categories.Select(category => new SelectListItem { Text = category.Name })
				);
			}

			// Fetch items
			var itemList = await apiClient.GetDataObject<ItemModelList>("getItems");

			Items = itemList?.Items.FindAll(obj => obj.IsClaimed == false);

			return Page();
		}
	}
}
