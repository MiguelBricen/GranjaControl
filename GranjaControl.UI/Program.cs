using GranjaControl.DAL.Common;
using GranjaControl.UI.Formularios;
using GranjaControl.UI.Formularios_derivados;

namespace GranjaControl.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            ConfiguracionDal configuracionDal = new();

            // Si no existe configuración, mostrar el formulario de configuración
            if (!configuracionDal.ExisteConfiguracion())
            {
                FrmConfigurarConexion frmConfig = new FrmConfigurarConexion();

                DialogResult resultado = frmConfig.ShowDialog();

                if (resultado != DialogResult.OK)
                {
                    // Si el usuario cancela la configuración, salir de la aplicación
                    return;
                }
            }
            // Leer la configuración de conexión
            var config = configuracionDal.Obtener();

            if (config is null)
            {
                MessageBox.Show("No se pudo obtener la configuración de conexión. La aplicación se cerrará.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear la cadena de conexión a partir de la configuración
            Conexion.CadenaConexion =
                $"Server={config.Servidor};" +
                $"Database={config.BaseDatos};" +
                $"User Id={config.UsuarioSql};" +
                $"Password={config.Password};" +
                $"TrustServerCertificate=True;";


            Application.Run(new FrmPrincipal());

        }
    }
    
}