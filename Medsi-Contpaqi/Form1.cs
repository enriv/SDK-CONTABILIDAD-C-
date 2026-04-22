using Diesel.SDK;
using Medsi_Contpaqi.Helpers;
using Medsi_Contpaqi.SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medsi_Contpaqi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            while (true)
            {
                int resultado = ClaseSDKContabilidad.IniciarSesion();

                if (resultado == SdkResult.Success)  // Login correcto
                    break;

                if (resultado == SdkResult.Fail)  // Usuario cancelo
                {
                    ClaseSDKContabilidad.CerrarSesion();
                    Application.Exit();
                    return;
                }
            }

            // 2. Seleccion de empresa
            using (var formEmpresa = new FormSeleccionEmpresa())
            {
                if (formEmpresa.ShowDialog() != DialogResult.OK)
                {
                    AppHelper.LimpiarSesion();
                    ClaseSDKContabilidad.CerrarSesion();
                    Application.Exit();
                    return;
                }
                else
                {
                    tslEmpresa.Text = $"Empresa: {AppHelper.EmpresaActiva.Nombre}";
                    tslRuta.Text = $"Ruta: {AppHelper.EmpresaActiva.NombreBaseDatos}";
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppHelper.LimpiarSesion();
            ClaseSDKContabilidad.CerrarSesion();
        }
    }
}
