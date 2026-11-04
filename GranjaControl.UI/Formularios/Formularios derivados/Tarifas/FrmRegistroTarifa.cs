using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Tarifas
{
    public partial class FrmRegistroTarifa : Form
    {
        private readonly TarifaPolloBLL _tarifaBLL;
        private readonly int? _idTarifa;
        private readonly Mensajes mensajes = new();

        private TarifaPolloEntity? _tarifa;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idTarifa.HasValue;

        public FrmRegistroTarifa(TarifaPolloBLL tarifaBLL)
        {
            InitializeComponent();

            _tarifaBLL = tarifaBLL;
            _idTarifa = null;

            lblTitulo.Text = "Nueva tarifa de pollo";
            lblDescripcion.Text =
                "Registre el precio por libra y su período de vigencia.";
        }

        public FrmRegistroTarifa(
            TarifaPolloBLL tarifaBLL,
            int idTarifa)
        {
            InitializeComponent();

            _tarifaBLL = tarifaBLL;
            _idTarifa = idTarifa;

            lblTitulo.Text = "Editar tarifa de pollo";
            lblDescripcion.Text =
                "Modifique el precio o el período de vigencia.";
        }

        private async Task CargarDatosAsync()
        {
            if (!_idTarifa.HasValue)
            {
                dtpFechaInicio.Value = DateTime.Today;
                dtpFechaFin.Value = DateTime.Today;

                chkSinFechaFin.Checked = true;
                dtpFechaFin.Enabled = false;

                return;
            }

            try
            {
                _tarifa = await _tarifaBLL.ObtenerPorIdAsync(
                    _idTarifa.Value);

                if (_tarifa is null)
                {
                    mensajes.MensajeError(
                        "No se encontró la tarifa especificada.");

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                numPrecio.Value = _tarifa.PrecioPorLibra;
                dtpFechaInicio.Value = _tarifa.FechaInicio;

                chkSinFechaFin.Checked = !_tarifa.FechaFin.HasValue;

                if (_tarifa.FechaFin.HasValue)
                    dtpFechaFin.Value = _tarifa.FechaFin.Value;

                dtpFechaFin.Enabled = _tarifa.FechaFin.HasValue;
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = false;

                mensajes.MensajeError(
                    $"Error al cargar los datos: {ex.Message}");
            }
        }

        private bool ValidarControles()
        {
            errorIcon.Clear();

            bool valido = true;

            if (numPrecio.Value <= 0)
            {
                errorIcon.SetError(
                    numPrecio,
                    "Ingrese un precio por libra mayor que cero.");

                valido = false;
            }

            if (!chkSinFechaFin.Checked &&
                dtpFechaFin.Value.Date < dtpFechaInicio.Value.Date)
            {
                errorIcon.SetError(
                    dtpFechaFin,
                    "La fecha fin no puede ser anterior a la fecha inicio.");

                valido = false;
            }

            return valido;
        }

        private TarifaPolloEntity CrearTarifa()
        {
            DateTime? fechaFin = chkSinFechaFin.Checked
                ? null
                : dtpFechaFin.Value;

            TarifaPolloEntity tarifa = new(
                numPrecio.Value,
                dtpFechaInicio.Value,
                fechaFin);

            if (EsEdicion)
            {
                tarifa.AsignarId(_idTarifa!.Value);
                tarifa.CambiarActivo(_tarifa?.Activo ?? true);
            }
            else
            {
                tarifa.CambiarActivo(true);
            }

            return tarifa;
        }

        private async void FrmRegistroTarifa_Load(
            object sender,
            EventArgs e)
        {
            await CargarDatosAsync();
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
                btnCancelar.Enabled = false;

                TarifaPolloEntity tarifa = CrearTarifa();

                if (!EsEdicion)
                {
                    await _tarifaBLL.AgregarAsync(
                        tarifa,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Tarifa registrada correctamente.");
                }
                else
                {
                    await _tarifaBLL.ActualizarAsync(
                        tarifa,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Tarifa actualizada correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo guardar la tarifa: {ex.Message}");
            }
        }

        private void chkSinFechaFin_CheckedChanged(
            object sender,
            EventArgs e)
        {
            dtpFechaFin.Enabled = !chkSinFechaFin.Checked;
            errorIcon.SetError(dtpFechaFin, string.Empty);
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

        private void FrmRegistroTarifa_Shown(
            object sender,
            EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlPrecio, 12);
            RedondearControlHelper.RedondearControl(pnlFechaInicio, 12);
            RedondearControlHelper.RedondearControl(pnlFechaFin, 12);
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
