using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Vitaminas
{
    public partial class FrmRegistroVitamina : Form
    {
        private readonly VitaminaBLL _vitaminaBLL;
        private readonly int? _idVitamina;
        private readonly Mensajes mensajes = new();

        private VitaminaEntity? _vitamina;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idVitamina.HasValue;

        public FrmRegistroVitamina(VitaminaBLL vitaminaBLL)
        {
            InitializeComponent();

            _vitaminaBLL = vitaminaBLL;
            _idVitamina = null;

            lblTitulo.Text = "Nueva vitamina";
            lblDescripcion.Text =
                "Complete la información de la vitamina.";
        }

        public FrmRegistroVitamina(
            VitaminaBLL vitaminaBLL,
            int idVitamina)
        {
            InitializeComponent();

            _vitaminaBLL = vitaminaBLL;
            _idVitamina = idVitamina;

            lblTitulo.Text = "Editar vitamina";
            lblDescripcion.Text =
                "Modifique la información de la vitamina.";
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                if (!_idVitamina.HasValue)
                    return;

                _vitamina = await _vitaminaBLL.ObtenerPorIdAsync(
                    _idVitamina.Value);

                if (_vitamina is null)
                {
                    mensajes.MensajeError(
                        "No se encontró la vitamina especificada.");

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                txtNombre.Text = _vitamina.NombreVitamina;
                txtDescripcion.Text =
                    _vitamina.Descripcion ?? string.Empty;
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
                    txtNombre,
                    errorIcon,
                    "Ingrese el nombre de la vitamina."))
            {
                valido = false;
            }

            if (txtNombre.Text.Trim().Length > 100)
            {
                errorIcon.SetError(
                    txtNombre,
                    "El nombre no puede superar los 100 caracteres.");

                valido = false;
            }

            if (txtDescripcion.Text.Trim().Length > 250)
            {
                errorIcon.SetError(
                    txtDescripcion,
                    "La descripción no puede superar los 250 caracteres.");

                valido = false;
            }

            return valido;
        }

        private VitaminaEntity CrearVitamina()
        {
            VitaminaEntity vitamina = new(
                txtNombre.Text,
                txtDescripcion.Text);

            if (EsEdicion)
            {
                vitamina.AsignarId(_idVitamina!.Value);
                vitamina.CambiarActivo(_vitamina?.Activo ?? true);
            }
            else
            {
                vitamina.CambiarActivo(true);
            }

            return vitamina;
        }

        private async void FrmRegistroVitamina_Load(
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

                VitaminaEntity vitamina = CrearVitamina();

                if (!EsEdicion)
                {
                    await _vitaminaBLL.AgregarAsync(
                        vitamina,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Vitamina registrada correctamente.");
                }
                else
                {
                    await _vitaminaBLL.ActualizarAsync(
                        vitamina,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Vitamina actualizada correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo guardar la vitamina: {ex.Message}");
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

        private void FrmRegistroVitamina_Shown(
            object sender,
            EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlNombre, 12);
            RedondearControlHelper.RedondearControl(pnlDescripcion, 12);
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
