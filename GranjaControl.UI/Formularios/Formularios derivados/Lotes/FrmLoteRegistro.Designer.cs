namespace GranjaControl.UI.Formularios.Formularios_derivados.Lotes
{
    partial class FrmLoteRegistro
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
            labelObservacion = new Label();
            pnlObservacion = new Panel();
            txtObservacion = new TextBox();
            labelCosto = new Label();
            pnlCosto = new Panel();
            numCostoPorPollito = new NumericUpDown();
            labelCantidad = new Label();
            pnlCantidad = new Panel();
            numCantidadInicial = new NumericUpDown();
            labelFecha = new Label();
            pnlFecha = new Panel();
            dtpFechaIngreso = new DateTimePicker();
            labelProveedor = new Label();
            pnlProveedor = new Panel();
            cboProveedor = new ComboBox();
            labelGalpon = new Label();
            pnlGalpon = new Panel();
            cboGalpon = new ComboBox();
            labelCodigo = new Label();
            pnlCodigo = new Panel();
            txtCodigo = new TextBox();
            pnlAcciones = new Panel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            errorIcon = new ErrorProvider(components);
            pnlSuperior.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlObservacion.SuspendLayout();
            pnlCosto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCostoPorPollito).BeginInit();
            pnlCantidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidadInicial).BeginInit();
            pnlFecha.SuspendLayout();
            pnlProveedor.SuspendLayout();
            pnlGalpon.SuspendLayout();
            pnlCodigo.SuspendLayout();
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
            pnlSuperior.Size = new Size(640, 64);
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
            lblDescripcion.Size = new Size(254, 23);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Complete la información del lote.";
            // 
            // btnCerrar
            // 
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Image = Properties.Resources._02_cerrar1;
            btnCerrar.Location = new Point(596, 12);
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
            lblTitulo.Size = new Size(150, 31);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Nuevo lote";
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
            pnlContenido.Controls.Add(labelObservacion);
            pnlContenido.Controls.Add(pnlObservacion);
            pnlContenido.Controls.Add(labelCosto);
            pnlContenido.Controls.Add(pnlCosto);
            pnlContenido.Controls.Add(labelCantidad);
            pnlContenido.Controls.Add(pnlCantidad);
            pnlContenido.Controls.Add(labelFecha);
            pnlContenido.Controls.Add(pnlFecha);
            pnlContenido.Controls.Add(labelProveedor);
            pnlContenido.Controls.Add(pnlProveedor);
            pnlContenido.Controls.Add(labelGalpon);
            pnlContenido.Controls.Add(pnlGalpon);
            pnlContenido.Controls.Add(labelCodigo);
            pnlContenido.Controls.Add(pnlCodigo);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 64);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(640, 481);
            pnlContenido.TabIndex = 1;
            // 
            // labelObservacion
            // 
            labelObservacion.AutoSize = true;
            labelObservacion.BackColor = Color.Transparent;
            labelObservacion.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelObservacion.ForeColor = Color.FromArgb(42, 52, 45);
            labelObservacion.Location = new Point(24, 368);
            labelObservacion.Name = "labelObservacion";
            labelObservacion.Size = new Size(120, 25);
            labelObservacion.TabIndex = 13;
            labelObservacion.Text = "Observación:";
            // 
            // pnlObservacion
            // 
            pnlObservacion.BackColor = Color.White;
            pnlObservacion.Controls.Add(txtObservacion);
            pnlObservacion.Location = new Point(165, 366);
            pnlObservacion.Name = "pnlObservacion";
            pnlObservacion.Size = new Size(430, 77);
            pnlObservacion.TabIndex = 12;
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
            txtObservacion.Size = new Size(412, 60);
            txtObservacion.TabIndex = 0;
            // 
            // labelCosto
            // 
            labelCosto.AutoSize = true;
            labelCosto.BackColor = Color.Transparent;
            labelCosto.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCosto.ForeColor = Color.FromArgb(42, 52, 45);
            labelCosto.Location = new Point(24, 310);
            labelCosto.Name = "labelCosto";
            labelCosto.Size = new Size(127, 25);
            labelCosto.TabIndex = 11;
            labelCosto.Text = "Costo unitario:";
            // 
            // pnlCosto
            // 
            pnlCosto.BackColor = Color.White;
            pnlCosto.Controls.Add(numCostoPorPollito);
            pnlCosto.Location = new Point(165, 308);
            pnlCosto.Name = "pnlCosto";
            pnlCosto.Size = new Size(430, 41);
            pnlCosto.TabIndex = 10;
            // 
            // numCostoPorPollito
            // 
            numCostoPorPollito.BackColor = Color.White;
            numCostoPorPollito.BorderStyle = BorderStyle.None;
            numCostoPorPollito.DecimalPlaces = 2;
            numCostoPorPollito.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numCostoPorPollito.ForeColor = Color.FromArgb(42, 52, 45);
            numCostoPorPollito.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numCostoPorPollito.Location = new Point(8, 8);
            numCostoPorPollito.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            numCostoPorPollito.Name = "numCostoPorPollito";
            numCostoPorPollito.Size = new Size(180, 27);
            numCostoPorPollito.TabIndex = 0;
            numCostoPorPollito.ThousandsSeparator = true;
            // 
            // labelCantidad
            // 
            labelCantidad.AutoSize = true;
            labelCantidad.BackColor = Color.Transparent;
            labelCantidad.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCantidad.ForeColor = Color.FromArgb(42, 52, 45);
            labelCantidad.Location = new Point(24, 252);
            labelCantidad.Name = "labelCantidad";
            labelCantidad.Size = new Size(91, 25);
            labelCantidad.TabIndex = 9;
            labelCantidad.Text = "Cantidad:";
            // 
            // pnlCantidad
            // 
            pnlCantidad.BackColor = Color.White;
            pnlCantidad.Controls.Add(numCantidadInicial);
            pnlCantidad.Location = new Point(165, 250);
            pnlCantidad.Name = "pnlCantidad";
            pnlCantidad.Size = new Size(430, 41);
            pnlCantidad.TabIndex = 8;
            // 
            // numCantidadInicial
            // 
            numCantidadInicial.BackColor = Color.White;
            numCantidadInicial.BorderStyle = BorderStyle.None;
            numCantidadInicial.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numCantidadInicial.ForeColor = Color.FromArgb(42, 52, 45);
            numCantidadInicial.Location = new Point(8, 8);
            numCantidadInicial.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            numCantidadInicial.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidadInicial.Name = "numCantidadInicial";
            numCantidadInicial.Size = new Size(180, 27);
            numCantidadInicial.TabIndex = 0;
            numCantidadInicial.ThousandsSeparator = true;
            numCantidadInicial.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelFecha
            // 
            labelFecha.AutoSize = true;
            labelFecha.BackColor = Color.Transparent;
            labelFecha.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFecha.ForeColor = Color.FromArgb(42, 52, 45);
            labelFecha.Location = new Point(24, 194);
            labelFecha.Name = "labelFecha";
            labelFecha.Size = new Size(90, 25);
            labelFecha.TabIndex = 7;
            labelFecha.Text = "Ingreso:";
            // 
            // pnlFecha
            // 
            pnlFecha.BackColor = Color.White;
            pnlFecha.Controls.Add(dtpFechaIngreso);
            pnlFecha.Location = new Point(165, 192);
            pnlFecha.Name = "pnlFecha";
            pnlFecha.Size = new Size(430, 41);
            pnlFecha.TabIndex = 6;
            // 
            // dtpFechaIngreso
            // 
            dtpFechaIngreso.CalendarFont = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFechaIngreso.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFechaIngreso.Format = DateTimePickerFormat.Short;
            dtpFechaIngreso.Location = new Point(8, 6);
            dtpFechaIngreso.Name = "dtpFechaIngreso";
            dtpFechaIngreso.Size = new Size(200, 31);
            dtpFechaIngreso.TabIndex = 0;
            // 
            // labelProveedor
            // 
            labelProveedor.AutoSize = true;
            labelProveedor.BackColor = Color.Transparent;
            labelProveedor.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelProveedor.ForeColor = Color.FromArgb(42, 52, 45);
            labelProveedor.Location = new Point(24, 136);
            labelProveedor.Name = "labelProveedor";
            labelProveedor.Size = new Size(99, 25);
            labelProveedor.TabIndex = 5;
            labelProveedor.Text = "Proveedor:";
            // 
            // pnlProveedor
            // 
            pnlProveedor.BackColor = Color.White;
            pnlProveedor.Controls.Add(cboProveedor);
            pnlProveedor.Location = new Point(165, 134);
            pnlProveedor.Name = "pnlProveedor";
            pnlProveedor.Size = new Size(430, 41);
            pnlProveedor.TabIndex = 4;
            // 
            // cboProveedor
            // 
            cboProveedor.BackColor = Color.White;
            cboProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProveedor.FlatStyle = FlatStyle.Flat;
            cboProveedor.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboProveedor.ForeColor = Color.FromArgb(42, 52, 45);
            cboProveedor.FormattingEnabled = true;
            cboProveedor.Location = new Point(8, 6);
            cboProveedor.Name = "cboProveedor";
            cboProveedor.Size = new Size(412, 28);
            cboProveedor.TabIndex = 0;
            // 
            // labelGalpon
            // 
            labelGalpon.AutoSize = true;
            labelGalpon.BackColor = Color.Transparent;
            labelGalpon.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelGalpon.ForeColor = Color.FromArgb(42, 52, 45);
            labelGalpon.Location = new Point(24, 78);
            labelGalpon.Name = "labelGalpon";
            labelGalpon.Size = new Size(76, 25);
            labelGalpon.TabIndex = 3;
            labelGalpon.Text = "Galpón:";
            // 
            // pnlGalpon
            // 
            pnlGalpon.BackColor = Color.White;
            pnlGalpon.Controls.Add(cboGalpon);
            pnlGalpon.Location = new Point(165, 76);
            pnlGalpon.Name = "pnlGalpon";
            pnlGalpon.Size = new Size(430, 41);
            pnlGalpon.TabIndex = 2;
            // 
            // cboGalpon
            // 
            cboGalpon.BackColor = Color.White;
            cboGalpon.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGalpon.FlatStyle = FlatStyle.Flat;
            cboGalpon.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboGalpon.ForeColor = Color.FromArgb(42, 52, 45);
            cboGalpon.FormattingEnabled = true;
            cboGalpon.Location = new Point(8, 6);
            cboGalpon.Name = "cboGalpon";
            cboGalpon.Size = new Size(412, 28);
            cboGalpon.TabIndex = 0;
            // 
            // labelCodigo
            // 
            labelCodigo.AutoSize = true;
            labelCodigo.BackColor = Color.Transparent;
            labelCodigo.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCodigo.ForeColor = Color.FromArgb(42, 52, 45);
            labelCodigo.Location = new Point(24, 20);
            labelCodigo.Name = "labelCodigo";
            labelCodigo.Size = new Size(77, 25);
            labelCodigo.TabIndex = 1;
            labelCodigo.Text = "Código:";
            // 
            // pnlCodigo
            // 
            pnlCodigo.BackColor = Color.White;
            pnlCodigo.Controls.Add(txtCodigo);
            pnlCodigo.Location = new Point(165, 18);
            pnlCodigo.Name = "pnlCodigo";
            pnlCodigo.Size = new Size(430, 41);
            pnlCodigo.TabIndex = 0;
            // 
            // txtCodigo
            // 
            txtCodigo.BackColor = Color.White;
            txtCodigo.BorderStyle = BorderStyle.None;
            txtCodigo.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCodigo.ForeColor = Color.FromArgb(42, 52, 45);
            txtCodigo.Location = new Point(8, 8);
            txtCodigo.MaxLength = 25;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(412, 24);
            txtCodigo.TabIndex = 0;
            // 
            // pnlAcciones
            // 
            pnlAcciones.BackColor = Color.FromArgb(226, 239, 228);
            pnlAcciones.Controls.Add(btnCancelar);
            pnlAcciones.Controls.Add(btnGuardar);
            pnlAcciones.Dock = DockStyle.Bottom;
            pnlAcciones.Location = new Point(0, 545);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(640, 55);
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
            btnCancelar.Location = new Point(340, 6);
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
            btnGuardar.Location = new Point(178, 6);
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
            // FrmLoteRegistro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 239, 228);
            ClientSize = new Size(640, 600);
            Controls.Add(pnlContenido);
            Controls.Add(pnlAcciones);
            Controls.Add(pnlSuperior);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLoteRegistro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmLoteRegistro";
            Load += FrmLoteRegistro_Load;
            Shown += FrmLoteRegistro_Shown;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            pnlObservacion.ResumeLayout(false);
            pnlObservacion.PerformLayout();
            pnlCosto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numCostoPorPollito).EndInit();
            pnlCantidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numCantidadInicial).EndInit();
            pnlFecha.ResumeLayout(false);
            pnlProveedor.ResumeLayout(false);
            pnlGalpon.ResumeLayout(false);
            pnlCodigo.ResumeLayout(false);
            pnlCodigo.PerformLayout();
            pnlAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSuperior;
        private Button btnCerrar;
        private Label lblTitulo;
        private Label lblDescripcion;
        private FontAwesome.Sharp.IconButton iconIcono;
        private Panel pnlContenido;
        private Label labelCodigo;
        private Panel pnlCodigo;
        private TextBox txtCodigo;
        private Label labelGalpon;
        private Panel pnlGalpon;
        private ComboBox cboGalpon;
        private Label labelProveedor;
        private Panel pnlProveedor;
        private ComboBox cboProveedor;
        private Label labelFecha;
        private Panel pnlFecha;
        private DateTimePicker dtpFechaIngreso;
        private Label labelCantidad;
        private Panel pnlCantidad;
        private NumericUpDown numCantidadInicial;
        private Label labelCosto;
        private Panel pnlCosto;
        private NumericUpDown numCostoPorPollito;
        private Label labelObservacion;
        private Panel pnlObservacion;
        private TextBox txtObservacion;
        private Panel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private ErrorProvider errorIcon;
    }
}
