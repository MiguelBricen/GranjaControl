using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.UI.Formularios_base;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Tarifas
{
    public partial class FrmTarifaListado : FrmBaseListado
    {
        protected override bool PuedeCrear => RolesSistema.PuedeAccederComercializacion();
        protected override bool PuedeEditar => RolesSistema.PuedeAccederComercializacion();
        protected override bool PuedeEliminar => RolesSistema.PuedeAccederComercializacion();

        private readonly TarifaPolloBLL _tarifaBLL;
        private readonly Mensajes mensajes = new();

        public FrmTarifaListado()
        {
            InitializeComponent();

            _tarifaBLL = new TarifaPolloBLL(new TarifaPolloDAL());

            lblTitulo.Text = "Listado de Tarifas de Pollo";
            label1.Text =
                "Consulte y administre los precios por libra.";

            iconTitulo.IconChar = FontAwesome.Sharp.IconChar.SackDollar;
            iconTitulo.IconColor = Color.FromArgb(23, 61, 43);

            txtBuscar.Parent!.Visible = false;
        }

        protected override async Task MostrarDatosAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                var resultado = await _tarifaBLL.MostrarAsync(
                    pagina,
                    tamPagina);

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
                mensajes.MensajeError(
                    $"Error al mostrar las tarifas: {ex.Message}");
            }
        }

        private void ConfigurarGrid()
        {
            if (!dgvListado.Columns.Contains("Id"))
                return;

            dgvListado.BorderStyle = BorderStyle.None;
            dgvListado.BackgroundColor = Color.White;
            dgvListado.GridColor = Color.FromArgb(220, 228, 222);

            dgvListado.EnableHeadersVisualStyles = false;
            dgvListado.RowHeadersVisible = false;

            dgvListado.AllowUserToAddRows = false;
            dgvListado.AllowUserToDeleteRows = false;
            dgvListado.ReadOnly = true;
            dgvListado.MultiSelect = false;
            dgvListado.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvListado.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            dgvListado.ColumnHeadersHeight = 42;
            dgvListado.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvListado.RowTemplate.Height = 36;
            dgvListado.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvListado.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(32, 76, 56);

            dgvListado.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

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

            dgvListado.DefaultCellStyle.Padding =
                new Padding(8, 0, 8, 0);

            dgvListado.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgvListado.Columns["Id"]!.Visible = false;
            dgvListado.Columns["Activo"]!.Visible = false;

            dgvListado.Columns["PrecioPorLibra"]!.HeaderText =
                "Precio por libra";

            dgvListado.Columns["FechaInicio"]!.HeaderText =
                "Fecha inicio";

            dgvListado.Columns["FechaFin"]!.HeaderText =
                "Fecha fin";

            dgvListado.Columns["Estado"]!.HeaderText = "Estado";

            dgvListado.Columns["PrecioPorLibra"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["PrecioPorLibra"]!.FillWeight = 30;

            dgvListado.Columns["FechaInicio"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["FechaInicio"]!.FillWeight = 25;

            dgvListado.Columns["FechaFin"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["FechaFin"]!.FillWeight = 25;

            dgvListado.Columns["Estado"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["PrecioPorLibra"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvListado.Columns["FechaInicio"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["FechaFin"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["Estado"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["PrecioPorLibra"]!.DisplayIndex = 0;
            dgvListado.Columns["FechaInicio"]!.DisplayIndex = 1;
            dgvListado.Columns["FechaFin"]!.DisplayIndex = 2;
            dgvListado.Columns["Estado"]!.DisplayIndex = 3;

            AjustarColumnas("Estado");
        }

        protected override void FormatearCelda(
            DataGridViewCellFormattingEventArgs e)
        {
            string columna = dgvListado.Columns[e.ColumnIndex].Name;

            if (columna == "FechaFin" &&
                (e.Value is null || e.Value == DBNull.Value))
            {
                e.Value = "Sin fecha";
                e.FormattingApplied = true;

                return;
            }

            if (e.Value is null || e.Value == DBNull.Value)
                return;

            if (columna == "PrecioPorLibra")
            {
                e.Value = Convert.ToDecimal(e.Value).ToString("N2");
                e.FormattingApplied = true;
            }
            else if (columna is "FechaInicio" or "FechaFin")
            {
                e.Value = Convert.ToDateTime(e.Value)
                    .ToString("dd/MM/yyyy");

                e.FormattingApplied = true;
            }

            base.FormatearCelda(e);
        }

        protected override Form? CrearFormularioAccionPrincipal()
        {
            return new FrmRegistroTarifa(_tarifaBLL);
        }

        protected override Form? CrearFormularioAccionSecundaria(int id)
        {
            return new FrmRegistroTarifa(_tarifaBLL, id);
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
                mensajes.MensajeInformacion(
                    "Seleccione una tarifa para cambiar su estado.");

                return;
            }

            bool activo = Convert.ToBoolean(
                dgvListado.CurrentRow!.Cells["Activo"].Value);

            string accion = activo ? "desactivar" : "activar";

            DialogResult respuesta = mensajes.MensajeConfirmacion(
                $"¿Desea {accion} esta tarifa?");

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                await _tarifaBLL.CambiarEstadoAsync(
                    id.Value,
                    !activo,
                    UsuarioSesion.NombreUsuario);

                mensajes.MensajeOk(
                    $"Tarifa {(activo ? "desactivada" : "activada")} correctamente.");

                await MostrarDatosAsync(
                    PaginaActual,
                    TamPagina);
            }
            catch (Exception ex)
            {
                mensajes.MensajeError(
                    $"No se pudo cambiar el estado: {ex.Message}");
            }
        }
    }
}
