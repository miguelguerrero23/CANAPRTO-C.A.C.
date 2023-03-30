using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Tipo_Inconsistencia_CI
    {
        //Definición y encapsulamiento de variables
        C_Tipo_Inconsistencia_CI ObjetoInconsistencia { get; set; }
        private readonly DataTable tabla;
        public int Código { get; set; }
        public int Id_inconsistencia { get; set; }
        public string Nombre_Inconsistencia { get; set; }
        public string ConsultaSQL { get; set; }

        //Método para validar que los usuarios no se repitan en la base de datos "CANAPRO"
        internal Boolean ValidarExistenciaDeInconsistencia(string Nombre)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Tipo_Inconsistencia_CI where NombreInconsistenciaCI = '" + Nombre + "'", LN.C_Conexión.ConexiónCanapro());
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
                
        //Método para guardar los registros de los Usuarios en la base de datos "CANAPRO"
        internal void GuardarInconsistencia()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Inconsistencia_CI) + 1 from Tipo_Inconsistencia_CI;", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_inconsistencia = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_inconsistencia = 1;
                    //throw;
                }
                SqlCommand GuardarInconsistencia = new SqlCommand("insert into Tipo_Inconsistencia_CI values ('" + Id_inconsistencia + "','" + Nombre_Inconsistencia + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarInconsistencia.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("La Inconsistencia se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }        

        //Método para modificar los registros de los usuarios en la base de datos "CANAPRO"
        internal void ModificarInconsistencia()
         {
             try
             {
                 SqlCommand ModificarInconsistencia = new SqlCommand("update Tipo_Inconsistencia_CI set Id_Inconsistencia_CI = '" + Id_inconsistencia + "', NombreInconsistenciaCI = '" + Nombre_Inconsistencia + "' where Id_Inconsistencia_CI = " + Id_inconsistencia, LN.C_Conexión.ConexiónCanapro());
                 ModificarInconsistencia.ExecuteNonQuery();
                 C_Conexión.ConexiónCanapro();
                 MessageBox.Show("La inconsistencia se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
             catch (Exception)
             {
                 MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 throw;
             }
         }

         //Método para eliminar los registros de los usuarios en la base de datos "CANAPRO"
         internal void EliminarInconsistencia(int Inconsistencia_ID)
         {
             try
             {
                 SqlCommand EliminarInconsistencia = new SqlCommand("delete from Tipo_Inconsistencia_CI where Id_Inconsistencia_CI = '" + Inconsistencia_ID + "'", LN.C_Conexión.ConexiónCanapro());
                 EliminarInconsistencia.ExecuteNonQuery();
                 C_Conexión.ConexiónCanapro().Close();
                 MessageBox.Show("La inconsistencia se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
             catch (Exception)
             {
                 MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 throw;
             }
         }

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dtgTInconsistenciaCI)
        {
            try
            {
                ConsultaSQL = "select Id_Inconsistencia_Ci as '" + "Código" + "', NombreInconsistenciaCI as'" + "Nombre de Inconsistencia" + "' from Tipo_Inconsistencia_CI";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgTInconsistenciaCI.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el id del Usuario
        public int LlenarCódigoInconsistencia(int Código)
        {
            try
            {
                SqlCommand Recibir_Id_Usuario = new SqlCommand("select max(Id_Inconsistencia_CI) + 1 from Tipo_Inconsistencia_CI", C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = Recibir_Id_Usuario.ExecuteReader();
                Id_Nuevo.Read();
                Código = Id_Nuevo.GetInt32(0);
                C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                Código = 1;
                //throw;
            }
            return Código;
        }

        //Método para consultar la información de las inconsistencias y almacenarla en un objeto para cargarla en el formulario de tipos de inconsistencia
        internal C_Tipo_Inconsistencia_CI MostrarInconsistencias(int Inconsistencia_ID)
        {
            try
            {
                ObjetoInconsistencia = new C_Tipo_Inconsistencia_CI();                
                SqlCommand Ver_Inconsistencias = new SqlCommand("select * from Tipo_Inconsistencia_CI where Id_Inconsistencia_CI = '" + Inconsistencia_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Inconsistencia = Ver_Inconsistencias.ExecuteReader();
                if (Inconsistencia.Read())
                {
                    ObjetoInconsistencia.Id_inconsistencia = Inconsistencia.GetInt32(0);
                    ObjetoInconsistencia.Nombre_Inconsistencia = Inconsistencia.GetString(1);
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoInconsistencia;
        }

        //Método que realiza la consulta de las inconsistencias en la base de datos y las almacena en una lista para llevarlas a un Combobox 
        internal System.Collections.ArrayList LlenarComboInconsistencias(System.Collections.ArrayList lista)
        {
            try
            {
                string Inconsistencias;
                SqlCommand consultar = new SqlCommand("select NombreInconsistenciaCI from Tipo_Inconsistencia_CI", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = consultar.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datos.Read() == true)
                {
                    Inconsistencias = datos.GetString(0);
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

        //Método para realizar consulta de una inconsistencia por nombre de usuario en la BD
        internal void ConsultarInconsistenciasPorNombreDeUsuario(DataGridView dtgHistorialInconsistencias, string Consulta_Nombre)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', (select nombreinconsistenciaCI from Tipo_Inconsistencia_CI where Tipo_Inconsistencia_CI.Id_Inconsistencia_CI = Registro_Inconsistencias_CI.Id_Inconsistencia_CI) as '" + "Inconsistencia" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_Inconsistencias_CI.Id_usuarios) as '" + "Nombre de Usuario" + "', Observaciones as '" + "Observaciones" + "', Fecha as '" + "Fecha Inconsistencia" + "', (select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = (select Id_Agencia from usuarios where usuarios.Id_Usuarios = Registro_Inconsistencias_CI.Id_usuarios)) as '" + "Agencia" + "', Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Responsable) as '" + "Responsable" + "' from Registro_Inconsistencias_CI where Id_Usuarios = (select Id_Usuarios from usuarios where Nombre = '" + Consulta_Nombre + "')";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de una inconsistencia por nombre de Agencia en la BD
        internal void ConsultarInconsistenciasPorAgencia(DataGridView dtgHistorialInconsistencias, string Consulta_Agencia)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', (select nombreinconsistenciaCI from Tipo_Inconsistencia_CI where Tipo_Inconsistencia_CI.Id_Inconsistencia_CI = Registro_Inconsistencias_CI.Id_Inconsistencia_CI) as '" + "Inconsistencia" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_Inconsistencias_CI.Id_usuarios) as '" + "Nombre de Usuario" + "', Observaciones as '" + "Observaciones" + "', Fecha as '" + "Fecha Inconsistencia" + "' , (select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = (select Id_Agencia from usuarios where usuarios.Id_Usuarios = Registro_Inconsistencias_CI.Id_usuarios)) as '" + "Agencia" + "', Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Responsable) as '" + "Responsable" + "' from Registro_Inconsistencias_CI where Id_Usuarios in (select Id_Usuarios from usuarios where Id_agencia in (select codigoagencia from CANAPRO.dbo.agencias where nombreagencia = '" + Consulta_Agencia + "'))";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de una inconsistencia por usuario en la BD
        internal void ConsultarInconsistenciasPorUsuario(DataGridView dtgHistorialInconsistencias, string Consulta_Usuario)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', (select nombreinconsistenciaCI from Tipo_Inconsistencia_CI where Tipo_Inconsistencia_CI.Id_Inconsistencia_CI = Registro_Inconsistencias_CI.Id_Inconsistencia_CI) as '" + "Inconsistencia" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_Inconsistencias_CI.Id_usuarios) as '" + "Nombre de Usuario" + "', Observaciones as '" + "Observaciones" + "', Fecha as '" + "Fecha Inconsistencia" + "', (select ltrim(rtrim(nombreagencia))from Canapro.dbo.agencias where codigoagencia = (select Id_Agencia from usuarios where usuarios.Id_Usuarios = Registro_Inconsistencias_CI.Id_usuarios)) as '" + "Agencia" + "' , Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Responsable) as '" + "Responsable" + "' from Registro_Inconsistencias_CI where Id_Usuarios = (select Id_Usuarios from usuarios where usuario = '" + Consulta_Usuario + "')";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de una inconsistencia por tipo de inconsistencia en la BD
        internal void ConsultarInconsistenciasPorTipoDeInconsistencia(DataGridView dtgHistorialInconsistencias, string Consulta_Tipo_Inconsistencia)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', (select nombreinconsistenciaCI from Tipo_Inconsistencia_CI where Tipo_Inconsistencia_CI.Id_Inconsistencia_CI = Registro_Inconsistencias_CI.Id_Inconsistencia_CI) as '" + "Inconsistencia" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_Inconsistencias_CI.Id_usuarios) as '" + "Nombre de Usuario" + "', Observaciones as '" + "Observaciones" + "', Fecha as '" + "Fecha Inconsistencia" + "', (select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = (select Id_Agencia from usuarios where usuarios.Id_Usuarios = Registro_Inconsistencias_CI.Id_usuarios)) as '" + "Agencia" + "' , Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Responsable) as '" + "Responsable" + "' from Registro_Inconsistencias_CI where Id_Inconsistencia_CI = (select Id_Inconsistencia_CI from Tipo_Inconsistencia_CI where NombreInconsistenciaCI = '" + Consulta_Tipo_Inconsistencia + "')";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de una inconsistencia por fecha en la BD
        internal void ConsultarInconsistenciasPorFecha(DataGridView dtgHistorialInconsistencias, string FechaDesde, string FechaHasta)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', (select nombreinconsistenciaCI from Tipo_Inconsistencia_CI where Tipo_Inconsistencia_CI.Id_Inconsistencia_CI = Registro_Inconsistencias_CI.Id_Inconsistencia_CI) as '" + "Inconsistencia" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_Inconsistencias_CI.Id_usuarios) as '" + "Nombre de Usuario" + "', Observaciones as '" + "Observaciones" + "', Fecha as '" + "Fecha Inconsistencia" + "', (select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = (select Id_Agencia from usuarios where usuarios.Id_Usuarios = Registro_Inconsistencias_CI.Id_usuarios)) as '" + "Agencia" + "' , Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Responsable) as '" + "Responsable" + "' from Registro_Inconsistencias_CI where fecha between '" + FechaDesde + "' and '" + FechaHasta + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

    }
}
