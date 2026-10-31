namespace GranjaControl.UI.Formularios.Formularios_derivados.Alimentos
{
    partial class FrmRegistroAlimentacion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlSuperior = new Panel();
            lblDescripcion = new Label();
            btnCerrar = new Button();
            lblTitulo = new Label();
            iconIcono = new FontAwesome.Sharp.IconButton();
            pnlContenido = new Panel();
            labelLote = new Label();
            pnlLote = new Panel();
            cboLote = new ComboBox();
            labelAlimento = new Label();
            pnlAlimento = new Panel();
            cboAlimento = new ComboBox();
            labelFecha = new Label();
            pnlFecha = new Panel();
            dtpFecha = new DateTimePicker();
            labelCantidad = new Label();
            pnlCantidad = new Panel();
            numCantidad = new NumericUpDown();
            labelUnidad = new Label();
            pnlUnidad = new Panel();
            cboUnidad = new ComboBox();
            labelObservacion = new Label();
            pnlObservacion = new Panel();
            txtObservacion = new TextBox();
            pnlAcciones = new Panel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            errorIcon = new ErrorProvider(components);
            pnlSuperior.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlLote.SuspendLayout();
            pnlAlimento.SuspendLayout();
            pnlFecha.SuspendLayout();
            pnlCantidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            pnlUnidad.SuspendLayout();
            pnlObservacion.SuspendLayout();
            pnlAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
            SuspendLayout();
            // 
            // pnlSuperior
            // 
            pnlSuperior.BackColor = Color.GhostWhite;
            pnlSuperior.Controls.Add(lblDescripcion);
            pnlSuperior.Controls.Add(btnCerrar);
            pnlSuperior.Controls.Add(lblTitulo);
            pnlSuperior.Controls.Add(iconIcono);
            pnlSuperior.Dock = DockStyle.Top;
            pnlSuperior.Location = new Point(0, 0);
            pnlSuperior.Name = "pnlSuperior";
            pnlSuperior.Size = new Size(580, 64);
            pnlSuperior.TabIndex = 0;
            pnlSuperior.MouseDown += pnlSuperior_MouseDown;
            pnlSuperior.MouseMove += pnlSuperior_MouseMove;
            pnlSuperior.MouseUp += pnlSuperior_MouseUp;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescripcion.ForeColor = SystemColors.ControlDarkDark;
            lblDescripcion.Location = new Point(88, 38);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(356, 23);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Complete la información de la alimentación.";
            // 
            // btnCerrar
            // 
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Image = Properties.Resources._02_cerrar1;
            btnCerrar.Location = new Point(536, 12);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(32, 32);
            btnCerrar.TabIndex = 2;
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(88, 6);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(219, 31);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Nueva alimentación";
            // 
            // iconIcono
            // 
            iconIcono.IconChar = FontAwesome.Sharp.IconChar.WheatAwn;
            iconIcono.IconColor = Color.FromArgb(23, 61, 43);
            iconIcono.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconIcono.IconSize = 42;
            iconIcono.Location = new Point(18, 7);
            iconIcono.Name = "iconIcono";
            iconIcono.Size = new Size(50, 50);
            iconIcono.TabIndex = 0;
            iconIcono.UseVisualStyleBackColor = true;
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(226, 239, 228);
            pnlContenido.Controls.Add(labelLote);
            pnlContenido.Controls.Add(pnlLote);
            pnlContenido.Controls.Add(labelAlimento);
            pnlContenido.Controls.Add(pnlAlimento);
            pnlContenido.Controls.Add(labelFecha);
            pnlContenido.Controls.Add(pnlFecha);
            pnlContenido.Controls.Add(labelCantidad);
            pnlContenido.Controls.Add(pnlCantidad);
            pnlContenido.Controls.Add(labelUnidad);
            pnlContenido.Controls.Add(pnlUnidad);
            pnlContenido.Controls.Add(labelObservacion);
            pnlContenido.Controls.Add(pnlObservacion);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 64);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(580, 441);
            pnlContenido.TabIndex = 1;
            // 
            // labelLote
            // 
            labelLote.AutoSize = true;
            labelLote.BackColor = Color.Transparent;
            labelLote.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelLote.ForeColor = Color.FromArgb(42, 52, 45);
            labelLote.Location = new Point(24, 26);
            labelLote.Name = "labelLote";
            labelLote.Size = new Size(56, 25);
            labelLote.TabIndex = 1;
            labelLote.Text = "Lote:";
            // 
            // pnlLote
            // 
            pnlLote.BackColor = Color.White;
            pnlLote.Controls.Add(cboLote);
            pnlLote.Location = new Point(180, 18);
            pnlLote.Name = "pnlLote";
            pnlLote.Size = new Size(370, 41);
            pnlLote.TabIndex = 0;
            // 
            // cboLote
            // 
            cboLote.BackColor = Color.White;
            cboLote.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLote.FlatStyle = FlatStyle.Flat;
            cboLote.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboLote.ForeColor = Color.FromArgb(42, 52, 45);
            cboLote.FormattingEnabled = true;
            cboLote.Location = new Point(8, 6);
            cboLote.Name = "cboLote";
            cboLote.Size = new Size(352, 28);
            cboLote.TabIndex = 0;
            // 
            // labelAlimento
            // 
            labelAlimento.AutoSize = true;
            labelAlimento.BackColor = Color.Transparent;
            labelAlimento.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelAlimento.ForeColor = Color.FromArgb(42, 52, 45);
            labelAlimento.Location = new Point(24, 80);
            labelAlimento.Name = "labelAlimento";
            labelAlimento.Size = new Size(97, 25);
            labelAlimento.TabIndex = 3;
            labelAlimento.Text = "Alimento:";
            // 
            // pnlAlimento
            // 
            pnlAlimento.BackColor = Color.White;
            pnlAlimento.Controls.Add(cboAlimento);
            pnlAlimento.Location = new Point(180, 72);
            pnlAlimento.Name = "pnlAlimento";
            pnlAlimento.Size = new Size(370, 41);
            pnlAlimento.TabIndex = 2;
            // 
            // cboAlimento
            // 
            cboAlimento.BackColor = Color.White;
            cboAlimento.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAlimento.FlatStyle = FlatStyle.Flat;
            cboAlimento.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboAlimento.ForeColor = Color.FromArgb(42, 52, 45);
            cboAlimento.FormattingEnabled = true;
            cboAlimento.Location = new Point(8, 6);
            cboAlimento.Name = "cboAlimento";
            cboAlimento.Size = new Size(352, 28);
            cboAlimento.TabIndex = 0;
            // 
            // labelFecha
            // 
            labelFecha.AutoSize = true;
            labelFecha.BackColor = Color.Transparent;
            labelFecha.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFecha.ForeColor = Color.FromArgb(42, 52, 45);
            labelFecha.Location = new Point(24, 134);
            labelFecha.Name = "labelFecha";
            labelFecha.Size = new Size(66, 25);
            labelFecha.TabIndex = 5;
            labelFecha.Text = "Fecha:";
            // 
            // pnlFecha
            // 
            pnlFecha.BackColor = Color.White;
            pnlFecha.Controls.Add(dtpFecha);
            pnlFecha.Location = new Point(180, 126);
            pnlFecha.Name = "pnlFecha";
            pnlFecha.Size = new Size(370, 41);
            pnlFecha.TabIndex = 4;
            // 
            // dtpFecha
            // 
            dtpFecha.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(8, 6);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(352, 31);
            dtpFecha.TabIndex = 0;
            // 
            // labelCantidad
            // 
            labelCantidad.AutoSize = true;
            labelCantidad.BackColor = Color.Transparent;
            labelCantidad.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCantidad.ForeColor = Color.FromArgb(42, 52, 45);
            labelCantidad.Location = new Point(24, 188);
            labelCantidad.Name = "labelCantidad";
            labelCantidad.Size = new Size(95, 25);
            labelCantidad.TabIndex = 7;
            labelCantidad.Text = "Cantidad:";
            // 
            // pnlCantidad
            // 
            pnlCantidad.BackColor = Color.White;
            pnlCantidad.Controls.Add(numCantidad);
            pnlCantidad.Location = new Point(180, 180);
            pnlCantidad.Name = "pnlCantidad";
            pnlCantidad.Size = new Size(370, 41);
            pnlCantidad.TabIndex = 6;
            // 
            // numCantidad
            // 
            numCantidad.DecimalPlaces = 2;
            numCantidad.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numCantidad.Increment = 0.5M;
            numCantidad.Location = new Point(8, 6);
            numCantidad.Maximum = 99999999.99M;
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(352, 31);
            numCantidad.TabIndex = 0;
            numCantidad.TextAlign = HorizontalAlignment.Right;
            numCantidad.ThousandsSeparator = true;
            // 
            // labelUnidad
            // 
            labelUnidad.AutoSize = true;
            labelUnidad.BackColor = Color.Transparent;
            labelUnidad.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelUnidad.ForeColor = Color.FromArgb(42, 52, 45);
            labelUnidad.Location = new Point(24, 242);
            labelUnidad.Name = "labelUnidad";
            labelUnidad.Size = new Size(84, 25);
            labelUnidad.TabIndex = 9;
            labelUnidad.Text = "Unidad:";
            // 
            // pnlUnidad
            // 
            pnlUnidad.BackColor = Color.White;
            pnlUnidad.Controls.Add(cboUnidad);
            pnlUnidad.Location = new Point(180, 234);
            pnlUnidad.Name = "pnlUnidad";
            pnlUnidad.Size = new Size(370, 41);
            pnlUnidad.TabIndex = 8;
            // 
            // cboUnidad
            // 
            cboUnidad.BackColor = Color.White;
            cboUnidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUnidad.FlatStyle = FlatStyle.Flat;
            cboUnidad.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboUnidad.ForeColor = Color.FromArgb(42, 52, 45);
            cboUnidad.FormattingEnabled = true;
            cboUnidad.Location = new Point(8, 6);
            cboUnidad.Name = "cboUnidad";
            cboUnidad.Size = new Size(352, 28);
            cboUnidad.TabIndex = 0;
            // 
            // labelObservacion
            // 
            labelObservacion.AutoSize = true;
            labelObservacion.BackColor = Color.Transparent;
            labelObservacion.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelObservacion.ForeColor = Color.FromArgb(42, 52, 45);
            labelObservacion.Location = new Point(24, 296);
            labelObservacion.Name = "labelObservacion";
            labelObservacion.Size = new Size(126, 25);
            labelObservacion.TabIndex = 11;
            labelObservacion.Text = "Observación:";
            // 
            // pnlObservacion
            // 
            pnlObservacion.BackColor = Color.White;
            pnlObservacion.Controls.Add(txtObservacion);
            pnlObservacion.Location = new Point(180, 288);
            pnlObservacion.Name = "pnlObservacion";
            pnlObservacion.Size = new Size(370, 111);
            pnlObservacion.TabIndex = 10;
            // 
            // txtObservacion
            // 
            txtObservacion.BackColor = Color.White;
            txtObservacion.BorderStyle = BorderStyle.None;
            txtObservacion.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtObservacion.ForeColor = Color.FromArgb(42, 52, 45);
            txtObservacion.Location = new Point(8, 8);
            txtObservacion.MaxLength = 250;
            txtObservacion.Multiline = true;
            txtObservacion.Name = "txtObservacion";
            txtObservacion.ScrollBars = ScrollBars.Vertical;
            txtObservacion.Size = new Size(352, 93);
            txtObservacion.TabIndex = 0;
            // 
            // pnlAcciones
            // 
            pnlAcciones.BackColor = Color.FromArgb(226, 239, 228);
            pnlAcciones.Controls.Add(btnCancelar);
            pnlAcciones.Controls.Add(btnGuardar);
            pnlAcciones.Dock = DockStyle.Bottom;
            pnlAcciones.Location = new Point(0, 505);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(580, 55);
            pnlAcciones.TabIndex = 2;
            // 
            // btnCancelar
            // 
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Red;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 26;
            btnCancelar.Location = new Point(296, 6);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(149, 43);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.FromArgb(23, 61, 43);
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 26;
            btnGuardar.Location = new Point(134, 6);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(149, 43);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // errorIcon
            // 
            errorIcon.ContainerControl = this;
            // 
            // FrmRegistroAlimentacion
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 239, 228);
            CancelButton = btnCancelar;
            ClientSize = new Size(580, 560);
            Controls.Add(pnlContenido);
            Controls.Add(pnlAcciones);
            Controls.Add(pnlSuperior);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRegistroAlimentacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmRegistroAlimentacion";
            Load += FrmRegistroAlimentacion_Load;
            Shown += FrmRegistroAlimentacion_Shown;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            pnlLote.ResumeLayout(false);
            pnlAlimento.ResumeLayout(false);
            pnlFecha.ResumeLayout(false);
            pnlCantidad.ResumeLayout(false);
            pnlCantidad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            pnlUnidad.ResumeLayout(false);
            pnlObservacion.ResumeLayout(false);
            pnlObservacion.PerformLayout();
            pnlAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSuperior;
        private Label lblDescripcion;
        private Button btnCerrar;
        private Label lblTitulo;
        private FontAwesome.Sharp.IconButton iconIcono;
        private Panel pnlContenido;
        private Label labelLote;
        private Panel pnlLote;
        private ComboBox cboLote;
        private Label labelAlimento;
        private Panel pnlAlimento;
        private ComboBox cboAlimento;
        private Label labelFecha;
        private Panel pnlFecha;
        private DateTimePicker dtpFecha;
        private Label labelCantidad;
        private Panel pnlCantidad;
        private NumericUpDown numCantidad;
        private Label labelUnidad;
        private Panel pnlUnidad;
        private ComboBox cboUnidad;
        private Label labelObservacion;
        private Panel pnlObservacion;
        private TextBox txtObservacion;
        private Panel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private ErrorProvider errorIcon;
    }
}
