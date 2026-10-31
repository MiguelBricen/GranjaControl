using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Mortalidades
{
    public partial class FrmRegistroMortalidad : Form
    {
        private readonly MortalidadBLL _mortalidadBLL;
        private readonly LoteBLL _loteBLL;
        private readonly int? _idMortalidad;
        private readonly Mensajes mensajes = new();

        private MortalidadEntity? _mortalidad;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idMortalidad.HasValue;

        public FrmRegistroMortalidad(
            MortalidadBLL mortalidadBLL,
            LoteBLL loteBLL)
        {
            InitializeComponent();

            _mortalidadBLL = mortalidadBLL;
            _loteBLL = loteBLL;
            _idMortalidad = null;

            lblTitulo.Text = "Nueva mortalidad";
            lblDescripcion.Text =
                "Registre la baja de aves del lote.";
        }

        public FrmRegistroMortalidad(
            MortalidadBLL mortalidadBLL,
            LoteBLL loteBLL,
            int idMortalidad)
        {
            InitializeComponent();

            _mortalidadBLL = mortalidadBLL;
            _loteBLL = loteBLL;
            _idMortalidad = idMortalidad;

            lblTitulo.Text = "Editar mortalidad";
            lblDescripcion.Text =
                "Modifique la información de la mortalidad.";
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

            if (lotes.Count == 0)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeInformacion(
                    "No hay lotes disponibles para registrar mortalidades.");
            }
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                if (!_idMortalidad.HasValue)
                {
                    dtpFecha.Value = DateTime.Today;
                    return;
                }

                _mortalidad = await _mortalidadBLL.ObtenerPorIdAsync(
                    _idMortalidad.Value);

                if (_mortalidad is null)
                {
                    mensajes.MensajeError(
                        "No se encontró la mortalidad especificada.");

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                cboLote.SelectedValue = _mortalidad.IdLote;
                dtpFecha.Value = _mortalidad.Fecha;
                numCantidad.Value = _mortalidad.Cantidad;
                txtCausa.Text = _mortalidad.Causa;
                txtObservacion.Text =
                    _mortalidad.Observacion ?? string.Empty;
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
                    "Seleccione el lote de la mortalidad.");

                valido = false;
            }

            if (numCantidad.Value <= 0)
            {
                errorIcon.SetError(
                    numCantidad,
                    "La cantidad debe ser mayor que cero.");

                valido = false;
            }

            if (!ValidationHelper.Requerido(
                    txtCausa,
                    errorIcon,
                    "Ingrese la causa de la mortalidad."))
            {
                valido = false;
            }

            return valido;
        }

        private MortalidadEntity CrearMortalidad()
        {
            MortalidadEntity mortalidad = new(
                Convert.ToInt32(cboLote.SelectedValue),
                dtpFecha.Value,
                Convert.ToInt32(numCantidad.Value),
                txtCausa.Text,
                txtObservacion.Text);

            if (EsEdicion)
                mortalidad.AsignarId(_idMortalidad!.Value);

            return mortalidad;
        }

        private async void FrmRegistroMortalidad_Load(
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

                MortalidadEntity mortalidad = CrearMortalidad();

                if (!EsEdicion)
                {
                    await _mortalidadBLL.AgregarAsync(
                        mortalidad,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Mortalidad registrada correctamente.");
                }
                else
                {
                    await _mortalidadBLL.ActualizarAsync(
                        mortalidad,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Mortalidad actualizada correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo guardar la mortalidad: {ex.Message}");
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

        private void FrmRegistroMortalidad_Shown(
            object sender,
            EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlLote, 12);
            RedondearControlHelper.RedondearControl(pnlFecha, 12);
            RedondearControlHelper.RedondearControl(pnlCantidad, 12);
            RedondearControlHelper.RedondearControl(pnlCausa, 12);
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
