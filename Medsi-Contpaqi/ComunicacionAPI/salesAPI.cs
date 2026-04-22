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
    public class SalesAPI
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

        // ─── POST /suppliers/sales ───────────────────────────────────────────

        public static async Task<ApiResult<List<Sale>>> ObtenerVentasAsync(string fecha)
        {
            try
            {
                ConfigurarHeaders();
                string url = $"{BaseUrl}/suppliers/sales";
                var body = new { search_date = fecha };
                string jsonBody = JsonConvert.SerializeObject(body);
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var resultado = JsonConvert.DeserializeObject<SalesResponse>(json);
                    return new ApiResult<List<Sale>>
                    {
                        Success = true,
                        Data = resultado?.Response ?? new List<Sale>()
                    };
                }
                else
                {
                    return new ApiResult<List<Sale>>
                    {
                        Success = false,
                        RawError = $"Error HTTP {(int)response.StatusCode}: {json}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<List<Sale>> { Success = false, RawError = ex.Message };
            }
        }

        public static ApiResult<List<Sale>> ObtenerVentas(string fecha)
        {
            return Task.Run(() => ObtenerVentasAsync(fecha)).GetAwaiter().GetResult();
        }
    }
}
