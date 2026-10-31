using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Compras
{
    public partial class FrmRegistroCompraAlimento : Form
    {
        private readonly CompraAlimentoBLL _compraBLL;
        private readonly ProveedorBLL _proveedorBLL;
        private readonly AlimentoBLL _alimentoBLL;
        private readonly int? _idCompra;
        private readonly Mensajes mensajes = new();
        private readonly List<DetalleCompraAlimentoEntity> _detalles = [];

        private int _indiceDetalleSeleccionado = -1;
        private bool _actualizandoDetalles;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idCompra.HasValue;

        public FrmRegistroCompraAlimento(
            CompraAlimentoBLL compraBLL,
            ProveedorBLL proveedorBLL,
            AlimentoBLL alimentoBLL)
        {
            InitializeComponent();

            _compraBLL = compraBLL;
            _proveedorBLL = proveedorBLL;
            _alimentoBLL = alimentoBLL;
            _idCompra = null;

            lblTitulo.Text = "Nueva compra de alimentos";
            lblDescripcion.Text =
                "Registre el encabezado y los alimentos de la compra.";
        }

        public FrmRegistroCompraAlimento(
            CompraAlimentoBLL compraBLL,
            ProveedorBLL proveedorBLL,
            AlimentoBLL alimentoBLL,
            int idCompra)
        {
            InitializeComponent();

            _compraBLL = compraBLL;
            _proveedorBLL = proveedorBLL;
            _alimentoBLL = alimentoBLL;
            _idCompra = idCompra;

            lblTitulo.Text = "Editar compra de alimentos";
            lblDescripcion.Text =
                "Modifique el encabezado y los detalles de la compra.";
        }

        private async Task CargarCombosAsync()
        {
            List<ProveedorEntity> proveedores =
                (await _proveedorBLL.MostrarProveedoresActivosAsync())
                .Where(p =>
                    p.TipoProveedor.Equals(
                        "Alimento",
                        StringComparison.OrdinalIgnoreCase) ||
                    p.TipoProveedor.Equals(
                        "Ambos",
                        StringComparison.OrdinalIgnoreCase))
                .ToList();

            cboProveedor.DisplayMember =
                nameof(ProveedorEntity.NombreProveedor);

            cboProveedor.ValueMember = nameof(ProveedorEntity.Id);
            cboProveedor.DataSource = proveedores;
            cboProveedor.SelectedIndex = -1;

            List<AlimentoEntity> alimentos =
                await _alimentoBLL.MostrarAlimentosActivosAsync();

            cboAlimentoDetalle.DisplayMember =
                nameof(AlimentoEntity.NombreAlimento);

            cboAlimentoDetalle.ValueMember = nameof(AlimentoEntity.Id);
            cboAlimentoDetalle.DataSource = alimentos;
            cboAlimentoDetalle.SelectedIndex = -1;

            cboUnidadDetalle.Items.Clear();
            cboUnidadDetalle.Items.AddRange(
                DetalleCompraAlimentoEntity.UnidadesPermitidas.ToArray());

            cboUnidadDetalle.SelectedIndex = -1;

            if (proveedores.Count == 0)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeInformacion(
                    "No hay proveedores activos que puedan suministrar alimentos.");
            }

            if (alimentos.Count == 0)
            {
                btnGuardar.Enabled = false;
                btnAgregarDetalle.Enabled = false;

                mensajes.MensajeInformacion(
                    "No hay alimentos activos disponibles para registrar compras.");
            }
        }

        private async Task CargarDatosAsync()
        {
            if (!_idCompra.HasValue)
            {
                dtpFecha.Value = DateTime.Today;
                cboUnidadDetalle.SelectedItem = "Libras";
                return;
            }

            CompraAlimentoEntity? compra =
                await _compraBLL.ObtenerPorIdAsync(_idCompra.Value);

            if (compra is null)
            {
                mensajes.MensajeError(
                    "No se encontró la compra especificada.");

                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            txtNumero.Text = compra.NumeroCompra;
            dtpFecha.Value = compra.FechaCompra;
            txtObservacion.Text = compra.Observacion ?? string.Empty;

            cboProveedor.SelectedValue = compra.IdProveedor;

            if (cboProveedor.SelectedIndex < 0)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeInformacion(
                    "El proveedor de esta compra ya no está activo o no puede suministrar alimentos.");
            }

            _detalles.Clear();

            _detalles.AddRange(
                await _compraBLL.ObtenerDetallesAsync(_idCompra.Value));

            RefrescarDetalles();
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

            ActualizarTotal();
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
            dgvDetalles.Columns["IdCompra"]!.Visible = false;
            dgvDetalles.Columns["NumeroCompra"]!.Visible = false;

            dgvDetalles.Columns["NombreAlimento"]!.HeaderText = "Alimento";
            dgvDetalles.Columns["TipoAlimento"]!.HeaderText = "Tipo";
            dgvDetalles.Columns["Cantidad"]!.HeaderText = "Cantidad";
            dgvDetalles.Columns["UnidadMedida"]!.HeaderText = "Unidad";
            dgvDetalles.Columns["CostoUnitario"]!.HeaderText = "Costo unit.";
            dgvDetalles.Columns["Subtotal"]!.HeaderText = "Subtotal";

            dgvDetalles.Columns["NombreAlimento"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dgvDetalles.Columns["NombreAlimento"]!.FillWeight = 35;

            dgvDetalles.Columns["TipoAlimento"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvDetalles.Columns["Cantidad"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvDetalles.Columns["UnidadMedida"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvDetalles.Columns["CostoUnitario"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvDetalles.Columns["Subtotal"]!.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;

            dgvDetalles.Columns["Cantidad"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetalles.Columns["UnidadMedida"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvDetalles.Columns["TipoAlimento"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvDetalles.Columns["CostoUnitario"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetalles.Columns["Subtotal"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvDetalles.Columns["NombreAlimento"]!.DisplayIndex = 0;
            dgvDetalles.Columns["TipoAlimento"]!.DisplayIndex = 1;
            dgvDetalles.Columns["Cantidad"]!.DisplayIndex = 2;
            dgvDetalles.Columns["UnidadMedida"]!.DisplayIndex = 3;
            dgvDetalles.Columns["CostoUnitario"]!.DisplayIndex = 4;
            dgvDetalles.Columns["Subtotal"]!.DisplayIndex = 5;
        }

        private void ActualizarTotal()
        {
            decimal total = CompraAlimentoBLL.CalcularTotal(_detalles);

            lblTotal.Text = $"Total: {total:N2}";
        }

        private void dgvDetalles_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (_actualizandoDetalles)
                return;

            if (dgvDetalles.CurrentRow?.DataBoundItem
                is not DetalleCompraAlimentoEntity detalle)
            {
                _indiceDetalleSeleccionado = -1;
                btnActualizarDetalle.Enabled = false;
                btnQuitarDetalle.Enabled = false;
                return;
            }

            _indiceDetalleSeleccionado = dgvDetalles.CurrentRow.Index;

            CargarDetalleEnControles(detalle);

            btnActualizarDetalle.Enabled = true;
            btnQuitarDetalle.Enabled = detalle.Id == 0;
        }

        private void CargarDetalleEnControles(
            DetalleCompraAlimentoEntity detalle)
        {
            cboAlimentoDetalle.SelectedValue = detalle.IdAlimento;
            numCantidadDetalle.Value = detalle.Cantidad;
            cboUnidadDetalle.SelectedItem = detalle.UnidadMedida;
            numCostoDetalle.Value = detalle.CostoUnitario;
        }

        private bool ValidarDetalle()
        {
            bool valido = true;

            if (cboAlimentoDetalle.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboAlimentoDetalle,
                    "Seleccione el alimento de la compra.");

                valido = false;
            }

            if (numCantidadDetalle.Value <= 0)
            {
                errorIcon.SetError(
                    numCantidadDetalle,
                    "La cantidad debe ser mayor que cero.");

                valido = false;
            }

            if (cboUnidadDetalle.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboUnidadDetalle,
                    "Seleccione la unidad de medida.");

                valido = false;
            }

            return valido;
        }

        private DetalleCompraAlimentoEntity CrearDetalleDesdeControles(
            int id = 0,
            int idCompra = 0)
        {
            AlimentoEntity alimento =
                (AlimentoEntity)cboAlimentoDetalle.SelectedItem!;

            DetalleCompraAlimentoEntity detalle = new(
                idCompra,
                alimento.Id,
                numCantidadDetalle.Value,
                cboUnidadDetalle.SelectedItem!.ToString()!,
                numCostoDetalle.Value);

            detalle.AsignarAlimento(
                alimento.Id,
                alimento.NombreAlimento,
                alimento.TipoAlimento);

            if (id > 0)
                detalle.AsignarId(id);

            return detalle;
        }

        private void LimpiarEntradaDetalle()
        {
            cboAlimentoDetalle.SelectedIndex = -1;
            numCantidadDetalle.Value = 0;
            numCostoDetalle.Value = 0;
        }

        private void SeleccionarDetalle(int indice)
        {
            if (indice < 0 || indice >= dgvDetalles.Rows.Count)
                return;

            _actualizandoDetalles = true;

            dgvDetalles.ClearSelection();
            dgvDetalles.Rows[indice].Selected = true;
            dgvDetalles.CurrentCell =
                dgvDetalles.Rows[indice].Cells["NombreAlimento"];

            _actualizandoDetalles = false;

            _indiceDetalleSeleccionado = indice;

            CargarDetalleEnControles(_detalles[indice]);

            btnActualizarDetalle.Enabled = true;
            btnQuitarDetalle.Enabled = _detalles[indice].Id == 0;
        }

        private void btnAgregarDetalle_Click(
            object sender,
            EventArgs e)
        {
            errorIcon.Clear();

            if (!ValidarDetalle())
                return;

            DetalleCompraAlimentoEntity detalle =
                CrearDetalleDesdeControles(
                    idCompra: _idCompra ?? 0);

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

            errorIcon.Clear();

            if (!ValidarDetalle())
                return;

            DetalleCompraAlimentoEntity actual =
                _detalles[_indiceDetalleSeleccionado];

            DetalleCompraAlimentoEntity actualizado =
                CrearDetalleDesdeControles(
                    actual.Id,
                    actual.IdCompra);

            actualizado.AsignarCompra(
                actual.IdCompra,
                actual.NumeroCompra);

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

            DetalleCompraAlimentoEntity detalle =
                _detalles[_indiceDetalleSeleccionado];

            if (detalle.Id > 0)
            {
                mensajes.MensajeInformacion(
                    "No se puede eliminar un detalle ya guardado porque la base de datos no dispone de esa operación.");

                return;
            }

            _detalles.RemoveAt(_indiceDetalleSeleccionado);

            RefrescarDetalles();
            LimpiarEntradaDetalle();
        }

        private bool ValidarCompra()
        {
            errorIcon.Clear();

            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                errorIcon.SetError(
                    txtNumero,
                    "Ingrese el número de compra.");

                valido = false;
            }

            if (cboProveedor.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboProveedor,
                    "Seleccione el proveedor de la compra.");

                valido = false;
            }

            if (_detalles.Count == 0)
            {
                mensajes.MensajeValidacion(
                    "Debe agregar al menos un alimento a la compra.");

                valido = false;
            }

            return valido;
        }

        private CompraAlimentoEntity CrearCompra()
        {
            CompraAlimentoEntity compra = new(
                txtNumero.Text,
                Convert.ToInt32(cboProveedor.SelectedValue),
                dtpFecha.Value,
                txtObservacion.Text);

            if (EsEdicion)
                compra.AsignarId(_idCompra!.Value);

            return compra;
        }

        private async void FrmRegistroCompraAlimento_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                await CargarCombosAsync();
                await CargarDatosAsync();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeError(
                    $"No se pudieron cargar los datos del formulario: {ex.Message}");
            }
        }

        private async void btnGuardar_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidarCompra())
                return;

            try
            {
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;

                CompraAlimentoEntity compra = CrearCompra();

                await _compraBLL.GuardarAsync(
                    compra,
                    _detalles,
                    UsuarioSesion.NombreUsuario);

                mensajes.MensajeOk(
                    EsEdicion
                        ? "Compra actualizada correctamente."
                        : "Compra registrada correctamente.");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo guardar la compra: {ex.Message}");
            }
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

        private void FrmRegistroCompraAlimento_Shown(
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
            RedondearControlHelper.RedondearControl(pnlProveedor, 12);
            RedondearControlHelper.RedondearControl(pnlObservacion, 12);

            RedondearControlHelper.RedondearControl(pnlAlimentoDetalle, 12);
            RedondearControlHelper.RedondearControl(pnlCantidadDetalle, 12);
            RedondearControlHelper.RedondearControl(pnlUnidadDetalle, 12);
            RedondearControlHelper.RedondearControl(pnlCostoDetalle, 12);

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
            if (e.Value is null)
                return;

            string columna = dgvDetalles.Columns[e.ColumnIndex].Name;

            if (columna is "Cantidad" or "CostoUnitario" or "Subtotal")
            {
                e.Value = Convert.ToDecimal(e.Value).ToString("N2");
                e.FormattingApplied = true;
            }
        }
    }
}
