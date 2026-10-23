namespace GranjaControl.UI.Formularios_base
{
    partial class FrmBaseListado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnlContenido = new Panel();
            picEmpty = new PictureBox();
            btnVolver = new Button();
            label1 = new Label();
            lblTitulo = new Label();
            iconTitulo = new FontAwesome.Sharp.IconPictureBox();
            picLinea = new PictureBox();
            pnlBuscar = new Panel();
            btnIcon = new Button();
            txtBuscar = new TextBox();
            pnlPaginacion = new TableLayoutPanel();
            btnUltimo = new FontAwesome.Sharp.IconButton();
            btnSiguiente = new FontAwesome.Sharp.IconButton();
            btnAnterior = new FontAwesome.Sharp.IconButton();
            btnPrimero = new FontAwesome.Sharp.IconButton();
            lblPagina = new Label();
            lblTotalregistro = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnNuevo = new Button();
            dgvListado = new DataGridView();
            pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picEmpty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconTitulo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLinea).BeginInit();
            pnlBuscar.SuspendLayout();
            pnlPaginacion.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvListado).BeginInit();
            SuspendLayout();
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(226, 239, 228);
            pnlContenido.Controls.Add(picEmpty);
            pnlContenido.Controls.Add(btnVolver);
            pnlContenido.Controls.Add(label1);
            pnlContenido.Controls.Add(lblTitulo);
            pnlContenido.Controls.Add(iconTitulo);
            pnlContenido.Controls.Add(picLinea);
            pnlContenido.Controls.Add(pnlBuscar);
            pnlContenido.Controls.Add(pnlPaginacion);
            pnlContenido.Controls.Add(tableLayoutPanel1);
            pnlContenido.Controls.Add(dgvListado);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pnlContenido.ForeColor = Color.White;
            pnlContenido.Location = new Point(0, 0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(1000, 630);
            pnlContenido.TabIndex = 3;
            // 
            // picEmpty
            // 
            picEmpty.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picEmpty.BackColor = Color.FromArgb(245, 247, 245);
            picEmpty.Image = Properties.Resources.Empty__2_;
            picEmpty.Location = new Point(12, 213);
            picEmpty.Name = "picEmpty";
            picEmpty.Size = new Size(976, 302);
            picEmpty.SizeMode = PictureBoxSizeMode.CenterImage;
            picEmpty.TabIndex = 17;
            picEmpty.TabStop = false;
            picEmpty.Visible = false;
            // 
            // btnVolver
            // 
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Image = Properties.Resources._25_volver;
            btnVolver.Location = new Point(12, 12);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(32, 32);
            btnVolver.TabIndex = 16;
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonShadow;
            label1.Location = new Point(73, 106);
            label1.Name = "label1";
            label1.Size = new Size(350, 23);
            label1.TabIndex = 15;
            label1.Text = "Consulte, administre y gestione los registros.";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.DimGray;
            lblTitulo.Location = new Point(73, 65);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(295, 41);
            lblTitulo.TabIndex = 14;
            lblTitulo.Text = "Listado de Galpones";
            // 
            // iconTitulo
            // 
            iconTitulo.BackColor = Color.FromArgb(230, 236, 243);
            iconTitulo.ForeColor = Color.FromArgb(37, 99, 235);
            iconTitulo.IconChar = FontAwesome.Sharp.IconChar.Tasks;
            iconTitulo.IconColor = Color.FromArgb(37, 99, 235);
            iconTitulo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconTitulo.IconSize = 55;
            iconTitulo.Location = new Point(12, 74);
            iconTitulo.Name = "iconTitulo";
            iconTitulo.Size = new Size(55, 55);
            iconTitulo.SizeMode = PictureBoxSizeMode.AutoSize;
            iconTitulo.TabIndex = 13;
            iconTitulo.TabStop = false;
            // 
            // picLinea
            // 
            picLinea.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            picLinea.BackColor = Color.Gray;
            picLinea.Location = new Point(12, 135);
            picLinea.Name = "picLinea";
            picLinea.Size = new Size(976, 1);
            picLinea.TabIndex = 12;
            picLinea.TabStop = false;
            // 
            // pnlBuscar
            // 
            pnlBuscar.BackColor = Color.White;
            pnlBuscar.Controls.Add(btnIcon);
            pnlBuscar.Controls.Add(txtBuscar);
            pnlBuscar.Location = new Point(12, 155);
            pnlBuscar.Name = "pnlBuscar";
            pnlBuscar.Size = new Size(473, 40);
            pnlBuscar.TabIndex = 11;
            // 
            // btnIcon
            // 
            btnIcon.FlatAppearance.BorderSize = 0;
            btnIcon.FlatStyle = FlatStyle.Flat;
            btnIcon.Image = Properties.Resources._08_buscar1;
            btnIcon.Location = new Point(3, 2);
            btnIcon.Name = "btnIcon";
            btnIcon.Size = new Size(35, 35);
            btnIcon.TabIndex = 8;
            btnIcon.UseVisualStyleBackColor = true;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.BackColor = Color.White;
            txtBuscar.BorderStyle = BorderStyle.None;
            txtBuscar.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtBuscar.Location = new Point(39, 9);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar registro";
            txtBuscar.Size = new Size(350, 23);
            txtBuscar.TabIndex = 7;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // pnlPaginacion
            // 
            pnlPaginacion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlPaginacion.BackColor = Color.White;
            pnlPaginacion.ColumnCount = 6;
            pnlPaginacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlPaginacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            pnlPaginacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            pnlPaginacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            pnlPaginacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            pnlPaginacion.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            pnlPaginacion.Controls.Add(btnUltimo, 5, 0);
            pnlPaginacion.Controls.Add(btnSiguiente, 4, 0);
            pnlPaginacion.Controls.Add(btnAnterior, 2, 0);
            pnlPaginacion.Controls.Add(btnPrimero, 1, 0);
            pnlPaginacion.Controls.Add(lblPagina, 3, 0);
            pnlPaginacion.Controls.Add(lblTotalregistro, 0, 0);
            pnlPaginacion.ImeMode = ImeMode.NoControl;
            pnlPaginacion.Location = new Point(12, 547);
            pnlPaginacion.Name = "pnlPaginacion";
            pnlPaginacion.Padding = new Padding(6, 4, 8, 4);
            pnlPaginacion.RowCount = 1;
            pnlPaginacion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlPaginacion.Size = new Size(976, 55);
            pnlPaginacion.TabIndex = 10;
            // 
            // btnUltimo
            // 
            btnUltimo.Cursor = Cursors.Hand;
            btnUltimo.FlatAppearance.BorderSize = 0;
            btnUltimo.IconChar = FontAwesome.Sharp.IconChar.FastForward;
            btnUltimo.IconColor = Color.Black;
            btnUltimo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUltimo.IconSize = 20;
            btnUltimo.Location = new Point(931, 7);
            btnUltimo.Name = "btnUltimo";
            btnUltimo.Size = new Size(34, 41);
            btnUltimo.TabIndex = 7;
            btnUltimo.UseVisualStyleBackColor = true;
            btnUltimo.Click += btnUltimo_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Cursor = Cursors.Hand;
            btnSiguiente.FlatAppearance.BorderSize = 0;
            btnSiguiente.IconChar = FontAwesome.Sharp.IconChar.CaretRight;
            btnSiguiente.IconColor = Color.Black;
            btnSiguiente.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSiguiente.IconSize = 20;
            btnSiguiente.Location = new Point(891, 7);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(34, 41);
            btnSiguiente.TabIndex = 6;
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // btnAnterior
            // 
            btnAnterior.Cursor = Cursors.Hand;
            btnAnterior.FlatAppearance.BorderSize = 0;
            btnAnterior.IconChar = FontAwesome.Sharp.IconChar.CaretLeft;
            btnAnterior.IconColor = Color.Black;
            btnAnterior.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAnterior.IconSize = 20;
            btnAnterior.Location = new Point(731, 7);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(34, 41);
            btnAnterior.TabIndex = 5;
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnPrimero
            // 
            btnPrimero.Cursor = Cursors.Hand;
            btnPrimero.FlatAppearance.BorderSize = 0;
            btnPrimero.IconChar = FontAwesome.Sharp.IconChar.FastBackward;
            btnPrimero.IconColor = Color.Black;
            btnPrimero.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPrimero.IconSize = 20;
            btnPrimero.Location = new Point(691, 7);
            btnPrimero.Name = "btnPrimero";
            btnPrimero.Size = new Size(34, 41);
            btnPrimero.TabIndex = 4;
            btnPrimero.UseVisualStyleBackColor = true;
            btnPrimero.Click += btnPrimero_Click;
            // 
            // lblPagina
            // 
            lblPagina.FlatStyle = FlatStyle.Flat;
            lblPagina.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPagina.ForeColor = Color.Black;
            lblPagina.Location = new Point(771, 4);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(114, 44);
            lblPagina.TabIndex = 3;
            lblPagina.Text = "página 0 de 0";
            lblPagina.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTotalregistro
            // 
            lblTotalregistro.FlatStyle = FlatStyle.Flat;
            lblTotalregistro.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalregistro.ForeColor = Color.Black;
            lblTotalregistro.Location = new Point(9, 4);
            lblTotalregistro.Name = "lblTotalregistro";
            lblTotalregistro.Size = new Size(231, 47);
            lblTotalregistro.TabIndex = 0;
            lblTotalregistro.Text = "Total registros: 0";
            lblTotalregistro.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(btnEliminar, 2, 0);
            tableLayoutPanel1.Controls.Add(btnEditar, 1, 0);
            tableLayoutPanel1.Controls.Add(btnNuevo, 0, 0);
            tableLayoutPanel1.Location = new Point(563, 142);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(425, 65);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(192, 0, 0);
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatAppearance.MouseOverBackColor = Color.FromArgb(151, 40, 40);
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Image = Properties.Resources._09_eliminar;
            btnEliminar.ImageAlign = ContentAlignment.MiddleLeft;
            btnEliminar.Location = new Point(285, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Padding = new Padding(10, 5, 5, 10);
            btnEliminar.Size = new Size(137, 59);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Estado";
            btnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.Navy;
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.FlatAppearance.MouseOverBackColor = Color.FromArgb(36, 83, 116);
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditar.ForeColor = Color.White;
            btnEditar.Image = Properties.Resources._11_actualizar;
            btnEditar.ImageAlign = ContentAlignment.MiddleLeft;
            btnEditar.Location = new Point(144, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.Padding = new Padding(10, 5, 5, 10);
            btnEditar.Size = new Size(134, 59);
            btnEditar.TabIndex = 7;
            btnEditar.Text = "Editar";
            btnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.Green;
            btnNuevo.Cursor = Cursors.Hand;
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(46, 104, 65);
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Image = Properties.Resources._06_nuevo;
            btnNuevo.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevo.Location = new Point(3, 3);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Padding = new Padding(10, 5, 5, 10);
            btnNuevo.Size = new Size(134, 59);
            btnNuevo.TabIndex = 4;
            btnNuevo.Text = "Nuevo";
            btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // dgvListado
            // 
            dgvListado.AllowUserToAddRows = false;
            dgvListado.AllowUserToDeleteRows = false;
            dgvListado.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(247, 249, 247);
            dgvListado.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvListado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvListado.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvListado.BackgroundColor = Color.FromArgb(245, 247, 245);
            dgvListado.BorderStyle = BorderStyle.None;
            dgvListado.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvListado.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(32, 76, 56);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(32, 76, 56);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvListado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListado.Cursor = Cursors.Hand;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(226, 239, 228);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(32, 76, 56);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvListado.DefaultCellStyle = dataGridViewCellStyle3;
            dgvListado.EnableHeadersVisualStyles = false;
            dgvListado.GridColor = Color.FromArgb(218, 225, 219);
            dgvListado.Location = new Point(12, 213);
            dgvListado.MultiSelect = false;
            dgvListado.Name = "dgvListado";
            dgvListado.ReadOnly = true;
            dgvListado.RowHeadersVisible = false;
            dgvListado.RowHeadersWidth = 51;
            dgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvListado.Size = new Size(976, 302);
            dgvListado.TabIndex = 5;
            dgvListado.CellFormatting += dgvGalpones_CellFormatting;
            // 
            // FrmBaseListado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 245);
            ClientSize = new Size(1000, 630);
            Controls.Add(pnlContenido);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmBaseListado";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmBaseListadoGalpon";
            Load += FrmBaseListadoGalpon_Load;
            Shown += FrmBaseListadoGalpon_Shown;
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picEmpty).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconTitulo).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLinea).EndInit();
            pnlBuscar.ResumeLayout(false);
            pnlBuscar.PerformLayout();
            pnlPaginacion.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvListado).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlContenido;
        public DataGridView dgvListado;
        public Button btnEliminar;
        public Button btnEditar;
        public Button btnNuevo;
        public TableLayoutPanel pnlPaginacion;
        public FontAwesome.Sharp.IconButton btnUltimo;
        public FontAwesome.Sharp.IconButton btnSiguiente;
        public FontAwesome.Sharp.IconButton btnAnterior;
        public FontAwesome.Sharp.IconButton btnPrimero;
        public Label lblPagina;
        public Label lblTotalregistro;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel pnlBuscar;
        public TextBox txtBuscar;
        private Button btnIcon;
        public Label label1;
        public Label lblTitulo;
        public FontAwesome.Sharp.IconPictureBox iconTitulo;
        private PictureBox picLinea;
        private Button btnVolver;
        private PictureBox picEmpty;
    }
}