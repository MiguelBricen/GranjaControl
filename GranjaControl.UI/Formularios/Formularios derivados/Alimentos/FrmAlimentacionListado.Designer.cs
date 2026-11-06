namespace GranjaControl.UI.Formularios.Formularios_derivados.Alimentos
{
    partial class FrmAlimentacionListado
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
            ((System.ComponentModel.ISupportInitialize)iconTitulo).BeginInit();
            SuspendLayout();
            // 
            // btnEliminar
            // 
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatAppearance.MouseOverBackColor = Color.FromArgb(151, 40, 40);
            // 
            // btnEditar
            // 
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.FlatAppearance.MouseOverBackColor = Color.FromArgb(36, 83, 116);
            // 
            // btnNuevo
            // 
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(46, 104, 65);
            // 
            // label1
            // 
            label1.Size = new Size(361, 23);
            label1.Text = "Consulte y administre las raciones registradas.";
            // 
            // lblTitulo
            // 
            lblTitulo.Size = new Size(349, 41);
            lblTitulo.Text = "Listado de Alimentación";
            // 
            // iconTitulo
            // 
            iconTitulo.ForeColor = Color.FromArgb(154, 24, 26);
            iconTitulo.IconChar = FontAwesome.Sharp.IconChar.WheatAwn;
            iconTitulo.IconColor = Color.FromArgb(154, 24, 26);
            // 
            // FrmAlimentacionListado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 630);
            Name = "FrmAlimentacionListado";
            Text = "FrmAlimentacionListado";
            ((System.ComponentModel.ISupportInitialize)iconTitulo).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
