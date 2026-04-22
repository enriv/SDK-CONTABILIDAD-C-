using Newtonsoft.Json;
using System.Collections.Generic;

namespace Medsi_Contpaqi.ModeloAPI
{
    public class SupplierBankDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("institution")]
        public string Institution { get; set; }

        [JsonProperty("clabe")]
        public string Clabe { get; set; }
    }

    public class Supplier
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("telephone")]
        public string Telephone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("clinic_info_id")]
        public int ClinicInfoId { get; set; }

        [JsonProperty("paym_term")]
        public int PaymTerm { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("include_portal")]
        public bool IncludePortal { get; set; }

        [JsonProperty("business_name")]
        public string BusinessName { get; set; }

        [JsonProperty("rfc")]
        public string Rfc { get; set; }

        [JsonProperty("is_disabled")]
        public bool IsDisabled { get; set; }

        [JsonProperty("apply_isr")]
        public bool ApplyIsr { get; set; }

        [JsonProperty("isr_amount")]
        public string IsrAmount { get; set; }

        [JsonProperty("send_contpaq")]
        public bool SendContpaq { get; set; }
    }

    public class SupplierRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("telephone")]
        public string Telephone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }

        [JsonProperty("paym_term")]
        public string PaymTerm { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("rfc")]
        public string Rfc { get; set; }

        [JsonProperty("supplier_bank_details")]
        public List<SupplierBankDetail> SupplierBankDetails { get; set; }
    }

    public class SuppliersListResponse
    {
        [JsonProperty("response")]
        public List<Supplier> Response { get; set; }
    }

    public class SupplierSingleResponse
    {
        [JsonProperty("response")]
        public Supplier Response { get; set; }
    }

    public class SupplierErrorResponse
    {
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }

    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public string RawError { get; set; }
    }
}