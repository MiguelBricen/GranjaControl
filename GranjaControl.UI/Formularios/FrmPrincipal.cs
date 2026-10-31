using GranjaControl.UI.Formularios.Formularios_derivados.Roles;
using GranjaControl.UI.Formularios.Formularios_derivados.Usuarios;
using GranjaControl.UI.Formularios.Formularios_derivados.Proveedores;
using GranjaControl.UI.Formularios.Formularios_derivados.Lotes;
using GranjaControl.UI.Formularios.Formularios_derivados.Alimentos;
using GranjaControl.UI.Formularios.Formularios_derivados.Mortalidades;
using GranjaControl.UI.Formularios.Formularios_derivados.Vitaminas;
using GranjaControl.UI.Formularios.Formularios_derivados.Vitaminizaciones;
using GranjaControl.UI.Formularios.Formularios_derivados.Clientes;
using GranjaControl.UI.Formularios.Formularios_derivados.Compras;
using GranjaControl.UI.Formularios.Formularios_derivados.Tarifas;
using GranjaControl.UI.Formularios.Formularios_derivados.Ventas;
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
            _catalogosAbierto = !_catalogosAbierto;

            if (_catalogosAbierto)
            {
                if (_seguridadAbierto)
                {
                    _seguridadAbierto = false;
                    pnlSeguridad.Visible = false;
                    btnSeguridad.BackColor = Color.FromArgb(23, 61, 43);
                }

                if (_produccionAbierto)
                {
                    _produccionAbierto = false;
                    pnlProduccion.Visible = false;
                    btnProduccion.BackColor = Color.FromArgb(23, 61, 43);
                }

                CerrarComercializacion();
            }

            btnCatalogos.BackColor = _catalogosAbierto
                ? Color.FromArgb(73, 111, 82)
                : Color.FromArgb(23, 61, 43);

            AjustarMenuSidebar();

            pnlCatalogos.Visible = _catalogosAbierto;
        }

        private void btnGalpones_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmGalponListado(),
                pnlContenedor,
                ref formularioActivo,
                btnGalpones);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmProveedorListado(),
                pnlContenedor,
                ref formularioActivo,
                btnProveedores);
        }

        private void btnAlimentos_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmAlimentoListado(),
                pnlContenedor,
                ref formularioActivo,
                btnAlimentos);
        }

        private void btnVitaminas_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmVitaminaListado(),
                pnlContenedor,
                ref formularioActivo,
                btnVitaminas);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmClienteListado(),
                pnlContenedor,
                ref formularioActivo,
                btnClientes);
        }

        private void btnProduccion_Click(object sender, EventArgs e)
        {
            _produccionAbierto = !_produccionAbierto;

            if (_produccionAbierto)
            {
                if (_catalogosAbierto)
                {
                    _catalogosAbierto = false;
                    pnlCatalogos.Visible = false;
                    btnCatalogos.BackColor = Color.FromArgb(23, 61, 43);
                }

                if (_seguridadAbierto)
                {
                    _seguridadAbierto = false;
                    pnlSeguridad.Visible = false;
                    btnSeguridad.BackColor = Color.FromArgb(23, 61, 43);
                }

                CerrarComercializacion();
            }

            btnProduccion.BackColor = _produccionAbierto
                ? Color.FromArgb(73, 111, 82)
                : Color.FromArgb(23, 61, 43);

            AjustarMenuSidebar();

            pnlProduccion.Visible = _produccionAbierto;
        }

        private void btnLotes_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmLoteListado(),
                pnlContenedor,
                ref formularioActivo,
                btnLotes);
        }

        private void btnAlimentacion_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmAlimentacionListado(),
                pnlContenedor,
                ref formularioActivo,
                btnAlimentacion);
        }

        private void btnMortalidades_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmMortalidadListado(),
                pnlContenedor,
                ref formularioActivo,
                btnMortalidades);
        }

        private void btnVitaminizacion_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmVitaminizacionListado(),
                pnlContenedor,
                ref formularioActivo,
                btnVitaminizacion);
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmCompraAlimentoListado(),
                pnlContenedor,
                ref formularioActivo,
                btnCompras);
        }

        private void btnComercializacion_Click(object sender, EventArgs e)
        {
            _comercializacionAbierto = !_comercializacionAbierto;

            if (_comercializacionAbierto)
            {
                CerrarSubmenusPrincipales();
            }

            btnComercializacion.BackColor = _comercializacionAbierto
                ? Color.FromArgb(73, 111, 82)
                : Color.FromArgb(23, 61, 43);

            AjustarMenuSidebar();

            pnlComercializacion.Visible = _comercializacionAbierto;
        }

        private void btnTarifas_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmTarifaListado(),
                pnlContenedor,
                ref formularioActivo,
                btnComercializacion);
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(
                new FrmVentaListado(),
                pnlContenedor,
                ref formularioActivo,
                btnComercializacion);
        }

        private void CerrarSubmenusPrincipales()
        {
            _seguridadAbierto = false;
            pnlSeguridad.Visible = false;
            btnSeguridad.BackColor = Color.FromArgb(23, 61, 43);

            _catalogosAbierto = false;
            pnlCatalogos.Visible = false;
            btnCatalogos.BackColor = Color.FromArgb(23, 61, 43);

            _produccionAbierto = false;
            pnlProduccion.Visible = false;
            btnProduccion.BackColor = Color.FromArgb(23, 61, 43);
        }

        private bool _seguridadAbierto = false;
        private bool _comercializacionAbierto = false;
        private bool _catalogosAbierto = false;
        private bool _produccionAbierto = false;
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

            if (_seguridadAbierto)
            {
                if (_catalogosAbierto)
                {
                    _catalogosAbierto = false;
                    pnlCatalogos.Visible = false;
                    btnCatalogos.BackColor = Color.FromArgb(23, 61, 43);
                }

                if (_produccionAbierto)
                {
                    _produccionAbierto = false;
                    pnlProduccion.Visible = false;
                    btnProduccion.BackColor = Color.FromArgb(23, 61, 43);
                }

                CerrarComercializacion();
            }

            pnlSeguridad.Visible = _seguridadAbierto;
            pnlSeguridad.Size = new Size(226, 106);

            btnSeguridad.BackColor = _seguridadAbierto
                ? Color.FromArgb(73, 111, 82)
                : Color.FromArgb(23, 61, 43);

            AjustarMenuSidebar();


        }

        private void CerrarComercializacion()
        {
            if (!_comercializacionAbierto)
                return;

            _comercializacionAbierto = false;
            pnlComercializacion.Visible = false;
            btnComercializacion.BackColor = Color.FromArgb(23, 61, 43);
        }

        private void AjustarMenuSidebar()
        {
            int desplazamientoSeguridad = _seguridadAbierto ? 108 : 0;
            int desplazamientoCatalogos = _catalogosAbierto ? 265 : 0;
            int desplazamientoProduccion = _produccionAbierto ? 208 : 0;
            int desplazamientoComercializacion =
                _comercializacionAbierto ? 106 : 0;

            foreach (var elemento in _posicionesOriginales)
            {
                int desplazamiento = desplazamientoSeguridad;

                if (elemento.Key != btnCatalogos &&
                    elemento.Key != picDif3)
                {
                    desplazamiento += desplazamientoCatalogos;
                }

                if (elemento.Key != btnCatalogos &&
                    elemento.Key != picDif3 &&
                    elemento.Key != btnProduccion &&
                    elemento.Key != picDif4)
                {
                    desplazamiento += desplazamientoProduccion;
                }

                if (elemento.Value >
                    _posicionesOriginales[btnComercializacion])
                {
                    desplazamiento += desplazamientoComercializacion;
                }

                elemento.Key.Top = elemento.Value + desplazamiento;
            }

            pnlCatalogos.Top = btnCatalogos.Top + 50;
            pnlProduccion.Top = btnProduccion.Top + 50;
            pnlComercializacion.Top = btnComercializacion.Top + 50;
        }


        private void btnRoles_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(new FrmRolesListado(), pnlContenedor, ref formularioActivo, btnRoles);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            NavegacionHelper.AbrirFormulario(new FrmUsuarioListado(), pnlContenedor, ref formularioActivo, btnUsuarios);
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            SesionHelper.CargarDatosSesion(toolUsuario, toolRol);
        }
    }
}

        
    

