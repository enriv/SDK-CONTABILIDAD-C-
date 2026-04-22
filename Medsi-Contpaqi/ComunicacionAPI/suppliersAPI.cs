using Newtonsoft.Json;
using Medsi_Contpaqi.Helpers;
using Medsi_Contpaqi.ModeloAPI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Medsi_Contpaqi.ComunicacionAPI
{
    public class SuppliersAPI
    {
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

        // ─── GET /suppliers ──────────────────────────────────────────────────────

        public static async Task<ApiResult<List<Supplier>>> ObtenerProveedoresAsync()
        {
            try
            {
                ConfigurarHeaders();
                HttpResponseMessage response = await _httpClient.GetAsync($"{AppHelper.BaseUrl}/suppliers");
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var resultado = JsonConvert.DeserializeObject<SuppliersListResponse>(json);
                    return new ApiResult<List<Supplier>>
                    {
                        Success = true,
                        Data = resultado?.Response ?? new List<Supplier>()
                    };
                }
                return new ApiResult<List<Supplier>>
                {
                    Success = false,
                    RawError = $"Error HTTP {(int)response.StatusCode}: {json}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<Supplier>> { Success = false, RawError = ex.Message };
            }
        }

        // ─── POST /suppliers/create_supplier ─────────────────────────────────────

        public static async Task<ApiResult<Supplier>> CrearProveedorAsync(SupplierRequest proveedor)
        {
            try
            {
                ConfigurarHeaders();
                string jsonBody = JsonConvert.SerializeObject(proveedor);
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(
                    $"{AppHelper.BaseUrl}/suppliers/create_supplier", content);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var resultado = JsonConvert.DeserializeObject<SupplierSingleResponse>(json);
                    return new ApiResult<Supplier> { Success = true, Data = resultado?.Response };
                }
                else if ((int)response.StatusCode == 422)
                {
                    var errores = JsonConvert.DeserializeObject<SupplierErrorResponse>(json);
                    return new ApiResult<Supplier> { Success = false, Errors = errores?.Errors };
                }
                return new ApiResult<Supplier>
                {
                    Success = false,
                    RawError = $"Error HTTP {(int)response.StatusCode}: {json}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<Supplier> { Success = false, RawError = ex.Message };
            }
        }

        // ─── PUT /suppliers/{id} ──────────────────────────────────────────────────

        public static async Task<ApiResult<Supplier>> ActualizarProveedorAsync(int id, SupplierRequest proveedor)
        {
            try
            {
                ConfigurarHeaders();
                string jsonBody = JsonConvert.SerializeObject(proveedor);
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(
                    $"{AppHelper.BaseUrl}/suppliers/{id}", content);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var resultado = JsonConvert.DeserializeObject<SupplierSingleResponse>(json);
                    return new ApiResult<Supplier> { Success = true, Data = resultado?.Response };
                }
                else if ((int)response.StatusCode == 422)
                {
                    var errores = JsonConvert.DeserializeObject<SupplierErrorResponse>(json);
                    return new ApiResult<Supplier> { Success = false, Errors = errores?.Errors };
                }
                return new ApiResult<Supplier>
                {
                    Success = false,
                    RawError = $"Error HTTP {(int)response.StatusCode}: {json}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<Supplier> { Success = false, RawError = ex.Message };
            }
        }

        // ─── Versiones síncronas ──────────────────────────────────────────────────

        public static ApiResult<List<Supplier>> ObtenerProveedores() =>
            Task.Run(() => ObtenerProveedoresAsync()).GetAwaiter().GetResult();

        public static ApiResult<Supplier> CrearProveedor(SupplierRequest proveedor) =>
            Task.Run(() => CrearProveedorAsync(proveedor)).GetAwaiter().GetResult();

        public static ApiResult<Supplier> ActualizarProveedor(int id, SupplierRequest proveedor) =>
            Task.Run(() => ActualizarProveedorAsync(id, proveedor)).GetAwaiter().GetResult();
    }
}