namespace GranjaControl.UI.Helpers
{
    public class GridDashboardHelper
    {
        public static void Personalizar(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.GridColor = Color.FromArgb(220, 228, 222);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 42;
            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(32, 76, 56);
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
                Color.FromArgb(32, 76, 56);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            dgv.DefaultCellStyle.BackColor =
                Color.White;
            dgv.DefaultCellStyle.ForeColor =
                Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(226, 239, 228);
            dgv.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(32, 76, 56);
            dgv.DefaultCellStyle.Font =
                new Font("Segoe UI", 9.5F);
            dgv.DefaultCellStyle.Padding =
                new Padding(9, 0, 9, 0);
            dgv.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgv.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgv.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(247, 249, 247);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor =
                Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(226, 239, 228);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor =
                Color.FromArgb(32, 76, 56);
            dgv.AlternatingRowsDefaultCellStyle.Padding =
                new Padding(9, 0, 9, 0);

            dgv.RowsDefaultCellStyle.BackColor =
                Color.White;
            dgv.RowsDefaultCellStyle.ForeColor =
                Color.Black;
            dgv.RowsDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(226, 239, 228);
            dgv.RowsDefaultCellStyle.SelectionForeColor =
                Color.FromArgb(32, 76, 56);

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

            dgv.ColumnAdded += (_, e) => AlinearColumnaNumerica(e.Column);
            dgv.DataBindingComplete += (_, _) =>
            {
                foreach (DataGridViewColumn columna in dgv.Columns)
                    AlinearColumnaNumerica(columna);
            };
        }

        private static void AlinearColumnaNumerica(
            DataGridViewColumn columna)
        {
            Type? tipo = columna.ValueType;

            if (tipo is null)
                return;

            tipo = Nullable.GetUnderlyingType(tipo) ?? tipo;

            if (Type.GetTypeCode(tipo) is
                TypeCode.Byte or
                TypeCode.SByte or
                TypeCode.Int16 or
                TypeCode.UInt16 or
                TypeCode.Int32 or
                TypeCode.UInt32 or
                TypeCode.Int64 or
                TypeCode.UInt64 or
                TypeCode.Decimal or
                TypeCode.Single or
                TypeCode.Double)
            {
                columna.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
}
