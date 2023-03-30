using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Conexión
    {
        /*//Método para establecer conexión de pruebas
        public static SqlConnection ConexiónCanapro()
        {
            SqlConnection a = null;
            try
            {
                SqlConnection conexion = new SqlConnection(Properties.Settings.Default.Conexión_Canapro);
                conexion.Open();
               a = conexion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido conectar con la base de datos: " + ex.ToString());
            }
            return a;
        }*/

        //Método para establecer conexión con la base de datos del servidor en la nube
        public static SqlConnection ConexiónCanapro()
        {
            SqlConnection a = null;
            try
            {
                SqlConnection conexion = new SqlConnection(Properties.Settings.Default.Conexión_BD_Nube);
                conexion.Open();
                a = conexion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido conectar con la base de datos: " + ex.ToString());
            }
            return a;
        }
    }
}
