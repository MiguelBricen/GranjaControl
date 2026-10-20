
namespace GranjaControl.Entitys.Entidades
{
    public class RolEntity
    {
        public int Id { get; private set; }
        public string NombreRol { get; private set; } = string.Empty;
        public bool Activo { get; private set; }

        public string Estado => Activo ? "Activo" : "Inactivo";

        #region Constructores

        public RolEntity() { }

        public RolEntity(string nombreRol)
        {
            CambiarNombre(nombreRol);
            Activo = true;
        }

        #endregion

        #region Métodos
        public void AsignarId(int id)
        {
            Id = id;
        }

        public void CambiarNombre(string nombreRol)
        {
            if (string.IsNullOrWhiteSpace(nombreRol))
                throw new Exception("Debe ingresar el nombre del rol");

            NombreRol = nombreRol.Trim();
        }

        public void CambiarEstado(bool activo)
        {
            Activo = activo;
        }
        #endregion
    }
}
