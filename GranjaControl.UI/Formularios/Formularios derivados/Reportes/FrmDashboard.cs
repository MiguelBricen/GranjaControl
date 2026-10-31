using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.Entitys.Dto;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Reportes
{
    public partial class FrmDashboard : Form
    {
        private readonly DashboardBLL _dashboardBLL;
        private readonly Mensajes _mensajes = new();
        private readonly Dictionary<string, Label> _valoresTarjetas = new();
        private readonly Dictionary<string, Label> _detallesTarjetas = new();
        private bool _cargando;

        public FrmDashboard()
        {
            InitializeComponent();

            _dashboardBLL = new DashboardBLL(new DashboardDAL());

            ConstruirTarjetas();
            ConfigurarGrids();
        }

        private void ConstruirTarjetas()
        {
            AgregarTarjeta(
                "lotes",
                "Lotes activos",
                EstiloVisualHelper.Azul,
                0,
                0);

            AgregarTarjeta(
                "disponibles",
                "Aves disponibles",
                EstiloVisualHelper.Verde,
                1,
                0);

            AgregarTarjeta(
                "vendidos",
                "Pollos vendidos",
                EstiloVisualHelper.Turquesa,
                2,
                0);

            AgregarTarjeta(
                "ventas",
                "Ventas del mes",
                EstiloVisualHelper.Morado,
                3,
                0);

            AgregarTarjeta(
                "ingresos",
                "Ingresos del mes",
                EstiloVisualHelper.Dorado,
                0,
                1);

            AgregarTarjeta(
                "mortalidad",
                "Mortalidad acumulada",
                EstiloVisualHelper.Rojo,
                1,
                1);

            AgregarTarjeta(
                "compras",
                "Compras de alimento",
                EstiloVisualHelper.Naranja,
                2,
                1);

            AgregarTarjeta(
                "clientes",
                "Clientes activos",
                EstiloVisualHelper.Indigo,
                3,
                1);
        }

        private void AgregarTarjeta(
            string clave,
            string titulo,
            Color color,
            int columna,
            int fila)
        {
            Panel tarjeta = EstiloVisualHelper.CrearTarjeta(
                titulo,
                color,
                true,
                21F,
                out Label lblValor,
                out Label lblDetalle);

            tarjeta.Margin = new Padding(5);
            tlpTarjetas.Controls.Add(tarjeta, columna, fila);

            _valoresTarjetas[clave] = lblValor;
            _detallesTarjetas[clave] = lblDetalle;
        }

        private void ConfigurarGrids()
        {
            GridDashboardHelper.Personalizar(dgvLotes);
            GridDashboardHelper.Personalizar(dgvVentas);

            dgvLotes.CellFormatting += FormatearCeldaLote;
            dgvVentas.CellFormatting += FormatearCeldaVenta;
        }

        private async void FrmDashboard_Load(object sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async Task CargarAsync()
        {
            if (_cargando)
                return;

            try
            {
                _cargando = true;
                btnActualizar.Enabled = false;

                DashboardDto datos = await _dashboardBLL.ObtenerAsync(5);

                MostrarResumen(datos.Resumen);
                MostrarLotes(datos.LotesActivos);
                MostrarVentas(datos.VentasRecientes);

                lblActualizado.Text =
                    $"Actualizado: {DateTime.Now:dd/MM/yyyy HH:mm}";
            }
            catch (Exception ex)
            {
                _mensajes.MensajeError(
                    $"No se pudo cargar el panel de control: {ex.Message}");
            }
            finally
            {
                _cargando = false;
                btnActualizar.Enabled = true;
            }
        }

        private void MostrarResumen(DashboardResumenDto resumen)
        {
            MostrarTarjeta(
                "lotes",
                resumen.LotesActivos.ToString("N0"),
                "En producción y listos para venta");

            MostrarTarjeta(
                "disponibles",
                resumen.AvesDisponibles.ToString("N0"),
                $"{resumen.AvesIniciales:N0} aves en lotes activos");

            MostrarTarjeta(
                "vendidos",
                resumen.PollosVendidosMes.ToString("N0"),
                "Ventas registradas del mes");

            MostrarTarjeta(
                "ventas",
                resumen.VentasMes.ToString("N0"),
                "Operaciones registradas");

            MostrarTarjeta(
                "ingresos",
                resumen.IngresosMes.ToString("N2"),
                "Facturación registrada del mes");

            MostrarTarjeta(
                "mortalidad",
                resumen.MortalidadAcumulada.ToString("N0"),
                "Aves acumuladas en lotes activos");

            MostrarTarjeta(
                "compras",
                resumen.ComprasMes.ToString("N2"),
                "Costo de alimento del mes");

            MostrarTarjeta(
                "clientes",
                resumen.ClientesActivos.ToString("N0"),
                "Clientes con estado activo");

            int porcentaje = resumen.AvesIniciales > 0
                ? (int)Math.Round(
                    resumen.AvesDisponibles * 100.0 /
                    resumen.AvesIniciales)
                : 0;

            prgDisponibilidad.Value = Math.Clamp(porcentaje, 0, 100);
            lblDisponibilidadValor.Text =
                $"{resumen.AvesDisponibles:N0} de {resumen.AvesIniciales:N0} aves";
        }

        private void MostrarTarjeta(
            string clave,
            string valor,
            string detalle)
        {
            if (_valoresTarjetas.TryGetValue(clave, out Label? lblValor))
                lblValor.Text = valor;

            if (_detallesTarjetas.TryGetValue(clave, out Label? lblDetalle))
                lblDetalle.Text = detalle;
        }

        private void MostrarLotes(List<DashboardLoteActivoDto> lotes)
        {
            dgvLotes.DataSource = null;
            dgvLotes.DataSource = lotes;

            if (!dgvLotes.Columns.Contains("Id"))
                return;

            dgvLotes.Columns["Id"]!.Visible = false;
            dgvLotes.Columns["NombreProveedor"]!.Visible = false;
            dgvLotes.Columns["FechaIngreso"]!.Visible = false;
            dgvLotes.Columns["Mortalidad"]!.Visible = false;
            dgvLotes.Columns["Vendidos"]!.Visible = false;

            dgvLotes.Columns["CodigoLote"]!.HeaderText = "Lote";
            dgvLotes.Columns["NombreGalpon"]!.HeaderText = "Galpón";
            dgvLotes.Columns["Estado"]!.HeaderText = "Estado";
            dgvLotes.Columns["CantidadInicial"]!.HeaderText = "Inicial";
            dgvLotes.Columns["Disponibles"]!.HeaderText = "Disponibles";
            dgvLotes.Columns["PorcentajeMortalidad"]!.HeaderText = "Mort. %";

            dgvLotes.Columns["CodigoLote"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;
            dgvLotes.Columns["CodigoLote"]!.MinimumWidth = 95;

            dgvLotes.Columns["NombreGalpon"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvLotes.Columns["NombreGalpon"]!.MinimumWidth = 140;

            dgvLotes.Columns["Estado"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;
            dgvLotes.Columns["Estado"]!.MinimumWidth = 110;

            dgvLotes.Columns["CantidadInicial"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvLotes.Columns["Disponibles"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvLotes.Columns["PorcentajeMortalidad"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvLotes.Columns["CantidadInicial"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvLotes.Columns["Disponibles"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvLotes.Columns["PorcentajeMortalidad"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvLotes.Columns["Estado"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }

        private void MostrarVentas(List<DashboardVentaRecienteDto> ventas)
        {
            dgvVentas.DataSource = null;
            dgvVentas.DataSource = ventas;

            if (!dgvVentas.Columns.Contains("Id"))
                return;

            dgvVentas.Columns["Id"]!.Visible = false;
            dgvVentas.Columns["CantidadPollos"]!.Visible = false;
            dgvVentas.Columns["PesoLibras"]!.Visible = false;

            dgvVentas.Columns["NumeroVenta"]!.HeaderText = "Venta";
            dgvVentas.Columns["NombreCliente"]!.HeaderText = "Cliente";
            dgvVentas.Columns["FechaVenta"]!.HeaderText = "Fecha";
            dgvVentas.Columns["Estado"]!.HeaderText = "Estado";
            dgvVentas.Columns["Total"]!.HeaderText = "Total";

            dgvVentas.Columns["NumeroVenta"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;
            dgvVentas.Columns["NumeroVenta"]!.MinimumWidth = 100;

            dgvVentas.Columns["NombreCliente"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvVentas.Columns["NombreCliente"]!.MinimumWidth = 140;

            dgvVentas.Columns["FechaVenta"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvVentas.Columns["Estado"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;
            dgvVentas.Columns["Estado"]!.MinimumWidth = 100;

            dgvVentas.Columns["Total"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvVentas.Columns["FechaVenta"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvVentas.Columns["Estado"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvVentas.Columns["Total"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
        }

        private void FormatearCeldaLote(
            object? sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.Value is null ||
                e.Value == DBNull.Value)
            {
                return;
            }

            string columna = dgvLotes.Columns[e.ColumnIndex].Name;

            if (columna == "PorcentajeMortalidad")
            {
                decimal valor = Convert.ToDecimal(e.Value);

                e.Value = $"{valor:N2} %";
                e.CellStyle.ForeColor = valor > 5
                    ? EstiloVisualHelper.Rojo
                    : EstiloVisualHelper.Verde;

                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
                e.FormattingApplied = true;
            }
            else if (columna == "Estado")
            {
                e.CellStyle.ForeColor = EstiloVisualHelper.Verde;
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
        }

        private void FormatearCeldaVenta(
            object? sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.Value is null ||
                e.Value == DBNull.Value)
            {
                return;
            }

            string columna = dgvVentas.Columns[e.ColumnIndex].Name;

            if (columna == "FechaVenta")
            {
                e.Value = Convert.ToDateTime(e.Value)
                    .ToString("dd/MM/yyyy");

                e.FormattingApplied = true;
            }
            else if (columna == "Total")
            {
                e.Value = Convert.ToDecimal(e.Value).ToString("N2");
                e.FormattingApplied = true;
            }
            else if (columna == "Estado")
            {
                string estado = e.Value.ToString() ?? string.Empty;

                e.CellStyle.ForeColor = estado.Equals(
                    "Anulada",
                    StringComparison.OrdinalIgnoreCase)
                    ? EstiloVisualHelper.Rojo
                    : EstiloVisualHelper.Verde;

                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
        }

        private void FrmDashboard_Shown(object sender, EventArgs e)
        {
            RedondearControlHelper.RedondearControlAlCambiarTamano(
                pnlDisponibilidad,
                12);

            RedondearControlHelper.RedondearControlAlCambiarTamano(
                pnlLotes,
                14);

            RedondearControlHelper.RedondearControlAlCambiarTamano(
                pnlVentas,
                14);

            RedondearControlHelper.RedondearControlAlCambiarTamano(
                dgvLotes,
                10);

            RedondearControlHelper.RedondearControlAlCambiarTamano(
                dgvVentas,
                10);

            RedondearControlHelper.RedondearControl(btnActualizar, 8);
        }
    }
}
