using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.UI.Formularios_base;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Ventas
{
    public partial class FrmVentaListado : FrmBaseListado
    {
        protected override bool PuedeCrear => RolesSistema.PuedeAccederComercializacion();
        protected override bool PuedeEditar => RolesSistema.PuedeAccederComercializacion();
        protected override bool PuedeEliminar => RolesSistema.PuedeAccederComercializacion();

        private readonly VentaBLL _ventaBLL;
        private readonly ClienteBLL _clienteBLL;
        private readonly LoteBLL _loteBLL;
        private readonly TarifaPolloBLL _tarifaBLL;
        private readonly Mensajes mensajes = new();

        public FrmVentaListado()
        {
            InitializeComponent();

            _ventaBLL = new VentaBLL(new VentaDAL(), new LoteDAL());
            _clienteBLL = new ClienteBLL(new ClienteDAL());
            _loteBLL = new LoteBLL(new LoteDAL());
            _tarifaBLL = new TarifaPolloBLL(new TarifaPolloDAL());

            lblTitulo.Text = "Listado de Ventas";
            label1.Text = "Consulte, registre y anule las ventas de pollos.";

            iconTitulo.IconChar = FontAwesome.Sharp.IconChar.SackDollar;

            txtBuscar.PlaceholderText = "Buscar por número, cliente o estado";

            btnEditar.Text = "Ver / Editar";
            btnEliminar.Text = "Anular";
        }

        protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
        {
            try
            {
                var resultado = await _ventaBLL.MostrarAsync(pagina, tamPagina, buscar);

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
                mensajes.MensajeError($"Error al mostrar las ventas: {ex.Message}");
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

            if (dgvListado.Columns.Count == 0)
                return;


            dgvListado.Columns["Id"]!.Visible = false;
            dgvListado.Columns["IdCliente"]!.Visible = false;
            dgvListado.Columns["Observacion"]!.Visible = false;

            dgvListado.Columns["NumeroVenta"]!.HeaderText = "N° venta";
            dgvListado.Columns["NombreCliente"]!.HeaderText = "Cliente";
            dgvListado.Columns["FechaVenta"]!.HeaderText = "Fecha";
            dgvListado.Columns["Estado"]!.HeaderText = "Estado";
            dgvListado.Columns["CantidadPollos"]!.HeaderText = "Pollos";
            dgvListado.Columns["PesoTotalLibras"]!.HeaderText = "Libras";
            dgvListado.Columns["TotalVenta"]!.HeaderText = "Total";

            dgvListado.Columns["NumeroVenta"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["NombreCliente"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["FechaVenta"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["Estado"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["CantidadPollos"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["PesoTotalLibras"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["TotalVenta"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            AjustarColumnas("NumeroVenta", "FechaVenta", "Estado", "CantidadPollos", "PesoTotalLibras", "TotalVenta");
        }

        protected override void FormatearCelda(DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is null || e.Value == DBNull.Value)
                return;

            string columna = dgvListado.Columns[e.ColumnIndex].Name;

            if (columna == "FechaVenta")
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd/MM/yyyy");
                e.FormattingApplied = true;
            }
            else if (columna is "PesoTotalLibras" or "TotalVenta")
            {
                e.Value = Convert.ToDecimal(e.Value).ToString("N2");
                e.FormattingApplied = true;
            }
            else if (columna == "Estado")
            {
                string estado = e.Value.ToString() ?? string.Empty;

                e.CellStyle.ForeColor = estado.Equals("Anulada",
                        StringComparison.OrdinalIgnoreCase)
                        ? Color.FromArgb(185, 28, 28)
                        : Color.FromArgb(22, 163, 74);

                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
        }

        protected override Form? CrearFormularioAccionPrincipal()
        {
            return new FrmRegistroVenta(_ventaBLL, _clienteBLL, _loteBLL, _tarifaBLL);
        }

        protected override Form? CrearFormularioAccionSecundaria(int id)
        {
            return new FrmRegistroVenta(_ventaBLL, _clienteBLL, _loteBLL, _tarifaBLL, id);
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

            if (id is null)
            {
                mensajes.MensajeInformacion("Seleccione una venta para anular.");
                return;
            }

            string estado = dgvListado.CurrentRow!.Cells["Estado"].Value?.ToString()
                ?? string.Empty;

            if (estado.Equals("Anulada", StringComparison.OrdinalIgnoreCase))
            {
                mensajes.MensajeInformacion("La venta seleccionada ya está anulada.");
                return;
            }

            DialogResult respuesta = mensajes.MensajeConfirmacion("¿Desea anular esta venta? Los detalles se conservarán, pero dejarán de consumir pollos del lote.");

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                await _ventaBLL.AnularAsync(id.Value, UsuarioSesion.NombreUsuario);

                mensajes.MensajeOk("Venta anulada correctamente.");

                await MostrarDatosAsync(PaginaActual,TamPagina, txtBuscar.Text);
            }
            catch (Exception ex)
            {
                mensajes.MensajeError($"No se pudo anular la venta: {ex.Message}");
            }
        }
    }
}
