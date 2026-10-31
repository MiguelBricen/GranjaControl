using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.Entitys.Dto;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Reportes
{
    public partial class FrmReportes : Form
    {
        private readonly ReporteBLL _reporteBLL;
        private readonly ClienteBLL _clienteBLL;
        private readonly GalponBLL _galponBLL;
        private readonly ProveedorBLL _proveedorBLL;
        private readonly Mensajes _mensajes = new();

        private readonly Dictionary<string, Label> _resumenVentas = new();
        private readonly Dictionary<string, Label> _resumenProduccion = new();
        private readonly Dictionary<string, Label> _resumenCompras = new();

        private DateTimePicker _dtpVentasInicio = null!;
        private DateTimePicker _dtpVentasFin = null!;
        private ComboBox _cmbVentasCliente = null!;
        private ComboBox _cmbVentasEstado = null!;
        private DataGridView _dgvVentas = null!;

        private DateTimePicker _dtpProduccionInicio = null!;
        private DateTimePicker _dtpProduccionFin = null!;
        private ComboBox _cmbProduccionEstado = null!;
        private ComboBox _cmbProduccionGalpon = null!;
        private DataGridView _dgvProduccion = null!;

        private DateTimePicker _dtpComprasInicio = null!;
        private DateTimePicker _dtpComprasFin = null!;
        private ComboBox _cmbComprasProveedor = null!;
        private DataGridView _dgvCompras = null!;

        public FrmReportes()
        {
            InitializeComponent();

            _reporteBLL = new ReporteBLL(new ReporteDAL());
            _clienteBLL = new ClienteBLL(new ClienteDAL());
            _galponBLL = new GalponBLL(new GalponDAL());
            _proveedorBLL = new ProveedorBLL(new ProveedorDAL());

            ConfigurarPestanas();
        }

        private void ConfigurarPestanas()
        {
            tabVentas.Controls.Add(ConstruirPaginaVentas());
            tabProduccion.Controls.Add(ConstruirPaginaProduccion());
            tabCompras.Controls.Add(ConstruirPaginaCompras());
        }

        private Control ConstruirPaginaVentas()
        {
            TableLayoutPanel layout = CrearLayoutPagina();
            FlowLayoutPanel filtros = CrearFlujoFiltros();

            DateTime hoy = DateTime.Today;

            _dtpVentasInicio = CrearFecha(
                new DateTime(hoy.Year, hoy.Month, 1));

            _dtpVentasFin = CrearFecha(hoy);

            _cmbVentasCliente = CrearCombo(205);
            _cmbVentasEstado = CrearCombo(155);

            _cmbVentasEstado.Items.Add("Todos los estados");

            foreach (string estado in VentaEntity.EstadosPermitidos)
                _cmbVentasEstado.Items.Add(estado);

            _cmbVentasEstado.SelectedIndex = 0;

            Button btnConsultar = CrearBoton("Consultar", true);
            Button btnLimpiar = CrearBoton("Limpiar", false);

            btnConsultar.Click += btnVentasConsultar_Click;
            btnLimpiar.Click += btnVentasLimpiar_Click;

            filtros.Controls.Add(CrearCampo("Desde", _dtpVentasInicio, 155));
            filtros.Controls.Add(CrearCampo("Hasta", _dtpVentasFin, 155));
            filtros.Controls.Add(CrearCampo("Cliente", _cmbVentasCliente, 205));
            filtros.Controls.Add(CrearCampo("Estado", _cmbVentasEstado, 155));
            filtros.Controls.Add(btnConsultar);
            filtros.Controls.Add(btnLimpiar);

            TableLayoutPanel resumen = CrearTablaResumen(5);

            AgregarResumen(
                resumen,
                _resumenVentas,
                "total",
                "Total ventas",
                EstiloVisualHelper.Azul);

            AgregarResumen(
                resumen,
                _resumenVentas,
                "registradas",
                "Registradas",
                EstiloVisualHelper.Verde);

            AgregarResumen(
                resumen,
                _resumenVentas,
                "pollos",
                "Pollos vendidos",
                EstiloVisualHelper.Turquesa);

            AgregarResumen(
                resumen,
                _resumenVentas,
                "libras",
                "Libras vendidas",
                EstiloVisualHelper.Morado);

            AgregarResumen(
                resumen,
                _resumenVentas,
                "ingresos",
                "Ingresos",
                EstiloVisualHelper.Dorado);

            _dgvVentas = CrearGrid();

            AgregarColumna(
                _dgvVentas,
                "NumeroVenta",
                "N° venta",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleLeft);

            AgregarColumna(
                _dgvVentas,
                "NombreCliente",
                "Cliente",
                DataGridViewAutoSizeColumnMode.Fill,
                DataGridViewContentAlignment.MiddleLeft,
                30);

            AgregarColumna(
                _dgvVentas,
                "FechaVenta",
                "Fecha",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleCenter);

            AgregarColumna(
                _dgvVentas,
                "Estado",
                "Estado",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleCenter);

            AgregarColumna(
                _dgvVentas,
                "CantidadPollos",
                "Pollos",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            AgregarColumna(
                _dgvVentas,
                "PesoLibras",
                "Libras",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            AgregarColumna(
                _dgvVentas,
                "Total",
                "Total",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            _dgvVentas.CellFormatting += FormatearCeldaVenta;

            layout.Controls.Add(CrearPanelFiltro(filtros), 0, 0);
            layout.Controls.Add(CrearPanelResumen(resumen), 0, 1);
            layout.Controls.Add(CrearPanelGrid(_dgvVentas), 0, 2);

            return layout;
        }

        private Control ConstruirPaginaProduccion()
        {
            TableLayoutPanel layout = CrearLayoutPagina();
            FlowLayoutPanel filtros = CrearFlujoFiltros();

            DateTime hoy = DateTime.Today;

            _dtpProduccionInicio = CrearFecha(
                new DateTime(hoy.Year, hoy.Month, 1));

            _dtpProduccionFin = CrearFecha(hoy);

            _cmbProduccionEstado = CrearCombo(170);
            _cmbProduccionGalpon = CrearCombo(190);

            _cmbProduccionEstado.Items.Add("Todos los estados");

            foreach (string estado in LoteEntity.EstadosPermitidos)
                _cmbProduccionEstado.Items.Add(estado);

            _cmbProduccionEstado.SelectedIndex = 0;

            Button btnConsultar = CrearBoton("Consultar", true);
            Button btnLimpiar = CrearBoton("Limpiar", false);

            btnConsultar.Click += btnProduccionConsultar_Click;
            btnLimpiar.Click += btnProduccionLimpiar_Click;

            filtros.Controls.Add(
                CrearCampo("Desde", _dtpProduccionInicio, 155));

            filtros.Controls.Add(
                CrearCampo("Hasta", _dtpProduccionFin, 155));

            filtros.Controls.Add(
                CrearCampo("Estado", _cmbProduccionEstado, 170));

            filtros.Controls.Add(
                CrearCampo("Galpón", _cmbProduccionGalpon, 190));

            filtros.Controls.Add(btnConsultar);
            filtros.Controls.Add(btnLimpiar);

            TableLayoutPanel resumen = CrearTablaResumen(5);

            AgregarResumen(
                resumen,
                _resumenProduccion,
                "lotes",
                "Lotes",
                EstiloVisualHelper.Azul);

            AgregarResumen(
                resumen,
                _resumenProduccion,
                "inicial",
                "Aves iniciales",
                EstiloVisualHelper.Verde);

            AgregarResumen(
                resumen,
                _resumenProduccion,
                "mortalidad",
                "Mortalidad",
                EstiloVisualHelper.Rojo);

            AgregarResumen(
                resumen,
                _resumenProduccion,
                "disponibles",
                "Disponibles",
                EstiloVisualHelper.Turquesa);

            AgregarResumen(
                resumen,
                _resumenProduccion,
                "porcentaje",
                "Mortalidad %",
                EstiloVisualHelper.Dorado);

            _dgvProduccion = CrearGrid();

            AgregarColumna(
                _dgvProduccion,
                "CodigoLote",
                "Lote",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleLeft);

            AgregarColumna(
                _dgvProduccion,
                "NombreGalpon",
                "Galpón",
                DataGridViewAutoSizeColumnMode.Fill,
                DataGridViewContentAlignment.MiddleLeft,
                24);

            AgregarColumna(
                _dgvProduccion,
                "NombreProveedor",
                "Proveedor",
                DataGridViewAutoSizeColumnMode.Fill,
                DataGridViewContentAlignment.MiddleLeft,
                26);

            AgregarColumna(
                _dgvProduccion,
                "FechaIngreso",
                "Ingreso",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleCenter);

            AgregarColumna(
                _dgvProduccion,
                "Estado",
                "Estado",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleCenter);

            AgregarColumna(
                _dgvProduccion,
                "CantidadInicial",
                "Inicial",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            AgregarColumna(
                _dgvProduccion,
                "Mortalidad",
                "Mortalidad",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            AgregarColumna(
                _dgvProduccion,
                "Vendidos",
                "Vendidos",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            AgregarColumna(
                _dgvProduccion,
                "Disponibles",
                "Disponibles",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            AgregarColumna(
                _dgvProduccion,
                "PorcentajeMortalidad",
                "Mort. %",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            _dgvProduccion.CellFormatting += FormatearCeldaProduccion;

            layout.Controls.Add(CrearPanelFiltro(filtros), 0, 0);
            layout.Controls.Add(CrearPanelResumen(resumen), 0, 1);
            layout.Controls.Add(CrearPanelGrid(_dgvProduccion), 0, 2);

            return layout;
        }

        private Control ConstruirPaginaCompras()
        {
            TableLayoutPanel layout = CrearLayoutPagina();
            FlowLayoutPanel filtros = CrearFlujoFiltros();

            DateTime hoy = DateTime.Today;

            _dtpComprasInicio = CrearFecha(
                new DateTime(hoy.Year, hoy.Month, 1));

            _dtpComprasFin = CrearFecha(hoy);

            _cmbComprasProveedor = CrearCombo(230);

            Button btnConsultar = CrearBoton("Consultar", true);
            Button btnLimpiar = CrearBoton("Limpiar", false);

            btnConsultar.Click += btnComprasConsultar_Click;
            btnLimpiar.Click += btnComprasLimpiar_Click;

            filtros.Controls.Add(
                CrearCampo("Desde", _dtpComprasInicio, 155));

            filtros.Controls.Add(
                CrearCampo("Hasta", _dtpComprasFin, 155));

            filtros.Controls.Add(
                CrearCampo("Proveedor", _cmbComprasProveedor, 230));

            filtros.Controls.Add(btnConsultar);
            filtros.Controls.Add(btnLimpiar);

            TableLayoutPanel resumen = CrearTablaResumen(4);

            AgregarResumen(
                resumen,
                _resumenCompras,
                "compras",
                "Compras",
                EstiloVisualHelper.Azul);

            AgregarResumen(
                resumen,
                _resumenCompras,
                "libras",
                "Libras",
                EstiloVisualHelper.Verde);

            AgregarResumen(
                resumen,
                _resumenCompras,
                "kilogramos",
                "Kilogramos",
                EstiloVisualHelper.Turquesa);

            AgregarResumen(
                resumen,
                _resumenCompras,
                "costo",
                "Costo total",
                EstiloVisualHelper.Dorado);

            _dgvCompras = CrearGrid();

            AgregarColumna(
                _dgvCompras,
                "NumeroCompra",
                "N° compra",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleLeft);

            AgregarColumna(
                _dgvCompras,
                "NombreProveedor",
                "Proveedor",
                DataGridViewAutoSizeColumnMode.Fill,
                DataGridViewContentAlignment.MiddleLeft,
                30);

            AgregarColumna(
                _dgvCompras,
                "FechaCompra",
                "Fecha",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleCenter);

            AgregarColumna(
                _dgvCompras,
                "Libras",
                "Libras",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            AgregarColumna(
                _dgvCompras,
                "Kilogramos",
                "Kilogramos",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            AgregarColumna(
                _dgvCompras,
                "CostoTotal",
                "Costo total",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            AgregarColumna(
                _dgvCompras,
                "CantidadItems",
                "Ítems",
                DataGridViewAutoSizeColumnMode.AllCells,
                DataGridViewContentAlignment.MiddleRight);

            _dgvCompras.CellFormatting += FormatearCeldaCompra;

            layout.Controls.Add(CrearPanelFiltro(filtros), 0, 0);
            layout.Controls.Add(CrearPanelResumen(resumen), 0, 1);
            layout.Controls.Add(CrearPanelGrid(_dgvCompras), 0, 2);

            return layout;
        }

        private static TableLayoutPanel CrearLayoutPagina()
        {
            TableLayoutPanel layout = new()
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                RowCount = 3
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 94F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            return layout;
        }

        private static FlowLayoutPanel CrearFlujoFiltros()
        {
            return new FlowLayoutPanel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0),
                WrapContents = false
            };
        }

        private static Panel CrearPanelFiltro(FlowLayoutPanel filtros)
        {
            Panel panel = new()
            {
                BackColor = EstiloVisualHelper.Superficie,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 8),
                Padding = new Padding(10, 5, 10, 5)
            };

            panel.Controls.Add(filtros);

            return panel;
        }

        private static Panel CrearPanelResumen(TableLayoutPanel resumen)
        {
            Panel panel = new()
            {
                BackColor = EstiloVisualHelper.Superficie,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 8),
                Padding = new Padding(8)
            };

            panel.Controls.Add(resumen);

            RedondearControlHelper.RedondearControlAlCambiarTamano(
                panel,
                12);

            return panel;
        }

        private static Panel CrearPanelGrid(DataGridView dgv)
        {
            Panel panel = new()
            {
                BackColor = EstiloVisualHelper.Superficie,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(10)
            };

            panel.Controls.Add(dgv);

            RedondearControlHelper.RedondearControlAlCambiarTamano(
                panel,
                12);

            return panel;
        }

        private static DateTimePicker CrearFecha(DateTime valor)
        {
            return new DateTimePicker
            {
                BackColor = EstiloVisualHelper.Superficie,
                Checked = true,
                Font = new Font("Segoe UI", 9.5F),
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = true,
                Value = valor
            };
        }

        private static ComboBox CrearCombo(int ancho)
        {
            return new ComboBox
            {
                BackColor = EstiloVisualHelper.Superficie,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = EstiloVisualHelper.TextoPrincipal,
                Width = ancho
            };
        }

        private static Button CrearBoton(string texto, bool principal)
        {
            Button boton = new()
            {
                BackColor = principal
                    ? EstiloVisualHelper.VerdeOscuro
                    : EstiloVisualHelper.VerdeClaro,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                ForeColor = principal
                    ? Color.White
                    : EstiloVisualHelper.VerdeOscuro,
                Height = 34,
                Margin = new Padding(0, 23, 8, 0),
                Text = texto,
                Width = 108,
                UseVisualStyleBackColor = false
            };

            boton.FlatAppearance.BorderSize = 0;

            RedondearControlHelper.RedondearControlAlCambiarTamano(
                boton,
                8);

            return boton;
        }

        private static Panel CrearCampo(
            string titulo,
            Control control,
            int ancho)
        {
            Panel campo = new()
            {
                BackColor = Color.Transparent,
                Height = 62,
                Margin = new Padding(0, 0, 10, 0),
                Width = ancho
            };

            Label etiqueta = new()
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                ForeColor = EstiloVisualHelper.TextoSecundario,
                Location = new Point(0, 0),
                Text = titulo
            };

            control.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            control.Location = new Point(0, 23);
            control.Width = ancho;

            campo.Controls.Add(control);
            campo.Controls.Add(etiqueta);

            return campo;
        }

        private static TableLayoutPanel CrearTablaResumen(int columnas)
        {
            TableLayoutPanel tabla = new()
            {
                ColumnCount = columnas,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                RowCount = 1
            };

            for (int i = 0; i < columnas; i++)
            {
                tabla.ColumnStyles.Add(
                    new ColumnStyle(
                        SizeType.Percent,
                        100F / columnas));
            }

            tabla.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            return tabla;
        }

        private void AgregarResumen(
            TableLayoutPanel tabla,
            Dictionary<string, Label> valores,
            string clave,
            string titulo,
            Color color)
        {
            Panel tarjeta = EstiloVisualHelper.CrearTarjeta(
                titulo,
                color,
                false,
                15.5F,
                out Label valor,
                out _);

            tarjeta.Margin = new Padding(3);

            tabla.Controls.Add(tarjeta, tabla.Controls.Count, 0);

            valores[clave] = valor;
        }

        private static DataGridView CrearGrid()
        {
            DataGridView dgv = new()
            {
                AutoGenerateColumns = false,
                Dock = DockStyle.Fill
            };

            GridDashboardHelper.Personalizar(dgv);

            return dgv;
        }

        private static void AgregarColumna(
            DataGridView dgv,
            string propiedad,
            string encabezado,
            DataGridViewAutoSizeColumnMode modo,
            DataGridViewContentAlignment alineacion,
            float peso = 100)
        {
            DataGridViewTextBoxColumn columna = new()
            {
                AutoSizeMode = modo,
                DataPropertyName = propiedad,
                FillWeight = peso,
                HeaderText = encabezado,
                Name = propiedad,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };

            columna.DefaultCellStyle.Alignment = alineacion;
            columna.MinimumWidth = modo == DataGridViewAutoSizeColumnMode.Fill
                ? 120
                : 70;

            dgv.Columns.Add(columna);
        }

        private async void FrmReportes_Load(object sender, EventArgs e)
        {
            await CargarFiltrosAsync();
            await CargarVentasAsync();
        }

        private async Task CargarFiltrosAsync()
        {
            await CargarClientesAsync();
            await CargarGalponesAsync();
            await CargarProveedoresAsync();
        }

        private async Task CargarClientesAsync()
        {
            _cmbVentasCliente.Items.Add(
                new OpcionFiltro(0, "Todos los clientes"));

            try
            {
                var resultado = await _clienteBLL.MostrarAsync(
                    1,
                    10000);

                foreach (ClienteEntity cliente in resultado.Registros
                    .OrderBy(c => c.NombreCliente))
                {
                    _cmbVentasCliente.Items.Add(
                        new OpcionFiltro(
                            cliente.Id,
                            cliente.NombreCliente));
                }
            }
            catch (Exception ex)
            {
                _mensajes.MensajeError(
                    $"No se pudieron cargar los clientes: {ex.Message}");
            }

            _cmbVentasCliente.SelectedIndex = 0;
        }

        private async Task CargarGalponesAsync()
        {
            _cmbProduccionGalpon.Items.Add(
                new OpcionFiltro(0, "Todos los galpones"));

            try
            {
                var resultado = await _galponBLL.MostrarAsync(
                    1,
                    10000);

                foreach (var galpon in resultado.Registros
                    .OrderBy(g => g.NombreGalpon))
                {
                    _cmbProduccionGalpon.Items.Add(
                        new OpcionFiltro(
                            galpon.Id,
                            galpon.NombreGalpon));
                }
            }
            catch (Exception ex)
            {
                _mensajes.MensajeError(
                    $"No se pudieron cargar los galpones: {ex.Message}");
            }

            _cmbProduccionGalpon.SelectedIndex = 0;
        }

        private async Task CargarProveedoresAsync()
        {
            _cmbComprasProveedor.Items.Add(
                new OpcionFiltro(0, "Todos los proveedores"));

            try
            {
                var resultado = await _proveedorBLL.MostrarAsync(
                    1,
                    10000);

                foreach (var proveedor in resultado.Registros
                    .OrderBy(p => p.NombreProveedor))
                {
                    _cmbComprasProveedor.Items.Add(
                        new OpcionFiltro(
                            proveedor.Id,
                            proveedor.NombreProveedor));
                }
            }
            catch (Exception ex)
            {
                _mensajes.MensajeError(
                    $"No se pudieron cargar los proveedores: {ex.Message}");
            }

            _cmbComprasProveedor.SelectedIndex = 0;
        }

        private async void tabReportes_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            if (tabReportes.SelectedTab == tabProduccion)
                await CargarProduccionAsync();
            else if (tabReportes.SelectedTab == tabCompras)
                await CargarComprasAsync();
        }

        private async void btnVentasConsultar_Click(
            object? sender,
            EventArgs e)
        {
            await CargarVentasAsync();
        }

        private async void btnVentasLimpiar_Click(
            object? sender,
            EventArgs e)
        {
            _dtpVentasInicio.Checked = false;
            _dtpVentasFin.Checked = false;
            _cmbVentasCliente.SelectedIndex = 0;
            _cmbVentasEstado.SelectedIndex = 0;

            await CargarVentasAsync();
        }

        private async void btnProduccionConsultar_Click(
            object? sender,
            EventArgs e)
        {
            await CargarProduccionAsync();
        }

        private async void btnProduccionLimpiar_Click(
            object? sender,
            EventArgs e)
        {
            _dtpProduccionInicio.Checked = false;
            _dtpProduccionFin.Checked = false;
            _cmbProduccionEstado.SelectedIndex = 0;
            _cmbProduccionGalpon.SelectedIndex = 0;

            await CargarProduccionAsync();
        }

        private async void btnComprasConsultar_Click(
            object? sender,
            EventArgs e)
        {
            await CargarComprasAsync();
        }

        private async void btnComprasLimpiar_Click(
            object? sender,
            EventArgs e)
        {
            _dtpComprasInicio.Checked = false;
            _dtpComprasFin.Checked = false;
            _cmbComprasProveedor.SelectedIndex = 0;

            await CargarComprasAsync();
        }

        private async Task CargarVentasAsync()
        {
            try
            {
                var datos = await _reporteBLL.ObtenerVentasAsync(
                    ObtenerFecha(_dtpVentasInicio),
                    ObtenerFecha(_dtpVentasFin),
                    ObtenerId(_cmbVentasCliente),
                    ObtenerEstado(_cmbVentasEstado));

                _dgvVentas.DataSource = null;
                _dgvVentas.DataSource = datos.Detalle;

                MostrarResumenVentas(datos.Resumen);
            }
            catch (Exception ex)
            {
                _mensajes.MensajeError(
                    $"No se pudo generar el reporte de ventas: {ex.Message}");
            }
        }

        private async Task CargarProduccionAsync()
        {
            try
            {
                var datos = await _reporteBLL.ObtenerProduccionAsync(
                    ObtenerFecha(_dtpProduccionInicio),
                    ObtenerFecha(_dtpProduccionFin),
                    ObtenerEstado(_cmbProduccionEstado),
                    ObtenerId(_cmbProduccionGalpon));

                _dgvProduccion.DataSource = null;
                _dgvProduccion.DataSource = datos.Detalle;

                MostrarResumenProduccion(datos.Resumen);
            }
            catch (Exception ex)
            {
                _mensajes.MensajeError(
                    $"No se pudo generar el reporte de producción: {ex.Message}");
            }
        }

        private async Task CargarComprasAsync()
        {
            try
            {
                var datos = await _reporteBLL.ObtenerComprasAsync(
                    ObtenerFecha(_dtpComprasInicio),
                    ObtenerFecha(_dtpComprasFin),
                    ObtenerId(_cmbComprasProveedor));

                _dgvCompras.DataSource = null;
                _dgvCompras.DataSource = datos.Detalle;

                MostrarResumenCompras(datos.Resumen);
            }
            catch (Exception ex)
            {
                _mensajes.MensajeError(
                    $"No se pudo generar el reporte de compras: {ex.Message}");
            }
        }

        private void MostrarResumenVentas(
            ReporteVentasResumenDto resumen)
        {
            _resumenVentas["total"].Text =
                resumen.TotalVentas.ToString("N0");

            _resumenVentas["registradas"].Text =
                resumen.Registradas.ToString("N0");

            _resumenVentas["pollos"].Text =
                resumen.PollosVendidos.ToString("N0");

            _resumenVentas["libras"].Text =
                resumen.LibrasVendidas.ToString("N2");

            _resumenVentas["ingresos"].Text =
                resumen.Ingresos.ToString("N2");
        }

        private void MostrarResumenProduccion(
            ReporteProduccionResumenDto resumen)
        {
            _resumenProduccion["lotes"].Text =
                resumen.TotalLotes.ToString("N0");

            _resumenProduccion["inicial"].Text =
                resumen.CantidadInicial.ToString("N0");

            _resumenProduccion["mortalidad"].Text =
                resumen.Mortalidad.ToString("N0");

            _resumenProduccion["disponibles"].Text =
                resumen.Disponibles.ToString("N0");

            _resumenProduccion["porcentaje"].Text =
                $"{resumen.PorcentajeMortalidad:N2} %";
        }

        private void MostrarResumenCompras(
            ReporteComprasResumenDto resumen)
        {
            _resumenCompras["compras"].Text =
                resumen.TotalCompras.ToString("N0");

            _resumenCompras["libras"].Text =
                resumen.TotalLibras.ToString("N2");

            _resumenCompras["kilogramos"].Text =
                resumen.TotalKilogramos.ToString("N2");

            _resumenCompras["costo"].Text =
                resumen.CostoTotal.ToString("N2");
        }

        private void FormatearCeldaVenta(
            object? sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value is null || e.Value == DBNull.Value)
                return;

            string columna = _dgvVentas.Columns[e.ColumnIndex].Name;

            if (columna == "FechaVenta")
            {
                e.Value = Convert.ToDateTime(e.Value)
                    .ToString("dd/MM/yyyy");

                e.FormattingApplied = true;
            }
            else if (columna is "PesoLibras" or "Total")
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

        private void FormatearCeldaProduccion(
            object? sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value is null || e.Value == DBNull.Value)
                return;

            string columna = _dgvProduccion.Columns[e.ColumnIndex].Name;

            if (columna == "FechaIngreso")
            {
                e.Value = Convert.ToDateTime(e.Value)
                    .ToString("dd/MM/yyyy");

                e.FormattingApplied = true;
            }
            else if (columna == "PorcentajeMortalidad")
            {
                decimal valor = Convert.ToDecimal(e.Value);

                e.Value = $"{valor:N2} %";
                e.CellStyle.ForeColor = valor > 5
                    ? EstiloVisualHelper.Rojo
                    : EstiloVisualHelper.Verde;

                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
                e.FormattingApplied = true;
            }
        }

        private void FormatearCeldaCompra(
            object? sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value is null || e.Value == DBNull.Value)
                return;

            string columna = _dgvCompras.Columns[e.ColumnIndex].Name;

            if (columna == "FechaCompra")
            {
                e.Value = Convert.ToDateTime(e.Value)
                    .ToString("dd/MM/yyyy");

                e.FormattingApplied = true;
            }
            else if (columna is "Libras" or "Kilogramos" or "CostoTotal")
            {
                e.Value = Convert.ToDecimal(e.Value).ToString("N2");
                e.FormattingApplied = true;
            }
        }

        private static DateTime? ObtenerFecha(DateTimePicker dtp)
        {
            return dtp.Checked ? dtp.Value.Date : null;
        }

        private static int? ObtenerId(ComboBox cmb)
        {
            if (cmb.SelectedItem is OpcionFiltro opcion &&
                opcion.Id > 0)
            {
                return opcion.Id;
            }

            return null;
        }

        private static string? ObtenerEstado(ComboBox cmb)
        {
            if (cmb.SelectedIndex <= 0)
                return null;

            return cmb.SelectedItem?.ToString();
        }

        private void tabReportes_DrawItem(
            object? sender,
            DrawItemEventArgs e)
        {
            TabPage pagina = tabReportes.TabPages[e.Index];

            bool seleccionada =
                tabReportes.SelectedIndex == e.Index;

            Color fondo = seleccionada
                ? EstiloVisualHelper.VerdeOscuro
                : EstiloVisualHelper.VerdeClaro;

            Color texto = seleccionada
                ? Color.White
                : EstiloVisualHelper.VerdeOscuro;

            using SolidBrush pincel = new(fondo);

            e.Graphics.FillRectangle(pincel, e.Bounds);

            TextRenderer.DrawText(
                e.Graphics,
                pagina.Text,
                tabReportes.Font,
                e.Bounds,
                texto,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis);
        }

        private void FrmReportes_Shown(object sender, EventArgs e)
        {
            RedondearControlHelper.RedondearControlAlCambiarTamano(
                _dgvVentas,
                10);

            RedondearControlHelper.RedondearControlAlCambiarTamano(
                _dgvProduccion,
                10);

            RedondearControlHelper.RedondearControlAlCambiarTamano(
                _dgvCompras,
                10);
        }

        private sealed class OpcionFiltro
        {
            public OpcionFiltro(int id, string nombre)
            {
                Id = id;
                Nombre = nombre;
            }

            public int Id { get; }

            public string Nombre { get; }

            public override string ToString()
            {
                return Nombre;
            }
        }
    }
}

