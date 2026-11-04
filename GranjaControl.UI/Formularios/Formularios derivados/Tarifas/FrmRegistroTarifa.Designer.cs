namespace GranjaControl.UI.Formularios.Formularios_derivados.Tarifas
{
    partial class FrmRegistroTarifa
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
            labelPrecio = new Label();
            pnlPrecio = new Panel();
            numPrecio = new NumericUpDown();
            labelFechaInicio = new Label();
            pnlFechaInicio = new Panel();
            dtpFechaInicio = new DateTimePicker();
            labelFechaFin = new Label();
            pnlFechaFin = new Panel();
            dtpFechaFin = new DateTimePicker();
            chkSinFechaFin = new CheckBox();
            lblAyuda = new Label();
            pnlAcciones = new Panel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            errorIcon = new ErrorProvider(components);
            pnlSuperior.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlPrecio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPrecio).BeginInit();
            pnlFechaInicio.SuspendLayout();
            pnlFechaFin.SuspendLayout();
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
            pnlSuperior.Size = new Size(520, 64);
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
            lblDescripcion.Size = new Size(391, 23);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Registre el precio por libra y su período de vigencia.";
            // 
            // btnCerrar
            // 
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Image = Properties.Resources._02_cerrar1;
            btnCerrar.Location = new Point(476, 12);
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
            lblTitulo.Size = new Size(215, 31);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Nueva tarifa de pollo";
            // 
            // iconIcono
            // 
            iconIcono.IconChar = FontAwesome.Sharp.IconChar.SackDollar;
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
            pnlContenido.Controls.Add(labelPrecio);
            pnlContenido.Controls.Add(pnlPrecio);
            pnlContenido.Controls.Add(labelFechaInicio);
            pnlContenido.Controls.Add(pnlFechaInicio);
            pnlContenido.Controls.Add(labelFechaFin);
            pnlContenido.Controls.Add(pnlFechaFin);
            pnlContenido.Controls.Add(chkSinFechaFin);
            pnlContenido.Controls.Add(lblAyuda);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 64);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(520, 301);
            pnlContenido.TabIndex = 1;
            // 
            // labelPrecio
            // 
            labelPrecio.AutoSize = true;
            labelPrecio.BackColor = Color.Transparent;
            labelPrecio.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPrecio.ForeColor = Color.FromArgb(42, 52, 45);
            labelPrecio.Location = new Point(24, 24);
            labelPrecio.Name = "labelPrecio";
            labelPrecio.Size = new Size(128, 25);
            labelPrecio.TabIndex = 1;
            labelPrecio.Text = "Precio por libra:";
            // 
            // pnlPrecio
            // 
            pnlPrecio.BackColor = Color.White;
            pnlPrecio.Controls.Add(numPrecio);
            pnlPrecio.Location = new Point(165, 22);
            pnlPrecio.Name = "pnlPrecio";
            pnlPrecio.Size = new Size(330, 41);
            pnlPrecio.TabIndex = 0;
            // 
            // numPrecio
            // 
            numPrecio.BackColor = Color.White;
            numPrecio.BorderStyle = BorderStyle.None;
            numPrecio.DecimalPlaces = 2;
            numPrecio.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numPrecio.ForeColor = Color.FromArgb(42, 52, 45);
            numPrecio.Increment = 0.01M;
            numPrecio.Location = new Point(8, 7);
            numPrecio.Maximum = 99999999.99M;
            numPrecio.Name = "numPrecio";
            numPrecio.Size = new Size(312, 31);
            numPrecio.TabIndex = 0;
            numPrecio.TextAlign = HorizontalAlignment.Right;
            numPrecio.ThousandsSeparator = true;
            // 
            // labelFechaInicio
            // 
            labelFechaInicio.AutoSize = true;
            labelFechaInicio.BackColor = Color.Transparent;
            labelFechaInicio.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFechaInicio.ForeColor = Color.FromArgb(42, 52, 45);
            labelFechaInicio.Location = new Point(24, 78);
            labelFechaInicio.Name = "labelFechaInicio";
            labelFechaInicio.Size = new Size(108, 25);
            labelFechaInicio.TabIndex = 3;
            labelFechaInicio.Text = "Fecha inicio:";
            // 
            // pnlFechaInicio
            // 
            pnlFechaInicio.BackColor = Color.White;
            pnlFechaInicio.Controls.Add(dtpFechaInicio);
            pnlFechaInicio.Location = new Point(165, 76);
            pnlFechaInicio.Name = "pnlFechaInicio";
            pnlFechaInicio.Size = new Size(330, 41);
            pnlFechaInicio.TabIndex = 2;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(8, 6);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(312, 31);
            dtpFechaInicio.TabIndex = 0;
            // 
            // labelFechaFin
            // 
            labelFechaFin.AutoSize = true;
            labelFechaFin.BackColor = Color.Transparent;
            labelFechaFin.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFechaFin.ForeColor = Color.FromArgb(42, 52, 45);
            labelFechaFin.Location = new Point(24, 132);
            labelFechaFin.Name = "labelFechaFin";
            labelFechaFin.Size = new Size(88, 25);
            labelFechaFin.TabIndex = 5;
            labelFechaFin.Text = "Fecha fin:";
            // 
            // pnlFechaFin
            // 
            pnlFechaFin.BackColor = Color.White;
            pnlFechaFin.Controls.Add(dtpFechaFin);
            pnlFechaFin.Location = new Point(165, 130);
            pnlFechaFin.Name = "pnlFechaFin";
            pnlFechaFin.Size = new Size(330, 41);
            pnlFechaFin.TabIndex = 4;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Enabled = false;
            dtpFechaFin.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(8, 6);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(312, 31);
            dtpFechaFin.TabIndex = 0;
            // 
            // chkSinFechaFin
            // 
            chkSinFechaFin.AutoSize = true;
            chkSinFechaFin.Checked = true;
            chkSinFechaFin.CheckState = CheckState.Checked;
            chkSinFechaFin.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkSinFechaFin.ForeColor = Color.FromArgb(42, 52, 45);
            chkSinFechaFin.Location = new Point(165, 184);
            chkSinFechaFin.Name = "chkSinFechaFin";
            chkSinFechaFin.Size = new Size(163, 27);
            chkSinFechaFin.TabIndex = 6;
            chkSinFechaFin.Text = "Sin fecha de fin";
            chkSinFechaFin.UseVisualStyleBackColor = true;
            chkSinFechaFin.CheckedChanged += chkSinFechaFin_CheckedChanged;
            // 
            // lblAyuda
            // 
            lblAyuda.BackColor = Color.Transparent;
            lblAyuda.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAyuda.ForeColor = SystemColors.ControlDarkDark;
            lblAyuda.Location = new Point(24, 224);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new Size(471, 46);
            lblAyuda.TabIndex = 7;
            lblAyuda.Text = "Los períodos no pueden cruzarse con otra tarifa, aunque esté inactiva.";
            // 
            // pnlAcciones
            // 
            pnlAcciones.BackColor = Color.FromArgb(226, 239, 228);
            pnlAcciones.Controls.Add(btnCancelar);
            pnlAcciones.Controls.Add(btnGuardar);
            pnlAcciones.Dock = DockStyle.Bottom;
            pnlAcciones.Location = new Point(0, 365);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(520, 55);
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
            btnCancelar.Location = new Point(278, 6);
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
            btnGuardar.Location = new Point(116, 6);
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
            // FrmRegistroTarifa
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 239, 228);
            CancelButton = btnCancelar;
            ClientSize = new Size(520, 420);
            Controls.Add(pnlContenido);
            Controls.Add(pnlAcciones);
            Controls.Add(pnlSuperior);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRegistroTarifa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tarifa de Pollo";
            Load += FrmRegistroTarifa_Load;
            Shown += FrmRegistroTarifa_Shown;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            pnlPrecio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numPrecio).EndInit();
            pnlFechaInicio.ResumeLayout(false);
            pnlFechaFin.ResumeLayout(false);
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
        private Label labelPrecio;
        private Panel pnlPrecio;
        private NumericUpDown numPrecio;
        private Label labelFechaInicio;
        private Panel pnlFechaInicio;
        private DateTimePicker dtpFechaInicio;
        private Label labelFechaFin;
        private Panel pnlFechaFin;
        private DateTimePicker dtpFechaFin;
        private CheckBox chkSinFechaFin;
        private Label lblAyuda;
        private Panel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private ErrorProvider errorIcon;
    }
}
