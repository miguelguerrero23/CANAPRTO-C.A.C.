using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Acceso
    {

        //Definición y encapsulamiento de variables
        internal C_Acceso ObjetoAcceso { get; set; }
        public string T_Usuario { get; set; }
        public string Control_Interno { get; set; }
        public string Novedades { get; set; }
        public string Lavado_Activos { get; set; }
        public string Contabilidad { get; set; }
        public string Facturación { get; set; }
        public string ComitéEducación { get; set; }
        public int Id_usuario { get; set; }
        public string NombreAgencia { get; set; }

        //Método para validar la existencia del usuario en la base de datos
        internal bool ValidarUsuario(string usuario, string contraseña)
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select * from usuarios where usuario = '" + usuario + "' and contraseña = '" + contraseña + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Datos = consulta.ExecuteReader();
                if (Datos.Read() == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        //Método para validar que el usuario esté activo en la base de datos
        internal bool ValidarEstadoUsuario(string usuario)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from usuarios where usuario = '" + usuario + "' and estado = 'A'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = validar.ExecuteReader();
                if (datos.Read() == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas" + Ex.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;                
            }
        }

        //Método para realizar búsqueda de tipo de usuario y los roles que desempeña cada usuario
        internal C_Acceso ConsultaUsuario (string usuario, string contraseña)
        {
            C_Acceso ObjetoAcceso = new C_Acceso();
            try
            {
                SqlCommand consulta = new SqlCommand("select tipousuario, Control_Interno, Novedades, Lavado_Activos, Contabilidad, Facturación, ComitéEducación from usuarios where usuario = '" + usuario + "' and contraseña = '" + contraseña + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Datos= consulta.ExecuteReader();
                if (Datos.Read() == true)
                {
                    ObjetoAcceso.T_Usuario = Datos.GetString(0);
                    ObjetoAcceso.Control_Interno = Datos.GetString(1);
                    ObjetoAcceso.Novedades = Datos.GetString(2);
                    ObjetoAcceso.Lavado_Activos = Datos.GetString(3);
                    ObjetoAcceso.Contabilidad = Datos.GetString(4);
                    ObjetoAcceso.Facturación = Datos.GetString(5);
                    ObjetoAcceso.ComitéEducación = Datos.GetString(6);
                }
                return ObjetoAcceso;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        
    }
}
