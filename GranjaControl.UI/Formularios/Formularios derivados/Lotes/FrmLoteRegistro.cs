using GranjaControl.BLL;
using GranjaControl.Entitys;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Lotes
{
    public partial class FrmLoteRegistro : Form
    {
        private readonly LoteBLL _loteBLL;
        private readonly GalponBLL _galponBLL;
        private readonly ProveedorBLL _proveedorBLL;
        private readonly int? _idLote;
        private readonly Mensajes mensajes = new();

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idLote.HasValue;

        public FrmLoteRegistro(
            LoteBLL loteBLL,
            GalponBLL galponBLL,
            ProveedorBLL proveedorBLL)
        {
            InitializeComponent();

            _loteBLL = loteBLL;
            _galponBLL = galponBLL;
            _proveedorBLL = proveedorBLL;
            _idLote = null;

            lblTitulo.Text = "Nuevo lote";
            lblDescripcion.Text = "Complete la información del lote.";
        }

        public FrmLoteRegistro(
            LoteBLL loteBLL,
            GalponBLL galponBLL,
            ProveedorBLL proveedorBLL,
            int idLote)
        {
            InitializeComponent();

            _loteBLL = loteBLL;
            _galponBLL = galponBLL;
            _proveedorBLL = proveedorBLL;
            _idLote = idLote;

            lblTitulo.Text = "Editar lote";
            lblDescripcion.Text = "Modifique la información del lote.";
        }

        private async Task CargarCombosAsync()
        {
            List<GalponEntity> galpones =
                await _galponBLL.MostrarGalponesActivosAsync();

            cboGalpon.DisplayMember = nameof(GalponEntity.NombreGalpon);
            cboGalpon.ValueMember = nameof(GalponEntity.Id);
            cboGalpon.DataSource = galpones;
            cboGalpon.SelectedIndex = -1;

            List<ProveedorEntity> proveedores =
                await _proveedorBLL.MostrarProveedoresActivosAsync();

            proveedores = proveedores
                .Where(p =>
                    p.TipoProveedor == "Pollitos" ||
                    p.TipoProveedor == "Ambos")
                .ToList();

            cboProveedor.DisplayMember =
                nameof(ProveedorEntity.NombreProveedor);

            cboProveedor.ValueMember = nameof(ProveedorEntity.Id);
            cboProveedor.DataSource = proveedores;
            cboProveedor.SelectedIndex = -1;

            if (galpones.Count == 0)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeInformacion(
                    "No hay galpones activos disponibles para registrar lotes.");
            }

            if (proveedores.Count == 0)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeInformacion(
                    "No hay proveedores activos de pollitos disponibles.");
            }
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                if (!_idLote.HasValue)
                {
                    dtpFechaIngreso.Value = DateTime.Today;
                    return;
                }

                LoteEntity? lote =
                    await _loteBLL.ObtenerPorIdAsync(_idLote.Value);

                if (lote is null)
                {
                    mensajes.MensajeError(
                        "No se encontró el lote especificado.");

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                txtCodigo.Text = lote.CodigoLote;
                cboGalpon.SelectedValue = lote.IdGalpon;
                cboProveedor.SelectedValue = lote.IdProveedor;
                dtpFechaIngreso.Value = lote.FechaIngreso;
                numCantidadInicial.Value = lote.CantidadInicial;
                numCostoPorPollito.Value = lote.CostoPorPollito;
                txtObservacion.Text = lote.Observacion ?? string.Empty;
            }
            catch (Exception ex)
            {
                mensajes.MensajeError(
                    $"Error al cargar los datos: {ex.Message}");
            }
        }

        private bool ValidarControles()
        {
            errorIcon.Clear();

            bool valido = true;

            if (!ValidationHelper.Requerido(
                    txtCodigo,
                    errorIcon,
                    "Ingrese el código del lote."))
            {
                valido = false;
            }

            if (cboGalpon.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboGalpon,
                    "Seleccione el galpón del lote.");

                valido = false;
            }

            if (cboProveedor.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboProveedor,
                    "Seleccione el proveedor de pollitos.");

                valido = false;
            }

            if (numCantidadInicial.Value <= 0)
            {
                errorIcon.SetError(
                    numCantidadInicial,
                    "La cantidad inicial debe ser mayor que cero.");

                valido = false;
            }

            if (numCostoPorPollito.Value < 0)
            {
                errorIcon.SetError(
                    numCostoPorPollito,
                    "El costo por pollito no puede ser negativo.");

                valido = false;
            }

            return valido;
        }

        private LoteEntity CrearLote()
        {
            LoteEntity lote = new(
                txtCodigo.Text,
                Convert.ToInt32(cboGalpon.SelectedValue),
                Convert.ToInt32(cboProveedor.SelectedValue),
                dtpFechaIngreso.Value,
                Convert.ToInt32(numCantidadInicial.Value),
                numCostoPorPollito.Value,
                txtObservacion.Text);

            if (EsEdicion)
                lote.AsignarId(_idLote!.Value);

            return lote;
        }

        private async void FrmLoteRegistro_Load(
            object sender,
            EventArgs e)
        {
            if (!RolesSistema.Autorizar(this, RolesSistema.PuedeGestionarLotes()))
                return;

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
            if (!ValidarControles())
                return;

            try
            {
                btnGuardar.Enabled = false;

                LoteEntity lote = CrearLote();

                if (!EsEdicion)
                {
                    await _loteBLL.AgregarAsync(
                        lote,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Lote registrado correctamente.");
                }
                else
                {
                    await _loteBLL.ActualizarAsync(
                        lote,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Lote actualizado correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo guardar el lote: {ex.Message}");
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

        private void FrmLoteRegistro_Shown(object sender, EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlCodigo, 12);
            RedondearControlHelper.RedondearControl(pnlGalpon, 12);
            RedondearControlHelper.RedondearControl(pnlProveedor, 12);
            RedondearControlHelper.RedondearControl(pnlFecha, 12);
            RedondearControlHelper.RedondearControl(pnlCantidad, 12);
            RedondearControlHelper.RedondearControl(pnlCosto, 12);
            RedondearControlHelper.RedondearControl(pnlObservacion, 12);
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
    }
}
