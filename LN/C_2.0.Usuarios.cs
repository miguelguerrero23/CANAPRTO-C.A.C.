using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Usuarios
    {
        //Definición y encapsulamiento de Variables
        internal C_Usuarios ObjetoUsuarios { get; set; }        
        public int CódigoAgencia { get; set; }
        public int CódigoUsuario { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Agencia { get; set; }
        public string Agencia { get; set; }
        public string Nombresapellidos { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string TipoUsuario { get; set; }
        public string ControlInterno { get; set; }
        public string Novedades { get; set; }
        public string LavadoActivos { get; set; }
        public string Contabilidad { get; set; }
        public string Facturación { get; set; }
        public string ComitéEducación { get; set; }
        public string Estado { get; set; }
        public string ConsultaSQL { get; set; }
        public string NombreAgencia { get; set; }


        //Método para validar que los usuarios no se repitan en la base de datos 
        internal Boolean ValidarExistenciaUsuario(string Nombre)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from usuarios where nombre = '" + Nombre + "'",LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros de los Usuarios en la base de datos 
        internal void GuardarUsuarios()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Usuarios) + 1 from usuarios;",LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_Usuario = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_Usuario = 1;
                    //throw;
                }
                SqlCommand GuardarUsuario = new SqlCommand("insert into usuarios values ('" + Id_Usuario + "','" + Id_Agencia + "','" + Nombresapellidos + "','" + Usuario + "','" + Contraseña + "','" + TipoUsuario + "','" + ControlInterno + "','" + Novedades + "','" + LavadoActivos + "','" + Contabilidad + "','" + Facturación + "','" + ComitéEducación + "','" + Estado + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarUsuario.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El usuario se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        //Método para modificar los registros de los usuarios en la base de datos 
        internal void ModificarUsuarios()
        {
            try
            {
                SqlCommand Modificar = new SqlCommand("update usuarios set Id_Agencia = '" + Id_Agencia + "', Nombre = '" + Nombresapellidos + "', Usuario = '" + Usuario + "', Contraseña = '" + Contraseña + "', TipoUsuario = '" + TipoUsuario + "', Control_Interno = '" + ControlInterno + "', Novedades = '" + Novedades + "', Lavado_Activos = '" + LavadoActivos + "', Contabilidad = '" + Contabilidad + "', Facturación = '" + Facturación + "', Estado = '" + Estado + "', ComiteEducacion = '" + ComitéEducación + "' where Id_Usuarios = " + Id_Usuario, LN.C_Conexión.ConexiónCanapro());
                Modificar.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El usuario se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas" , "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        //Método para eliminar los registros de los usuarios en la base de datos 
        internal void EliminarUsuario(int Usuario_ID)
        {
            try
            {
                SqlCommand EliminarUsuario = new SqlCommand("delete from usuarios where Id_Usuarios = '" + Usuario_ID + "'",LN.C_Conexión.ConexiónCanapro());
                EliminarUsuario.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El usuario se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }
        
        //Método que realiza la consulta de las agencias en la base de datos "CANAPRO" y las almacena en una lista para llevarlas a un Combobox 
        internal System.Collections.ArrayList LlenarComboAgencias(System.Collections.ArrayList lista)
        {
            try
            {
                string NombreAgencia;
                SqlCommand consultar = new SqlCommand("select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = consultar.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datos.Read() == true)
                {
                    NombreAgencia = datos.GetString(0);
                    lista.Add(datos.GetString(0));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            return lista;
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
            }
            return lista;
        }

        //Método que realiza la consulta del usuario en la base de datos "CANAPRO" y las almacena en una lista para llevarlas a un Combobox 
        internal System.Collections.ArrayList LlenarComboUsuarios(System.Collections.ArrayList lista)
        {
            try
            {
                string Usuario;
                SqlCommand consultar = new SqlCommand("select Usuario from usuarios", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = consultar.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datos.Read() == true)
                {
                    Usuario = datos.GetString(0);
                    lista.Add(datos.GetString(0));
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas" , "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }
        
        //Método para llenar el textbox con el id de la agencia cuando se selecciona un item en el combobox agencias
        internal int Id_Agencias(string Agencia)
        {
            try
            {
                SqlCommand Recibir_Id_Agencia = new SqlCommand("select codigoagencia from Canapro.dbo.agencias where nombreagencia = '" + Agencia + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader CódigoNuevo = Recibir_Id_Agencia.ExecuteReader();
                CódigoNuevo.Read();
                CódigoAgencia = CódigoNuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoAgencia = 1;
            }            
            return CódigoAgencia;
        }

        //Método para llenar el textbox con el id del Usuario
        public int Código_Usuarios(int ID)
        {
            try
            {
                SqlCommand Recibir_Id_Usuario = new SqlCommand("select max(Id_usuarios) + 1 from usuarios", C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = Recibir_Id_Usuario.ExecuteReader();
                Id_Nuevo.Read();
                CódigoUsuario = Id_Nuevo.GetInt32(0);
                C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoUsuario = 1;
            }            
            return CódigoUsuario;
        }
        
        //Método para realizar consulta de un usuario por nombre en la BD 
        internal void ConsultarUsuariosPorNombre(DataGridView dtgHistorialUsuarios, string Consulta_Nombre)
        {
            try
            {
                ConsultaSQL = "select Id_Usuarios as '" + "Id" + "', (select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = Id_Agencia) as '" + "Agencia" + "', Nombre as '" + "Nombre de Usuario" + "', Usuario as '" + "Usuario" + "', Contraseña as '" + "Contraseña" + "', TipoUsuario as '" + "Tipo" + "', Control_Interno as '" + "CI" + "', Novedades as '" + "NV" + "', Lavado_Activos as '" + "LA" + "', Contabilidad as '" + "CT" + "', Facturación as '" + "FT" + "', ComiteEducacion as '" + "CE" + "', Estado as '" + "Estado" + "' from usuarios where Nombre like '" + "%" + Consulta_Nombre + "%" + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialUsuarios.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas" + Ex.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Método para realizar consulta de un usuario por Agencias en la BD 
        internal void ConsultarUsuariosPorAgencia(DataGridView dtgHistorialUsuarios, string Consulta_Agencia)
        {
            try
            {
                ConsultaSQL = "select Id_Usuarios as '" + "Id" + "', (select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = Id_Agencia) as '" + "Agencia" + "', Nombre as '" + "Nombre de Usuario" + "', Usuario as '" + "Usuario" + "', Contraseña as '" + "Contraseña" + "', TipoUsuario as '" + "Tipo" + "', Control_Interno as '" + "CI" + "', Novedades as '" + "NV" + "', Lavado_Activos as '" + "LA" + "', Contabilidad as '" + "CT" + "', Facturación as '" + "FT" + "', ComiteEducacion as '" + "CE" + "', Estado as '" + "Estado" + "' from usuarios where Id_Agencia = (select codigoagencia from Canapro.dbo.agencias where nombreagencia = '" + Consulta_Agencia + "')";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialUsuarios.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas" + Ex.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Método para realizar consulta de un usuario por usuario en la BD 
        internal void ConsultarUsuariosPorUsuario(DataGridView dtgHistorialUsuarios, string Consulta_Usuario)
        {
            try
            {
                ConsultaSQL = "select Id_Usuarios as '" + "Id" + "', (select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = Id_Agencia) as '" + "Agencia" + "', Nombre as '" + "Nombre de Usuario" + "', Usuario as '" + "Usuario" + "', Contraseña as '" + "Contraseña" + "', TipoUsuario as '" + "Tipo" + "', Control_Interno as '" + "CI" + "', Novedades as '" + "NV" + "', Lavado_Activos as '" + "LA" + "', Contabilidad as '" + "CT" + "', Facturación as '" + "FT" + "', ComiteEducacion as '" + "CE" + "', Estado as '" + "Estado" + "' from usuarios where usuario = '" + Consulta_Usuario +"'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialUsuarios.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas" + Ex.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Método para realizar consulta de un usuario por tipo de usuario en la BD 
        internal void ConsultarUsuariosPorTipo(DataGridView dtgHistorialUsuarios, string Consulta_Tipo_Usuario)
        {
            try
            {                
                ConsultaSQL = "select Id_Usuarios as '" + "Id" + "',(select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = Id_Agencia) as '" + "Agencia" + "', Nombre as '" + "Nombre de Usuario" + "', Usuario as '" + "Usuario" + "', Contraseña as '" + "Contraseña" + "', TipoUsuario as '" + "Tipo" + "', Control_Interno as '" + "CI" + "', Novedades as '" + "NV" + "', Lavado_Activos as '" + "LA" + "', Contabilidad as '" + "CT" + "', Facturación as '" + "FT" + "', ComiteEducacion as '" + "CE" + "', Estado as '" + "Estado" + "' from usuarios where TipoUsuario = '" + Consulta_Tipo_Usuario + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialUsuarios.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas" + Ex.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Método para realizar consulta de un usuario por Estado en la BD 
        internal void ConsultarUsuariosPorEstado(DataGridView dtgHistorialUsuarios, string Consulta_Estado_Usuario)
        {
            try
            {
                ConsultaSQL = "select Id_Usuarios as '" + "Id" + "', (select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = Id_Agencia) as '" + "Agencia" + "', Nombre as '" + "Nombre de Usuario" + "', Usuario as '" + "Usuario" + "', Contraseña as '" + "Contraseña" + "', LTRIM(RTRIM(TipoUsuario)) as '" + "Tipo" + "', LTRIM(RTRIM(Control_Interno)) as '" + "CI" + "', LTRIM(RTRIM(Novedades)) as '" + "NV" + "', LTRIM(RTRIM(Lavado_Activos)) as '" + "LA" + "', LTRIM(RTRIM(Contabilidad)) as '" + "CT" + "', LTRIM(RTRIM(Facturación)) as '" + "FT" + "', LTRIM(RTRIM(ComiteEducacion)) as '" + "CE" + "', LTRIM(RTRIM(Estado)) as '" + "Estado" + "' from usuarios where Estado = '" + Consulta_Estado_Usuario + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialUsuarios.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas" + Ex.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Método para traer la información de los usuarios y almacenarla en un objeto para que se cargue en el formulario Usuarios y permita su modificación
        internal C_Usuarios MostrarUsuarios(int Usuario_ID)
        {
            try
            {
                ObjetoUsuarios = new C_Usuarios();
                SqlCommand Ver_Usuarios = new SqlCommand("select * from usuarios where Id_Usuarios = ltrim(rtrim('"+Usuario_ID+"'))", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader user = Ver_Usuarios.ExecuteReader();
                user.Read();
                ObjetoUsuarios.Id_Usuario = user.GetInt32(0);
                ObjetoUsuarios.Id_Agencia = user.GetInt32(1);
                ObjetoUsuarios.Nombresapellidos = user.GetString(2);
                ObjetoUsuarios.Usuario = user.GetString(3);
                ObjetoUsuarios.Contraseña = user.GetString(4);
                ObjetoUsuarios.TipoUsuario = user.GetString(5);
                ObjetoUsuarios.ControlInterno = user.GetString(6);
                ObjetoUsuarios.Novedades = user.GetString(7);
                ObjetoUsuarios.LavadoActivos = user.GetString(8);
                ObjetoUsuarios.Contabilidad = user.GetString(9);
                ObjetoUsuarios.Facturación = user.GetString(10);
                ObjetoUsuarios.ComitéEducación = user.GetString(11);
                ObjetoUsuarios.Estado = user.GetString(12);
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas" + Ex.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ObjetoUsuarios;
        }

        //Método para consultar el nombre de la agencia a la que pertenece un usuario
        internal C_Usuarios ConsultarNombreAgencia(string id_agencia)
        {
            ObjetoUsuarios = new C_Usuarios();
            try
            {
                SqlCommand sql = new SqlCommand("select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = '" + id_agencia + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader reader = sql.ExecuteReader();
                reader.Read();
                ObjetoUsuarios.NombreAgencia = reader.GetString(0);
            }
            catch (Exception)
            {
                ObjetoUsuarios.NombreAgencia = "";
            }
            return ObjetoUsuarios;
        }


    }
}
