namespace GranjaControl.UI.Formularios
{
    partial class FrmMensajes
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
            picIcono = new PictureBox();
            lblMensaje = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            timerEntrada = new System.Windows.Forms.Timer(components);
            btnCancelar = new Button();
            btnAceptar = new Button();
            btnCerrar = new Button();
            panel1 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)picIcono).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // picIcono
            // 
            picIcono.Image = Properties.Resources.error;
            picIcono.Location = new Point(12, 65);
            picIcono.Name = "picIcono";
            picIcono.Size = new Size(50, 50);
            picIcono.TabIndex = 1;
            picIcono.TabStop = false;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMensaje.ForeColor = Color.FromArgb(48, 56, 48);
            lblMensaje.Location = new Point(68, 74);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(59, 25);
            lblMensaje.TabIndex = 3;
            lblMensaje.Text = "label1";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick_1;
            // 
            // timerEntrada
            // 
            timerEntrada.Tick += timerEntrada_Tick_1;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(231, 232, 222);
            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(207, 211, 198);
            btnCancelar.FlatAppearance.BorderSize = 1;
            btnCancelar.FlatAppearance.MouseDownBackColor = Color.FromArgb(203, 209, 191);
            btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(218, 222, 208);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.ForeColor = Color.FromArgb(55, 63, 53);
            btnCancelar.Location = new Point(285, 140);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(131, 60);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.FromArgb(66, 101, 70);
            btnAceptar.FlatAppearance.BorderColor = Color.FromArgb(57, 87, 61);
            btnAceptar.FlatAppearance.BorderSize = 1;
            btnAceptar.FlatAppearance.MouseDownBackColor = Color.FromArgb(53, 82, 57);
            btnAceptar.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 117, 84);
            btnAceptar.FlatStyle = FlatStyle.Flat;
            btnAceptar.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAceptar.ForeColor = Color.FromArgb(255, 255, 250);
            btnAceptar.Location = new Point(145, 140);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(131, 60);
            btnAceptar.TabIndex = 5;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.FromArgb(55, 86, 66);
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatAppearance.MouseDownBackColor = Color.FromArgb(47, 70, 53);
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(68, 101, 78);
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Image = Properties.Resources.Cerrar;
            btnCerrar.Location = new Point(395, 14);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(32, 32);
            btnCerrar.TabIndex = 1;
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(55, 86, 66);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnCerrar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(428, 64);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(250, 249, 244);
            label1.Location = new Point(4, 19);
            label1.Name = "label1";
            label1.Size = new Size(173, 31);
            label1.TabIndex = 2;
            label1.Text = "CONFIRMAR OPERACIÓN";
            // 
            // FrmMensajes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 247, 241);
            ClientSize = new Size(428, 212);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            Controls.Add(lblMensaje);
            Controls.Add(picIcono);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmMensajes";
            Text = "FrmMensajes";
            Load += FrmMensajes_Load_1;
            ((System.ComponentModel.ISupportInitialize)picIcono).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox picIcono;
        private Label lblMensaje;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerEntrada;
        private Button btnCancelar;
        private Button btnAceptar;
        private Button btnCerrar;
        private Panel panel1;
        private Label label1;
    }
}
