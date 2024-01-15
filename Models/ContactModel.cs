using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LostNFound.Models {
	public class ContactModel {
		[Required(ErrorMessage = "Es wird ein Vorname benötigt.")]
		[Display(Name = "Vorname:")]
		public string Firstname { get; set; }

		[Required(ErrorMessage = "Es wird ein Nachname benötigt.")]
		[Display(Name = "Nachname:")]
		public string Lastname { get; set; }

		[Required(ErrorMessage = "Es wird eine Klasse benötigt.")]
		[Display(Name = "Klasse:")]
		public string? Class { get; set; }

		[Required(ErrorMessage = "Es wird eine E-Mail benötigt.")]
		[Display(Name = "E-Mail:")]
		public string EMail { get; set; }

		[Display(Name = "Deshalb gehört mir der Gegenstand:")]
		public string? Note { get; set; }

		public int ItemId { get; set; }

		[Display(Name = "Ich akzeptiere die Bedingungen.")]
		[CheckBoxRequired(ErrorMessage = "Bitte akzeptiere die Bedingungen")]
		public bool HasAccepted { get; set; }
	}

	public class CheckBoxRequired : ValidationAttribute, IClientModelValidator {
		public override bool IsValid(object value) {
			if (value is bool) {
				return (bool)value;
			}

			return false;
		}

		public void AddValidation(ClientModelValidationContext context) {
			context.Attributes.Add("data-msg-required", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
		}
	}
}
