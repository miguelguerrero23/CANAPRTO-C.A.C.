using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Registro_LA
    {
        public int Código { get; set; }
        public int CódigoInconsistencia { get; set; }
        public int Id_Registro { get; set; }
        public int Id_Nits { get; set; }
        public int Id_OrigenFondos { get; set; }
        public int Id_Usuario { get; set; }
        public string Fecha { get; set; }
        public string Detalle_LA { get; set; }
        public decimal Valor_OF { get; set; }
        public byte[] Documento_adjunto { get; set; }
        public string RutaArchivo { get; set; }
        public int Usuario_ID { get; set; }
        public string NombreAgencia { get; set; }
        public string NombreIntegrado { get; set; }
        public string Dirección { get; set; }
        public string NombreOF { get; set; }
        internal C_Registro_LA ObjetoConsulta { get; set; }
        public string Fecha_Registro { get; set; }
        public string ConsultaSQL { get; set; }
        public string User { get; set; }


        //Método para validar que los registros de las inconsistencias no se repitan en la base de datos
        internal Boolean ValidarExistenciaRegistroLavadoActivos(int Registro_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Registro_LA where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
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
        internal void GuardarRegistroLavadoDeActivos()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Registro) + 1 from Registro_LA", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_Registro = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_Registro = 1;
                    //throw;
                }
                SqlCommand GuardarRegistroLavadoDeActivos = new SqlCommand("insert into Registro_LA values ('" + Id_Registro + "','" + Id_Nits + "', '" + Id_OrigenFondos + "', '" + Id_Usuario + "', '" + Fecha + "','" + Detalle_LA + "','" + Valor_OF + "', '" + RutaArchivo + "', '" + Fecha_Registro + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarRegistroLavadoDeActivos.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros de las inconsistencias en la base de datos 
        internal void ModificarRegistroLavadoDeActivos()
        {
            try
            {
                SqlCommand ModificarRegistroLavadoDeActivos = new SqlCommand("update Registro_LA set Id_Registro = '" + Id_Registro + "', Id_Nits = '" + Id_Nits + "', Id_Origen_Fondos = '" + Id_OrigenFondos + "', Id_Usuarios = '" + Id_Usuario + "', Fecha = '" + Fecha + "', DetalleLA = '" + Detalle_LA + "', Valor_OF = '" + Valor_OF + "', Ruta_Archivo = '" + RutaArchivo + "', Fecha_Registro = '" + Fecha_Registro + "' where Id_Registro = " + Id_Registro, LN.C_Conexión.ConexiónCanapro());
                ModificarRegistroLavadoDeActivos.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El registro se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        //Método para eliminar los registros de las inconsistenciasen la base de datos 
        internal void EliminarRegistroLavadoDeActivos(int Registro_ID)
        {
            try
            {
                SqlCommand EliminarRegistroLavadoDeActivos = new SqlCommand("delete from Registro_LA where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarRegistroLavadoDeActivos.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);               
            }
        }        

        //Método que realiza la consulta del nombre del origen de fondos en la base de datos y las almacena en una lista para llevarlas a un Combobox 
        internal System.Collections.ArrayList LlenarComboOrigenDeFondos(System.Collections.ArrayList lista)
        {
            try
            {
                string NombreOrigenFondos;
                SqlCommand consultar = new SqlCommand("select Nombre_OF from Origen_Fondos", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = consultar.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datos.Read() == true)
                {
                    NombreOrigenFondos = datos.GetString(0);
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
        public int Código_Registro(int ID)
        {
            try
            {
                SqlCommand Recibir_Id = new SqlCommand("select max(Id_Registro) + 1 from Registro_LA", C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = Recibir_Id.ExecuteReader();
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

        //Método para llenar el textbox con el id de la inconsistencia cuando se selecciona un item de inconsistencia en el formulario de registro lavado de activos
        internal int Id_Inconsistencias(string Inconsistencia)
        {
            try
            {
                SqlCommand Recibir_Id_Inconsistencia = new SqlCommand("select Id_Origen_Fondos from Origen_Fondos where Nombre_OF = '" + Inconsistencia + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para llenar el textbox con el código de origen de fondos cuando se selecciona un item de origen de fondos en el formulario de registro de Lavado de Activos
        internal int Código_OrigenFondos(string OrigenFondos)
        {
            try
            {
                SqlCommand Recibir_CódigoOrigenFondos= new SqlCommand("select Id_Origen_Fondos from Origen_Fondos where Nombre_OF = '" + OrigenFondos + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader CódigoNuevo = Recibir_CódigoOrigenFondos.ExecuteReader();
                CódigoNuevo.Read();
                Id_OrigenFondos = CódigoNuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                Id_OrigenFondos = 1;
                //throw;
            }
            return Id_OrigenFondos;
        }

        //Método que realiza consulta a la tabla Registro LA y almacena la información en un objeto para luego cargarla en el formulario de registro y permitir modificar y eliminar los registros
        internal C_Registro_LA MostrarInconsistencias(int Registro_ID)
        {
            try
            {
                ObjetoConsulta = new C_Registro_LA();
                SqlCommand Ver_Inconsistencias = new SqlCommand("select * from Registro_LA where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader inconsistencia = Ver_Inconsistencias.ExecuteReader();
                inconsistencia.Read();
                int Usuario_ID = inconsistencia.GetInt32(3);
                int Inconsistencia_ID = inconsistencia.GetInt32(2);
                int Nit = inconsistencia.GetInt32(1);
                ObjetoConsulta.Id_Registro = inconsistencia.GetInt32(0);
                ObjetoConsulta.Id_Nits = inconsistencia.GetInt32(1);
                ObjetoConsulta.Id_OrigenFondos = inconsistencia.GetInt32(2);
                ObjetoConsulta.Id_Usuario = inconsistencia.GetInt32(3);
                ObjetoConsulta.Fecha = Convert.ToString(inconsistencia.GetDateTime(4));
                ObjetoConsulta.Detalle_LA = inconsistencia.GetString(5);
                ObjetoConsulta.Valor_OF = inconsistencia.GetDecimal(6);
                ObjetoConsulta.RutaArchivo = inconsistencia.GetString(7);
                ObjetoConsulta.Fecha_Registro = Convert.ToString(inconsistencia.GetDateTime(8));
                LN.C_Conexión.ConexiónCanapro().Close();

                SqlCommand Ver_Usuarios = new SqlCommand("select ltrim(rtrim(nombreagencia)) , nombreintegrado, Canapro.dbo.nits.direccion from Canapro.dbo.agencias, Canapro.dbo.nits where codigoagencia = (select id_agencia from Usuarios where Id_Usuarios = '" + Usuario_ID + "') and nit = '" + Nit + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader usuario = Ver_Usuarios.ExecuteReader();
                usuario.Read();
                ObjetoConsulta.NombreAgencia = usuario.GetString(0);
                ObjetoConsulta.NombreIntegrado = usuario.GetString(1);
                ObjetoConsulta.Dirección = usuario.GetString(2);
                LN.C_Conexión.ConexiónCanapro().Close();

                SqlCommand Ver_TipoInconsistencia = new SqlCommand("select Nombre_OF from Origen_Fondos where Id_Origen_Fondos = '" + Inconsistencia_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Tipo = Ver_TipoInconsistencia.ExecuteReader();
                Tipo.Read();
                ObjetoConsulta.NombreOF = Tipo.GetString(0);                
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); throw;
            }
            return ObjetoConsulta;
        }

        //Método para consultar el ID del usuario y a qué agencia pertenece
        internal C_Registro_LA ConsultaUsuarioAgencia(string usuario)
        {
            ObjetoConsulta = new C_Registro_LA();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select id_usuarios from usuarios where usuario = '" + usuario + "'", LN.C_Conexión.ConexiónCanapro());
                SqlCommand ConsultaAgencia = new SqlCommand("select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = (select id_agencia from Usuarios where usuario = '" + usuario + "')", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader user = ConsultaUsuario.ExecuteReader();
                SqlDataReader agencia = ConsultaAgencia.ExecuteReader();

                if (user.Read() == true && agencia.Read() == true)
                {
                    ObjetoConsulta.Usuario_ID = user.GetInt32(0);
                    ObjetoConsulta.NombreAgencia = agencia.GetString(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoConsulta;
        }

        //Método para consultar el ID del usuario y a qué agencia pertenece
        internal C_Registro_LA DatosUsuario(string documento)
        {
            ObjetoConsulta = new C_Registro_LA();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select nombreintegrado, direccion from Canapro.dbo.nits where nit = '" + documento + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    ObjetoConsulta.NombreIntegrado = datos.GetString(0);
                    ObjetoConsulta.Dirección = datos.GetString(1);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoConsulta;
        }

        //Método para validar que el documento del asociado existe en la base de datos
        internal Boolean ValidarDocumentoAsociado(string documento)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Canapro.dbo.nits where nit = '" + documento + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para realizar consulta de informe de lavado de activos en la BD
        internal void ConsultarInformePorFecha(DataGridView DtgInformeLAFT, string FechaDesde, string FechaHasta)
        {
            try
            {
                ConsultaSQL = "select count(*) as '" + "Cant." + "', (select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = (select Id_Agencia from Usuarios where Id_Usuarios = Registro_LA.Id_Usuarios)) as Agencia, (select Nombre_OF from Origen_Fondos where Id_Origen_Fondos = Registro_LA.Id_Origen_Fondos) as Origen, sum(Valor_OF) as ValorTotal from Registro_LA where fecha between '" + FechaDesde + "' and '" + FechaHasta + "' group by Id_Usuarios, Id_Origen_fondos order by Id_Usuarios";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                DtgInformeLAFT.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para consultar cargar el usuario en el formulario historial LA
        internal C_Registro_LA ConsultaUsuarioHLA(string usuario)
        {
            ObjetoConsulta = new C_Registro_LA();
            try
            {
                ObjetoConsulta.User = usuario;
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoConsulta;
        }
    }
}
