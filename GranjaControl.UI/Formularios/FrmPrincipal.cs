using GranjaControl.UI.Formularios.Formularios_derivados.Roles;
using GranjaControl.UI.Formularios_derivados;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios
{
    public partial class FrmPrincipal : Form
    {
        private Form? formularioActivo;
        private bool _cerrandoSesion = false;
        Mensajes mensajes = new Mensajes();

        public FrmPrincipal()
        {
            InitializeComponent();
            GuardarPosicionesMenu();
        }

        #region Sidebar
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SesionHelper.CerrarSesion(this);
        }
        #endregion

        #region Métodos
        public void CerrarSesion()
        {
            _cerrandoSesion = true;
            DialogResult = DialogResult.Retry;
            Close();
        }
        #endregion

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_cerrandoSesion)
            {                 // Si se está cerrando la sesión, no se cancela el cierre del formulario
                return;
            }

            DialogResult respuesta = mensajes.MensajeConfirmacion("¿Desea salir de la aplicación?");

            if (respuesta != DialogResult.Yes)
            {
                e.Cancel = true; // Cancela el cierre del formulario
            }
        }

        private void btnCatalogos_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(new FrmGalponListado(), pnlContenedor, ref formularioActivo, btnCatalogos);
        }

        private bool _seguridadAbierto = false;
        private readonly Dictionary<Control, int> _posicionesOriginales = new();

        private void GuardarPosicionesMenu()
        {
            _posicionesOriginales.Clear();

            Control[] controlesMenu =
            {
                btnCatalogos, picDif3,
                btnProduccion, picDif4,
                btnCompras, picDif5,
                btnComercializacion, picDif6,
                btnReportes, picDif7
            };

            foreach (Control control in controlesMenu)
            {
                _posicionesOriginales[control] = control.Top;
            }
        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {

            _seguridadAbierto = !_seguridadAbierto;

            pnlSeguridad.Visible = _seguridadAbierto;
            pnlSeguridad.Size = new Size(226, 106);

            btnSeguridad.BackColor = _seguridadAbierto
                ? Color.FromArgb(73, 111, 82)
                : Color.FromArgb(23, 61, 43);

            AjustarMenuSidebar();


        }

        private void AjustarMenuSidebar()
        {
            foreach (var elemento in _posicionesOriginales)
            {
                elemento.Key.Top = elemento.Value +
                    (_seguridadAbierto ? 108 : 0);
            }
        }


        private void btnRoles_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(new FrmRolesListado(), pnlContenedor, ref formularioActivo, btnRoles);
        }
    }
}
