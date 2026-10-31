namespace GranjaControl.UI.Formularios.Formularios_derivados.Compras
{
    partial class FrmRegistroCompraAlimento
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
            labelProveedor = new Label();
            pnlProveedor = new Panel();
            cboProveedor = new ComboBox();
            labelObservacion = new Label();
            pnlObservacion = new Panel();
            txtObservacion = new TextBox();
            lblDetalles = new Label();
            lblDetalleDescripcion = new Label();
            labelAlimentoDetalle = new Label();
            pnlAlimentoDetalle = new Panel();
            cboAlimentoDetalle = new ComboBox();
            labelCantidadDetalle = new Label();
            pnlCantidadDetalle = new Panel();
            numCantidadDetalle = new NumericUpDown();
            labelUnidadDetalle = new Label();
            pnlUnidadDetalle = new Panel();
            cboUnidadDetalle = new ComboBox();
            labelCostoDetalle = new Label();
            pnlCostoDetalle = new Panel();
            numCostoDetalle = new NumericUpDown();
            btnAgregarDetalle = new FontAwesome.Sharp.IconButton();
            btnActualizarDetalle = new FontAwesome.Sharp.IconButton();
            btnQuitarDetalle = new FontAwesome.Sharp.IconButton();
            dgvDetalles = new DataGridView();
            lblTotal = new Label();
            pnlAcciones = new Panel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            errorIcon = new ErrorProvider(components);
            pnlSuperior.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlNumero.SuspendLayout();
            pnlFecha.SuspendLayout();
            pnlProveedor.SuspendLayout();
            pnlObservacion.SuspendLayout();
            pnlAlimentoDetalle.SuspendLayout();
            pnlCantidadDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidadDetalle).BeginInit();
            pnlUnidadDetalle.SuspendLayout();
            pnlCostoDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCostoDetalle).BeginInit();
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
            lblDescripcion.Size = new Size(407, 23);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Registre el encabezado y los alimentos de la compra.";
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
            lblTitulo.Size = new Size(276, 31);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Nueva compra de alimentos";
            // 
            // iconIcono
            // 
            iconIcono.IconChar = FontAwesome.Sharp.IconChar.CartShopping;
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
            pnlContenido.Controls.Add(labelProveedor);
            pnlContenido.Controls.Add(pnlProveedor);
            pnlContenido.Controls.Add(labelObservacion);
            pnlContenido.Controls.Add(pnlObservacion);
            pnlContenido.Controls.Add(lblDetalles);
            pnlContenido.Controls.Add(lblDetalleDescripcion);
            pnlContenido.Controls.Add(labelAlimentoDetalle);
            pnlContenido.Controls.Add(pnlAlimentoDetalle);
            pnlContenido.Controls.Add(labelCantidadDetalle);
            pnlContenido.Controls.Add(pnlCantidadDetalle);
            pnlContenido.Controls.Add(labelUnidadDetalle);
            pnlContenido.Controls.Add(pnlUnidadDetalle);
            pnlContenido.Controls.Add(labelCostoDetalle);
            pnlContenido.Controls.Add(pnlCostoDetalle);
            pnlContenido.Controls.Add(btnAgregarDetalle);
            pnlContenido.Controls.Add(btnActualizarDetalle);
            pnlContenido.Controls.Add(btnQuitarDetalle);
            pnlContenido.Controls.Add(dgvDetalles);
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
            labelNumero.Size = new Size(118, 25);
            labelNumero.TabIndex = 0;
            labelNumero.Text = "N° de compra:";
            // 
            // pnlNumero
            // 
            pnlNumero.BackColor = Color.White;
            pnlNumero.Controls.Add(txtNumero);
            pnlNumero.Location = new Point(190, 10);
            pnlNumero.Name = "pnlNumero";
            pnlNumero.Size = new Size(300, 41);
            pnlNumero.TabIndex = 1;
            // 
            // txtNumero
            // 
            txtNumero.BackColor = Color.White;
            txtNumero.BorderStyle = BorderStyle.None;
            txtNumero.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNumero.ForeColor = Color.FromArgb(42, 52, 45);
            txtNumero.Location = new Point(8, 7);
            txtNumero.MaxLength = 25;
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(284, 25);
            txtNumero.TabIndex = 0;
            // 
            // labelFecha
            // 
            labelFecha.AutoSize = true;
            labelFecha.BackColor = Color.Transparent;
            labelFecha.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFecha.ForeColor = Color.FromArgb(42, 52, 45);
            labelFecha.Location = new Point(520, 18);
            labelFecha.Name = "labelFecha";
            labelFecha.Size = new Size(66, 25);
            labelFecha.TabIndex = 2;
            labelFecha.Text = "Fecha:";
            // 
            // pnlFecha
            // 
            pnlFecha.BackColor = Color.White;
            pnlFecha.Controls.Add(dtpFecha);
            pnlFecha.Location = new Point(590, 10);
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
            // 
            // labelProveedor
            // 
            labelProveedor.AutoSize = true;
            labelProveedor.BackColor = Color.Transparent;
            labelProveedor.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelProveedor.ForeColor = Color.FromArgb(42, 52, 45);
            labelProveedor.Location = new Point(24, 74);
            labelProveedor.Name = "labelProveedor";
            labelProveedor.Size = new Size(102, 25);
            labelProveedor.TabIndex = 4;
            labelProveedor.Text = "Proveedor:";
            // 
            // pnlProveedor
            // 
            pnlProveedor.BackColor = Color.White;
            pnlProveedor.Controls.Add(cboProveedor);
            pnlProveedor.Location = new Point(190, 66);
            pnlProveedor.Name = "pnlProveedor";
            pnlProveedor.Size = new Size(580, 41);
            pnlProveedor.TabIndex = 5;
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
            cboProveedor.Size = new Size(564, 28);
            cboProveedor.TabIndex = 0;
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
            labelObservacion.TabIndex = 6;
            labelObservacion.Text = "Observación:";
            // 
            // pnlObservacion
            // 
            pnlObservacion.BackColor = Color.White;
            pnlObservacion.Controls.Add(txtObservacion);
            pnlObservacion.Location = new Point(190, 122);
            pnlObservacion.Name = "pnlObservacion";
            pnlObservacion.Size = new Size(580, 70);
            pnlObservacion.TabIndex = 7;
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
            txtObservacion.Size = new Size(564, 54);
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
            lblDetalles.Size = new Size(255, 31);
            lblDetalles.TabIndex = 8;
            lblDetalles.Text = "Detalles de la compra";
            // 
            // lblDetalleDescripcion
            // 
            lblDetalleDescripcion.AutoSize = true;
            lblDetalleDescripcion.BackColor = Color.Transparent;
            lblDetalleDescripcion.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetalleDescripcion.ForeColor = SystemColors.ControlDarkDark;
            lblDetalleDescripcion.Location = new Point(25, 239);
            lblDetalleDescripcion.Name = "lblDetalleDescripcion";
            lblDetalleDescripcion.Size = new Size(376, 21);
            lblDetalleDescripcion.TabIndex = 9;
            lblDetalleDescripcion.Text = "Agregue uno o más alimentos y luego guarde la compra.";
            // 
            // labelAlimentoDetalle
            // 
            labelAlimentoDetalle.AutoSize = true;
            labelAlimentoDetalle.BackColor = Color.Transparent;
            labelAlimentoDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelAlimentoDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            labelAlimentoDetalle.Location = new Point(24, 268);
            labelAlimentoDetalle.Name = "labelAlimentoDetalle";
            labelAlimentoDetalle.Size = new Size(69, 20);
            labelAlimentoDetalle.TabIndex = 10;
            labelAlimentoDetalle.Text = "Alimento";
            // 
            // pnlAlimentoDetalle
            // 
            pnlAlimentoDetalle.BackColor = Color.White;
            pnlAlimentoDetalle.Controls.Add(cboAlimentoDetalle);
            pnlAlimentoDetalle.Location = new Point(24, 292);
            pnlAlimentoDetalle.Name = "pnlAlimentoDetalle";
            pnlAlimentoDetalle.Size = new Size(310, 41);
            pnlAlimentoDetalle.TabIndex = 11;
            // 
            // cboAlimentoDetalle
            // 
            cboAlimentoDetalle.BackColor = Color.White;
            cboAlimentoDetalle.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAlimentoDetalle.FlatStyle = FlatStyle.Flat;
            cboAlimentoDetalle.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboAlimentoDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            cboAlimentoDetalle.FormattingEnabled = true;
            cboAlimentoDetalle.Location = new Point(8, 6);
            cboAlimentoDetalle.Name = "cboAlimentoDetalle";
            cboAlimentoDetalle.Size = new Size(294, 28);
            cboAlimentoDetalle.TabIndex = 0;
            // 
            // labelCantidadDetalle
            // 
            labelCantidadDetalle.AutoSize = true;
            labelCantidadDetalle.BackColor = Color.Transparent;
            labelCantidadDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCantidadDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            labelCantidadDetalle.Location = new Point(350, 268);
            labelCantidadDetalle.Name = "labelCantidadDetalle";
            labelCantidadDetalle.Size = new Size(68, 20);
            labelCantidadDetalle.TabIndex = 12;
            labelCantidadDetalle.Text = "Cantidad";
            // 
            // pnlCantidadDetalle
            // 
            pnlCantidadDetalle.BackColor = Color.White;
            pnlCantidadDetalle.Controls.Add(numCantidadDetalle);
            pnlCantidadDetalle.Location = new Point(350, 292);
            pnlCantidadDetalle.Name = "pnlCantidadDetalle";
            pnlCantidadDetalle.Size = new Size(120, 41);
            pnlCantidadDetalle.TabIndex = 13;
            // 
            // numCantidadDetalle
            // 
            numCantidadDetalle.BackColor = Color.White;
            numCantidadDetalle.BorderStyle = BorderStyle.None;
            numCantidadDetalle.DecimalPlaces = 2;
            numCantidadDetalle.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numCantidadDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            numCantidadDetalle.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numCantidadDetalle.Location = new Point(8, 7);
            numCantidadDetalle.Maximum = new decimal(new int[] { 99999999, 0, 0, 131072 });
            numCantidadDetalle.Name = "numCantidadDetalle";
            numCantidadDetalle.Size = new Size(104, 26);
            numCantidadDetalle.TabIndex = 0;
            numCantidadDetalle.TextAlign = HorizontalAlignment.Right;
            numCantidadDetalle.ThousandsSeparator = true;
            // 
            // labelUnidadDetalle
            // 
            labelUnidadDetalle.AutoSize = true;
            labelUnidadDetalle.BackColor = Color.Transparent;
            labelUnidadDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelUnidadDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            labelUnidadDetalle.Location = new Point(486, 268);
            labelUnidadDetalle.Name = "labelUnidadDetalle";
            labelUnidadDetalle.Size = new Size(55, 20);
            labelUnidadDetalle.TabIndex = 14;
            labelUnidadDetalle.Text = "Unidad";
            // 
            // pnlUnidadDetalle
            // 
            pnlUnidadDetalle.BackColor = Color.White;
            pnlUnidadDetalle.Controls.Add(cboUnidadDetalle);
            pnlUnidadDetalle.Location = new Point(486, 292);
            pnlUnidadDetalle.Name = "pnlUnidadDetalle";
            pnlUnidadDetalle.Size = new Size(130, 41);
            pnlUnidadDetalle.TabIndex = 15;
            // 
            // cboUnidadDetalle
            // 
            cboUnidadDetalle.BackColor = Color.White;
            cboUnidadDetalle.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUnidadDetalle.FlatStyle = FlatStyle.Flat;
            cboUnidadDetalle.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboUnidadDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            cboUnidadDetalle.FormattingEnabled = true;
            cboUnidadDetalle.Location = new Point(8, 6);
            cboUnidadDetalle.Name = "cboUnidadDetalle";
            cboUnidadDetalle.Size = new Size(114, 28);
            cboUnidadDetalle.TabIndex = 0;
            // 
            // labelCostoDetalle
            // 
            labelCostoDetalle.AutoSize = true;
            labelCostoDetalle.BackColor = Color.Transparent;
            labelCostoDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCostoDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            labelCostoDetalle.Location = new Point(632, 268);
            labelCostoDetalle.Name = "labelCostoDetalle";
            labelCostoDetalle.Size = new Size(100, 20);
            labelCostoDetalle.TabIndex = 16;
            labelCostoDetalle.Text = "Costo unitario";
            // 
            // pnlCostoDetalle
            // 
            pnlCostoDetalle.BackColor = Color.White;
            pnlCostoDetalle.Controls.Add(numCostoDetalle);
            pnlCostoDetalle.Location = new Point(632, 292);
            pnlCostoDetalle.Name = "pnlCostoDetalle";
            pnlCostoDetalle.Size = new Size(140, 41);
            pnlCostoDetalle.TabIndex = 17;
            // 
            // numCostoDetalle
            // 
            numCostoDetalle.BackColor = Color.White;
            numCostoDetalle.BorderStyle = BorderStyle.None;
            numCostoDetalle.DecimalPlaces = 2;
            numCostoDetalle.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numCostoDetalle.ForeColor = Color.FromArgb(42, 52, 45);
            numCostoDetalle.Increment = new decimal(new int[] { 1, 0, 0, 0 });
            numCostoDetalle.Location = new Point(8, 7);
            numCostoDetalle.Maximum = new decimal(new int[] { 99999999, 0, 0, 131072 });
            numCostoDetalle.Name = "numCostoDetalle";
            numCostoDetalle.Size = new Size(124, 26);
            numCostoDetalle.TabIndex = 0;
            numCostoDetalle.TextAlign = HorizontalAlignment.Right;
            numCostoDetalle.ThousandsSeparator = true;
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
            btnAgregarDetalle.TabIndex = 18;
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
            btnActualizarDetalle.TabIndex = 19;
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
            btnQuitarDetalle.TabIndex = 20;
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
            dgvDetalles.Size = new Size(936, 150);
            dgvDetalles.TabIndex = 21;
            dgvDetalles.CellFormatting += dgvDetalles_CellFormatting;
            dgvDetalles.SelectionChanged += dgvDetalles_SelectionChanged;
            // 
            // lblTotal
            // 
            lblTotal.BackColor = Color.Transparent;
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = Color.FromArgb(23, 61, 43);
            lblTotal.Location = new Point(650, 550);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(310, 28);
            lblTotal.TabIndex = 22;
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
            // FrmRegistroCompraAlimento
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
            Name = "FrmRegistroCompraAlimento";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmRegistroCompraAlimento";
            Load += FrmRegistroCompraAlimento_Load;
            Shown += FrmRegistroCompraAlimento_Shown;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            pnlNumero.ResumeLayout(false);
            pnlNumero.PerformLayout();
            pnlFecha.ResumeLayout(false);
            pnlProveedor.ResumeLayout(false);
            pnlObservacion.ResumeLayout(false);
            pnlObservacion.PerformLayout();
            pnlAlimentoDetalle.ResumeLayout(false);
            pnlCantidadDetalle.ResumeLayout(false);
            pnlCantidadDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidadDetalle).EndInit();
            pnlUnidadDetalle.ResumeLayout(false);
            pnlCostoDetalle.ResumeLayout(false);
            pnlCostoDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCostoDetalle).EndInit();
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
        private Label labelProveedor;
        private Panel pnlProveedor;
        private ComboBox cboProveedor;
        private Label labelObservacion;
        private Panel pnlObservacion;
        private TextBox txtObservacion;
        private Label lblDetalles;
        private Label lblDetalleDescripcion;
        private Label labelAlimentoDetalle;
        private Panel pnlAlimentoDetalle;
        private ComboBox cboAlimentoDetalle;
        private Label labelCantidadDetalle;
        private Panel pnlCantidadDetalle;
        private NumericUpDown numCantidadDetalle;
        private Label labelUnidadDetalle;
        private Panel pnlUnidadDetalle;
        private ComboBox cboUnidadDetalle;
        private Label labelCostoDetalle;
        private Panel pnlCostoDetalle;
        private NumericUpDown numCostoDetalle;
        private FontAwesome.Sharp.IconButton btnAgregarDetalle;
        private FontAwesome.Sharp.IconButton btnActualizarDetalle;
        private FontAwesome.Sharp.IconButton btnQuitarDetalle;
        private DataGridView dgvDetalles;
        private Label lblTotal;
        private Panel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private ErrorProvider errorIcon;
    }
}
