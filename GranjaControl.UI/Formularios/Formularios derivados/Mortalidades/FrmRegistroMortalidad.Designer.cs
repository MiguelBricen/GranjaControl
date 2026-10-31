namespace GranjaControl.UI.Formularios.Formularios_derivados.Mortalidades
{
    partial class FrmRegistroMortalidad
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
            labelFecha = new Label();
            pnlFecha = new Panel();
            dtpFecha = new DateTimePicker();
            labelCantidad = new Label();
            pnlCantidad = new Panel();
            numCantidad = new NumericUpDown();
            labelCausa = new Label();
            pnlCausa = new Panel();
            txtCausa = new TextBox();
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
            pnlFecha.SuspendLayout();
            pnlCantidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            pnlCausa.SuspendLayout();
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
            lblDescripcion.Size = new Size(267, 23);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Registre la baja de aves del lote.";
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
            lblTitulo.Size = new Size(200, 31);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Nueva mortalidad";
            // 
            // iconIcono
            // 
            iconIcono.IconChar = FontAwesome.Sharp.IconChar.SkullCrossbones;
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
            pnlContenido.Controls.Add(labelFecha);
            pnlContenido.Controls.Add(pnlFecha);
            pnlContenido.Controls.Add(labelCantidad);
            pnlContenido.Controls.Add(pnlCantidad);
            pnlContenido.Controls.Add(labelCausa);
            pnlContenido.Controls.Add(pnlCausa);
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
            // labelFecha
            // 
            labelFecha.AutoSize = true;
            labelFecha.BackColor = Color.Transparent;
            labelFecha.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFecha.ForeColor = Color.FromArgb(42, 52, 45);
            labelFecha.Location = new Point(24, 80);
            labelFecha.Name = "labelFecha";
            labelFecha.Size = new Size(66, 25);
            labelFecha.TabIndex = 3;
            labelFecha.Text = "Fecha:";
            // 
            // pnlFecha
            // 
            pnlFecha.BackColor = Color.White;
            pnlFecha.Controls.Add(dtpFecha);
            pnlFecha.Location = new Point(180, 72);
            pnlFecha.Name = "pnlFecha";
            pnlFecha.Size = new Size(370, 41);
            pnlFecha.TabIndex = 2;
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
            labelCantidad.Location = new Point(24, 134);
            labelCantidad.Name = "labelCantidad";
            labelCantidad.Size = new Size(95, 25);
            labelCantidad.TabIndex = 5;
            labelCantidad.Text = "Cantidad:";
            // 
            // pnlCantidad
            // 
            pnlCantidad.BackColor = Color.White;
            pnlCantidad.Controls.Add(numCantidad);
            pnlCantidad.Location = new Point(180, 126);
            pnlCantidad.Name = "pnlCantidad";
            pnlCantidad.Size = new Size(370, 41);
            pnlCantidad.TabIndex = 4;
            // 
            // numCantidad
            // 
            numCantidad.DecimalPlaces = 0;
            numCantidad.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numCantidad.Increment = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Location = new Point(8, 6);
            numCantidad.Maximum = new decimal(new int[] { 2147483647, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(352, 31);
            numCantidad.TabIndex = 0;
            numCantidad.TextAlign = HorizontalAlignment.Right;
            numCantidad.ThousandsSeparator = true;
            // 
            // labelCausa
            // 
            labelCausa.AutoSize = true;
            labelCausa.BackColor = Color.Transparent;
            labelCausa.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCausa.ForeColor = Color.FromArgb(42, 52, 45);
            labelCausa.Location = new Point(24, 188);
            labelCausa.Name = "labelCausa";
            labelCausa.Size = new Size(72, 25);
            labelCausa.TabIndex = 7;
            labelCausa.Text = "Causa:";
            // 
            // pnlCausa
            // 
            pnlCausa.BackColor = Color.White;
            pnlCausa.Controls.Add(txtCausa);
            pnlCausa.Location = new Point(180, 180);
            pnlCausa.Name = "pnlCausa";
            pnlCausa.Size = new Size(370, 41);
            pnlCausa.TabIndex = 6;
            // 
            // txtCausa
            // 
            txtCausa.BackColor = Color.White;
            txtCausa.BorderStyle = BorderStyle.None;
            txtCausa.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCausa.ForeColor = Color.FromArgb(42, 52, 45);
            txtCausa.Location = new Point(8, 8);
            txtCausa.MaxLength = 150;
            txtCausa.Name = "txtCausa";
            txtCausa.Size = new Size(352, 24);
            txtCausa.TabIndex = 0;
            // 
            // labelObservacion
            // 
            labelObservacion.AutoSize = true;
            labelObservacion.BackColor = Color.Transparent;
            labelObservacion.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelObservacion.ForeColor = Color.FromArgb(42, 52, 45);
            labelObservacion.Location = new Point(24, 242);
            labelObservacion.Name = "labelObservacion";
            labelObservacion.Size = new Size(126, 25);
            labelObservacion.TabIndex = 9;
            labelObservacion.Text = "Observación:";
            // 
            // pnlObservacion
            // 
            pnlObservacion.BackColor = Color.White;
            pnlObservacion.Controls.Add(txtObservacion);
            pnlObservacion.Location = new Point(180, 234);
            pnlObservacion.Name = "pnlObservacion";
            pnlObservacion.Size = new Size(370, 155);
            pnlObservacion.TabIndex = 8;
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
            txtObservacion.Size = new Size(352, 137);
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
            // FrmRegistroMortalidad
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
            Name = "FrmRegistroMortalidad";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmRegistroMortalidad";
            Load += FrmRegistroMortalidad_Load;
            Shown += FrmRegistroMortalidad_Shown;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            pnlLote.ResumeLayout(false);
            pnlFecha.ResumeLayout(false);
            pnlCantidad.ResumeLayout(false);
            pnlCantidad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            pnlCausa.ResumeLayout(false);
            pnlCausa.PerformLayout();
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
        private Label labelFecha;
        private Panel pnlFecha;
        private DateTimePicker dtpFecha;
        private Label labelCantidad;
        private Panel pnlCantidad;
        private NumericUpDown numCantidad;
        private Label labelCausa;
        private Panel pnlCausa;
        private TextBox txtCausa;
        private Label labelObservacion;
        private Panel pnlObservacion;
        private TextBox txtObservacion;
        private Panel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private ErrorProvider errorIcon;
    }
}
