namespace GranjaControl.UI.Helpers
{
    public class GridDashboardHelper
    {
        public static void Personalizar(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = EstiloVisualHelper.Superficie;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.GridColor = EstiloVisualHelper.Borde;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 42;
            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                EstiloVisualHelper.EncabezadoTabla;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding =
                new Padding(9, 0, 9, 0);
            dgv.ColumnHeadersDefaultCellStyle.WrapMode =
                DataGridViewTriState.False;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                EstiloVisualHelper.EncabezadoTabla;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            dgv.DefaultCellStyle.BackColor =
                EstiloVisualHelper.Superficie;
            dgv.DefaultCellStyle.ForeColor =
                EstiloVisualHelper.TextoPrincipal;
            dgv.DefaultCellStyle.SelectionBackColor =
                EstiloVisualHelper.Seleccion;
            dgv.DefaultCellStyle.SelectionForeColor =
                EstiloVisualHelper.TextoPrincipal;
            dgv.DefaultCellStyle.Font =
                new Font("Segoe UI", 9.5F);
            dgv.DefaultCellStyle.Padding =
                new Padding(9, 0, 9, 0);
            dgv.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgv.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgv.AlternatingRowsDefaultCellStyle.BackColor =
                EstiloVisualHelper.FilaAlterna;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor =
                EstiloVisualHelper.TextoPrincipal;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor =
                EstiloVisualHelper.Seleccion;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor =
                EstiloVisualHelper.TextoPrincipal;
            dgv.AlternatingRowsDefaultCellStyle.Padding =
                new Padding(9, 0, 9, 0);

            dgv.RowsDefaultCellStyle.BackColor =
                EstiloVisualHelper.Superficie;
            dgv.RowsDefaultCellStyle.ForeColor =
                EstiloVisualHelper.TextoPrincipal;

            dgv.RowTemplate.Height = 34;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgv.Cursor = Cursors.Hand;
        }
    }
}
