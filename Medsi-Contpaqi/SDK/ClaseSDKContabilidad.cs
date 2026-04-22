using Medsi_Contpaqi.Helpers;
using Medsi_Contpaqi.SDK;
using SDKCONTPAQNGLib;
using System;
using System.Collections.Generic;

namespace Diesel.SDK
{
    class ClaseSDKContabilidad
    {
        private static TSdkSesion _sdkSesion;

        /// <summary>
        /// Muestra el dialogo de inicio de sesion del SDK.
        /// Regresa true si el usuario ingreso correctamente.
        /// </summary>
        public static int IniciarSesion()
        {
            try
            {
                _sdkSesion = new TSdkSesion();
                _sdkSesion.firmaUsuario();

                if (_sdkSesion.ingresoUsuario == SdkResult.Success)
                    AppHelper.SdkSesion = _sdkSesion;

                return _sdkSesion.ingresoUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar sesión: " + ex.Message);
            }
        }

        /// <summary>
        /// Abre una empresa por su alias.
        /// Regresa 0 si fue exitoso.
        /// </summary>
        public static int AbrirEmpresa(string aliasEmpresa)
        {
            return _sdkSesion.abreEmpresa(aliasEmpresa);
        }

        /// <summary>
        /// Cierra la empresa activa.
        /// </summary>
        public static void CerrarEmpresa()
        {
            _sdkSesion?.cierraEmpresa();
        }

        /// <summary>
        /// Cierra la sesion del SDK.
        /// </summary>
        public static void CerrarSesion()
        {
            _sdkSesion?.finalizaConexion();
        }

        /// <summary>
        /// Obtiene el ultimo mensaje de error del SDK.
        /// </summary>
        public static string ObtenerUltimoError()
        {
            return _sdkSesion?.UltimoMsjError ?? string.Empty;
        }

        public static List<EmpresaDto> ObtenerEmpresas()
        {
            var lista = new List<EmpresaDto>();
            TSdkListaEmpresas empresa = new TSdkListaEmpresas();

            if (empresa.buscaPrimero() == SdkResult.Success)
            {
                lista.Add(new EmpresaDto
                {
                    Id = empresa.Id,
                    Nombre = empresa.Nombre,
                    NombreBaseDatos = empresa.NombreBDD,
                    //RutaDatos = empresa.RutaDatos
                });

                while (empresa.buscaSiguiente() == SdkResult.Success)
                {
                    lista.Add(new EmpresaDto
                    {
                        Id = empresa.Id,
                        Nombre = empresa.Nombre,
                        NombreBaseDatos = empresa.NombreBDD,
                        //RutaDatos = empresa.RutaDatos
                    });
                }
            }

            return lista;
        }
    }
}