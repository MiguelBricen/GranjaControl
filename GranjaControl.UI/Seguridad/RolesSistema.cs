using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Seguridad
{
    public static class RolesSistema
    {
        public const string Administrador = "Administrador";
        public const string Usuario = "Usuario";

        public static bool TieneSesionActiva()
        {
            return UsuarioSesion.Autenticado;
        }

        public static bool EsAdministrador()
        {
            return TieneSesionActiva() &&
                string.Equals(
                    UsuarioSesion.NombreRol,
                    Administrador,
                    StringComparison.OrdinalIgnoreCase);
        }

        public static bool EsUsuarioOperativo()
        {
            return TieneSesionActiva() &&
                string.Equals(
                    UsuarioSesion.NombreRol,
                    Usuario,
                    StringComparison.OrdinalIgnoreCase);
        }

        public static bool TieneAccesoAlSistema()
        {
            return EsAdministrador() || EsUsuarioOperativo();
        }

        public static bool PuedeAccederSeguridad()
        {
            return EsAdministrador();
        }

        public static bool PuedeGestionarUsuarios()
        {
            return EsAdministrador();
        }

        public static bool PuedeGestionarRoles()
        {
            return EsAdministrador();
        }

        public static bool PuedeConsultarCatalogos()
        {
            return TieneAccesoAlSistema();
        }

        public static bool PuedeAdministrarCatalogos()
        {
            return EsAdministrador();
        }

        public static bool PuedeConsultarLotes()
        {
            return TieneAccesoAlSistema();
        }

        public static bool PuedeGestionarLotes()
        {
            return EsAdministrador();
        }

        public static bool PuedeRegistrarProduccion()
        {
            return TieneAccesoAlSistema();
        }

        public static bool PuedeEditarProduccion()
        {
            return EsAdministrador();
        }

        public static bool PuedeAccederCompras()
        {
            return EsAdministrador();
        }

        public static bool PuedeAccederComercializacion()
        {
            return EsAdministrador();
        }

        public static bool PuedeConsultarReportesProduccion()
        {
            return TieneAccesoAlSistema();
        }

        public static bool PuedeConsultarReportesComerciales()
        {
            return EsAdministrador();
        }

        public static bool Autorizar(
            Form formulario,
            bool permitido,
            string mensaje = "No tiene permisos para realizar esta operación.")
        {
            if (permitido)
                return true;

            Mensajes mensajes = new();
            mensajes.MensajeError(mensaje);

            formulario.DialogResult = DialogResult.Cancel;
            formulario.Close();

            return false;
        }
    }
}
