using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medsi_Contpaqi.ModeloAPI
{
    public class PurchaseImpuesto
    {
        [JsonProperty("importe")]
        public decimal Importe { get; set; }

        [JsonProperty("impuesto")]
        public string ImpuestoNombre { get; set; }

        [JsonProperty("tipoFactor")]
        public string TipoFactor { get; set; }

        [JsonProperty("tasaOCuota")]
        public decimal TasaOCuota { get; set; }

        [JsonProperty("base")]
        public decimal Base { get; set; }
    }

    public class PurchasePartida
    {
        [JsonProperty("producto")]
        public string Producto { get; set; }

        [JsonProperty("cantidad")]
        public decimal Cantidad { get; set; }

        [JsonProperty("precioPorUnidad")]
        public decimal PrecioPorUnidad { get; set; }

        [JsonProperty("claveProductoSAT")]
        public string ClaveProductoSAT { get; set; }

        [JsonProperty("unidadMedida")]
        public string UnidadMedida { get; set; }

        [JsonProperty("claveUnidadSAT")]
        public string ClaveUnidadSAT { get; set; }

        [JsonProperty("impuestosTrasladados")]
        public List<PurchaseImpuesto> ImpuestosTrasladados { get; set; }

        [JsonProperty("impuestosRetenidos")]
        public List<PurchaseImpuesto> ImpuestosRetenidos { get; set; }
    }

    public class PurchaseTimbre
    {
        [JsonProperty("folioFiscal")]
        public string FolioFiscal { get; set; }

        [JsonProperty("fechaHoraTimbrado")]
        public string FechaHoraTimbrado { get; set; }
    }

    public class Purchase
    {
        [JsonProperty("serie")]
        public string Serie { get; set; }

        [JsonProperty("folio")]
        public int Folio { get; set; }

        [JsonProperty("codigoConcepto")]
        public string CodigoConcepto { get; set; }

        [JsonProperty("rfcClienteProveedor")]
        public string RfcClienteProveedor { get; set; }

        [JsonProperty("razonSocialClienteProveedor")]
        public string RazonSocialClienteProveedor { get; set; }

        [JsonProperty("fechaDocumento")]
        public string FechaDocumento { get; set; }

        [JsonProperty("usoCFDi")]
        public string UsoCFDi { get; set; }

        [JsonProperty("metodoPago")]
        public string MetodoPago { get; set; }

        [JsonProperty("formaPago")]
        public string FormaPago { get; set; }

        [JsonProperty("referencia")]
        public string Referencia { get; set; }

        [JsonProperty("observaciones")]
        public string Observaciones { get; set; }

        [JsonProperty("timbre")]
        public PurchaseTimbre Timbre { get; set; }

        [JsonProperty("partidas")]
        public List<PurchasePartida> Partidas { get; set; }
    }

    public class PurchasesResponse
    {
        [JsonProperty("response")]
        public List<Purchase> Response { get; set; }
    }
}
