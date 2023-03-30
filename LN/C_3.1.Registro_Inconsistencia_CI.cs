using CANAPRO.UI;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Registro_Inconsistencia_CI
    {
        //Definición y encapsulamiento de variables
        public string Buscar_Usuario { get; set; }
        public int Id_Registro_Inconsistencia { get; set; }
        public string Observaciones { get; set; }
        public string Fecha { get; set; }
        internal C_Registro_Inconsistencia_CI ObjetoControlInterno { get; set; }
        public int CódigoInconsistencia { get; set; }
        public int CódigoUsuario { get; set; }
        public int Id_Inconsistencia_CI { get; set; }
        public int Id_Responsable { get; set; }
        public int Usuario_ID { get; set; }
        public string Nombre { get; set; }
        public string TipoInconsistencia { get; set; }
        public string Usuario { get; set; }
        public string ConsultaSQL { get; set; }
        public int Inconsistencia_ID { get; set; }
        public string Fecha_Registro { get; set; }

        //Método para validar que los registros de las inconsistencias no se repitan en la base de datos
        internal Boolean ValidarExistenciaRegistroInconsistenciaCI(int Registro_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Registro_Inconsistencias_CI where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
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
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                throw;
            }
        }

        //Método para guardar los registros de las inconsistencias en la base de datos 
        internal void GuardarRegistroInconsistenciasCI()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Registro) + 1 from Registro_Inconsistencias_CI", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_Registro_Inconsistencia = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_Registro_Inconsistencia = 1;
                    //throw;
                }
                SqlCommand GuardarRegistroInconsistenciaCI = new SqlCommand("insert into Registro_Inconsistencias_CI values ('" + Id_Registro_Inconsistencia + "','" + Id_Inconsistencia_CI + "', '" + Usuario_ID + "','" + Id_Responsable + "','" + Observaciones + "','" + Fecha + "','" + Fecha_Registro + "')", LN.C_Conexión.ConexiónCanapro());                
                GuardarRegistroInconsistenciaCI.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro de la inconsistencia se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros de las inconsistencias en la base de datos 
        internal void ModificarRegistroInconsistenciasCI()
        {
            try
            {
                SqlCommand ModificarRegistroInconsistenciaCI = new SqlCommand("update Registro_Inconsistencias_CI set Id_Registro = '" + Id_Registro_Inconsistencia + "', Id_Inconsistencia_CI = '" + Id_Inconsistencia_CI + "', Id_Usuarios = '" + Usuario_ID + "', Id_Responsable = '" + Id_Responsable + "', Observaciones = '" + Observaciones + "', Fecha = '" + Fecha + "', Fecha_Registro = '" + Fecha_Registro + "' where Id_Registro = " + Id_Registro_Inconsistencia, LN.C_Conexión.ConexiónCanapro());
                ModificarRegistroInconsistenciaCI.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El registro de la inconsistencia se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de las inconsistenciasen la base de datos 
        internal void EliminarRegistroInconsistenciasCI(int Registro_ID)
        {
            try
            {
                SqlCommand EliminarRegistroInconsistenciaCI = new SqlCommand("delete from Registro_Inconsistencias_CI where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarRegistroInconsistenciaCI.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro de inconsistencia se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }        

        //Método para consultar el ID del usuario en la base de datos
        internal C_Registro_Inconsistencia_CI ConsultarDatosUsuario(string usuario)
        {
            ObjetoControlInterno = new C_Registro_Inconsistencia_CI();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select id_usuarios from usuarios where usuario = '" + usuario + "'", LN.C_Conexión.ConexiónCanapro()); SqlDataReader user = ConsultaUsuario.ExecuteReader();

                if (user.Read())
                {
                    ObjetoControlInterno.Id_Responsable = user.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoControlInterno;
        }

        //Método que realiza la consulta del nombre completo de los usuarios en la base de datos "CANAPRO" y las almacena en una lista para llevarlas a un Combobox 
        internal System.Collections.ArrayList LlenarComboNombreUsuarios(System.Collections.ArrayList lista)
        {
            try
            {
                string NombreUsuario;
                SqlCommand consultar = new SqlCommand("select Nombre from usuarios", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = consultar.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datos.Read() == true)
                {
                    NombreUsuario = datos.GetString(0);
                    lista.Add(datos.GetString(0));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;
        }

        //Método que realiza la consulta del nombre completo de los usuarios en la base de datos "CANAPRO" y las almacena en una lista para llevarlas a un Combobox 
        internal System.Collections.ArrayList LlenarComboTipoInconsistencias(System.Collections.ArrayList lista)
        {
            try
            {
                string NombreUsuario;
                SqlCommand consultar = new SqlCommand("select NombreInconsistenciaCI from Tipo_Inconsistencia_CI", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = consultar.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datos.Read() == true)
                {
                    NombreUsuario = datos.GetString(0);
                    lista.Add(datos.GetString(0));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;
        }

        //Método para llenar el textbox con el id del registro
        public int Código_Registro()
        {
            try
            {
                SqlCommand Recibir_Id = new SqlCommand("select max(Id_Registro) + 1 from Registro_Inconsistencias_CI", C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = Recibir_Id.ExecuteReader();
                Id_Nuevo.Read();
                CódigoUsuario = Id_Nuevo.GetInt32(0);
                C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoUsuario = 1;
                //throw;
            }
            return CódigoUsuario;
        }

        //Método para llenar el textbox con el usuario cuando se selecciona un nombre de usuario en el formulario de registro de control interno
        internal string Usuarios(string Nombre_Usuario)
        {
            try
            {
                SqlCommand Recibir_Usuario = new SqlCommand("select usuario from usuarios where nombre = '" + Nombre_Usuario + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Usuario_Nuevo = Recibir_Usuario.ExecuteReader();
                Usuario_Nuevo.Read();
                Buscar_Usuario = Usuario_Nuevo.GetString(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                Buscar_Usuario = "";
                //throw;
            }

            return Buscar_Usuario;
        }

        //Método para llenar el textbox con el id de la inconsistencia cuando se selecciona un item de inconsistencia en el formulario de registro de control interno
        internal int Id_Usuario(string Nombre_Usuario)
        {
            try
            {
                SqlCommand Recibir_Id_Usuario = new SqlCommand("select Id_Usuarios from usuarios where Nombre = '" + Nombre_Usuario + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader CódigoNuevo = Recibir_Id_Usuario.ExecuteReader();
                CódigoNuevo.Read();
                CódigoUsuario = CódigoNuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoUsuario = 1;
                //throw;
            }

            return CódigoUsuario;
        }

        //Método para llenar el textbox con el id de la inconsistencia cuando se selecciona un item de inconsistencia en el formulario de registro de control interno
        internal int Id_Inconsistencias(string Inconsistencia)
        {
            try
            {
                SqlCommand Recibir_Id_Inconsistencia = new SqlCommand("select Id_Inconsistencia_CI from Tipo_Inconsistencia_CI where NombreInconsistenciaCI = '" + Inconsistencia + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader CódigoNuevo = Recibir_Id_Inconsistencia.ExecuteReader();
                CódigoNuevo.Read();
                CódigoInconsistencia = CódigoNuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoInconsistencia = 1;
                //throw;
            }

            return CódigoInconsistencia;
        }

        //Método que realiza consulta a la tabla Registro de Inconsistencias y almacena la información en un objeto para luego cargarla en el formulario de registro y permitir modificar y eliminar los registros
        internal C_Registro_Inconsistencia_CI MostrarInconsistencias(int Registro_ID)
        {
            try
            {
                ObjetoControlInterno = new C_Registro_Inconsistencia_CI();
                SqlCommand Ver_Inconsistencias = new SqlCommand("select * from Registro_Inconsistencias_CI where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader inconsistencia = Ver_Inconsistencias.ExecuteReader();
                inconsistencia.Read();
                Usuario_ID = inconsistencia.GetInt32(2);
                Inconsistencia_ID = inconsistencia.GetInt32(1);
                ObjetoControlInterno.Id_Registro_Inconsistencia = inconsistencia.GetInt32(0);
                ObjetoControlInterno.Id_Inconsistencia_CI = inconsistencia.GetInt32(1);
                ObjetoControlInterno.Usuario_ID = inconsistencia.GetInt32(2);
                ObjetoControlInterno.Id_Responsable = inconsistencia.GetInt32(3);
                ObjetoControlInterno.Observaciones = inconsistencia.GetString(4);
                ObjetoControlInterno.Fecha = Convert.ToString(inconsistencia.GetDateTime(5));
                ObjetoControlInterno.Fecha_Registro = Convert.ToString(inconsistencia.GetDateTime(6));

                SqlCommand Ver_Usuarios = new SqlCommand("select usuario, Nombre from usuarios where Id_Usuarios = '" + Usuario_ID+ "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader usuario = Ver_Usuarios.ExecuteReader();
                SqlCommand Ver_TipoInconsistencia = new SqlCommand("select NombreInconsistenciaCI from Tipo_Inconsistencia_CI where Id_Inconsistencia_CI = '" + Inconsistencia_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Tipo = Ver_TipoInconsistencia.ExecuteReader();
                if (usuario.Read() && Tipo.Read())
                {                    
                    ObjetoControlInterno.Usuario = Convert.ToString(usuario.GetString(0));
                    ObjetoControlInterno.Nombre = Convert.ToString(usuario.GetString(1));
                    ObjetoControlInterno.TipoInconsistencia = Convert.ToString(Tipo.GetString(0));
                }
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoControlInterno;
        }
    }    
}
