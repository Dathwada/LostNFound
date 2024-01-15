using Newtonsoft.Json;
using Serilog;
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
					Log.Error($"Error: {response.StatusCode} - {response.ReasonPhrase}");
					return null;
				}
			} catch (Exception ex) {
				Log.Error($"Message: {ex.Message}\n    StackTrace: {ex.StackTrace}");
				return null;
			}
		}

		public async Task<T?> GetDataObject<T>(string endpoint) {
			string? apiResponse = await GetDataFromApi(endpoint);

			if (apiResponse != null) {
				try {
					T? data = JsonConvert.DeserializeObject<T>(apiResponse);
					return data;
				} catch (Exception ex) {
					Log.Error($"Message: {ex.Message}\n    StackTrace: {ex.StackTrace}");
					return default;
				}
			}

			return default;
		}

		public async Task<HttpStatusCode> PostDataObject<T>(string endpoint, T data) {
			string jsonData = JsonConvert.SerializeObject(data);

			using (var client = this._httpClient) {
				StringContent content = new(jsonData, Encoding.UTF8, "application/json");

				// Send POST
				HttpResponseMessage response = await client.PostAsync($"{this._baseUrl}/{endpoint}", content);

				// Check StatusCode
				if (response.IsSuccessStatusCode) {
					Log.Information($"Message: Daten erfolgreich an den API-Endpunkt '{endpoint}' gesendet.");
				} else {
					Log.Error($"Message: Fehler beim Senden der Daten. Statuscode: {response.StatusCode}");
				}

				return response.StatusCode;
			}

		}
	}
}