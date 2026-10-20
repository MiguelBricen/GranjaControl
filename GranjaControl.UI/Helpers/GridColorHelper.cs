
namespace GranjaControl.UI.Helpers
{
    public static class GridColorHelper
    {
        #region Estado Activo / Inactivo

        public static void FormatearActivoInactivo(DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;

            switch (e.Value.ToString())
            {
                case "Activo":
                    AplicarEstilo(e, Color.FromArgb(37, 99, 235));
                    break;

                case "Inactivo":

                    AplicarEstilo(e, Color.FromArgb(185, 28, 28));
                    break;
            }
        }

        #endregion

        #region Estado Ticket

        public static void FormatearEstadoTicket(DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;

            switch (e.Value.ToString())
            {
                case "Abierto":
                    AplicarEstilo(e, Color.FromArgb(37, 99, 235));
                    break;

                case "En proceso":
                    AplicarEstilo(e, Color.FromArgb(217, 119, 6));
                    break;

                case "Cerrado":
                    AplicarEstilo(e, Color.FromArgb(22, 163, 74));
                    break;

                case "Cancelado":
                    AplicarEstilo(e, Color.FromArgb(220, 38, 38));
                    break;
            }
        }

        #endregion

        #region Prioridad Ticket

        public static void FormatearPrioridadTicket(DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;

            switch (e.Value.ToString())
            {
                case "Baja":
                    AplicarEstilo(e, Color.FromArgb(22, 163, 74));
                    break;

                case "Media":
                    AplicarEstilo(e, Color.FromArgb(37, 99, 235));
                    break;

                case "Alta":
                    AplicarEstilo(e, Color.FromArgb(217, 119, 6));
                    break;

                case "Crítica":
                    AplicarEstilo(e, Color.FromArgb(220, 38, 38));
                    break;
            }
        }

        #endregion

        #region Método privado

        private static void AplicarEstilo(DataGridViewCellFormattingEventArgs e, Color colorTexto)
        {
            e.CellStyle.ForeColor = colorTexto;

            // Mantener el mismo color cuando la fila está seleccionada
            e.CellStyle.SelectionForeColor = colorTexto; ;
        }

        #endregion
    }
}
