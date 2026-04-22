using Medsi_Contpaqi.SDK;
using SDKCONTPAQNGLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medsi_Contpaqi.Helpers
{
    public static class AppHelper
    {
        // ─── SESION ───────────────────────────────────────────────────────────
        public static TSdkSesion SdkSesion { get; set; }

        // ─── EMPRESA SELECCIONADA ─────────────────────────────────────────────
        public static EmpresaDto EmpresaActiva { get; set; }

        // ─── USUARIO ──────────────────────────────────────────────────────────
        public static string UsuarioActivo { get; set; }

        // ─── METODOS DE UTILIDAD ──────────────────────────────────────────────

        /// <summary>
        /// Verifica si hay una sesion activa.
        /// </summary>
        public static bool HaySesionActiva => SdkSesion != null;

        /// <summary>
        /// Verifica si hay una empresa seleccionada.
        /// </summary>
        public static bool HayEmpresaActiva => EmpresaActiva != null;

        public const string BaseUrl = "https://cmenonitatest.medsi.com.mx/api/menonita";
        public static string TokenAPI = "b6cbdbcd1f695a94118094bed2560b60a19c2494e197ee803af191aba19f8623";
        public static string Servidor = string.Empty;
        public static string Instancia = string.Empty;
        public static string usuario = string.Empty;
        public static string clave = string.Empty;
        public static System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder conexionglobal;
        public static System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder conexionglobalContpaq;

        public static void CambiarDataSourceInterno()
        {
            //StrconexionInterna = "Data Source=" + Servidor + "\\" + Instancia + ";Initial Catalog=" + BD + ";user id=" + usuario + ";password=" + clave;
            string cnstr = "data source=" + Servidor + "\\" + Instancia + ";initial catalog=dbPRECSOINCIDENCIAS;persist security info=True;user id=" + usuario + ";password=" + clave + ";MultipleActiveResultSets=True;Connection Timeout=1200;";
            System.Data.SqlClient.SqlConnectionStringBuilder scsb = new System.Data.SqlClient.SqlConnectionStringBuilder(cnstr);
            System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder ecb = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder();
            ecb.Metadata = "res://*/Modelo.ModeloControl.csdl|res://*/Modelo.ModeloControl.ssdl|res://*/Modelo.ModeloControl.msl";
            ecb.Provider = "System.Data.SqlClient";
            ecb.ProviderConnectionString = scsb.ConnectionString;
            conexionglobal = ecb;

        }

        public static void CambiarDataSourceContpaq(string BD)
        {
            //StrconexionInterna = "Data Source=" + Servidor + "\\" + Instancia + ";Initial Catalog=" + BD + ";user id=" + usuario + ";password=" + clave;
            string cnstr = "data source=" + Servidor + "\\" + Instancia + ";initial catalog=" + BD + ";persist security info=True;user id=" + usuario + ";password=" + clave + ";MultipleActiveResultSets=True;Connection Timeout=1200;";
            System.Data.SqlClient.SqlConnectionStringBuilder scsb = new System.Data.SqlClient.SqlConnectionStringBuilder(cnstr);

            System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder ecb = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder();
            ecb.Metadata = "res://*/Modelo.ModeloNominas.csdl|res://*/Modelo.ModeloNominas.ssdl|res://*/Modelo.ModeloNominas.msl";
            ecb.Provider = "System.Data.SqlClient";
            ecb.ProviderConnectionString = scsb.ConnectionString;

            conexionglobalContpaq = ecb;

        }

        /// <summary>
        /// Limpia toda la sesion al cerrar la app.
        /// </summary>
        public static void LimpiarSesion()
        {
            SdkSesion = null;
            EmpresaActiva = null;
            UsuarioActivo = null;
        }
    }
}
