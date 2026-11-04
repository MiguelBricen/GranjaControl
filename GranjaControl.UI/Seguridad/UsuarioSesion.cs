

using GranjaControl.Entitys.Entidades;

namespace GranjaControl.UI.Seguridad
{
    public class UsuarioSesion
    {

        public static bool Autenticado { get; private set; }

        public static int IdUsuario { get; private set; }

        public static string NombreCompleto { get; private set; } = string.Empty;

        public static string NombreUsuario { get; private set; } = string.Empty;

        public static int IdRol { get; private set; }

        public static string NombreRol { get; private set; } = string.Empty;


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
            Autenticado = true;
        }


        /// <summary>
        /// Cerrar sesión
        /// </summary>
        public static void CerrarSesion()
        {
            Autenticado = false;
            IdUsuario = 0;
            NombreCompleto = string.Empty;
            NombreUsuario = string.Empty;
            IdRol = 0;
            NombreRol = string.Empty;
        }
    }
}
