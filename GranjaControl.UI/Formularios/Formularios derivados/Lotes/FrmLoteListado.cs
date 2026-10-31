using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.UI.Formularios_base;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Lotes
{
    public partial class FrmLoteListado : FrmBaseListado
    {
        protected override bool PuedeCrear => RolesSistema.PuedeGestionarLotes();
        protected override bool PuedeEditar => RolesSistema.PuedeGestionarLotes();
        protected override bool PuedeEliminar => RolesSistema.PuedeGestionarLotes();

        private readonly LoteBLL _loteBLL;
        private readonly GalponBLL _galponBLL;
        private readonly ProveedorBLL _proveedorBLL;
        private readonly Mensajes mensajes = new();

        public FrmLoteListado()
        {
            InitializeComponent();

            _loteBLL = new LoteBLL(new LoteDAL());
            _galponBLL = new GalponBLL(new GalponDAL());
            _proveedorBLL = new ProveedorBLL(new ProveedorDAL());
        }

        protected override async Task MostrarDatosAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                var resultado = await _loteBLL.MostrarAsync(
                    pagina,
                    tamPagina,
                    buscar);

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
                    $"Error al mostrar los lotes: {ex.Message}");
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
            dgvListado.Columns["IdGalpon"]!.Visible = false;
            dgvListado.Columns["IdProveedor"]!.Visible = false;

            dgvListado.Columns["CodigoLote"]!.HeaderText = "Código";
            dgvListado.Columns["NombreGalpon"]!.HeaderText = "Galpón";
            dgvListado.Columns["NombreProveedor"]!.HeaderText = "Proveedor";
            dgvListado.Columns["FechaIngreso"]!.HeaderText = "Ingreso";
            dgvListado.Columns["CantidadInicial"]!.HeaderText = "Cantidad";
            dgvListado.Columns["CostoPorPollito"]!.HeaderText = "Costo";
            dgvListado.Columns["Estado"]!.HeaderText = "Estado";
            dgvListado.Columns["Observacion"]!.HeaderText = "Observación";

            dgvListado.Columns["CodigoLote"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["CodigoLote"]!.FillWeight = 14;

            dgvListado.Columns["NombreGalpon"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["NombreGalpon"]!.FillWeight = 17;

            dgvListado.Columns["NombreProveedor"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["NombreProveedor"]!.FillWeight = 17;

            dgvListado.Columns["FechaIngreso"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["CantidadInicial"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["CostoPorPollito"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["Estado"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["Observacion"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["Observacion"]!.FillWeight = 22;

            dgvListado.Columns["CantidadInicial"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["CostoPorPollito"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvListado.Columns["FechaIngreso"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["Estado"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            AjustarColumnas("Estado");
        }

        protected override void FormatearCelda(
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is null)
                return;

            string columna = dgvListado.Columns[e.ColumnIndex].Name;

            switch (columna)
            {
                case "FechaIngreso":
                    e.Value = Convert.ToDateTime(e.Value)
                        .ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                    break;

                case "CostoPorPollito":
                    e.Value = Convert.ToDecimal(e.Value)
                        .ToString("N2");
                    e.FormattingApplied = true;
                    break;

                case "Estado":
                    AplicarColorEstado(e, e.Value.ToString()!);
                    break;
            }
        }

        private static void AplicarColorEstado(
            DataGridViewCellFormattingEventArgs e,
            string estado)
        {
            Color color = estado switch
            {
                "En producción" => Color.FromArgb(22, 163, 74),
                "Listo para venta" => Color.FromArgb(37, 99, 235),
                "Finalizado" => Color.FromArgb(107, 114, 128),
                "Cancelado" => Color.FromArgb(185, 28, 28),
                _ => Color.Black
            };

            e.CellStyle.ForeColor = color;
            e.CellStyle.SelectionForeColor = color;
        }

        protected override Form? CrearFormularioAccionPrincipal()
        {
            return new FrmLoteRegistro(
                _loteBLL,
                _galponBLL,
                _proveedorBLL);
        }

        protected override Form? CrearFormularioAccionSecundaria(int id)
        {
            return new FrmLoteRegistro(
                _loteBLL,
                _galponBLL,
                _proveedorBLL,
                id);
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

            if (id is null)
            {
                mensajes.MensajeInformacion(
                    "Seleccione un lote para cambiar su estado.");

                return;
            }

            string estado = dgvListado.CurrentRow!
                .Cells["Estado"].Value?.ToString() ?? string.Empty;

            using FrmCambiarEstadoLote frm =
                new(_loteBLL, id.Value, estado);

            DialogResult resultado =
                MostrarModal.MostrarConCapa(this, frm);

            if (resultado == DialogResult.OK)
            {
                await MostrarDatosAsync(
                    PaginaActual,
                    TamPagina,
                    txtBuscar.Text);
            }
        }
    }
}
