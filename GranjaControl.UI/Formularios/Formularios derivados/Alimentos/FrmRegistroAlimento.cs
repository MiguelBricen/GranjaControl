using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Alimentos
{
    public partial class FrmRegistroAlimento : Form
    {
        private readonly AlimentoBLL _alimentoBLL;
        private readonly int? _idAlimento;
        private readonly Mensajes mensajes = new();

        private AlimentoEntity? _alimento;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idAlimento.HasValue;

        public FrmRegistroAlimento(AlimentoBLL alimentoBLL)
        {
            InitializeComponent();

            _alimentoBLL = alimentoBLL;
            _idAlimento = null;

            lblTitulo.Text = "Nuevo alimento";
            lblDescripcion.Text =
                "Complete la información del alimento.";
        }

        public FrmRegistroAlimento(
            AlimentoBLL alimentoBLL,
            int idAlimento)
        {
            InitializeComponent();

            _alimentoBLL = alimentoBLL;
            _idAlimento = idAlimento;

            lblTitulo.Text = "Editar alimento";
            lblDescripcion.Text =
                "Modifique la información del alimento.";
        }

        private void CargarTiposAlimento()
        {
            cboTipoAlimento.Items.Clear();
            cboTipoAlimento.Items.AddRange(
                AlimentoEntity.TiposPermitidos.ToArray());

            cboTipoAlimento.SelectedIndex = -1;
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                if (!_idAlimento.HasValue)
                    return;

                _alimento = await _alimentoBLL.ObtenerPorIdAsync(
                    _idAlimento.Value);

                if (_alimento is null)
                {
                    mensajes.MensajeError(
                        "No se encontró el alimento especificado.");

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                txtNombre.Text = _alimento.NombreAlimento;
                cboTipoAlimento.SelectedItem = _alimento.TipoAlimento;
                txtDescripcion.Text =
                    _alimento.Descripcion ?? string.Empty;
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
                    "Ingrese el nombre del alimento."))
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

            if (cboTipoAlimento.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboTipoAlimento,
                    "Seleccione el tipo de alimento.");

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

        private AlimentoEntity CrearAlimento()
        {
            AlimentoEntity alimento = new(
                txtNombre.Text,
                cboTipoAlimento.SelectedItem!.ToString()!,
                txtDescripcion.Text);

            if (EsEdicion)
            {
                alimento.AsignarId(_idAlimento!.Value);
                alimento.CambiarActivo(_alimento?.Activo ?? true);
            }
            else
            {
                alimento.CambiarActivo(true);
            }

            return alimento;
        }

        private async void FrmRegistroAlimento_Load(
            object sender,
            EventArgs e)
        {
            CargarTiposAlimento();

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

                AlimentoEntity alimento = CrearAlimento();

                if (!EsEdicion)
                {
                    await _alimentoBLL.AgregarAsync(
                        alimento,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Alimento registrado correctamente.");
                }
                else
                {
                    await _alimentoBLL.ActualizarAsync(
                        alimento,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Alimento actualizado correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo guardar el alimento: {ex.Message}");
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

        private void FrmRegistroAlimento_Shown(
            object sender,
            EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlNombre, 12);
            RedondearControlHelper.RedondearControl(pnlTipo, 12);
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
