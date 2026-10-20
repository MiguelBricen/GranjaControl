using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranjaControl.DAL.Common
{
    public class SqlErrorHelper
    {
        public static Exception TraducirError(SqlException ex)
        {
            return ex.Number switch
            {
                // Roles
                50001 => new Exception("El rol ya existe."),
                50002 => new Exception("El rol no existe."),
                50003 => new Exception("El rol tiene usuarios asignados."),

                // Usuarios
                51001 => new Exception("El usuario ya existe."),
                51002 => new Exception("El usuario no existe."),
                51003 => new Exception("El usuario está inactivo."),
                51004 => new Exception("Contraseña incorrecta."),
                51005 => new Exception("El correo ya existe."),
                51006 => new Exception("El rol no existe o está inactivo."),
                51007 => new Exception("No se puede desactivar el administrador."),
                51008 => new Exception("La nueva contraseña debe ser diferente a la actual."),

                // Categorías
                52001 => new Exception("La categoría ya existe."),
                52002 => new Exception("La categoría no existe o está inactiva."),

                // Tickets
                53001 => new Exception("El usuario solicitante no existe."),
                53002 => new Exception("El ticket no existe."),
                53003 => new Exception("El estado seleccionado no es válido."),
                53004 => new Exception("No es posible realizar esa transición de estado."),
                53005 => new Exception("El técnico seleccionado no es válido."),
                53006 => new Exception("Debe asignar un técnico para este estado."),

                // Seguimientos
                54001 => new Exception("El seguimiento no existe."),

                // Adjuntos
                55001 => new Exception("El archivo adjunto no existe."),

                _ => new Exception(
                    "Ocurrió un error inesperado.",
                    ex)
            };
        }
    }
}
