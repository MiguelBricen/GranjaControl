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
            if (ex.Number is 52037 or 52064 or 52069 or 52071)
                return ErrorUsuario(ex.Message);

            if (ex.Number == 52070)
                return ErrorUsuario(
                    "La cantidad inicial no puede ser menor que los pollitos ya vendidos o fallecidos.");

            if (ex.Number == 2812)
                return ErrorUsuario(
                    "La base de datos requiere una actualización para completar esta operación. Contacte al administrador.");

            if (ex.Number is 2 or 26 or 53 or -2 or 233 or 258 or 18456 or 10053 or 10054 or 10060 or 10061 or 11001 or 4060)
            {
                return ErrorUsuario(
                    "No fue posible conectar con la base de datos. Compruebe la conexión e inténtelo nuevamente.");
            }

            if (ex.Number is 2601 or 2627)
            {
                if (ex.Message.Contains("UQ_tblLotes_codigoLote", StringComparison.OrdinalIgnoreCase))
                    return ErrorUsuario("Ya existe un lote registrado con ese código. Ingrese un código diferente.");

                if (ex.Message.Contains("UQ_tblGalpones_nombreGalpon", StringComparison.OrdinalIgnoreCase))
                    return ErrorUsuario("Ya existe un galpón con ese nombre. Ingrese un nombre diferente.");

                return ErrorUsuario(
                    "Este registro ya existe y no puede guardarse nuevamente. Ingrese un valor diferente.");
            }

            Exception error = ex.Number switch
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
                52012 => new Exception("Ya existe un lote registrado con ese código. Ingrese un código diferente."),
                52013 => new Exception("El galpón seleccionado ya no está disponible. Seleccione un galpón activo."),
                52014 => new Exception("Seleccione un proveedor activo que suministre pollitos."),
                52015 => new Exception("La cantidad inicial de pollitos debe ser mayor que cero."),
                52016 => new Exception("El costo por pollito no puede ser negativo."),
                52017 => new Exception("El lote no existe."),
                52018 => new Exception("El estado del lote no es válido."),

                // Galpones
                52006 => new Exception("Ya existe un galpón con ese nombre. Ingrese un nombre diferente."),
                52007 => new Exception("La capacidad del galpón debe ser mayor que cero."),
                52008 => new Exception("No se encontró el galpón solicitado."),

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

                // Vitaminización
                52029 => new Exception("El lote no existe o está cancelado."),
                52030 => new Exception("La vitamina no existe o está inactiva."),
                52031 => new Exception("La cantidad de vitamina debe ser mayor que cero."),
                52032 => new Exception("La unidad de medida no es válida."),
                52033 => new Exception("El registro de aplicación de vitamina no existe."),

                // Mortalidades
                52034 => new Exception("El lote no existe o está cancelado."),
                52035 => new Exception("La cantidad de mortalidad debe ser mayor que cero."),
                52036 => new Exception("La causa de mortalidad es obligatoria."),
                52037 => new Exception("La cantidad de mortalidad supera la cantidad disponible del lote."),
                52038 => new Exception("El registro de mortalidad no existe."),

                // Compras de alimentos
                52039 => new Exception("El número de compra ya existe."),
                52040 => new Exception("El proveedor seleccionado no puede suministrar alimento o está inactivo."),
                52041 => new Exception("La compra de alimento no existe."),
                52042 => new Exception("La cantidad de alimento debe ser mayor que cero."),
                52043 => new Exception("El costo unitario no puede ser negativo."),
                52044 => new Exception("El detalle de compra no existe."),

                // Clientes
                52045 => new Exception("El nombre del cliente es obligatorio."),
                52046 => new Exception("El tipo de cliente no es válido."),
                52047 => new Exception("Ya existe un cliente con ese nombre."),
                52048 => new Exception("El cliente no existe."),
                52049 => new Exception("El precio por libra debe ser mayor que cero."),
                52050 => new Exception("La fecha fin no puede ser anterior a la fecha inicio."),
                52051 => new Exception("El período de la tarifa se cruza con otra tarifa existente."),
                52052 => new Exception("La tarifa de pollo no existe."),

                // Ventas
                52053 => new Exception("El número de venta no tiene un formato válido."),
                52054 => new Exception("Ya existe una venta con ese número."),
                52055 => new Exception("El cliente no existe o está inactivo."),
                52056 => new Exception("La venta no existe."),
                52057 => new Exception("El lote no existe."),
                52058 => new Exception("El peso total en libras debe ser mayor que cero."),
                52059 => new Exception("El lote no está listo para la venta o no existe."),
                52060 => new Exception("El lote no tiene pollos disponibles para la venta."),
                52061 => new Exception("No existe una tarifa vigente para la fecha de la venta."),
                52062 => new Exception("El detalle de venta no existe."),
                52063 => new Exception("La cantidad de pollos debe ser mayor que cero."),
                52064 => new Exception("La cantidad de pollos supera la disponibilidad del lote."),
                52065 => new Exception("La venta está anulada y no puede modificarse."),
                52066 => new Exception("La venta ya está anulada."),
                52067 => new Exception("El lote ya está incluido en los detalles de esta venta."),
                52068 => new Exception("No está configurado el contador de numeración de ventas."),

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
                    "No se pudo completar la operación. No se realizaron cambios. Inténtelo nuevamente.",
                    ex)
            };

            if (error.InnerException is null)
                error.Data["MensajeUsuario"] = true;

            return error;
        }

        private static Exception ErrorUsuario(string mensaje)
        {
            Exception error = new(mensaje);
            error.Data["MensajeUsuario"] = true;
            return error;
        }
    }
}
