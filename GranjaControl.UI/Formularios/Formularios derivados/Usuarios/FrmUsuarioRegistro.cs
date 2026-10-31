using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Usuarios
{
    public partial class FrmUsuarioRegistro : Form
    {
        private readonly UsuarioBll _usuarioBll;
        private readonly RolBll _rolBll;
        private readonly int? _idUsuario;

        private readonly bool _esAutoregistro;
        private readonly Mensajes mensajes = new();
        private int? _idRolAutoregistro;
        private UsuarioEntity? _usuario;

        private bool EsEdicion => _idUsuario.HasValue;

        #region Constructores

        // Constructor para nuevo registro
        public FrmUsuarioRegistro(UsuarioBll usuarioBll, RolBll rolBll)
        {
            InitializeComponent();
            _usuarioBll = usuarioBll;
            _rolBll = rolBll;
            _idUsuario = null;

            lblTitulo.Text = "Nuevo Usuario";
            lblDescripcion.Text = "Complete la información del usuario.";
        }

        // Constructor para edición
        public FrmUsuarioRegistro(UsuarioBll usuarioBll, RolBll rolBll, int idUsuario)
        {
            InitializeComponent();
            _usuarioBll = usuarioBll;
            _rolBll = rolBll;
            _idUsuario = idUsuario;

            txtUsuario.Enabled = false;
            txtPassword.Enabled = false;

            lblTitulo.Text = "Editar Usuario";
            lblDescripcion.Text = "Complete la información del usuario.";
        }

        // Constructor para auto registro desde el login
        public FrmUsuarioRegistro(UsuarioBll usuarioBll, RolBll rolBll, bool esAutoregistro)
        {
            InitializeComponent();

            _usuarioBll = usuarioBll;
            _rolBll = rolBll;

            _esAutoregistro = esAutoregistro;

            lblTitulo.Text = "Crear cuenta";
            lblDescripcion.Text = "Complete la información para crear su cuenta.";
        }
        #endregion

        #region Métodos

        private async Task CargarRolesAsync()
        {
            var roles = await _rolBll.MostrarRolesActivosAsync();

            cboRoles.DataSource = roles;
            cboRoles.DisplayMember = "NombreRol";
            cboRoles.ValueMember = "Id";
            cboRoles.SelectedIndex = -1;
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                if (!_idUsuario.HasValue)
                    return;

                _usuario = await _usuarioBll.ObtenerPorIdAsync(_idUsuario!.Value);

                if (_usuario is null)
                {
                    mensajes.MensajeInformacion("No se encontró el usuario.");
                    DialogResult = DialogResult.Cancel;

                    Close();
                    return;
                }

                txtNombre.Text = _usuario.NombreCompleto;
                txtUsuario.Text = _usuario.NombreUsuario;
                txtCorreo.Text = _usuario.CorreoElectronico;
                cboRoles.SelectedValue = _usuario.IdRol;
            }
            catch (Exception ex)
            {
                mensajes.MensajeError(ex.Message);
            }
        }

        private bool ValidarControles()
        {
            errorIcon.Clear();

            bool valido = true;

            if (!ValidationHelper.Requerido(txtNombre, errorIcon, "Ingrese el nombre completo."))
                valido = false;

            if (!EsEdicion)
            {
                if (!ValidationHelper.Requerido(txtUsuario, errorIcon, "Ingrese el nombre de usuario."))
                    valido = false;
            }

            if (!ValidationHelper.Requerido(txtCorreo, errorIcon, "Ingrese el correo electrónico."))
                valido = false;

            if (!ValidationHelper.EmailValido(txtCorreo, errorIcon))
                valido = false;

            if (!_esAutoregistro && cboRoles.SelectedIndex < 0)
            {
                errorIcon.SetError(cboRoles, "Seleccione un rol.");
                valido = false;
            }

            if (!EsEdicion)
            {
                if (!ValidationHelper.Requerido(txtPassword, errorIcon, "Ingrese la contraseña."))
                    valido = false;
            }

            return valido;
        }

        private void Cancelar()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region Eventos
        private async void FrmUsuarioRegistro_Load(object sender, EventArgs e)
        {
            if (!_esAutoregistro &&
                !RolesSistema.Autorizar(this, RolesSistema.PuedeGestionarUsuarios()))
            {
                return;
            }

            await CargarRolesAsync();

            if (_esAutoregistro)
            {
                // Asignar automáticamente el rol Usuario.

                RolEntity? rolUsuario = cboRoles.Items
                    .Cast<RolEntity>()
                    .FirstOrDefault(r =>
                        r.NombreRol.Equals("Usuario", StringComparison.OrdinalIgnoreCase));

                if (rolUsuario == null)
                {
                    mensajes.MensajeError(
                        "No existe un rol Usuario activo. Contacte al administrador.");

                    btnGuardar.Enabled = false;
                    return;
                }

                _idRolAutoregistro = rolUsuario.Id;
                cboRoles.SelectedValue = rolUsuario.Id;

                // Ocultar el rol solamente durante el autorregistro.
                label3.Visible = false;
                pnlRolUsuario.Visible = false;
                cboRoles.Visible = false;

                // Ajustar la posición de la contraseña.
                label5.Location = new Point(16, 230);
                pnlPassword.Location = new Point(157, 230);
            }
            else
            {
                // Mostrar el rol al registrar o editar desde el sistema.
                label3.Visible = true;
                pnlRolUsuario.Visible = true;
                cboRoles.Visible = true;
            }

            if (EsEdicion)
                await CargarDatosAsync();

        }
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarControles())
                return;

            try
            {
                string usuarioApp = _esAutoregistro
                                    ? "Autoregistro"
                                    : UsuarioSesion.NombreUsuario;

                int idRol = ObtenerIdRol();

                if (!EsEdicion)
                {
                    UsuarioEntity nuevoUsuario = new();

                    nuevoUsuario.CambiarNombreCompleto(txtNombre.Text);
                    nuevoUsuario.CambiarNombreUsuario(txtUsuario.Text);
                    nuevoUsuario.CambiarCorreo(txtCorreo.Text);
                    nuevoUsuario.CambiarRol(idRol);

                    await _usuarioBll.AgregarAsync(nuevoUsuario, txtPassword.Text, usuarioApp);
                }
                else
                {
                    UsuarioEntity usuarioEditado = new();

                    usuarioEditado.AsignarId(_idUsuario!.Value);
                    usuarioEditado.CambiarNombreCompleto(txtNombre.Text);
                    usuarioEditado.CambiarCorreo(txtCorreo.Text);
                    usuarioEditado.CambiarRol(idRol);

                    await _usuarioBll.ActualizarAsync(usuarioEditado, usuarioApp);
                }

                mensajes.MensajeOk("Registro guardado correctamente.");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                mensajes.MensajeError(
                    $"No se pudo guardar el usuario: {ex.Message}");
            }
        }

        private int ObtenerIdRol()
        {
            if (_esAutoregistro)
            {
                if (!_idRolAutoregistro.HasValue)
                {
                    throw new InvalidOperationException(
                        "No se pudo determinar el rol Usuario para el autoregistro.");
                }

                return _idRolAutoregistro.Value;
            }

            if (cboRoles.SelectedValue is null)
            {
                throw new InvalidOperationException(
                    "Debe seleccionar un rol.");
            }

            return Convert.ToInt32(cboRoles.SelectedValue);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void btnIconCerrar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void FrmUsuarioRegistro_Shown(object sender, EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 25);
            RedondearControlHelper.RedondearControl(pnlContenido, 25);
            RedondearControlHelper.RedondearControl(pnlNombre, 15);
            RedondearControlHelper.RedondearControl(pnlNombreUsuario, 15);
            RedondearControlHelper.RedondearControl(pnlCorreo, 15);
            RedondearControlHelper.RedondearControl(pnlRolUsuario, 15);
            RedondearControlHelper.RedondearControl(pnlPassword, 15);
        }
        #endregion
    }
}
