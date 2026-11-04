namespace GranjaControl.UI.Formularios.Formularios_derivados.Tarifas
{
    partial class FrmTarifaListado
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
            // iconTitulo
            // 
            iconTitulo.IconChar = FontAwesome.Sharp.IconChar.SackDollar;
            iconTitulo.IconColor = Color.FromArgb(23, 61, 43);
            // 
            // lblTitulo
            // 
            lblTitulo.Text = "Listado de Tarifas de Pollo";
            // 
            // label1
            // 
            label1.Text = "Consulte y administre los precios por libra.";
            // 
            // FrmTarifaListado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 630);
            Name = "FrmTarifaListado";
            Text = "Tarifas de Pollo";
            ((System.ComponentModel.ISupportInitialize)iconTitulo).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
