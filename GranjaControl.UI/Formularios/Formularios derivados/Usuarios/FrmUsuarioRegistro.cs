using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Usuarios
{
    public partial class FrmUsuarioRegistro : Form
    {
        private readonly UsuarioBll _usuarioBll;
        private readonly RolBll _rolBll;
        private readonly int? _idUsuario;

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
                    MessageBox.Show("No se encontró el usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            await CargarRolesAsync();

            if (EsEdicion)
                await CargarDatosAsync();
        }
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarControles())
                return;

            try
            {
                string usuarioApp = UsuarioSesion.NombreUsuario;

                if (!EsEdicion)
                {
                    UsuarioEntity nuevoUsuario = new();

                    nuevoUsuario.CambiarNombreCompleto(txtNombre.Text);
                    nuevoUsuario.CambiarNombreUsuario(txtUsuario.Text);
                    nuevoUsuario.CambiarCorreo(txtCorreo.Text);
                    nuevoUsuario.CambiarRol(Convert.ToInt32(cboRoles.SelectedValue));

                    await _usuarioBll.AgregarAsync(nuevoUsuario, txtPassword.Text, usuarioApp);
                }
                else
                {
                    UsuarioEntity usuarioEditado = new();

                    usuarioEditado.AsignarId(_idUsuario!.Value);
                    usuarioEditado.CambiarNombreCompleto(txtNombre.Text);
                    usuarioEditado.CambiarCorreo(txtCorreo.Text);
                    usuarioEditado.CambiarRol(Convert.ToInt32(cboRoles.SelectedValue));

                    await _usuarioBll.ActualizarAsync(usuarioEditado, usuarioApp);
                }

                MessageBox.Show("Registro guardado correctamente.", "Información", MessageBoxButtons.OK);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Error de diagnóstico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
