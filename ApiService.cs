using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace LostNFound {
	public class ApiService {
		private readonly HttpClient _httpClient;
		private readonly string _baseUrl;

		public ApiService() {
			_httpClient = new HttpClient();
			_baseUrl = "http://127.0.0.1:5000/api";
		}

		public ApiService(string baseUrl) {
			_httpClient = new HttpClient();
			_baseUrl = baseUrl;
		}

		private async Task<string?> GetDataFromApi(string endpoint) {
			string apiUrl = $"{_baseUrl}/{endpoint}";

			try {
				HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

				if (response.IsSuccessStatusCode) {
					return await response.Content.ReadAsStringAsync();
				} else {
					Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
					return null;
				}
			} catch (Exception ex) {
				Console.WriteLine($"Exception: {ex.Message}");
				return null;
			}
		}

		public async Task<T?> GetDataObject<T>(string endpoint) {
			string? apiResponse = await GetDataFromApi(endpoint);

			if (apiResponse != null) {
				try {
					T? data = JsonConvert.DeserializeObject<T>(apiResponse);
					return data;
				} catch (Exception) {
					return default;
				}
			}

			return default;
		}

		public async Task<HttpStatusCode> PostDataObject<T>(string endpoint, T data) {
			// Die Daten, die du senden möchtest
			string jsonData = JsonConvert.SerializeObject(data);

			// Erstelle eine Instanz von HttpClient
			using (var client = this._httpClient) {
				// Setze den Inhalt der Anfrage
				StringContent content = new(jsonData, Encoding.UTF8, "application/json");

				// Sende die POST-Anfrage
				HttpResponseMessage response = await client.PostAsync($"{this._baseUrl}/{endpoint}", content);

				// Überprüfe die Antwort
				if (response.IsSuccessStatusCode) {
					//Console.WriteLine("Daten erfolgreich an den API-Endpunkt gesendet.");
				} else {
					//Console.WriteLine($"Fehler beim Senden der Daten. Statuscode: {response.StatusCode}");
				}

				return response.StatusCode;
			}

		}
	}
}