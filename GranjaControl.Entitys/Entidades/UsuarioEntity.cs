

namespace GranjaControl.Entitys.Entidades
{
    public class UsuarioEntity
    {
        public int Id { get; private set; }
        public string NombreCompleto { get; private set; } = string.Empty;
        public string NombreUsuario { get; private set; } = string.Empty;
        public string CorreoElectronico { get; private set; } = string.Empty;
        public int IdRol { get; private set; }
        public string NombreRol { get; private set; } = string.Empty;
        public bool Activo { get; private set; } = false;
        public string estado => Activo ? "Activo" : "Inactivo";

        #region Constructores

        public UsuarioEntity() { }

        public UsuarioEntity(string nombreCompleto, string nombreUsuario, string correoElectronico, int idRol)
        {
            CambiarNombreCompleto(nombreCompleto);
            CambiarNombreUsuario(nombreUsuario);
            CambiarCorreo(correoElectronico);
            CambiarRol(idRol);
            Activo = true;
        }

        #endregion

        #region Métodos

        public void AsignarId(int id)
        {
            Id = id;
        }

        public void CambiarNombreCompleto(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Debe especificar el nombre completo del usuario.");

            NombreCompleto = nombre.Trim();
        }

        public void CambiarNombreUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("Debe especificar el nombre de usuario.");

            NombreUsuario = usuario.Trim();
        }

        public void CambiarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new Exception("Debe especificar el correo electrónico.");

            CorreoElectronico = correo.Trim();
        }

        public void CambiarRol(int idRol)
        {
            if (idRol <= 0)
                throw new Exception("Debe seleccionarl un rol.");

            IdRol = idRol;
        }

        public void CambiarNombreRol(string nombreRol)
        {
            NombreRol = nombreRol.Trim();
        }

        public void CambiarEstado(bool activo)
        {
            Activo = activo;
        }

        #endregion
    }
}
