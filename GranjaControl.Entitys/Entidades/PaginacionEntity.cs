using System;
using System.Collections.Generic;
using System.Text;

namespace GranjaControl.Entitys.Entidades
{
    public class PaginacionEntity<T>
    {

        // Registros de la página actual
        public List<T> Registros { get; set; } = [];

        // Total de registros encontrados
        public int TotalRegistros { get; set; }

        // Página actual
        public int PaginaActual { get; set; }

        // Tamaño de página
        public int TamPagina { get; set; }

        // Total de páginas

        public int TotalPaginas =>
            TotalRegistros == 0
            ? 1
            : (int)Math.Ceiling((double)TotalRegistros / TamPagina);
    }
}
