using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.UI.Formularios_base;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Proveedores
{
    public partial class FrmProveedorListado : FrmBaseListado
    {
        private readonly ProveedorBLL _proveedorBLL;
        private readonly Mensajes mensajes = new();

        public FrmProveedorListado()
        {
            InitializeComponent();

            _proveedorBLL = new ProveedorBLL(new ProveedorDAL());
        }

        protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
        {
            try
            {
                var resultado = await _proveedorBLL.MostrarAsync(pagina, tamPagina, buscar);

                dgvListado.DataSource = null;
                dgvListado.DataSource = resultado.Registros;

                PaginaActual = resultado.PaginaActual;
                TamPagina = resultado.TamPagina;
                TotalRegistros = resultado.TotalRegistros;
                TotalPaginas = resultado.TotalPaginas;

                ActualizarPaginacion();
                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                mensajes.MensajeError($"Error al mostrar los proveedores: {ex.Message}");
            }
        }

        private void ConfigurarGrid()
        {
            dgvListado.BorderStyle = BorderStyle.None;
            dgvListado.BackgroundColor = Color.White;
            dgvListado.GridColor = Color.FromArgb(220, 228, 222);

            dgvListado.EnableHeadersVisualStyles = false;
            dgvListado.RowHeadersVisible = false;

            dgvListado.AllowUserToAddRows = false;
            dgvListado.AllowUserToDeleteRows = false;
            dgvListado.ReadOnly = true;
            dgvListado.MultiSelect = false;
            dgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgvListado.ColumnHeadersHeight = 42;
            dgvListado.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvListado.RowTemplate.Height = 36;
            dgvListado.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvListado.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(32, 76, 56);

            dgvListado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvListado.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(32, 76, 56);

            dgvListado.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvListado.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 12, FontStyle.Bold);

            dgvListado.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvListado.ColumnHeadersDefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgvListado.DefaultCellStyle.BackColor = Color.White;
            dgvListado.DefaultCellStyle.ForeColor = Color.Black;

            dgvListado.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(226, 239, 228);

            dgvListado.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(32, 76, 56);

            dgvListado.DefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);

            dgvListado.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvListado.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);

            dgvListado.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgvListado.Columns["Id"]!.Visible = false;
            dgvListado.Columns["Activo"]!.Visible = false;

            dgvListado.Columns["NombreProveedor"]!.HeaderText =
                "Nombre del proveedor";

            dgvListado.Columns["TipoProveedor"]!.HeaderText = "Tipo";
            dgvListado.Columns["Telefono"]!.HeaderText = "Teléfono";
            dgvListado.Columns["CorreoElectronico"]!.HeaderText = "Correo";
            dgvListado.Columns["Direccion"]!.HeaderText = "Dirección";
            dgvListado.Columns["Estado"]!.HeaderText = "Estado";

            dgvListado.Columns["NombreProveedor"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["NombreProveedor"]!.FillWeight = 25;

            dgvListado.Columns["TipoProveedor"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["Telefono"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["CorreoElectronico"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["CorreoElectronico"]!.FillWeight = 25;

            dgvListado.Columns["Direccion"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["Direccion"]!.FillWeight = 30;

            dgvListado.Columns["Estado"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["TipoProveedor"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["Telefono"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["Estado"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["NombreProveedor"]!.DisplayIndex = 0;
            dgvListado.Columns["TipoProveedor"]!.DisplayIndex = 1;
            dgvListado.Columns["Telefono"]!.DisplayIndex = 2;
            dgvListado.Columns["CorreoElectronico"]!.DisplayIndex = 3;
            dgvListado.Columns["Direccion"]!.DisplayIndex = 4;
            dgvListado.Columns["Estado"]!.DisplayIndex = 5;
        }

        protected override Form? CrearFormularioAccionPrincipal()
        {
            return new FrmRegistroProveedor(_proveedorBLL);
        }

        protected override Form? CrearFormularioAccionSecundaria(int id)
        {
            return new FrmRegistroProveedor(_proveedorBLL, id);
        }

        protected override int? ObtenerIdSeleccionado()
        {
            if (dgvListado.CurrentRow == null)
                return null;

            return Convert.ToInt32(
                dgvListado.CurrentRow.Cells["Id"].Value);
        }

        protected override async Task DesactivarAsync()
        {
            int? id = ObtenerIdSeleccionado();

            if (id == null)
            {
                mensajes.MensajeInformacion("Seleccione un proveedor para cambiar su estado.");

                return;
            }

            bool activo = Convert.ToBoolean(
                dgvListado.CurrentRow!.Cells["Activo"].Value);

            string accion = activo ? "desactivar" : "activar";

            DialogResult respuesta = mensajes.MensajeConfirmacion($"¿Desea {accion} este proveedor?");

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                await _proveedorBLL.CambiarEstadoAsync(
                    id.Value,
                    !activo,
                    UsuarioSesion.NombreUsuario);

                mensajes.MensajeOk($"Proveedor {(activo ? "desactivado" : "activado")} correctamente.");

                await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
            }
            catch (Exception ex)
            {
                mensajes.MensajeError($"No se pudo cambiar el estado: {ex.Message}");
            }
        }
    }
}
