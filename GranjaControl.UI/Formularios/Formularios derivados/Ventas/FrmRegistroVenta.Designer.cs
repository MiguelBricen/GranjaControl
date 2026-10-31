namespace GranjaControl.UI.Formularios.Formularios_derivados.Ventas
{
    partial class FrmRegistroVenta
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
            labelNumero = new Label();
            pnlNumero = new Panel();
            txtNumero = new TextBox();
            labelFecha = new Label();
            pnlFecha = new Panel();
            dtpFecha = new DateTimePicker();
            lblPrecioVigente = new Label();
            labelCliente = new Label();
            pnlCliente = new Panel();
            cboCliente = new ComboBox();
            lblEstadoVenta = new Label();
            labelObservacion = new Label();
            pnlObservacion = new Panel();
            txtObservacion = new TextBox();
            lblDetalles = new Label();
            lblDetalleDescripcion = new Label();
            labelLoteDetalle = new Label();
            pnlLoteDetalle = new Panel();
            cboLoteDetalle = new ComboBox();
            labelCantidadPollos = new Label();
            pnlCantidadPollos = new Panel();
            numCantidadPollos = new NumericUpDown();
            labelPesoLibras = new Label();
            pnlPesoLibras = new Panel();
            numPesoLibras = new NumericUpDown();
            labelPrecioDetalle = new Label();
            pnlPrecioDetalle = new Panel();
            lblPrecioDetalle = new Label();
            btnAgregarDetalle = new FontAwesome.Sharp.IconButton();
            btnActualizarDetalle = new FontAwesome.Sharp.IconButton();
            btnQuitarDetalle = new FontAwesome.Sharp.IconButton();
            dgvDetalles = new DataGridView();
            lblTotales = new Label();
            lblTotal = new Label();
            pnlAcciones = new Panel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            errorIcon = new ErrorProvider(components);
            pnlSuperior.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlNumero.SuspendLayout();
            pnlFecha.SuspendLayout();
            pnlCliente.SuspendLayout();
            pnlObservacion.SuspendLayout();
            pnlLoteDetalle.SuspendLayout();
            pnlCantidadPollos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidadPollos).BeginInit();
            pnlPesoLibras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPesoLibras).BeginInit();
            pnlPrecioDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).BeginInit();
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
            pnlSuperior.Size = new Size(1000, 64);
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
            lblDescripcion.Size = new Size(422, 23);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Registre el encabezado y los lotes incluidos en la venta.";
            // 
            // btnCerrar
            // 
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Image = Properties.Resources._02_cerrar1;
            btnCerrar.Location = new Point(956, 12);
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
            lblTitulo.Size = new Size(157, 31);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Nueva venta";
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
            pnlContenido.Controls.Add(labelNumero);
            pnlContenido.Controls.Add(pnlNumero);
            pnlContenido.Controls.Add(labelFecha);
            pnlContenido.Controls.Add(pnlFecha);
            pnlContenido.Controls.Add(lblPrecioVigente);
            pnlContenido.Controls.Add(labelCliente);
            pnlContenido.Controls.Add(pnlCliente);
            pnlContenido.Controls.Add(lblEstadoVenta);
            pnlContenido.Controls.Add(labelObservacion);
            pnlContenido.Controls.Add(pnlObservacion);
            pnlContenido.Controls.Add(lblDetalles);
            pnlContenido.Controls.Add(lblDetalleDescripcion);
            pnlContenido.Controls.Add(labelLoteDetalle);
            pnlContenido.Controls.Add(pnlLoteDetalle);
            pnlContenido.Controls.Add(labelCantidadPollos);
            pnlContenido.Controls.Add(pnlCantidadPollos);
            pnlContenido.Controls.Add(labelPesoLibras);
            pnlContenido.Controls.Add(pnlPesoLibras);
            pnlContenido.Controls.Add(labelPrecioDetalle);
            pnlContenido.Controls.Add(pnlPrecioDetalle);
            pnlContenido.Controls.Add(btnAgregarDetalle);
            pnlContenido.Controls.Add(btnActualizarDetalle);
            pnlContenido.Controls.Add(btnQuitarDetalle);
            pnlContenido.Controls.Add(dgvDetalles);
            pnlContenido.Controls.Add(lblTotales);
            pnlContenido.Controls.Add(lblTotal);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 64);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(1000, 601);
            pnlContenido.TabIndex = 1;
            // 
            // labelNumero
            // 
            labelNumero.AutoSize = true;
            labelNumero.BackColor = Color.Transparent;
            labelNumero.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNumero.ForeColor = Color.FromArgb(42, 52, 45);
            labelNumero.Location = new Point(24, 18);
            labelNumero.Name = "labelNumero";
            labelNumero.Size = new Size(98, 25);
            labelNumero.TabIndex = 0;
            labelNumero.Text = "N° de venta:";
            // 
            // pnlNumero
            // 
            pnlNumero.BackColor = Color.White;
            pnlNumero.Controls.Add(txtNumero);
            pnlNumero.Location = new Point(160, 10);
            pnlNumero.Name = "pnlNumero";
            pnlNumero.Size = new Size(250, 41);
            pnlNumero.TabIndex = 1;
            // 
            // txtNumero
            // 
            txtNumero.BackColor = Color.White;
            txtNumero.BorderStyle = BorderStyle.None;
            txtNumero.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNumero.ForeColor = Color.FromArgb(42, 52, 45);
            txtNumero.Location = new Point(8, 7);
            txtNumero.MaxLength = 50;
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(234, 25);
            txtNumero.TabIndex = 0;
            // 
            // labelFecha
            // 
            labelFecha.AutoSize = true;
            labelFecha.BackColor = Color.Transparent;
            labelFecha.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFecha.ForeColor = Color.FromArgb(42, 52, 45);
            labelFecha.Location = new Point(430, 18);
            labelFecha.Name = "labelFecha";
            labelFecha.Size = new Size(66, 25);
            labelFecha.TabIndex = 2;
            labelFecha.Text = "Fecha:";
            // 
            // pnlFecha
            // 
            pnlFecha.BackColor = Color.White;
            pnlFecha.Controls.Add(dtpFecha);
            pnlFecha.Location = new Point(500, 10);
            pnlFecha.Name = "pnlFecha";
            pnlFecha.Size = new Size(180, 41);
            pnlFecha.TabIndex = 3;
            // 
            // dtpFecha
            // 
            dtpFecha.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(8, 6);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(164, 31);
            dtpFecha.TabIndex = 0;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;
            // 
            // lblPrecioVigente
            // 
            lblPrecioVigente.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPrecioVigente.ForeColor = Color.FromArgb(23, 61, 43);
            lblPrecioVigente.Location = new Point(700, 10);
            lblPrecioVigente.Name = "lblPrecioVigente";
            lblPrecioVigente.Size = new Size(270, 41);
            lblPrecioVigente.TabIndex = 4;
            lblPrecioVigente.Text = "Precio vigente: --";
            lblPrecioVigente.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelCliente
            // 
            labelCliente.AutoSize = true;
            labelCliente.BackColor = Color.Transparent;
            labelCliente.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCliente.ForeColor = Color.FromArgb(42, 52, 45);
            labelCliente.Location = new Point(24, 74);
            labelCliente.Name = "labelCliente";
            labelCliente.Size = new Size(75, 25);
            labelCliente.TabIndex = 5;
            labelCliente.Text = "Cliente:";
            // 
            // pnlCliente
            // 
            pnlCliente.BackColor = Color.White;
            pnlCliente.Controls.Add(cboCliente);
            pnlCliente.Location = new Point(160, 66);
            pnlCliente.Name = "pnlCliente";
            pnlCliente.Size = new Size(520, 41);
            pnlCliente.TabIndex = 6;
            // 
            // cboCliente
            // 
            cboCliente.BackColor = Color.White;
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCliente.FlatStyle = FlatStyle.Flat;
            cboCliente.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboCliente.ForeColor = Color.FromArgb(42, 52, 45);
            cboCliente.FormattingEnabled = true;
            cboCliente.Location = new Point(8, 6);
            cboCliente.Name = "cboCliente";
            cboCliente.Size = new Size(504, 28);
            cboCliente.TabIndex = 0;
            // 
            // lblEstadoVenta
            // 
            lblEstadoVenta.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEstadoVenta.ForeColor = Color.FromArgb(23, 61, 43);
            lblEstadoVenta.Location = new Point(700, 66);
            lblEstadoVenta.Name = "lblEstadoVenta";
            lblEstadoVenta.Size = new Size(270, 41);
            lblEstadoVenta.TabIndex = 7;
            lblEstadoVenta.Text = "Estado: Registrada";
            lblEstadoVenta.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelObservacion
            // 
            labelObservacion.AutoSize = true;
            labelObservacion.BackColor = Color.Transparent;
            labelObservacion.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelObservacion.ForeColor = Color.FromArgb(42, 52, 45);
            labelObservacion.Location = new Point(24, 130);
            labelObservacion.Name = "labelObservacion";
            labelObservacion.Size = new Size(126, 25);
            labelObservacion.TabIndex = 8;
            labelObservacion.Text = "Observación:";
            // 
            // pnlObservacion
            // 
            pnlObservacion.BackColor = Color.White;
            pnlObservacion.Controls.Add(txtObservacion);
            pnlObservacion.Location = new Point(160, 122);
            pnlObservacion.Name = "pnlObservacion";
            pnlObservacion.Size = new Size(520, 70);
            pnlObservacion.TabIndex = 9;
            // 
            // txtObservacion
            // 
            txtObservacion.BackColor = Color.White;
            txtObservacion.BorderStyle = BorderStyle.None;
            txtObservacion.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtObservacion.ForeColor = Color.FromArgb(42, 52, 45);
            txtObservacion.Location = new Point(8, 8);
            txtObservacion.MaxLength = 500;
            txtObservacion.Multiline = true;
            txtObservacion.Name = "txtObservacion";
            txtObservacion.ScrollBars = ScrollBars.Vertical;
            txtObservacion.Size = new Size(504, 54);
            txtObservacion.TabIndex = 0;
            // 
            // lblDetalles
            // 
            lblDetalles.AutoSize = true;
            lblDetalles.BackColor = Color.Transparent;
            lblDetalles.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetalles.ForeColor = Color.Black;
            lblDetalles.Location = new Point(24, 208);
            lblDetalles.Name = "lblDetalles";
            lblDetalles.Size = new Size(231, 31);
            lblDetalles.TabIndex = 10;
            lblDetalles.Text = "Detalles de la venta";
            // 
            // lblDetalleDescripcion
            // 
            lblDetalleDescripcion.AutoSize = true;
            lblDetalleDescripcion.BackColor = Color.Transparent;
            lblDetalleDescripcion.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetalleDescripcion.ForeColor = SystemColors.ControlDarkDark;
            lblDetalleDescripcion.Location = new Point(25, 239);
            lblDetalleDescripcion.Name = "lblDetalleDescripcion";
            lblDetalleDescripcion.Size = new Size(515, 21);
            lblDetalleDescripcion.TabIndex = 11;
            lblDetalleDescripcion.Text = "Seleccione un lote, registre la cantidad de pollos y el peso total.";
            // 
            // labelLoteDetalle
            // 
            labelLoteDetalle.AutoSize = true;
            labelLoteDetalle.BackColor = Color.Transparent;
            labelLoteDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelLoteDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            labelLoteDetalle.Location = new Point(24, 268);
            labelLoteDetalle.Name = "labelLoteDetalle";
            labelLoteDetalle.Size = new Size(40, 20);
            labelLoteDetalle.TabIndex = 12;
            labelLoteDetalle.Text = "Lote";
            // 
            // pnlLoteDetalle
            // 
            pnlLoteDetalle.BackColor = Color.White;
            pnlLoteDetalle.Controls.Add(cboLoteDetalle);
            pnlLoteDetalle.Location = new Point(24, 292);
            pnlLoteDetalle.Name = "pnlLoteDetalle";
            pnlLoteDetalle.Size = new Size(290, 41);
            pnlLoteDetalle.TabIndex = 13;
            // 
            // cboLoteDetalle
            // 
            cboLoteDetalle.BackColor = Color.White;
            cboLoteDetalle.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoteDetalle.FlatStyle = FlatStyle.Flat;
            cboLoteDetalle.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboLoteDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            cboLoteDetalle.FormattingEnabled = true;
            cboLoteDetalle.Location = new Point(8, 6);
            cboLoteDetalle.Name = "cboLoteDetalle";
            cboLoteDetalle.Size = new Size(274, 28);
            cboLoteDetalle.TabIndex = 0;
            // 
            // labelCantidadPollos
            // 
            labelCantidadPollos.AutoSize = true;
            labelCantidadPollos.BackColor = Color.Transparent;
            labelCantidadPollos.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCantidadPollos.ForeColor = Color.FromArgb(42, 52, 45);
            labelCantidadPollos.Location = new Point(330, 268);
            labelCantidadPollos.Name = "labelCantidadPollos";
            labelCantidadPollos.Size = new Size(75, 20);
            labelCantidadPollos.TabIndex = 14;
            labelCantidadPollos.Text = "Pollos";
            // 
            // pnlCantidadPollos
            // 
            pnlCantidadPollos.BackColor = Color.White;
            pnlCantidadPollos.Controls.Add(numCantidadPollos);
            pnlCantidadPollos.Location = new Point(330, 292);
            pnlCantidadPollos.Name = "pnlCantidadPollos";
            pnlCantidadPollos.Size = new Size(110, 41);
            pnlCantidadPollos.TabIndex = 15;
            // 
            // numCantidadPollos
            // 
            numCantidadPollos.BackColor = Color.White;
            numCantidadPollos.BorderStyle = BorderStyle.None;
            numCantidadPollos.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numCantidadPollos.ForeColor = Color.FromArgb(42, 52, 45);
            numCantidadPollos.Location = new Point(8, 7);
            numCantidadPollos.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCantidadPollos.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidadPollos.Name = "numCantidadPollos";
            numCantidadPollos.Size = new Size(94, 26);
            numCantidadPollos.TabIndex = 0;
            numCantidadPollos.TextAlign = HorizontalAlignment.Right;
            numCantidadPollos.ThousandsSeparator = true;
            numCantidadPollos.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelPesoLibras
            // 
            labelPesoLibras.AutoSize = true;
            labelPesoLibras.BackColor = Color.Transparent;
            labelPesoLibras.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPesoLibras.ForeColor = Color.FromArgb(42, 52, 45);
            labelPesoLibras.Location = new Point(456, 268);
            labelPesoLibras.Name = "labelPesoLibras";
            labelPesoLibras.Size = new Size(80, 20);
            labelPesoLibras.TabIndex = 16;
            labelPesoLibras.Text = "Libras";
            // 
            // pnlPesoLibras
            // 
            pnlPesoLibras.BackColor = Color.White;
            pnlPesoLibras.Controls.Add(numPesoLibras);
            pnlPesoLibras.Location = new Point(456, 292);
            pnlPesoLibras.Name = "pnlPesoLibras";
            pnlPesoLibras.Size = new Size(120, 41);
            pnlPesoLibras.TabIndex = 17;
            // 
            // numPesoLibras
            // 
            numPesoLibras.BackColor = Color.White;
            numPesoLibras.BorderStyle = BorderStyle.None;
            numPesoLibras.DecimalPlaces = 2;
            numPesoLibras.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numPesoLibras.ForeColor = Color.FromArgb(42, 52, 45);
            numPesoLibras.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numPesoLibras.Location = new Point(8, 7);
            numPesoLibras.Maximum = new decimal(new int[] { 99999999, 0, 0, 131072 });
            numPesoLibras.Name = "numPesoLibras";
            numPesoLibras.Size = new Size(104, 26);
            numPesoLibras.TabIndex = 0;
            numPesoLibras.TextAlign = HorizontalAlignment.Right;
            numPesoLibras.ThousandsSeparator = true;
            // 
            // labelPrecioDetalle
            // 
            labelPrecioDetalle.AutoSize = true;
            labelPrecioDetalle.BackColor = Color.Transparent;
            labelPrecioDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPrecioDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            labelPrecioDetalle.Location = new Point(592, 268);
            labelPrecioDetalle.Name = "labelPrecioDetalle";
            labelPrecioDetalle.Size = new Size(113, 20);
            labelPrecioDetalle.TabIndex = 18;
            labelPrecioDetalle.Text = "Precio por libra";
            // 
            // pnlPrecioDetalle
            // 
            pnlPrecioDetalle.BackColor = Color.White;
            pnlPrecioDetalle.Controls.Add(lblPrecioDetalle);
            pnlPrecioDetalle.Location = new Point(592, 292);
            pnlPrecioDetalle.Name = "pnlPrecioDetalle";
            pnlPrecioDetalle.Size = new Size(130, 41);
            pnlPrecioDetalle.TabIndex = 19;
            // 
            // lblPrecioDetalle
            // 
            lblPrecioDetalle.Dock = DockStyle.Fill;
            lblPrecioDetalle.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPrecioDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            lblPrecioDetalle.Location = new Point(0, 0);
            lblPrecioDetalle.Name = "lblPrecioDetalle";
            lblPrecioDetalle.Size = new Size(130, 41);
            lblPrecioDetalle.TabIndex = 0;
            lblPrecioDetalle.Text = "0.00";
            lblPrecioDetalle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnAgregarDetalle
            // 
            btnAgregarDetalle.BackColor = Color.White;
            btnAgregarDetalle.Cursor = Cursors.Hand;
            btnAgregarDetalle.FlatAppearance.BorderSize = 0;
            btnAgregarDetalle.FlatStyle = FlatStyle.Flat;
            btnAgregarDetalle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregarDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            btnAgregarDetalle.IconChar = FontAwesome.Sharp.IconChar.Plus;
            btnAgregarDetalle.IconColor = Color.FromArgb(23, 61, 43);
            btnAgregarDetalle.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAgregarDetalle.IconSize = 22;
            btnAgregarDetalle.Location = new Point(790, 292);
            btnAgregarDetalle.Name = "btnAgregarDetalle";
            btnAgregarDetalle.Size = new Size(170, 41);
            btnAgregarDetalle.TabIndex = 20;
            btnAgregarDetalle.Text = "Agregar detalle";
            btnAgregarDetalle.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregarDetalle.UseVisualStyleBackColor = false;
            btnAgregarDetalle.Click += btnAgregarDetalle_Click;
            // 
            // btnActualizarDetalle
            // 
            btnActualizarDetalle.BackColor = Color.White;
            btnActualizarDetalle.Cursor = Cursors.Hand;
            btnActualizarDetalle.Enabled = false;
            btnActualizarDetalle.FlatAppearance.BorderSize = 0;
            btnActualizarDetalle.FlatStyle = FlatStyle.Flat;
            btnActualizarDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnActualizarDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            btnActualizarDetalle.IconChar = FontAwesome.Sharp.IconChar.Pen;
            btnActualizarDetalle.IconColor = Color.FromArgb(36, 83, 116);
            btnActualizarDetalle.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnActualizarDetalle.IconSize = 18;
            btnActualizarDetalle.Location = new Point(790, 340);
            btnActualizarDetalle.Name = "btnActualizarDetalle";
            btnActualizarDetalle.Size = new Size(82, 36);
            btnActualizarDetalle.TabIndex = 21;
            btnActualizarDetalle.Text = "Editar";
            btnActualizarDetalle.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnActualizarDetalle.UseVisualStyleBackColor = false;
            btnActualizarDetalle.Click += btnActualizarDetalle_Click;
            // 
            // btnQuitarDetalle
            // 
            btnQuitarDetalle.BackColor = Color.White;
            btnQuitarDetalle.Cursor = Cursors.Hand;
            btnQuitarDetalle.Enabled = false;
            btnQuitarDetalle.FlatAppearance.BorderSize = 0;
            btnQuitarDetalle.FlatStyle = FlatStyle.Flat;
            btnQuitarDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuitarDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            btnQuitarDetalle.IconChar = FontAwesome.Sharp.IconChar.Trash;
            btnQuitarDetalle.IconColor = Color.Red;
            btnQuitarDetalle.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnQuitarDetalle.IconSize = 18;
            btnQuitarDetalle.Location = new Point(878, 340);
            btnQuitarDetalle.Name = "btnQuitarDetalle";
            btnQuitarDetalle.Size = new Size(82, 36);
            btnQuitarDetalle.TabIndex = 22;
            btnQuitarDetalle.Text = "Quitar";
            btnQuitarDetalle.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnQuitarDetalle.UseVisualStyleBackColor = false;
            btnQuitarDetalle.Click += btnQuitarDetalle_Click;
            // 
            // dgvDetalles
            // 
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToDeleteRows = false;
            dgvDetalles.AllowUserToResizeRows = false;
            dgvDetalles.BackgroundColor = Color.White;
            dgvDetalles.BorderStyle = BorderStyle.None;
            dgvDetalles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDetalles.Location = new Point(24, 392);
            dgvDetalles.MultiSelect = false;
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.ReadOnly = true;
            dgvDetalles.RowHeadersVisible = false;
            dgvDetalles.RowTemplate.Height = 32;
            dgvDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalles.Size = new Size(936, 146);
            dgvDetalles.TabIndex = 23;
            dgvDetalles.CellFormatting += dgvDetalles_CellFormatting;
            dgvDetalles.SelectionChanged += dgvDetalles_SelectionChanged;
            // 
            // lblTotales
            // 
            lblTotales.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotales.ForeColor = Color.FromArgb(42, 52, 45);
            lblTotales.Location = new Point(24, 548);
            lblTotales.Name = "lblTotales";
            lblTotales.Size = new Size(620, 28);
            lblTotales.TabIndex = 24;
            lblTotales.Text = "Pollos: 0    Libras: 0.00";
            lblTotales.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotal
            // 
            lblTotal.BackColor = Color.Transparent;
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = Color.FromArgb(23, 61, 43);
            lblTotal.Location = new Point(650, 548);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(310, 28);
            lblTotal.TabIndex = 25;
            lblTotal.Text = "Total: 0.00";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlAcciones
            // 
            pnlAcciones.BackColor = Color.FromArgb(226, 239, 228);
            pnlAcciones.Controls.Add(btnCancelar);
            pnlAcciones.Controls.Add(btnGuardar);
            pnlAcciones.Dock = DockStyle.Bottom;
            pnlAcciones.Location = new Point(0, 665);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(1000, 55);
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
            btnCancelar.Location = new Point(830, 6);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(150, 43);
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
            btnGuardar.Location = new Point(670, 6);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(150, 43);
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
            // FrmRegistroVenta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 239, 228);
            CancelButton = btnCancelar;
            ClientSize = new Size(1000, 720);
            Controls.Add(pnlContenido);
            Controls.Add(pnlAcciones);
            Controls.Add(pnlSuperior);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRegistroVenta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmRegistroVenta";
            Load += FrmRegistroVenta_Load;
            Shown += FrmRegistroVenta_Shown;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            pnlNumero.ResumeLayout(false);
            pnlNumero.PerformLayout();
            pnlFecha.ResumeLayout(false);
            pnlCliente.ResumeLayout(false);
            pnlObservacion.ResumeLayout(false);
            pnlObservacion.PerformLayout();
            pnlLoteDetalle.ResumeLayout(false);
            pnlCantidadPollos.ResumeLayout(false);
            pnlCantidadPollos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidadPollos).EndInit();
            pnlPesoLibras.ResumeLayout(false);
            pnlPesoLibras.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPesoLibras).EndInit();
            pnlPrecioDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).EndInit();
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
        private Label labelNumero;
        private Panel pnlNumero;
        private TextBox txtNumero;
        private Label labelFecha;
        private Panel pnlFecha;
        private DateTimePicker dtpFecha;
        private Label lblPrecioVigente;
        private Label labelCliente;
        private Panel pnlCliente;
        private ComboBox cboCliente;
        private Label lblEstadoVenta;
        private Label labelObservacion;
        private Panel pnlObservacion;
        private TextBox txtObservacion;
        private Label lblDetalles;
        private Label lblDetalleDescripcion;
        private Label labelLoteDetalle;
        private Panel pnlLoteDetalle;
        private ComboBox cboLoteDetalle;
        private Label labelCantidadPollos;
        private Panel pnlCantidadPollos;
        private NumericUpDown numCantidadPollos;
        private Label labelPesoLibras;
        private Panel pnlPesoLibras;
        private NumericUpDown numPesoLibras;
        private Label labelPrecioDetalle;
        private Panel pnlPrecioDetalle;
        private Label lblPrecioDetalle;
        private FontAwesome.Sharp.IconButton btnAgregarDetalle;
        private FontAwesome.Sharp.IconButton btnActualizarDetalle;
        private FontAwesome.Sharp.IconButton btnQuitarDetalle;
        private DataGridView dgvDetalles;
        private Label lblTotales;
        private Label lblTotal;
        private Panel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private ErrorProvider errorIcon;
    }
}
