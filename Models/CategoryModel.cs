namespace LostNFound.Models {
	public class CategoryModelList {
		public List<CategoryModel> Categories { get; set; }
	}

	public class CategoryModel {
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
