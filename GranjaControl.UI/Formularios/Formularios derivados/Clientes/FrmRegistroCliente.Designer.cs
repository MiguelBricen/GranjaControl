namespace GranjaControl.UI.Formularios.Formularios_derivados.Clientes
{
    partial class FrmRegistroCliente
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
            labelNombre = new Label();
            pnlNombre = new Panel();
            txtNombre = new TextBox();
            labelTipo = new Label();
            pnlTipo = new Panel();
            cboTipoCliente = new ComboBox();
            labelTelefono = new Label();
            pnlTelefono = new Panel();
            txtTelefono = new TextBox();
            labelDireccion = new Label();
            pnlDireccion = new Panel();
            txtDireccion = new TextBox();
            pnlAcciones = new Panel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            errorIcon = new ErrorProvider(components);
            pnlSuperior.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlNombre.SuspendLayout();
            pnlTipo.SuspendLayout();
            pnlTelefono.SuspendLayout();
            pnlDireccion.SuspendLayout();
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
            lblDescripcion.Size = new Size(265, 23);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Complete la información del cliente.";
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
            lblTitulo.Size = new Size(149, 31);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Nuevo cliente";
            // 
            // iconIcono
            // 
            iconIcono.IconChar = FontAwesome.Sharp.IconChar.Users;
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
            pnlContenido.Controls.Add(labelNombre);
            pnlContenido.Controls.Add(pnlNombre);
            pnlContenido.Controls.Add(labelTipo);
            pnlContenido.Controls.Add(pnlTipo);
            pnlContenido.Controls.Add(labelTelefono);
            pnlContenido.Controls.Add(pnlTelefono);
            pnlContenido.Controls.Add(labelDireccion);
            pnlContenido.Controls.Add(pnlDireccion);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 64);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(520, 241);
            pnlContenido.TabIndex = 1;
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.BackColor = Color.Transparent;
            labelNombre.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNombre.ForeColor = Color.FromArgb(42, 52, 45);
            labelNombre.Location = new Point(24, 20);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(90, 25);
            labelNombre.TabIndex = 1;
            labelNombre.Text = "Nombre:";
            // 
            // pnlNombre
            // 
            pnlNombre.BackColor = Color.White;
            pnlNombre.Controls.Add(txtNombre);
            pnlNombre.Location = new Point(165, 18);
            pnlNombre.Name = "pnlNombre";
            pnlNombre.Size = new Size(330, 41);
            pnlNombre.TabIndex = 0;
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.White;
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNombre.ForeColor = Color.FromArgb(42, 52, 45);
            txtNombre.Location = new Point(8, 8);
            txtNombre.MaxLength = 100;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(312, 24);
            txtNombre.TabIndex = 0;
            // 
            // labelTipo
            // 
            labelTipo.AutoSize = true;
            labelTipo.BackColor = Color.Transparent;
            labelTipo.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTipo.ForeColor = Color.FromArgb(42, 52, 45);
            labelTipo.Location = new Point(24, 74);
            labelTipo.Name = "labelTipo";
            labelTipo.Size = new Size(53, 25);
            labelTipo.TabIndex = 3;
            labelTipo.Text = "Tipo:";
            // 
            // pnlTipo
            // 
            pnlTipo.BackColor = Color.White;
            pnlTipo.Controls.Add(cboTipoCliente);
            pnlTipo.Location = new Point(165, 72);
            pnlTipo.Name = "pnlTipo";
            pnlTipo.Size = new Size(330, 41);
            pnlTipo.TabIndex = 2;
            // 
            // cboTipoCliente
            // 
            cboTipoCliente.BackColor = Color.White;
            cboTipoCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoCliente.FlatStyle = FlatStyle.Flat;
            cboTipoCliente.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboTipoCliente.ForeColor = Color.FromArgb(42, 52, 45);
            cboTipoCliente.FormattingEnabled = true;
            cboTipoCliente.Location = new Point(8, 8);
            cboTipoCliente.Name = "cboTipoCliente";
            cboTipoCliente.Size = new Size(312, 25);
            cboTipoCliente.TabIndex = 0;
            // 
            // labelTelefono
            // 
            labelTelefono.AutoSize = true;
            labelTelefono.BackColor = Color.Transparent;
            labelTelefono.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTelefono.ForeColor = Color.FromArgb(42, 52, 45);
            labelTelefono.Location = new Point(24, 128);
            labelTelefono.Name = "labelTelefono";
            labelTelefono.Size = new Size(85, 25);
            labelTelefono.TabIndex = 5;
            labelTelefono.Text = "Teléfono:";
            // 
            // pnlTelefono
            // 
            pnlTelefono.BackColor = Color.White;
            pnlTelefono.Controls.Add(txtTelefono);
            pnlTelefono.Location = new Point(165, 126);
            pnlTelefono.Name = "pnlTelefono";
            pnlTelefono.Size = new Size(330, 41);
            pnlTelefono.TabIndex = 4;
            // 
            // txtTelefono
            // 
            txtTelefono.BackColor = Color.White;
            txtTelefono.BorderStyle = BorderStyle.None;
            txtTelefono.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTelefono.ForeColor = Color.FromArgb(42, 52, 45);
            txtTelefono.Location = new Point(8, 8);
            txtTelefono.MaxLength = 25;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(312, 24);
            txtTelefono.TabIndex = 0;
            // 
            // labelDireccion
            // 
            labelDireccion.AutoSize = true;
            labelDireccion.BackColor = Color.Transparent;
            labelDireccion.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelDireccion.ForeColor = Color.FromArgb(42, 52, 45);
            labelDireccion.Location = new Point(24, 182);
            labelDireccion.Name = "labelDireccion";
            labelDireccion.Size = new Size(92, 25);
            labelDireccion.TabIndex = 7;
            labelDireccion.Text = "Dirección:";
            // 
            // pnlDireccion
            // 
            pnlDireccion.BackColor = Color.White;
            pnlDireccion.Controls.Add(txtDireccion);
            pnlDireccion.Location = new Point(165, 180);
            pnlDireccion.Name = "pnlDireccion";
            pnlDireccion.Size = new Size(330, 41);
            pnlDireccion.TabIndex = 6;
            // 
            // txtDireccion
            // 
            txtDireccion.BackColor = Color.White;
            txtDireccion.BorderStyle = BorderStyle.None;
            txtDireccion.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDireccion.ForeColor = Color.FromArgb(42, 52, 45);
            txtDireccion.Location = new Point(8, 8);
            txtDireccion.MaxLength = 200;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(312, 24);
            txtDireccion.TabIndex = 0;
            // 
            // pnlAcciones
            // 
            pnlAcciones.BackColor = Color.FromArgb(226, 239, 228);
            pnlAcciones.Controls.Add(btnCancelar);
            pnlAcciones.Controls.Add(btnGuardar);
            pnlAcciones.Dock = DockStyle.Bottom;
            pnlAcciones.Location = new Point(0, 305);
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
            // FrmRegistroCliente
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 239, 228);
            CancelButton = btnCancelar;
            ClientSize = new Size(520, 360);
            Controls.Add(pnlContenido);
            Controls.Add(pnlAcciones);
            Controls.Add(pnlSuperior);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRegistroCliente";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmRegistroCliente";
            Load += FrmRegistroCliente_Load;
            Shown += FrmRegistroCliente_Shown;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            pnlNombre.ResumeLayout(false);
            pnlNombre.PerformLayout();
            pnlTipo.ResumeLayout(false);
            pnlTelefono.ResumeLayout(false);
            pnlTelefono.PerformLayout();
            pnlDireccion.ResumeLayout(false);
            pnlDireccion.PerformLayout();
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
        private Label labelNombre;
        private Panel pnlNombre;
        private TextBox txtNombre;
        private Label labelTipo;
        private Panel pnlTipo;
        private ComboBox cboTipoCliente;
        private Label labelTelefono;
        private Panel pnlTelefono;
        private TextBox txtTelefono;
        private Label labelDireccion;
        private Panel pnlDireccion;
        private TextBox txtDireccion;
        private Panel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private ErrorProvider errorIcon;
    }
}
