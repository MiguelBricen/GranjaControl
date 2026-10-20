namespace GranjaControl.UI
{
    partial class FrmConfigurarConexion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigurarConexion));
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            txtPassword = new TextBox();
            txtUsuario = new TextBox();
            txtBaseDatos = new TextBox();
            txtServidor = new TextBox();
            btnGuardar = new Button();
            btnProbarConexion = new Button();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            errorIcono = new ErrorProvider(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcono).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonShadow;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(347, 64);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 21);
            label1.Name = "label1";
            label1.Size = new Size(249, 28);
            label1.TabIndex = 1;
            label1.Text = "CONEXIÓN AL SERVIDOR";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlDark;
            panel2.Controls.Add(txtPassword);
            panel2.Controls.Add(txtUsuario);
            panel2.Controls.Add(txtBaseDatos);
            panel2.Controls.Add(txtServidor);
            panel2.Controls.Add(btnGuardar);
            panel2.Controls.Add(btnProbarConexion);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(12, 77);
            panel2.Name = "panel2";
            panel2.Size = new Size(323, 283);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.Gray;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Location = new Point(14, 187);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(258, 20);
            txtPassword.TabIndex = 9;
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.Gray;
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Location = new Point(14, 139);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(258, 20);
            txtUsuario.TabIndex = 8;
            // 
            // txtBaseDatos
            // 
            txtBaseDatos.BackColor = Color.Gray;
            txtBaseDatos.BorderStyle = BorderStyle.None;
            txtBaseDatos.Location = new Point(14, 91);
            txtBaseDatos.Name = "txtBaseDatos";
            txtBaseDatos.Size = new Size(258, 20);
            txtBaseDatos.TabIndex = 7;
            // 
            // txtServidor
            // 
            txtServidor.BackColor = Color.Gray;
            txtServidor.BorderStyle = BorderStyle.None;
            txtServidor.Location = new Point(14, 43);
            txtServidor.Name = "txtServidor";
            txtServidor.Size = new Size(258, 20);
            txtServidor.TabIndex = 6;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.DarkRed;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(186, 232);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(134, 45);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnProbarConexion
            // 
            btnProbarConexion.BackColor = Color.Green;
            btnProbarConexion.FlatAppearance.BorderSize = 0;
            btnProbarConexion.FlatStyle = FlatStyle.Flat;
            btnProbarConexion.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProbarConexion.ForeColor = Color.White;
            btnProbarConexion.Location = new Point(3, 232);
            btnProbarConexion.Name = "btnProbarConexion";
            btnProbarConexion.Size = new Size(177, 45);
            btnProbarConexion.TabIndex = 4;
            btnProbarConexion.Text = "Probar conexión";
            btnProbarConexion.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(3, 164);
            label5.Name = "label5";
            label5.Size = new Size(90, 25);
            label5.TabIndex = 3;
            label5.Text = "Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(3, 116);
            label4.Name = "label4";
            label4.Size = new Size(114, 25);
            label4.TabIndex = 2;
            label4.Text = "Usuario SQL";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(3, 68);
            label3.Name = "label3";
            label3.Size = new Size(129, 25);
            label3.TabIndex = 1;
            label3.Text = "Base de Datos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(3, 16);
            label2.Name = "label2";
            label2.Size = new Size(83, 25);
            label2.TabIndex = 0;
            label2.Text = "Servidor";
            // 
            // errorIcono
            // 
            errorIcono.ContainerControl = this;
            errorIcono.Icon = (Icon)resources.GetObject("errorIcono.Icon");
            // 
            // FrmConfigurarConexion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(347, 371);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmConfigurarConexion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmConfigurarConexion";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcono).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox txtServidor;
        private Button btnGuardar;
        private Button btnProbarConexion;
        private TextBox txtPassword;
        private TextBox txtUsuario;
        private TextBox txtBaseDatos;
        private ErrorProvider errorIcono;
    }
}