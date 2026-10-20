

using GranjaControl.Entitys.Entidades;

namespace GranjaControl.UI.Seguridad
{
    public class UsuarioSesion
    {

        public static int IdUsuario { get; set; } = 1;
        public static string NombreCompleto { get; set; } = "Administrador del Sistema";

        public static string NombreUsuario { get; set; } = "admin";

        public static int IdRol { get; set; } = 1;

        public static string NombreRol { get; set; } = "Administrador";


        /// <summary>
        /// Registrar sesión
        /// </summary>
        public static void IniciarSesion(UsuarioEntity usuario)
        {
            IdUsuario = usuario.Id;
            NombreCompleto = usuario.NombreCompleto;
            NombreUsuario = usuario.NombreUsuario;
            IdRol = usuario.IdRol;
            NombreRol = usuario.NombreRol;
        }


        /// <summary>
        /// Cerrar sesión
        /// </summary>
        public static void CerrarSesion()
        {
            IdUsuario = 0;
            NombreCompleto = string.Empty;
            NombreUsuario = string.Empty;
            IdRol = 0;
            NombreRol = string.Empty;
        }
    }
}
