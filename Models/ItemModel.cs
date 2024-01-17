namespace LostNFound.Models {

	public class ItemModelList {
		public List<ItemModel> Items { get; set; }
	}

	public class ItemModel {
		public int Id { get; set; }

		public string Category { get; set; }

		public string? Description { get; set; }

		public string ImagePath { get; set; }

		public DateTime DateFound { get; set; }

		public string LocationFound { get; set; }

		// public int FinderId { get; set; }
		// public int? ClaimedUserId { get; set; }
		// public DateTime? ClaimedDate { get; set; }
		 public bool IsClaimed { get; set; }
	}
}
