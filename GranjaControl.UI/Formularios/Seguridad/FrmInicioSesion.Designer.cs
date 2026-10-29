namespace GranjaControl.UI.Formularios.Seguridad
{
    partial class FrmInicioSesion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInicioSesion));
            picLoginFondo = new PictureBox();
            pnlContenido = new Panel();
            textBox1 = new TextBox();
            picLinea2 = new PictureBox();
            picLinea = new PictureBox();
            button3 = new Button();
            btnLogin = new Button();
            pnlUsuario = new Panel();
            button2 = new Button();
            txtUsuario = new TextBox();
            pnlClave = new Panel();
            button1 = new Button();
            txtClave = new TextBox();
            btnCerrar = new Button();
            btnIconoCentral = new GranjaControl.UI.Utilidades.BotonCircularImagen();
            errorIcon = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)picLoginFondo).BeginInit();
            pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLinea2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLinea).BeginInit();
            pnlUsuario.SuspendLayout();
            pnlClave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
            SuspendLayout();
            // 
            // picLoginFondo
            // 
            picLoginFondo.Dock = DockStyle.Fill;
            picLoginFondo.Image = Properties.Resources.PicLoginFondo;
            picLoginFondo.Location = new Point(0, 0);
            picLoginFondo.Name = "picLoginFondo";
            picLoginFondo.Size = new Size(747, 377);
            picLoginFondo.SizeMode = PictureBoxSizeMode.StretchImage;
            picLoginFondo.TabIndex = 0;
            picLoginFondo.TabStop = false;
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.White;
            pnlContenido.Controls.Add(textBox1);
            pnlContenido.Controls.Add(picLinea2);
            pnlContenido.Controls.Add(picLinea);
            pnlContenido.Controls.Add(button3);
            pnlContenido.Controls.Add(btnLogin);
            pnlContenido.Controls.Add(pnlUsuario);
            pnlContenido.Controls.Add(pnlClave);
            pnlContenido.Location = new Point(368, 55);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(356, 306);
            pnlContenido.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(175, 229);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "O";
            textBox1.Size = new Size(19, 24);
            textBox1.TabIndex = 22;
            // 
            // picLinea2
            // 
            picLinea2.BackColor = Color.FromArgb(85, 64, 42);
            picLinea2.Location = new Point(195, 243);
            picLinea2.Name = "picLinea2";
            picLinea2.Size = new Size(148, 1);
            picLinea2.TabIndex = 21;
            picLinea2.TabStop = false;
            // 
            // picLinea
            // 
            picLinea.BackColor = Color.FromArgb(85, 64, 42);
            picLinea.Location = new Point(21, 243);
            picLinea.Name = "picLinea";
            picLinea.Size = new Size(148, 1);
            picLinea.TabIndex = 20;
            picLinea.TabStop = false;
            // 
            // button3
            // 
            button3.BackColor = Color.White;
            button3.Cursor = Cursors.Hand;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.FromArgb(85, 64, 42);
            button3.Location = new Point(18, 242);
            button3.Name = "button3";
            button3.Size = new Size(322, 55);
            button3.TabIndex = 19;
            button3.Text = "¿No tienes cuenta? Registrate";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(190, 152, 72);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(21, 171);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(322, 55);
            btnLogin.TabIndex = 18;
            btnLogin.Text = "Iniciar sesión";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // pnlUsuario
            // 
            pnlUsuario.BackColor = Color.FromArgb(220, 224, 207);
            pnlUsuario.BorderStyle = BorderStyle.FixedSingle;
            pnlUsuario.Controls.Add(button2);
            pnlUsuario.Controls.Add(txtUsuario);
            pnlUsuario.Cursor = Cursors.Hand;
            pnlUsuario.Location = new Point(18, 48);
            pnlUsuario.Name = "pnlUsuario";
            pnlUsuario.Size = new Size(325, 41);
            pnlUsuario.TabIndex = 16;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(220, 224, 207);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Image = Properties.Resources.User2;
            button2.Location = new Point(1, 4);
            button2.Name = "button2";
            button2.Size = new Size(32, 32);
            button2.TabIndex = 3;
            button2.UseVisualStyleBackColor = false;
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.FromArgb(220, 224, 207);
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Location = new Point(44, 10);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Ingrese su usuario";
            txtUsuario.Size = new Size(251, 20);
            txtUsuario.TabIndex = 0;
            // 
            // pnlClave
            // 
            pnlClave.BackColor = Color.FromArgb(220, 224, 207);
            pnlClave.BorderStyle = BorderStyle.FixedSingle;
            pnlClave.Controls.Add(button1);
            pnlClave.Controls.Add(txtClave);
            pnlClave.Cursor = Cursors.Hand;
            pnlClave.Location = new Point(21, 113);
            pnlClave.Name = "pnlClave";
            pnlClave.Size = new Size(322, 41);
            pnlClave.TabIndex = 17;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(220, 224, 207);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = Properties.Resources.Candado2;
            button1.Location = new Point(3, 5);
            button1.Name = "button1";
            button1.Size = new Size(32, 32);
            button1.TabIndex = 4;
            button1.UseVisualStyleBackColor = false;
            // 
            // txtClave
            // 
            txtClave.BackColor = Color.FromArgb(220, 224, 207);
            txtClave.BorderStyle = BorderStyle.None;
            txtClave.Location = new Point(41, 10);
            txtClave.Name = "txtClave";
            txtClave.PasswordChar = '*';
            txtClave.PlaceholderText = "Ingrese su contraseña";
            txtClave.Size = new Size(251, 20);
            txtClave.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.FromArgb(220, 224, 207);
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Image = Properties.Resources.icono_cerrar;
            btnCerrar.Location = new Point(712, 2);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(32, 32);
            btnCerrar.TabIndex = 2;
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnIconoCentral
            // 
            btnIconoCentral.BackColor = Color.White;
            btnIconoCentral.Location = new Point(489, 0);
            btnIconoCentral.Name = "btnIconoCentral";
            btnIconoCentral.Size = new Size(100, 88);
            btnIconoCentral.TabIndex = 4;
            // 
            // errorIcon
            // 
            errorIcon.ContainerControl = this;
            errorIcon.Icon = (Icon)resources.GetObject("errorIcon.Icon");
            // 
            // FrmInicioSesion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(747, 377);
            Controls.Add(btnIconoCentral);
            Controls.Add(btnCerrar);
            Controls.Add(pnlContenido);
            Controls.Add(picLoginFondo);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmInicioSesion";
            StartPosition = FormStartPosition.CenterScreen;
            Shown += FrmInicioSesion_Shown;
            ((System.ComponentModel.ISupportInitialize)picLoginFondo).EndInit();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLinea2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLinea).EndInit();
            pnlUsuario.ResumeLayout(false);
            pnlUsuario.PerformLayout();
            pnlClave.ResumeLayout(false);
            pnlClave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picLoginFondo;
        private Panel pnlContenido;
        private Button btnCerrar;
        private Panel pnlUsuario;
        private TextBox txtUsuario;
        private Panel pnlClave;
        private TextBox txtClave;
        private Button button2;
        private Button button1;
        private Button btnLogin;
        private PictureBox picLinea;
        private Button button3;
        private PictureBox picLinea2;
        private TextBox textBox1;
        private Utilidades.BotonCircularImagen btnIconoCentral;
        private ErrorProvider errorIcon;
    }
}