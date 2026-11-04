using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Alimentos
{
    public partial class FrmRegistroAlimentacion : Form
    {
        private readonly AlimentacionBLL _alimentacionBLL;
        private readonly LoteBLL _loteBLL;
        private readonly AlimentoBLL _alimentoBLL;
        private readonly int? _idAlimentacion;
        private readonly Mensajes mensajes = new();

        private AlimentacionEntity? _alimentacion;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idAlimentacion.HasValue;

        public FrmRegistroAlimentacion(
            AlimentacionBLL alimentacionBLL,
            LoteBLL loteBLL,
            AlimentoBLL alimentoBLL)
        {
            InitializeComponent();

            _alimentacionBLL = alimentacionBLL;
            _loteBLL = loteBLL;
            _alimentoBLL = alimentoBLL;
            _idAlimentacion = null;

            lblTitulo.Text = "Nueva alimentación";
            lblDescripcion.Text =
                "Complete la información de la alimentación.";
        }

        public FrmRegistroAlimentacion(
            AlimentacionBLL alimentacionBLL,
            LoteBLL loteBLL,
            AlimentoBLL alimentoBLL,
            int idAlimentacion)
        {
            InitializeComponent();

            _alimentacionBLL = alimentacionBLL;
            _loteBLL = loteBLL;
            _alimentoBLL = alimentoBLL;
            _idAlimentacion = idAlimentacion;

            lblTitulo.Text = "Editar alimentación";
            lblDescripcion.Text =
                "Modifique la información de la alimentación.";
        }

        private async Task CargarCombosAsync()
        {
            var resultadoLotes = await _loteBLL.MostrarAsync(
                1,
                10000,
                null);

            List<LoteEntity> lotes = resultadoLotes.Registros
                .Where(l => l.Estado != "Cancelado")
                .ToList();

            cboLote.DisplayMember = nameof(LoteEntity.CodigoLote);
            cboLote.ValueMember = nameof(LoteEntity.Id);
            cboLote.DataSource = lotes;
            cboLote.SelectedIndex = -1;

            List<AlimentoEntity> alimentos =
                await _alimentoBLL.MostrarAlimentosActivosAsync();

            cboAlimento.DisplayMember =
                nameof(AlimentoEntity.NombreAlimento);

            cboAlimento.ValueMember = nameof(AlimentoEntity.Id);
            cboAlimento.DataSource = alimentos;
            cboAlimento.SelectedIndex = -1;

            cboUnidad.Items.Clear();
            cboUnidad.Items.AddRange(
                AlimentacionEntity.UnidadesPermitidas.ToArray());

            cboUnidad.SelectedIndex = -1;

            if (lotes.Count == 0)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeInformacion(
                    "No hay lotes disponibles para registrar alimentaciones.");
            }

            if (alimentos.Count == 0)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeInformacion(
                    "No hay alimentos activos disponibles.");
            }
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                if (!_idAlimentacion.HasValue)
                {
                    dtpFecha.Value = DateTime.Today;
                    cboUnidad.SelectedItem = "Libras";
                    return;
                }

                _alimentacion = await _alimentacionBLL.ObtenerPorIdAsync(
                    _idAlimentacion.Value);

                if (_alimentacion is null)
                {
                    mensajes.MensajeError(
                        "No se encontró la alimentación especificada.");

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                cboLote.SelectedValue = _alimentacion.IdLote;
                cboAlimento.SelectedValue = _alimentacion.IdAlimento;
                dtpFecha.Value = _alimentacion.Fecha;
                numCantidad.Value = _alimentacion.Cantidad;
                cboUnidad.SelectedItem = _alimentacion.UnidadMedida;
                txtObservacion.Text =
                    _alimentacion.Observacion ?? string.Empty;
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

            if (cboLote.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboLote,
                    "Seleccione el lote de la alimentación.");

                valido = false;
            }

            if (cboAlimento.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboAlimento,
                    "Seleccione el alimento de la alimentación.");

                valido = false;
            }

            if (numCantidad.Value <= 0)
            {
                errorIcon.SetError(
                    numCantidad,
                    "La cantidad debe ser mayor que cero.");

                valido = false;
            }

            if (cboUnidad.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboUnidad,
                    "Seleccione la unidad de medida.");

                valido = false;
            }

            return valido;
        }

        private AlimentacionEntity CrearAlimentacion()
        {
            AlimentacionEntity alimentacion = new(
                Convert.ToInt32(cboLote.SelectedValue),
                Convert.ToInt32(cboAlimento.SelectedValue),
                dtpFecha.Value,
                numCantidad.Value,
                cboUnidad.SelectedItem!.ToString()!,
                txtObservacion.Text);

            if (EsEdicion)
                alimentacion.AsignarId(_idAlimentacion!.Value);

            return alimentacion;
        }

        private async void FrmRegistroAlimentacion_Load(
            object sender,
            EventArgs e)
        {
            bool permitido = EsEdicion
                ? RolesSistema.PuedeEditarProduccion()
                : RolesSistema.PuedeRegistrarProduccion();

            if (!RolesSistema.Autorizar(this, permitido))
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

                AlimentacionEntity alimentacion = CrearAlimentacion();

                if (!EsEdicion)
                {
                    await _alimentacionBLL.AgregarAsync(
                        alimentacion,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Alimentación registrada correctamente.");
                }
                else
                {
                    await _alimentacionBLL.ActualizarAsync(
                        alimentacion,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Alimentación actualizada correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo guardar la alimentación: {ex.Message}");
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

        private void FrmRegistroAlimentacion_Shown(
            object sender,
            EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlLote, 12);
            RedondearControlHelper.RedondearControl(pnlAlimento, 12);
            RedondearControlHelper.RedondearControl(pnlFecha, 12);
            RedondearControlHelper.RedondearControl(pnlCantidad, 12);
            RedondearControlHelper.RedondearControl(pnlUnidad, 12);
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
