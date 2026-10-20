using System;
using System.Collections.Generic;
using System.Text;

namespace GranjaControl.Entitys.Entidades
{
    public class ConexionEntity
    {

        public string Servidor { get; set; } = string.Empty;
        public string BaseDatos { get; set; } = string.Empty;
        public string UsuarioSql { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
