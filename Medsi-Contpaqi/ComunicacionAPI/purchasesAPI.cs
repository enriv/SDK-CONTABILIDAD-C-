using Medsi_Contpaqi.Helpers;
using Medsi_Contpaqi.ModeloAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Medsi_Contpaqi.ComunicacionAPI
{
    public class PurchasesAPI
    {
        private const string BaseUrl = "https://cmenonitatest.medsi.com.mx/api/menonita";
        private static readonly HttpClient _httpClient = new HttpClient();

        private static void ConfigurarHeaders()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(AppHelper.TokenAPI))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AppHelper.TokenAPI);
        }

        // ─── POST /suppliers/purchases ───────────────────────────────────────

        public static async Task<ApiResult<List<Purchase>>> ObtenerComprasAsync(string fecha)
        {
            try
            {
                ConfigurarHeaders();
                string url = $"{BaseUrl}/suppliers/purchases";
                var body = new { search_date = fecha };
                string jsonBody = JsonConvert.SerializeObject(body);
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var resultado = JsonConvert.DeserializeObject<PurchasesResponse>(json);
                    return new ApiResult<List<Purchase>>
                    {
                        Success = true,
                        Data = resultado?.Response ?? new List<Purchase>()
                    };
                }
                else
                {
                    return new ApiResult<List<Purchase>>
                    {
                        Success = false,
                        RawError = $"Error HTTP {(int)response.StatusCode}: {json}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<List<Purchase>> { Success = false, RawError = ex.Message };
            }
        }

        public static ApiResult<List<Purchase>> ObtenerCompras(string fecha)
        {
            return Task.Run(() => ObtenerComprasAsync(fecha)).GetAwaiter().GetResult();
        }
    }
}
