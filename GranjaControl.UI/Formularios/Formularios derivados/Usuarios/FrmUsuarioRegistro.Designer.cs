namespace GranjaControl.UI.Formularios.Formularios_derivados.Usuarios
{
    partial class FrmUsuarioRegistro
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
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            btnIconCerrar = new Button();
            lblDescripcion = new Label();
            lblTitulo = new Label();
            iconIcono = new FontAwesome.Sharp.IconButton();
            pictureBox1 = new PictureBox();
            txtPassword = new TextBox();
            label1 = new Label();
            pnlNombre = new Panel();
            txtNombre = new TextBox();
            txtUsuario = new TextBox();
            label2 = new Label();
            label4 = new Label();
            pnlNombreUsuario = new Panel();
            txtCorreo = new TextBox();
            pnlCorreo = new Panel();
            label5 = new Label();
            pnlPassword = new Panel();
            label3 = new Label();
            cboRoles = new ComboBox();
            pnlRolUsuario = new Panel();
            pnlContenido = new TableLayoutPanel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            errorIcon = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlNombre.SuspendLayout();
            pnlNombreUsuario.SuspendLayout();
            pnlCorreo.SuspendLayout();
            pnlPassword.SuspendLayout();
            pnlRolUsuario.SuspendLayout();
            pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnIconCerrar);
            panel1.Controls.Add(lblDescripcion);
            panel1.Controls.Add(lblTitulo);
            panel1.Controls.Add(iconIcono);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(472, 64);
            panel1.TabIndex = 2;
            // 
            // btnIconCerrar
            // 
            btnIconCerrar.FlatAppearance.BorderSize = 0;
            btnIconCerrar.FlatStyle = FlatStyle.Flat;
            btnIconCerrar.Image = Properties.Resources._02_cerrar1;
            btnIconCerrar.Location = new Point(428, 9);
            btnIconCerrar.Name = "btnIconCerrar";
            btnIconCerrar.Size = new Size(32, 32);
            btnIconCerrar.TabIndex = 3;
            btnIconCerrar.UseVisualStyleBackColor = true;
            btnIconCerrar.Click += btnIconCerrar_Click;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescripcion.ForeColor = SystemColors.ControlDarkDark;
            lblDescripcion.Location = new Point(68, 37);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(263, 20);
            lblDescripcion.TabIndex = 2;
            lblDescripcion.Text = "Complete la información del registro ";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(68, 6);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(174, 31);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Nuevo registro";
            // 
            // iconIcono
            // 
            iconIcono.IconChar = FontAwesome.Sharp.IconChar.Vcard;
            iconIcono.IconColor = Color.Blue;
            iconIcono.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconIcono.IconSize = 50;
            iconIcono.Location = new Point(12, 7);
            iconIcono.Name = "iconIcono";
            iconIcono.Size = new Size(50, 50);
            iconIcono.TabIndex = 0;
            iconIcono.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Lavender;
            pictureBox1.Location = new Point(0, 65);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(472, 265);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Location = new Point(3, 11);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(265, 20);
            txtPassword.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Lavender;
            label1.Enabled = false;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(14, 130);
            label1.Name = "label1";
            label1.Size = new Size(171, 23);
            label1.TabIndex = 21;
            label1.Text = "Nombre de Usuario:";
            // 
            // pnlNombre
            // 
            pnlNombre.BackColor = Color.White;
            pnlNombre.Controls.Add(txtNombre);
            pnlNombre.Location = new Point(186, 83);
            pnlNombre.Name = "pnlNombre";
            pnlNombre.Size = new Size(272, 41);
            pnlNombre.TabIndex = 18;
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.White;
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Location = new Point(3, 11);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(234, 20);
            txtNombre.TabIndex = 0;
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.White;
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Location = new Point(3, 11);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(234, 20);
            txtUsuario.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Lavender;
            label2.Enabled = false;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(14, 177);
            label2.Name = "label2";
            label2.Size = new Size(162, 23);
            label2.TabIndex = 23;
            label2.Text = "Correo electrónico:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Lavender;
            label4.Enabled = false;
            label4.FlatStyle = FlatStyle.Flat;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(14, 83);
            label4.Name = "label4";
            label4.Size = new Size(162, 23);
            label4.TabIndex = 19;
            label4.Text = "Nombre completo:";
            // 
            // pnlNombreUsuario
            // 
            pnlNombreUsuario.BackColor = Color.White;
            pnlNombreUsuario.Controls.Add(txtUsuario);
            pnlNombreUsuario.Location = new Point(186, 130);
            pnlNombreUsuario.Name = "pnlNombreUsuario";
            pnlNombreUsuario.Size = new Size(272, 41);
            pnlNombreUsuario.TabIndex = 20;
            // 
            // txtCorreo
            // 
            txtCorreo.BackColor = Color.White;
            txtCorreo.BorderStyle = BorderStyle.None;
            txtCorreo.Location = new Point(3, 11);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(234, 20);
            txtCorreo.TabIndex = 0;
            // 
            // pnlCorreo
            // 
            pnlCorreo.BackColor = Color.White;
            pnlCorreo.Controls.Add(txtCorreo);
            pnlCorreo.Location = new Point(186, 177);
            pnlCorreo.Name = "pnlCorreo";
            pnlCorreo.Size = new Size(272, 41);
            pnlCorreo.TabIndex = 22;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Lavender;
            label5.Enabled = false;
            label5.FlatStyle = FlatStyle.Flat;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(14, 271);
            label5.Name = "label5";
            label5.Size = new Size(104, 23);
            label5.TabIndex = 27;
            label5.Text = "Contraseña:";
            // 
            // pnlPassword
            // 
            pnlPassword.BackColor = Color.White;
            pnlPassword.Controls.Add(txtPassword);
            pnlPassword.Location = new Point(155, 271);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(303, 41);
            pnlPassword.TabIndex = 26;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Lavender;
            label3.Enabled = false;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(14, 224);
            label3.Name = "label3";
            label3.Size = new Size(131, 23);
            label3.TabIndex = 25;
            label3.Text = "Rol de Usuario:";
            // 
            // cboRoles
            // 
            cboRoles.BackColor = Color.White;
            cboRoles.FlatStyle = FlatStyle.Flat;
            cboRoles.FormattingEnabled = true;
            cboRoles.Location = new Point(3, 9);
            cboRoles.Name = "cboRoles";
            cboRoles.Size = new Size(265, 28);
            cboRoles.TabIndex = 0;
            // 
            // pnlRolUsuario
            // 
            pnlRolUsuario.BackColor = Color.White;
            pnlRolUsuario.Controls.Add(cboRoles);
            pnlRolUsuario.Location = new Point(155, 224);
            pnlRolUsuario.Name = "pnlRolUsuario";
            pnlRolUsuario.Size = new Size(303, 41);
            pnlRolUsuario.TabIndex = 24;
            // 
            // pnlContenido
            // 
            pnlContenido.ColumnCount = 4;
            pnlContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 123F));
            pnlContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 123F));
            pnlContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            pnlContenido.Controls.Add(btnCancelar, 2, 0);
            pnlContenido.Controls.Add(btnGuardar, 1, 0);
            pnlContenido.Dock = DockStyle.Bottom;
            pnlContenido.Location = new Point(0, 339);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.RowCount = 1;
            pnlContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlContenido.Size = new Size(472, 56);
            pnlContenido.TabIndex = 28;
            // 
            // btnCancelar
            // 
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Red;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 28;
            btnCancelar.Location = new Point(332, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(117, 50);
            btnCancelar.TabIndex = 2;
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
            btnGuardar.IconColor = Color.Blue;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 28;
            btnGuardar.Location = new Point(209, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(117, 50);
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
            // FrmUsuarioRegistro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(472, 395);
            Controls.Add(pnlContenido);
            Controls.Add(label1);
            Controls.Add(pnlNombre);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(pnlNombreUsuario);
            Controls.Add(pnlCorreo);
            Controls.Add(label5);
            Controls.Add(pnlPassword);
            Controls.Add(label3);
            Controls.Add(pnlRolUsuario);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmUsuarioRegistro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmUsuarioRegistro";
            Load += FrmUsuarioRegistro_Load;
            Shown += FrmUsuarioRegistro_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlNombre.ResumeLayout(false);
            pnlNombre.PerformLayout();
            pnlNombreUsuario.ResumeLayout(false);
            pnlNombreUsuario.PerformLayout();
            pnlCorreo.ResumeLayout(false);
            pnlCorreo.PerformLayout();
            pnlPassword.ResumeLayout(false);
            pnlPassword.PerformLayout();
            pnlRolUsuario.ResumeLayout(false);
            pnlContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnIconCerrar;
        private Label lblDescripcion;
        private Label lblTitulo;
        private FontAwesome.Sharp.IconButton iconIcono;
        private PictureBox pictureBox1;
        private TextBox txtPassword;
        private Label label1;
        private Panel pnlNombre;
        private TextBox txtNombre;
        private TextBox txtUsuario;
        private Label label2;
        private Label label4;
        private Panel pnlNombreUsuario;
        private TextBox txtCorreo;
        private Panel pnlCorreo;
        private Label label5;
        private Panel pnlPassword;
        private Label label3;
        private ComboBox cboRoles;
        private Panel pnlRolUsuario;
        private TableLayoutPanel pnlContenido;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private ErrorProvider errorIcon;
    }
}