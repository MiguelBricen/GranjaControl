using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Formularios.Formularios_derivados.Usuarios;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Seguridad
{
    public partial class FrmInicioSesion : Form
    {
        private readonly UsuarioBll _usuarioBll;
        private readonly RolBll _rolBll;
        public FrmInicioSesion()
        {
            InitializeComponent();

            _usuarioBll = new UsuarioBll(new UsuarioDal());
            _rolBll = new RolBll(new RolDal());

            btnIconoCentral.Imagen = Properties.Resources.UserCentr;
            btnIconoCentral.ModoImagen = PictureBoxSizeMode.StretchImage;
        }

        private bool ValidarControles()
        {
            errorIcon.Clear();

            bool valido = true;

            if (!ValidationHelper.Requerido(txtUsuario, errorIcon, "Ingrese el usuario"))
                valido = false;

            if (!ValidationHelper.Requerido(txtClave, errorIcon, "Ingrese la contraseña"))
                valido = false;

            return valido;
        }
        private void FrmInicioSesion_Shown(object sender, EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 25);
            RedondearControlHelper.RedondearControl(picLoginFondo, 25);

            RedondearControlHelper.RedondearControl(pnlContenido, 15);
            //RedondearControlHelper.RedondearControl(pnlUserCentral, 15);

            //RedondearControlHelper.RedondearControl(pnlUsuario, 15);
            // RedondearControlHelper.RedondearControl(pnlClave, 15);

            // RedondearControlHelper.RedondearControl(btnLogin, 8);
            // RedondearControlHelper.RedondearControl(btnNuevoregistro, 8);


        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Cerrar la aplicación con ESC
            if (keyData == Keys.Escape)
            {
                Application.Exit();
                return true;
            }

            // Si se preciona ENTER estando en txtClave
            if (keyData == Keys.Enter && ActiveControl == txtClave)
            {
                btnLogin.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidarControles())
                return;

            try
            {
                UsuarioEntity usuario = await _usuarioBll.AutenticarAsync(txtUsuario.Text, txtClave.Text);

                UsuarioSesion.IniciarSesion(usuario);

                Hide();

                if (usuario.NombreRol == RolesSistema.Administrador)
                {
                    using FrmPrincipal frm = new FrmPrincipal();
                    DialogResult resultado = frm.ShowDialog();

                    if (resultado == DialogResult.Retry)
                    {
                        txtClave.Clear();
                        txtClave.Focus();
                        Show();
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    // Funcionalidad para iniciar como usuario del sistema
                    MessageBox.Show("Bienvenido usuario del sistema");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            FrmUsuarioRegistro frm = new FrmUsuarioRegistro(_usuarioBll, _rolBll, true);
            frm.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
