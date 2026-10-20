using GranjaControl.BLL.Common;
using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.UI
{
    public partial class FrmConfigurarConexion : Form
    {
        public FrmConfigurarConexion()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private bool ValidarCampos()
        {
            errorIcono.Clear();

            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtServidor.Text))
            {

                errorIcono.SetError(txtServidor, "Debe ingresar el nombre del servidor");
                valido = false;

            }

            if (string.IsNullOrWhiteSpace(txtBaseDatos.Text))
            {
                errorIcono.SetError(txtBaseDatos, "Debe ingresar el nombre de la base de datos");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                errorIcono.SetError(txtUsuario, "Debe ingresar el nombre de usuario");
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorIcono.SetError(txtPassword, "Debe ingresar la contraseña");
                valido = false;
            }

            return valido;

        }

        private async Task btnProbarConexion_ClickAsync(object sender, EventArgs e)
        {

            if (!ValidarCampos())
                return;

            ConexionBLL conexionBLL = new();

            bool ok = await conexionBLL.ProbarConexion(
               txtServidor.Text.Trim(),
               txtBaseDatos.Text.Trim(),
               txtUsuario.Text.Trim(),
               txtPassword.Text.Trim());


            if (ok)
            {
                MessageBox.Show("Conexión exitosa", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnGuardar.Enabled = true;
                txtServidor.Enabled = false;
                txtBaseDatos.Enabled = false;
                txtUsuario.Enabled = false;
                txtPassword.Enabled = false;

            }
            else
            {
                MessageBox.Show("Error al conectar a la base de datos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnGuardar.Enabled = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ConexionEntity config = new()
            {
                Servidor = txtServidor.Text.Trim(),
                BaseDatos = txtBaseDatos.Text.Trim(),
                UsuarioSql = txtUsuario.Text.Trim(),
                Password = txtPassword.Text.Trim()
            };

            ConfiguracionDal dal = new();

            bool resultado = dal.Guardar(config);

            if (!resultado)
            {

                MessageBox.Show("Error al guardar la configuración", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show("Configuración guardada correctamente", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
        }
    }
}
