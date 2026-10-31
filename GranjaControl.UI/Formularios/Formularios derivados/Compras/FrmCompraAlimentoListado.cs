using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.UI.Formularios_base;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Compras
{
    public partial class FrmCompraAlimentoListado : FrmBaseListado
    {
        private readonly CompraAlimentoBLL _compraBLL;
        private readonly ProveedorBLL _proveedorBLL;
        private readonly AlimentoBLL _alimentoBLL;
        private readonly Mensajes mensajes = new();

        protected override bool MostrarBotonTerciario => false;

        public FrmCompraAlimentoListado()
        {
            InitializeComponent();

            _compraBLL = new CompraAlimentoBLL(
                new CompraAlimentoDAL(),
                new DetalleCompraAlimentoDAL());

            _proveedorBLL = new ProveedorBLL(new ProveedorDAL());
            _alimentoBLL = new AlimentoBLL(new AlimentoDAL());

            lblTitulo.Text = "Listado de Compras de Alimentos";
            label1.Text =
                "Consulte y administre las compras de alimentos registradas.";

            iconTitulo.IconChar = FontAwesome.Sharp.IconChar.CartShopping;

            txtBuscar.PlaceholderText =
                "Buscar por número de compra o proveedor";

            btnEditar.Text = "Ver / Editar";
        }

        protected override async Task MostrarDatosAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                var resultado = await _compraBLL.MostrarAsync(
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
                    $"Error al mostrar las compras: {ex.Message}");
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
            dgvListado.Columns["IdProveedor"]!.Visible = false;

            dgvListado.Columns["NumeroCompra"]!.HeaderText = "N° compra";
            dgvListado.Columns["NombreProveedor"]!.HeaderText = "Proveedor";
            dgvListado.Columns["FechaCompra"]!.HeaderText = "Fecha";
            dgvListado.Columns["Observacion"]!.HeaderText = "Observación";

            dgvListado.Columns["NumeroCompra"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["NombreProveedor"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["NombreProveedor"]!.FillWeight = 28;

            dgvListado.Columns["FechaCompra"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["Observacion"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvListado.Columns["Observacion"]!.FillWeight = 40;

            dgvListado.Columns["FechaCompra"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvListado.Columns["NumeroCompra"]!.DisplayIndex = 0;
            dgvListado.Columns["NombreProveedor"]!.DisplayIndex = 1;
            dgvListado.Columns["FechaCompra"]!.DisplayIndex = 2;
            dgvListado.Columns["Observacion"]!.DisplayIndex = 3;

            AjustarColumnas("NumeroCompra", "FechaCompra");
        }

        protected override void FormatearCelda(
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is null)
                return;

            string columna = dgvListado.Columns[e.ColumnIndex].Name;

            if (columna == "FechaCompra")
            {
                e.Value = Convert.ToDateTime(e.Value)
                    .ToString("dd/MM/yyyy");

                e.FormattingApplied = true;
            }
        }

        protected override Form? CrearFormularioAccionPrincipal()
        {
            return new FrmRegistroCompraAlimento(
                _compraBLL,
                _proveedorBLL,
                _alimentoBLL);
        }

        protected override Form? CrearFormularioAccionSecundaria(int id)
        {
            return new FrmRegistroCompraAlimento(
                _compraBLL,
                _proveedorBLL,
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
    }
}
