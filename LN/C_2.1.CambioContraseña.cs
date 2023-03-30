using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_CambioContraseña
    {
        //Definición de variables 
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string ContraseñaNueva { get; set; }

        //Método para validar que el usuario y la contraseña coincidan con el registro de la base de datos
        internal Boolean ValidarUsuarioYContraseña(string usuario, string contraseña)
        {
            Usuario = usuario;
            Contraseña = contraseña;
            try
            {
                SqlCommand validar = new SqlCommand("select * from usuarios where usuario = '" + Usuario + "' and contraseña = '" + Contraseña + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para actualizar la contraseña de un usuario en la base de datos
        internal void ActualizarContraseña()
        {
            //Usuario = usuario;
            //Contraseña = contraseña;
            try
            {
                SqlCommand Actualizar = new SqlCommand("update usuarios set contraseña = '" + ContraseñaNueva + "' where usuario = '" + Usuario + "' ", LN.C_Conexión.ConexiónCanapro());
                Actualizar.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("La contraseña se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas" + Ex.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
