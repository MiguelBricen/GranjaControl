using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.UI.Formularios_base;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Roles
{
    public partial class FrmRolesListado : FrmBaseListado
    {
        protected override bool PuedeCrear => RolesSistema.PuedeGestionarRoles();
        protected override bool PuedeEditar => RolesSistema.PuedeGestionarRoles();
        protected override bool PuedeEliminar => RolesSistema.PuedeGestionarRoles();

        private readonly RolBll _rolBll;
        private readonly Mensajes mensajes = new();
        public FrmRolesListado()
        {
            InitializeComponent();

            _rolBll = new RolBll(new RolDal());
        }

        protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
        {
            try
            {
                var resultado = await _rolBll.MostrarAsync(pagina, tamPagina, buscar);

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
                mensajes.MensajeError(ex.Message);
            }
        }

        private void ConfigurarGrid()
        {
            // Configuracion general del GridView
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
            dgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvListado.RowTemplate.Height = 36;
            dgvListado.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;


            // Encabezados         
            dgvListado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 76, 56);

            dgvListado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvListado.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(32, 76, 56);

            dgvListado.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgvListado.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            dgvListado.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvListado.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Filas       
            dgvListado.DefaultCellStyle.BackColor = Color.White;

            dgvListado.DefaultCellStyle.ForeColor = Color.Black;

            dgvListado.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 239, 228);

            dgvListado.DefaultCellStyle.SelectionForeColor = Color.FromArgb(32, 76, 56);

            dgvListado.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            dgvListado.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvListado.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);

            dgvListado.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvListado.Columns["Id"]!.Visible = false;
            dgvListado.Columns["NombreRol"]!.HeaderText = "Nombre del Rol";



            dgvListado.Columns["Activo"]!.Visible = false;
            dgvListado.Columns["Estado"]!.HeaderText = "Estado";

            dgvListado.Columns["NombreRol"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["NombreRol"]!.FillWeight = 30;
            dgvListado.Columns["Estado"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["NombreRol"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            AjustarColumnas("Estado");
        }

        protected override Form CrearFormularioAccionPrincipal()
        {
            return new FrmRolesRegistro(_rolBll);
        }

        protected override Form? CrearFormularioAccionSecundaria(int id)
        {
            return new FrmRolesRegistro(_rolBll, id);
        }

        protected override int? ObtenerIdSeleccionado()
        {
            if (dgvListado.CurrentRow == null)
                return null;

            return Convert.ToInt32(dgvListado.CurrentRow.Cells["Id"].Value);
        }

        protected override async Task DesactivarAsync()
        {
            int? id = ObtenerIdSeleccionado();

            if (id == null)
            {
                mensajes.MensajeInformacion("Debe seleccionar un registro.");
                return;
            }

            bool activo = Convert.ToBoolean(dgvListado.CurrentRow!.Cells["Activo"].Value);

            string accion = activo ? "desactivar" : "activar";

            DialogResult respuesta = mensajes.MensajeConfirmacion(
                $"¿Desea {accion} este registro?");

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                await _rolBll.CambiarEstadoAsync(id.Value, !activo, UsuarioSesion.NombreUsuario);
                await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
            }
            catch (Exception ex)
            {
                mensajes.MensajeError(ex.Message);
            }
        }
    }
}
