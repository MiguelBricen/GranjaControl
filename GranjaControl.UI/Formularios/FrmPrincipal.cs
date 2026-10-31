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
using GranjaControl.UI.Formularios.Formularios_derivados.Reportes;
using GranjaControl.UI.Formularios_derivados;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
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
            ConfigurarMenuPorRol();
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

        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirDashboard();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            CerrarSubmenusPrincipales();
            CerrarComercializacion();

            NavegacionHelper.AbrirFormulario(
                new FrmReportes(),
                pnlContenedor,
                ref formularioActivo,
                btnReportes);
        }

        private void AbrirDashboard()
        {
            CerrarSubmenusPrincipales();
            CerrarComercializacion();

            NavegacionHelper.AbrirFormulario(
                new FrmDashboard(),
                pnlContenedor,
                ref formularioActivo,
                btnInicio);
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
            if (!VerificarPermiso(
                RolesSistema.PuedeAccederCompras(),
                "No tiene permisos para acceder a las compras."))
            {
                return;
            }

            NavegacionHelper.AbrirFormulario(
                new FrmCompraAlimentoListado(),
                pnlContenedor,
                ref formularioActivo,
                btnCompras);
        }

        private void btnComercializacion_Click(object sender, EventArgs e)
        {
            if (!VerificarPermiso(
                RolesSistema.PuedeAccederComercializacion(),
                "No tiene permisos para acceder a la comercialización."))
            {
                return;
            }

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
            if (!VerificarPermiso(
                RolesSistema.PuedeAccederComercializacion(),
                "No tiene permisos para acceder a las tarifas."))
            {
                return;
            }

            NavegacionHelper.AbrirFormulario(
                new FrmTarifaListado(),
                pnlContenedor,
                ref formularioActivo,
                btnComercializacion);
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            if (!VerificarPermiso(
                RolesSistema.PuedeAccederComercializacion(),
                "No tiene permisos para acceder a las ventas."))
            {
                return;
            }

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

            AjustarMenuSidebar();
        }

        private bool _seguridadAbierto = false;
        private bool _comercializacionAbierto = false;
        private bool _catalogosAbierto = false;
        private bool _produccionAbierto = false;

        private void ConfigurarMenuPorRol()
        {
            bool esAdministrador = RolesSistema.EsAdministrador();

            btnSeguridad.Visible = esAdministrador;
            picDif2.Visible = esAdministrador;
            pnlSeguridad.Visible = false;

            btnCompras.Visible = esAdministrador;
            picDif5.Visible = esAdministrador;

            btnComercializacion.Visible = esAdministrador;
            picDif6.Visible = esAdministrador;
            pnlComercializacion.Visible = false;

            _seguridadAbierto = false;
            _comercializacionAbierto = false;
            _catalogosAbierto = false;
            _produccionAbierto = false;

            AjustarMenuSidebar();
        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {
            if (!VerificarPermiso(
                RolesSistema.PuedeAccederSeguridad(),
                "No tiene permisos para acceder a la seguridad."))
            {
                return;
            }

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

            AjustarMenuSidebar();
        }

        private void AjustarMenuSidebar()
        {
            int posicion = 166;

            AjustarElementoMenu(btnInicio, picDif, null, false, ref posicion);
            AjustarElementoMenu(btnSeguridad, picDif2, pnlSeguridad, _seguridadAbierto, ref posicion);
            AjustarElementoMenu(btnCatalogos, picDif3, pnlCatalogos, _catalogosAbierto, ref posicion);
            AjustarElementoMenu(btnProduccion, picDif4, pnlProduccion, _produccionAbierto, ref posicion);
            AjustarElementoMenu(btnCompras, picDif5, null, false, ref posicion);
            AjustarElementoMenu(btnComercializacion, picDif6, pnlComercializacion, _comercializacionAbierto, ref posicion);
            AjustarElementoMenu(btnReportes, picDif7, null, false, ref posicion);
        }

        private static void AjustarElementoMenu(
            Control boton,
            Control separador,
            Panel? panel,
            bool submenuAbierto,
            ref int posicion)
        {
            if (!boton.Visible)
            {
                separador.Visible = false;

                if (panel != null)
                    panel.Visible = false;

                return;
            }

            separador.Visible = true;
            separador.Top = posicion;
            boton.Top = posicion;

            if (panel == null)
            {
                posicion += 56;
                return;
            }

            panel.Top = posicion + 56;
            panel.Visible = submenuAbierto;

            posicion = submenuAbierto
                ? panel.Top + panel.Height + 6
                : posicion + 56;
        }


        private void btnRoles_Click(object sender, EventArgs e)
        {
            if (!VerificarPermiso(
                RolesSistema.PuedeGestionarRoles(),
                "No tiene permisos para gestionar roles."))
            {
                return;
            }

            NavegacionHelper.AbrirFormulario(new FrmRolesListado(), pnlContenedor, ref formularioActivo, btnRoles);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (!VerificarPermiso(
                RolesSistema.PuedeGestionarUsuarios(),
                "No tiene permisos para gestionar usuarios."))
            {
                return;
            }

            NavegacionHelper.AbrirFormulario(new FrmUsuarioListado(), pnlContenedor, ref formularioActivo, btnUsuarios);
        }

        private bool VerificarPermiso(bool permitido, string mensaje)
        {
            if (permitido)
                return true;

            mensajes.MensajeError(mensaje);
            return false;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            if (!RolesSistema.TieneAccesoAlSistema())
            {
                _cerrandoSesion = true;
                DialogResult = DialogResult.Retry;
                Close();
                return;
            }

            SesionHelper.CargarDatosSesion(toolUsuario, toolRol);
            AbrirDashboard();
        }
    }
}

        
    

