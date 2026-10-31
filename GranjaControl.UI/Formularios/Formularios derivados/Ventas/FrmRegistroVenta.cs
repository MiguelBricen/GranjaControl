using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Ventas
{
    public partial class FrmRegistroVenta : Form
    {
        private readonly VentaBLL _ventaBLL;
        private readonly ClienteBLL _clienteBLL;
        private readonly LoteBLL _loteBLL;
        private readonly TarifaPolloBLL _tarifaBLL;
        private readonly int? _idVenta;
        private readonly Mensajes mensajes = new();
        private readonly List<DetalleVentaEntity> _detalles = [];
        private readonly List<TarifaPolloEntity> _tarifas = [];

        private VentaEntity? _ventaActual;
        private decimal? _precioVigente;
        private int _indiceDetalleSeleccionado = -1;
        private bool _actualizandoDetalles;
        private bool _cargando;
        private bool _esSoloLectura;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idVenta.HasValue;

        public FrmRegistroVenta(
            VentaBLL ventaBLL,
            ClienteBLL clienteBLL,
            LoteBLL loteBLL,
            TarifaPolloBLL tarifaBLL)
        {
            InitializeComponent();

            _ventaBLL = ventaBLL;
            _clienteBLL = clienteBLL;
            _loteBLL = loteBLL;
            _tarifaBLL = tarifaBLL;
            _idVenta = null;

            lblTitulo.Text = "Nueva venta";
            lblDescripcion.Text =
                "Registre el encabezado y los lotes incluidos en la venta.";
        }

        public FrmRegistroVenta(
            VentaBLL ventaBLL,
            ClienteBLL clienteBLL,
            LoteBLL loteBLL,
            TarifaPolloBLL tarifaBLL,
            int idVenta)
        {
            InitializeComponent();

            _ventaBLL = ventaBLL;
            _clienteBLL = clienteBLL;
            _loteBLL = loteBLL;
            _tarifaBLL = tarifaBLL;
            _idVenta = idVenta;

            lblTitulo.Text = "Editar venta";
            lblDescripcion.Text =
                "Modifique el encabezado y los detalles de la venta.";
        }

        private async Task CargarCombosAsync()
        {
            if (_idVenta.HasValue)
            {
                _ventaActual = await _ventaBLL.ObtenerPorIdAsync(
                    _idVenta.Value);

                if (_ventaActual is null)
                {
                    mensajes.MensajeError(
                        "No se encontró la venta especificada.");

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
            }

            List<ClienteEntity> clientes =
                await _clienteBLL.MostrarClientesActivosAsync();

            if (_ventaActual is not null &&
                !clientes.Any(c => c.Id == _ventaActual.IdCliente))
            {
                ClienteEntity? cliente =
                    await _clienteBLL.ObtenerPorIdAsync(
                        _ventaActual.IdCliente);

                if (cliente is not null)
                    clientes.Add(cliente);
            }

            cboCliente.DisplayMember =
                nameof(ClienteEntity.NombreCliente);

            cboCliente.ValueMember = nameof(ClienteEntity.Id);
            cboCliente.DataSource = clientes;
            cboCliente.SelectedIndex = -1;

            List<LoteEntity> lotes =
                await _loteBLL.MostrarLotesActivosAsync();

            if (_ventaActual is not null)
            {
                List<DetalleVentaEntity> detalles =
                    await _ventaBLL.ObtenerDetallesAsync(
                        _ventaActual.Id);

                _detalles.AddRange(detalles);

                foreach (DetalleVentaEntity detalle in detalles)
                {
                    if (lotes.Any(l => l.Id == detalle.IdLote))
                        continue;

                    LoteEntity? lote =
                        await _loteBLL.ObtenerPorIdAsync(
                            detalle.IdLote);

                    if (lote is not null)
                        lotes.Add(lote);
                }
            }

            cboLoteDetalle.DisplayMember =
                nameof(LoteEntity.CodigoLote);

            cboLoteDetalle.ValueMember = nameof(LoteEntity.Id);
            cboLoteDetalle.DataSource = lotes;
            cboLoteDetalle.SelectedIndex = -1;

            _tarifas.Clear();

            _tarifas.AddRange(
                (await _tarifaBLL.MostrarActivasAsync())
                .OrderByDescending(t => t.FechaInicio));

            if (clientes.Count == 0)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeInformacion(
                    "No hay clientes activos disponibles para registrar ventas.");
            }

            if (lotes.Count == 0)
            {
                btnGuardar.Enabled = false;
                btnAgregarDetalle.Enabled = false;

                mensajes.MensajeInformacion(
                    "No hay lotes activos disponibles para registrar ventas.");
            }
        }

        private void CargarDatos()
        {
            _cargando = true;

            if (_ventaActual is null)
            {
                dtpFecha.Value = DateTime.Today;

                _cargando = false;

                ActualizarPrecioVigente();
                RefrescarDetalles();
                LimpiarEntradaDetalle();
                return;
            }

            txtNumero.Text = _ventaActual.NumeroVenta;
            dtpFecha.Value = _ventaActual.FechaVenta;
            txtObservacion.Text =
                _ventaActual.Observacion ?? string.Empty;

            cboCliente.SelectedValue = _ventaActual.IdCliente;

            lblEstadoVenta.Text = $"Estado: {_ventaActual.Estado}";

            _esSoloLectura = _ventaActual.Estado.Equals(
                "Anulada",
                StringComparison.OrdinalIgnoreCase);

            if (_esSoloLectura)
            {
                lblTitulo.Text = "Venta anulada";
                lblDescripcion.Text =
                    "La venta está anulada y se muestra en modo solo lectura.";
            }

            _cargando = false;

            ActualizarPrecioVigente();
            RefrescarDetalles();

            if (_esSoloLectura)
                ConfigurarSoloLectura();
        }

        private void ConfigurarSoloLectura()
        {
            txtNumero.ReadOnly = true;
            dtpFecha.Enabled = false;
            cboCliente.Enabled = false;
            cboLoteDetalle.Enabled = false;
            txtObservacion.ReadOnly = true;

            numCantidadPollos.Enabled = false;
            numPesoLibras.Enabled = false;

            btnAgregarDetalle.Enabled = false;
            btnActualizarDetalle.Enabled = false;
            btnQuitarDetalle.Enabled = false;
            btnGuardar.Visible = false;

            btnCancelar.Text = "Cerrar";
            lblEstadoVenta.ForeColor = Color.FromArgb(185, 28, 28);
        }

        private void ActualizarPrecioVigente()
        {
            DateTime fecha = dtpFecha.Value.Date;

            TarifaPolloEntity? tarifa = _tarifas
                .Where(t =>
                    fecha >= t.FechaInicio.Date &&
                    (t.FechaFin is null ||
                     fecha <= t.FechaFin.Value.Date))
                .OrderByDescending(t => t.FechaInicio)
                .FirstOrDefault();

            _precioVigente = tarifa?.PrecioPorLibra;

            lblPrecioVigente.Text = tarifa is null
                ? "Precio vigente: sin tarifa"
                : $"Precio vigente: {tarifa.PrecioPorLibra:N2} / lb";

            lblPrecioVigente.ForeColor = tarifa is null
                ? Color.FromArgb(185, 28, 28)
                : Color.FromArgb(23, 61, 43);

            lblPrecioDetalle.Text = tarifa is null
                ? "--"
                : tarifa.PrecioPorLibra.ToString("N2");

            if (_precioVigente.HasValue)
            {
                foreach (DetalleVentaEntity detalle in _detalles)
                {
                    detalle.CambiarPrecioPorLibra(
                        _precioVigente.Value);
                }
            }

            bool puedeEditar =
                !_esSoloLectura && _precioVigente.HasValue;

            btnAgregarDetalle.Enabled = puedeEditar;
            btnGuardar.Enabled = puedeEditar;
        }

        private void RefrescarDetalles()
        {
            _actualizandoDetalles = true;

            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = _detalles.ToList();

            ConfigurarGridDetalles();

            _indiceDetalleSeleccionado = -1;

            dgvDetalles.ClearSelection();

            btnActualizarDetalle.Enabled = false;
            btnQuitarDetalle.Enabled = false;

            _actualizandoDetalles = false;

            ActualizarTotales();
        }

        private void ConfigurarGridDetalles()
        {
            if (!dgvDetalles.Columns.Contains("Id"))
                return;

            dgvDetalles.BorderStyle = BorderStyle.None;
            dgvDetalles.BackgroundColor = Color.White;
            dgvDetalles.GridColor = Color.FromArgb(220, 228, 222);

            dgvDetalles.EnableHeadersVisualStyles = false;
            dgvDetalles.RowHeadersVisible = false;

            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToDeleteRows = false;
            dgvDetalles.ReadOnly = true;
            dgvDetalles.MultiSelect = false;
            dgvDetalles.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDetalles.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            dgvDetalles.ColumnHeadersHeight = 38;
            dgvDetalles.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvDetalles.RowTemplate.Height = 32;
            dgvDetalles.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvDetalles.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(32, 76, 56);

            dgvDetalles.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvDetalles.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(32, 76, 56);

            dgvDetalles.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvDetalles.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10.5F, FontStyle.Bold);

            dgvDetalles.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvDetalles.DefaultCellStyle.BackColor = Color.White;
            dgvDetalles.DefaultCellStyle.ForeColor = Color.Black;

            dgvDetalles.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(226, 239, 228);

            dgvDetalles.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(32, 76, 56);

            dgvDetalles.DefaultCellStyle.Font =
                new Font("Segoe UI", 10.5F, FontStyle.Bold);

            dgvDetalles.DefaultCellStyle.Padding =
                new Padding(6, 0, 6, 0);

            dgvDetalles.Columns["Id"]!.Visible = false;
            dgvDetalles.Columns["IdVenta"]!.Visible = false;
            dgvDetalles.Columns["NumeroVenta"]!.Visible = false;
            dgvDetalles.Columns["IdLote"]!.Visible = false;

            dgvDetalles.Columns["CodigoLote"]!.HeaderText = "Lote";
            dgvDetalles.Columns["CantidadPollos"]!.HeaderText = "Pollos";
            dgvDetalles.Columns["PesoLibras"]!.HeaderText = "Libras";
            dgvDetalles.Columns["PrecioPorLibra"]!.HeaderText =
                "Precio / libra";

            dgvDetalles.Columns["Subtotal"]!.HeaderText = "Subtotal";

            dgvDetalles.Columns["CodigoLote"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvDetalles.Columns["CodigoLote"]!.FillWeight = 28;

            dgvDetalles.Columns["CantidadPollos"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvDetalles.Columns["PesoLibras"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvDetalles.Columns["PrecioPorLibra"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvDetalles.Columns["Subtotal"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvDetalles.Columns["CantidadPollos"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetalles.Columns["PesoLibras"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetalles.Columns["PrecioPorLibra"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetalles.Columns["Subtotal"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetalles.Columns["CodigoLote"]!.DisplayIndex = 0;
            dgvDetalles.Columns["CantidadPollos"]!.DisplayIndex = 1;
            dgvDetalles.Columns["PesoLibras"]!.DisplayIndex = 2;
            dgvDetalles.Columns["PrecioPorLibra"]!.DisplayIndex = 3;
            dgvDetalles.Columns["Subtotal"]!.DisplayIndex = 4;
        }

        private void ActualizarTotales()
        {
            int pollos = VentaBLL.CalcularCantidadPollos(_detalles);
            decimal libras = VentaBLL.CalcularPesoTotal(_detalles);
            decimal total = VentaBLL.CalcularTotal(_detalles);

            lblTotales.Text =
                $"Pollos: {pollos:N0}    Libras: {libras:N2}";

            lblTotal.Text = $"Total: {total:N2}";
        }

        private void dgvDetalles_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (_actualizandoDetalles)
                return;

            if (dgvDetalles.CurrentRow?.DataBoundItem
                is not DetalleVentaEntity detalle)
            {
                _indiceDetalleSeleccionado = -1;
                btnActualizarDetalle.Enabled = false;
                btnQuitarDetalle.Enabled = false;
                return;
            }

            _indiceDetalleSeleccionado = dgvDetalles.CurrentRow.Index;

            CargarDetalleEnControles(detalle);

            btnActualizarDetalle.Enabled =
                !_esSoloLectura && _precioVigente.HasValue;

            btnQuitarDetalle.Enabled = !_esSoloLectura;
        }

        private void CargarDetalleEnControles(
            DetalleVentaEntity detalle)
        {
            cboLoteDetalle.SelectedValue = detalle.IdLote;

            numCantidadPollos.Value = detalle.CantidadPollos;
            numPesoLibras.Value = detalle.PesoLibras;
        }

        private bool ValidarDetalle()
        {
            errorIcon.Clear();

            bool valido = true;

            if (_precioVigente is null)
            {
                mensajes.MensajeValidacion(
                    "No existe una tarifa vigente para la fecha seleccionada.");

                return false;
            }

            if (cboLoteDetalle.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboLoteDetalle,
                    "Seleccione el lote de la venta.");

                valido = false;
            }
            else
            {
                int idLote = Convert.ToInt32(
                    cboLoteDetalle.SelectedValue);

                bool repetido = _detalles
                    .Where((d, i) => i != _indiceDetalleSeleccionado)
                    .Any(d => d.IdLote == idLote);

                if (repetido)
                {
                    errorIcon.SetError(
                        cboLoteDetalle,
                        "El lote ya está incluido en la venta.");

                    valido = false;
                }
            }

            if (numCantidadPollos.Value <= 0)
            {
                errorIcon.SetError(
                    numCantidadPollos,
                    "La cantidad de pollos debe ser mayor que cero.");

                valido = false;
            }

            if (numPesoLibras.Value <= 0)
            {
                errorIcon.SetError(
                    numPesoLibras,
                    "El peso total debe ser mayor que cero.");

                valido = false;
            }

            return valido;
        }

        private DetalleVentaEntity CrearDetalleDesdeControles(
            int id = 0,
            int idVenta = 0)
        {
            LoteEntity lote =
                (LoteEntity)cboLoteDetalle.SelectedItem!;

            DetalleVentaEntity detalle = new(
                idVenta,
                lote.Id,
                (int)numCantidadPollos.Value,
                numPesoLibras.Value,
                _precioVigente!.Value);

            detalle.AsignarLote(lote.Id, lote.CodigoLote);

            if (id > 0)
                detalle.AsignarId(id);

            return detalle;
        }

        private void LimpiarEntradaDetalle()
        {
            cboLoteDetalle.SelectedIndex = -1;
            numCantidadPollos.Value = 1;
            numPesoLibras.Value = 0;

            errorIcon.SetError(cboLoteDetalle, string.Empty);
            errorIcon.SetError(numCantidadPollos, string.Empty);
            errorIcon.SetError(numPesoLibras, string.Empty);
        }

        private void SeleccionarDetalle(int indice)
        {
            if (indice < 0 || indice >= dgvDetalles.Rows.Count)
                return;

            _actualizandoDetalles = true;

            dgvDetalles.ClearSelection();
            dgvDetalles.Rows[indice].Selected = true;
            dgvDetalles.CurrentCell =
                dgvDetalles.Rows[indice].Cells["CodigoLote"];

            _actualizandoDetalles = false;

            _indiceDetalleSeleccionado = indice;

            CargarDetalleEnControles(_detalles[indice]);

            btnActualizarDetalle.Enabled =
                !_esSoloLectura && _precioVigente.HasValue;

            btnQuitarDetalle.Enabled = !_esSoloLectura;
        }

        private void btnAgregarDetalle_Click(
            object sender,
            EventArgs e)
        {
            _indiceDetalleSeleccionado = -1;

            if (!ValidarDetalle())
                return;

            DetalleVentaEntity detalle =
                CrearDetalleDesdeControles(
                    idVenta: _idVenta ?? 0);

            _detalles.Add(detalle);

            RefrescarDetalles();
            LimpiarEntradaDetalle();
        }

        private void btnActualizarDetalle_Click(
            object sender,
            EventArgs e)
        {
            if (_indiceDetalleSeleccionado < 0 ||
                _indiceDetalleSeleccionado >= _detalles.Count)
            {
                mensajes.MensajeInformacion(
                    "Debe seleccionar un detalle para actualizar.");

                return;
            }

            if (!ValidarDetalle())
                return;

            DetalleVentaEntity actual =
                _detalles[_indiceDetalleSeleccionado];

            DetalleVentaEntity actualizado =
                CrearDetalleDesdeControles(
                    actual.Id,
                    actual.IdVenta);

            actualizado.AsignarVenta(
                actual.IdVenta,
                actual.NumeroVenta);

            _detalles[_indiceDetalleSeleccionado] = actualizado;

            int indice = _indiceDetalleSeleccionado;

            RefrescarDetalles();
            SeleccionarDetalle(indice);
        }

        private void btnQuitarDetalle_Click(
            object sender,
            EventArgs e)
        {
            if (_indiceDetalleSeleccionado < 0 ||
                _indiceDetalleSeleccionado >= _detalles.Count)
            {
                mensajes.MensajeInformacion(
                    "Debe seleccionar un detalle para quitar.");

                return;
            }

            _detalles.RemoveAt(_indiceDetalleSeleccionado);

            RefrescarDetalles();
            LimpiarEntradaDetalle();
        }

        private bool ValidarVenta()
        {
            errorIcon.Clear();

            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                errorIcon.SetError(
                    txtNumero,
                    "Ingrese el número de venta.");

                valido = false;
            }

            if (cboCliente.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboCliente,
                    "Seleccione el cliente de la venta.");

                valido = false;
            }

            if (_precioVigente is null)
            {
                mensajes.MensajeValidacion(
                    "No existe una tarifa vigente para la fecha seleccionada.");

                valido = false;
            }

            if (_detalles.Count == 0)
            {
                mensajes.MensajeValidacion(
                    "Debe agregar al menos un lote a la venta.");

                valido = false;
            }

            return valido;
        }

        private VentaEntity CrearVenta()
        {
            VentaEntity venta = new(
                txtNumero.Text,
                Convert.ToInt32(cboCliente.SelectedValue),
                dtpFecha.Value,
                txtObservacion.Text);

            if (EsEdicion)
                venta.AsignarId(_idVenta!.Value);

            return venta;
        }

        private async void FrmRegistroVenta_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                _cargando = true;

                await CargarCombosAsync();

                if (IsDisposed)
                    return;

                CargarDatos();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeError(
                    $"No se pudieron cargar los datos del formulario: {ex.Message}");
            }
            finally
            {
                _cargando = false;
            }
        }

        private async void btnGuardar_Click(
            object sender,
            EventArgs e)
        {
            if (_esSoloLectura)
                return;

            if (!ValidarVenta())
                return;

            try
            {
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;

                VentaEntity venta = CrearVenta();

                await _ventaBLL.GuardarAsync(
                    venta,
                    _detalles,
                    UsuarioSesion.NombreUsuario);

                mensajes.MensajeOk(
                    EsEdicion
                        ? "Venta actualizada correctamente."
                        : "Venta registrada correctamente.");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo guardar la venta: {ex.Message}");
            }
        }

        private void dtpFecha_ValueChanged(
            object sender,
            EventArgs e)
        {
            if (_cargando || _esSoloLectura)
                return;

            ActualizarPrecioVigente();
            RefrescarDetalles();
        }

        private void Cancelar()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void FrmRegistroVenta_Shown(
            object sender,
            EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlNumero, 12);
            RedondearControlHelper.RedondearControl(pnlFecha, 12);
            RedondearControlHelper.RedondearControl(pnlCliente, 12);
            RedondearControlHelper.RedondearControl(pnlObservacion, 12);

            RedondearControlHelper.RedondearControl(pnlLoteDetalle, 12);
            RedondearControlHelper.RedondearControl(pnlCantidadPollos, 12);
            RedondearControlHelper.RedondearControl(pnlPesoLibras, 12);
            RedondearControlHelper.RedondearControl(pnlPrecioDetalle, 12);

            RedondearControlHelper.RedondearControl(dgvDetalles, 12);
        }

        private void pnlSuperior_MouseDown(
            object sender,
            MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = Location;
        }

        private void pnlSuperior_MouseMove(
            object sender,
            MouseEventArgs e)
        {
            if (dragging)
            {
                Point diferencia = Point.Subtract(
                    Cursor.Position,
                    new Size(dragCursorPoint));

                Location = Point.Add(
                    dragFormPoint,
                    new Size(diferencia));
            }
        }

        private void pnlSuperior_MouseUp(
            object sender,
            MouseEventArgs e)
        {
            dragging = false;
        }

        private void dgvDetalles_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is null || e.Value == DBNull.Value)
                return;

            string columna = dgvDetalles.Columns[e.ColumnIndex].Name;

            if (columna is "PesoLibras" or "PrecioPorLibra" or "Subtotal")
            {
                e.Value = Convert.ToDecimal(e.Value).ToString("N2");
                e.FormattingApplied = true;
            }
        }
    }
}
