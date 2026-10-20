
using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Roles
{
    public partial class FrmRolesRegistro : Form
    {
        private readonly RolBll _rolBll;
        private readonly int? _idRol;
        private RolEntity? _rol;

        private bool EsEdicion => _idRol.HasValue;
        public FrmRolesRegistro(RolBll rolBll)
        {
            InitializeComponent();


            _rolBll = rolBll;
            _idRol = null;

            lblTitulo.Text = "Nuevo Rol";
            lblDescripcion.Text = "Complete la información del rol.";
        }

        public FrmRolesRegistro(RolBll rolBll, int idRol)
        {
            InitializeComponent();
            _rolBll = rolBll;
            _idRol = idRol;

            lblTitulo.Text = "Editar Rol";
            lblDescripcion.Text = "Modifique la información del rol.";
        }

        #region Métodos

        private async Task CargarDatosAsync()
        {
            try
            {
                _rol = await _rolBll.ObtenerPorIdAsync(_idRol!.Value);

                if (_rol is null)
                {
                    MessageBox.Show("No se encontró el rol.");
                    DialogResult = DialogResult.Cancel;

                    Close();
                    return;
                }

                txtRol.Text = _rol.NombreRol;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK);
            }
        }

        private bool ValidarControles()
        {
            errorIcon.Clear();

            return ValidationHelper.Requerido(txtRol, errorIcon, "Ingrese el nombre del rol.");
        }

        private void Cancelar()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        #region Evento al cargar el frm
        private async void FrmRolesRegistro_Load(object sender, EventArgs e)
        {
            if (EsEdicion)
                await CargarDatosAsync();
        }
        #endregion

        #region Botones de comando

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarControles())
                return;

            try
            {
                if (!EsEdicion)
                {
                    RolEntity nuevoRol = new();

                    nuevoRol.CambiarNombre(txtRol.Text);
                    await _rolBll.AgregarAsync(nuevoRol, UsuarioSesion.NombreUsuario);
                }
                else
                {
                    RolEntity rolEditado = new();

                    rolEditado.AsignarId(_idRol!.Value);
                    rolEditado.CambiarNombre(txtRol.Text);

                    await _rolBll.ActualizarAsync(rolEditado, UsuarioSesion.NombreUsuario);
                }

                MessageBox.Show("Registro guardado correctamente.", "Información", MessageBoxButtons.OK);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atencíón", MessageBoxButtons.OK);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }
        #endregion

        private void btnIconCerrar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void FrmRolesRegistro_Shown(object sender, EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 25);
            RedondearControlHelper.RedondearControl(pnlContenido, 25);
            RedondearControlHelper.RedondearControl(pnlNombre, 15);
        }
    }
}
