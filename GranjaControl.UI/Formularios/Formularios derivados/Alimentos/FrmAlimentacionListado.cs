using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.UI.Formularios_base;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Alimentos
{
    public partial class FrmAlimentacionListado : FrmBaseListado
    {
        private readonly AlimentacionBLL _alimentacionBLL;
        private readonly LoteBLL _loteBLL;
        private readonly AlimentoBLL _alimentoBLL;
        private readonly Mensajes mensajes = new();
        private readonly ComboBox cboLoteFiltro = new();

        private bool _cargandoFiltro;

        protected override bool MostrarBotonTerciario => false;
        protected override bool PuedeCrear => RolesSistema.PuedeRegistrarProduccion();
        protected override bool PuedeEditar => RolesSistema.PuedeEditarProduccion();

        public FrmAlimentacionListado()
        {
            InitializeComponent();

            _alimentacionBLL =
                new AlimentacionBLL(new AlimentacionDAL());

            _loteBLL = new LoteBLL(new LoteDAL());
            _alimentoBLL = new AlimentoBLL(new AlimentoDAL());

            ConfigurarFiltroLote();
        }

        private void ConfigurarFiltroLote()
        {
            txtBuscar.Visible = false;

            cboLoteFiltro.BackColor = Color.White;
            cboLoteFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoteFiltro.FlatStyle = FlatStyle.Flat;
            cboLoteFiltro.Font = new Font(
                "Segoe UI Semibold",
                10.2F,
                FontStyle.Bold);

            cboLoteFiltro.ForeColor = Color.FromArgb(42, 52, 45);
            cboLoteFiltro.FormattingEnabled = true;
            cboLoteFiltro.Location = new Point(39, 7);
            cboLoteFiltro.Name = "cboLoteFiltro";
            cboLoteFiltro.Size = new Size(424, 28);
            cboLoteFiltro.TabIndex = 0;

            cboLoteFiltro.SelectedIndexChanged +=
                cboLoteFiltro_SelectedIndexChanged;

            if (txtBuscar.Parent is Panel panelBuscar)
            {
                panelBuscar.Controls.Add(cboLoteFiltro);
                cboLoteFiltro.BringToFront();
            }
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            await CargarFiltroLotesAsync();
        }

        private async Task CargarFiltroLotesAsync()
        {
            try
            {
                var resultado = await _loteBLL.MostrarAsync(
                    1,
                    10000,
                    null);

                _cargandoFiltro = true;

                cboLoteFiltro.Items.Clear();
                cboLoteFiltro.Items.Add(
                    new LoteFiltroItem(0, "Todos los lotes"));

                foreach (var lote in resultado.Registros)
                {
                    cboLoteFiltro.Items.Add(
                        new LoteFiltroItem(
                            lote.Id,
                            lote.CodigoLote));
                }

                cboLoteFiltro.DisplayMember =
                    nameof(LoteFiltroItem.Descripcion);

                cboLoteFiltro.ValueMember =
                    nameof(LoteFiltroItem.Id);

                cboLoteFiltro.SelectedIndex = 0;

                _cargandoFiltro = false;

                await MostrarDatosAsync(1, TamPagina);
            }
            catch (Exception ex)
            {
                _cargandoFiltro = false;

                mensajes.MensajeError(
                    $"No se pudieron cargar los lotes: {ex.Message}");
            }
        }

        private async void cboLoteFiltro_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            if (_cargandoFiltro)
                return;

            PaginaActual = 1;

            await MostrarDatosAsync(PaginaActual, TamPagina);
        }

        private int? ObtenerIdLoteFiltro()
        {
            if (cboLoteFiltro.SelectedItem is LoteFiltroItem item &&
                item.Id > 0)
            {
                return item.Id;
            }

            return null;
        }

        protected override async Task MostrarDatosAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                var resultado = await _alimentacionBLL.MostrarAsync(
                    pagina,
                    tamPagina,
                    ObtenerIdLoteFiltro());

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
                    $"Error al mostrar las alimentaciones: {ex.Message}");
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
            dgvListado.Columns["IdLote"]!.Visible = false;
            dgvListado.Columns["IdAlimento"]!.Visible = false;

            dgvListado.Columns["Fecha"]!.HeaderText = "Fecha";
            dgvListado.Columns["CodigoLote"]!.HeaderText = "Lote";
            dgvListado.Columns["NombreAlimento"]!.HeaderText = "Alimento";
            dgvListado.Columns["TipoAlimento"]!.HeaderText = "Tipo";
            dgvListado.Columns["Cantidad"]!.HeaderText = "Cantidad";
            dgvListado.Columns["UnidadMedida"]!.HeaderText = "Unidad";
            dgvListado.Columns["Observacion"]!.HeaderText = "Observación";

            dgvListado.Columns["Fecha"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["CodigoLote"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["NombreAlimento"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["NombreAlimento"]!.FillWeight = 27;

            dgvListado.Columns["TipoAlimento"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["Cantidad"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["UnidadMedida"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["Observacion"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["Observacion"]!.FillWeight = 30;

            dgvListado.Columns["Fecha"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["Cantidad"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvListado.Columns["UnidadMedida"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["TipoAlimento"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["Fecha"]!.DisplayIndex = 0;
            dgvListado.Columns["CodigoLote"]!.DisplayIndex = 1;
            dgvListado.Columns["NombreAlimento"]!.DisplayIndex = 2;
            dgvListado.Columns["TipoAlimento"]!.DisplayIndex = 3;
            dgvListado.Columns["Cantidad"]!.DisplayIndex = 4;
            dgvListado.Columns["UnidadMedida"]!.DisplayIndex = 5;
            dgvListado.Columns["Observacion"]!.DisplayIndex = 6;

            AjustarColumnas(
                "Fecha",
                "CodigoLote",
                "TipoAlimento",
                "Cantidad",
                "UnidadMedida");
        }

        protected override void FormatearCelda(
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is null)
                return;

            string columna = dgvListado.Columns[e.ColumnIndex].Name;

            switch (columna)
            {
                case "Fecha":
                    e.Value = Convert.ToDateTime(e.Value)
                        .ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                    break;

                case "Cantidad":
                    e.Value = Convert.ToDecimal(e.Value)
                        .ToString("N2");
                    e.FormattingApplied = true;
                    break;
            }
        }

        protected override Form? CrearFormularioAccionPrincipal()
        {
            return new FrmRegistroAlimentacion(
                _alimentacionBLL,
                _loteBLL,
                _alimentoBLL);
        }

        protected override Form? CrearFormularioAccionSecundaria(int id)
        {
            return new FrmRegistroAlimentacion(
                _alimentacionBLL,
                _loteBLL,
                _alimentoBLL,
                id);
        }

        protected override int? ObtenerIdSeleccionado()
        {
            if (dgvListado.CurrentRow == null)
                return null;

            return Convert.ToInt32(
                dgvListado.CurrentRow.Cells["Id"].Value);
        }

        private sealed class LoteFiltroItem
        {
            public LoteFiltroItem(int id, string descripcion)
            {
                Id = id;
                Descripcion = descripcion;
            }

            public int Id { get; }

            public string Descripcion { get; }

            public override string ToString()
            {
                return Descripcion;
            }
        }
    }
}
