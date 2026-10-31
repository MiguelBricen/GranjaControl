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

                // Proveedores
                52009 => new Exception("El proveedor ya existe."),
                52010 => new Exception("El tipo de proveedor no es válido."),
                52011 => new Exception("El proveedor no existe."),

                // Lotes
                52012 => new Exception("El código del lote ya existe."),
                52013 => new Exception("El galpón seleccionado no existe o está inactivo."),
                52014 => new Exception("El proveedor seleccionado no puede suministrar pollitos o está inactivo."),
                52015 => new Exception("La cantidad inicial de pollitos debe ser mayor que cero."),
                52016 => new Exception("El costo por pollito no puede ser negativo."),
                52017 => new Exception("El lote no existe."),
                52018 => new Exception("El estado del lote no es válido."),

                // Alimentos
                52019 => new Exception("El alimento ya existe."),
                52020 => new Exception("El tipo de alimento no es válido."),
                52021 => new Exception("El alimento no existe."),

                // Alimentación
                52022 => new Exception("El lote no existe o está cancelado."),
                52023 => new Exception("El alimento no existe o está inactivo."),
                52024 => new Exception("La cantidad de alimento debe ser mayor que cero."),
                52025 => new Exception("La unidad de medida no es válida."),
                52026 => new Exception("El registro de alimentación no existe."),

                // Vitaminas
                52027 => new Exception("La vitamina ya existe."),
                52028 => new Exception("La vitamina no existe."),

                // Mortalidades
                52034 => new Exception("El lote no existe o está cancelado."),
                52035 => new Exception("La cantidad de mortalidad debe ser mayor que cero."),
                52036 => new Exception("La causa de mortalidad es obligatoria."),
                52037 => new Exception("La cantidad de mortalidad supera la cantidad disponible del lote."),
                52038 => new Exception("El registro de mortalidad no existe."),

                // Clientes
                52045 => new Exception("El nombre del cliente es obligatorio."),
                52046 => new Exception("El tipo de cliente no es válido."),
                52047 => new Exception("Ya existe un cliente con ese nombre."),
                52048 => new Exception("El cliente no existe."),

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
