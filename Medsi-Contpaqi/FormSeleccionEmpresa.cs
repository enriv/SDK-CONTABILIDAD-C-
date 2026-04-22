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
    public partial class FormSeleccionEmpresa : Form
    {
        public string EmpresaSeleccionada { get; private set; }
        public FormSeleccionEmpresa()
        {
            InitializeComponent();
        }

        private void FormSeleccionEmpresa_Load(object sender, EventArgs e)
        {
            CargarEmpresas();
        }

        private void CargarEmpresas()
        {
            try
            {
                var empresas = ClaseSDKContabilidad.ObtenerEmpresas();

                var tabla = new System.Data.DataTable();
                tabla.Columns.Add("Id", typeof(int));
                tabla.Columns.Add("Nombre", typeof(string));
                tabla.Columns.Add("Base de Datos", typeof(string));
                //tabla.Columns.Add("Ruta", typeof(string));

                foreach (var e in empresas)
                {
                    tabla.Rows.Add(e.Id, e.Nombre, e.NombreBaseDatos);
                }

                dgvEmpresas.DataSource = tabla;
                dgvEmpresas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvEmpresas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvEmpresas.MultiSelect = false;
                dgvEmpresas.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empresas: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClaseSDKContabilidad.CerrarSesion();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvEmpresas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una empresa.");
                return;
            }

            string alias = dgvEmpresas.SelectedRows[0].Cells["Base de Datos"].Value.ToString();
            int resultado = ClaseSDKContabilidad.AbrirEmpresa(alias);

            if (resultado == SdkResult.Success)
            {
                AppHelper.EmpresaActiva = new EmpresaDto
                {
                    Id = (int)dgvEmpresas.SelectedRows[0].Cells["Id"].Value,
                    Nombre = dgvEmpresas.SelectedRows[0].Cells["Nombre"].Value.ToString(),
                    NombreBaseDatos = dgvEmpresas.SelectedRows[0].Cells["Base de Datos"].Value.ToString()
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al abrir empresa: " + ClaseSDKContabilidad.ObtenerUltimoError());
            }
        }
    }
}
