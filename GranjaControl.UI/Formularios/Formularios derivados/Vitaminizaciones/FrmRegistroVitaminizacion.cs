using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Vitaminizaciones
{
    public partial class FrmRegistroVitaminizacion : Form
    {
        private readonly VitaminizacionBLL _vitaminizacionBLL;
        private readonly LoteBLL _loteBLL;
        private readonly VitaminaBLL _vitaminaBLL;
        private readonly int? _idVitaminizacion;
        private readonly Mensajes mensajes = new();

        private VitaminizacionEntity? _vitaminizacion;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idVitaminizacion.HasValue;

        public FrmRegistroVitaminizacion(
            VitaminizacionBLL vitaminizacionBLL,
            LoteBLL loteBLL,
            VitaminaBLL vitaminaBLL)
        {
            InitializeComponent();

            _vitaminizacionBLL = vitaminizacionBLL;
            _loteBLL = loteBLL;
            _vitaminaBLL = vitaminaBLL;
            _idVitaminizacion = null;

            lblTitulo.Text = "Nueva vitaminización";
            lblDescripcion.Text =
                "Registre la aplicación de vitaminas del lote.";
        }

        public FrmRegistroVitaminizacion(
            VitaminizacionBLL vitaminizacionBLL,
            LoteBLL loteBLL,
            VitaminaBLL vitaminaBLL,
            int idVitaminizacion)
        {
            InitializeComponent();

            _vitaminizacionBLL = vitaminizacionBLL;
            _loteBLL = loteBLL;
            _vitaminaBLL = vitaminaBLL;
            _idVitaminizacion = idVitaminizacion;

            lblTitulo.Text = "Editar vitaminización";
            lblDescripcion.Text =
                "Modifique la información de la aplicación de vitaminas.";
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

            List<VitaminaEntity> vitaminas =
                await _vitaminaBLL.MostrarVitaminasActivasAsync();

            cboVitamina.DisplayMember =
                nameof(VitaminaEntity.NombreVitamina);
            cboVitamina.ValueMember = nameof(VitaminaEntity.Id);
            cboVitamina.DataSource = vitaminas;
            cboVitamina.SelectedIndex = -1;

            cboUnidad.DataSource =
                VitaminizacionEntity.UnidadesPermitidas.ToList();
            cboUnidad.SelectedIndex = -1;

            if (lotes.Count == 0 || vitaminas.Count == 0)
            {
                btnGuardar.Enabled = false;

                string faltante = lotes.Count == 0
                    ? "lotes disponibles"
                    : "vitaminas activas";

                mensajes.MensajeInformacion(
                    $"No hay {faltante} para registrar vitaminizaciones.");
            }
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                if (!_idVitaminizacion.HasValue)
                {
                    dtpFecha.Value = DateTime.Today;
                    return;
                }

                _vitaminizacion =
                    await _vitaminizacionBLL.ObtenerPorIdAsync(
                        _idVitaminizacion.Value);

                if (_vitaminizacion is null)
                {
                    mensajes.MensajeError(
                        "No se encontró la vitaminización especificada.");

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                cboLote.SelectedValue = _vitaminizacion.IdLote;
                cboVitamina.SelectedValue = _vitaminizacion.IdVitamina;
                dtpFecha.Value = _vitaminizacion.Fecha;
                numCantidad.Value = _vitaminizacion.Cantidad;
                cboUnidad.SelectedItem = _vitaminizacion.UnidadMedida;
                txtObservacion.Text =
                    _vitaminizacion.Observacion ?? string.Empty;
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
                    "Seleccione el lote de la vitaminización.");

                valido = false;
            }

            if (cboVitamina.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboVitamina,
                    "Seleccione la vitamina aplicada.");

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

        private VitaminizacionEntity CrearVitaminizacion()
        {
            VitaminizacionEntity vitaminizacion = new(
                Convert.ToInt32(cboLote.SelectedValue),
                Convert.ToInt32(cboVitamina.SelectedValue),
                dtpFecha.Value,
                numCantidad.Value,
                cboUnidad.SelectedItem!.ToString()!,
                txtObservacion.Text);

            if (EsEdicion)
                vitaminizacion.AsignarId(_idVitaminizacion!.Value);

            return vitaminizacion;
        }

        private async void FrmRegistroVitaminizacion_Load(
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
            if (!ValidarControles())
                return;

            try
            {
                btnGuardar.Enabled = false;

                VitaminizacionEntity vitaminizacion =
                    CrearVitaminizacion();

                if (!EsEdicion)
                {
                    await _vitaminizacionBLL.AgregarAsync(
                        vitaminizacion,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Vitaminización registrada correctamente.");
                }
                else
                {
                    await _vitaminizacionBLL.ActualizarAsync(
                        vitaminizacion,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Vitaminización actualizada correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo guardar la vitaminización: {ex.Message}");
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

        private void FrmRegistroVitaminizacion_Shown(
            object sender,
            EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlLote, 12);
            RedondearControlHelper.RedondearControl(pnlVitamina, 12);
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
