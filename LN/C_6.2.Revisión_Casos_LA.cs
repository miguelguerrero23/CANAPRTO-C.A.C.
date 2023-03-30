using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Revisión_Casos_LA
    {
        //Definición y encapsulamiento de variables
        public int Id_Revisión { get; set; }
        public int Id_RegistroLA { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_OrigenFondos { get; set; }
        public char Sí_Adjunta_evidencia { get; set; }
        public char No_Adjunta_evidencia { get; set; }
        public char Sí_Reporta_caso { get; set; }
        public char No_Reporta_caso { get; set; }
        public char Estado_Caso_Abierto { get; set; }
        public char Estado_Caso_Cerrado { get; set; }
        public char Estado_Caso_Pendiente { get; set; }
        public string Observaciones { get; set; }
        public string Fecha { get; set; }
        public int Id_Responsable { get; set; }
        internal C_Revisión_Casos_LA ObjetoConsulta { get; set; }
        public string ConsultaSQL { get; set; }
        //Variables datos del asociado
        public string Documento { get; set; }
        public string NombreIntegrado { get; set; }
        public string Dirección { get; set; }
        public string Teléfono { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        //Variables datos de la transacción
        public string NombreOF { get; set; }
        public string DetalleOF { get; set; }
        public string FechaRegistroOF { get; set; }
        public string ValorOF { get; set; }
        public string AgenciaRegistroOF { get; set; }
        public string NombreUsuario { get; set; }


        //Método para validar que los registros de las revisiones no se repitan en la base de datos
        internal Boolean ValidarExistenciaRevisiónLA(int Registro_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Revisión_Casos_LA where Id_Registro_LA = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
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
        internal void GuardarRevisiónLA()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Revisión) + 1 from Revisión_Casos_LA", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_Revisión = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_Revisión = 1;
                    //throw;
                }
                SqlCommand GuardarRevisiónLA = new SqlCommand("insert into Revisión_Casos_LA values ('" + Id_Revisión + "','" + Id_RegistroLA + "', '" + Id_Responsable + "', '" + Sí_Adjunta_evidencia + "','" + No_Adjunta_evidencia + "','" + Sí_Reporta_caso + "','" + No_Reporta_caso + "', '" + Estado_Caso_Abierto +"', '" + Estado_Caso_Cerrado +"', '" + Estado_Caso_Pendiente +"', '" + Fecha + "', '" + Observaciones +"')", LN.C_Conexión.ConexiónCanapro());
                GuardarRevisiónLA.ExecuteNonQuery();
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
        internal void ModificarRevisiónLA()
        {
            try
            {
                SqlCommand ModificarRevisiónLA = new SqlCommand("update Revisión_Casos_LA set Id_Revisión = '" + Id_Revisión + "', Id_Registro_LA = '" + Id_RegistroLA + "', Id_Usuario = '" + Id_Responsable + "', Adjunta_Evidencia_Si = '" + Sí_Adjunta_evidencia + "', Adjunta_Evidencia_No = '" + No_Adjunta_evidencia + "', Reportar_Caso_Si = '" + Sí_Reporta_caso + "', Reportar_Caso_No = '" + No_Reporta_caso + "', Estado_Caso_Abierto = '" + Estado_Caso_Abierto + "', Estado_Caso_Cerrado = '" + Estado_Caso_Cerrado + "', Estado_Caso_Pendiente = '" + Estado_Caso_Pendiente + "', Fecha = '" + Fecha + "', Observaciones = '" + Observaciones + "' where Id_Revisión = " + Id_Revisión, LN.C_Conexión.ConexiónCanapro());
                ModificarRevisiónLA.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El registro se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de las inconsistenciasen la base de datos 
        internal void EliminarRevisiónLA(int Registro_ID)
        {
            try
            {
                SqlCommand EliminarRevisiónLA = new SqlCommand("delete from Revisión_Casos_LA where Id_Revisión = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarRevisiónLA.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el id de la revisión
        public int Código_Registro(int ID)
        {
            try
            {
                SqlCommand Recibir_Id = new SqlCommand("select max(Id_Revisión) + 1 from Revisión_Casos_LA", C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = Recibir_Id.ExecuteReader();
                Id_Nuevo.Read();
                Id_Revisión = Id_Nuevo.GetInt32(0);
                C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                Id_Revisión = 1;
                //throw;
            }
            return Id_Revisión;
        }

        //Método para consultar el ID del usuario en la base de datos
        internal C_Revisión_Casos_LA ConsultarDatosUsuario(string usuario)
        {
            ObjetoConsulta = new C_Revisión_Casos_LA();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select id_usuarios from usuarios where usuario = '" + usuario + "'", LN.C_Conexión.ConexiónCanapro()); SqlDataReader user = ConsultaUsuario.ExecuteReader();

                if (user.Read())
                {
                    ObjetoConsulta.Id_Responsable = user.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoConsulta;
        }

        //Método para consultar los datos del usuario 
        internal C_Revisión_Casos_LA DatosUsuario(string documento)
        {
            Documento = documento;
            ObjetoConsulta = new C_Revisión_Casos_LA();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select nombreintegrado, direccion, telefono1, email, celular  from Canapro.dbo.nits where nit = '" + Documento + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read())
                {
                    ObjetoConsulta.NombreIntegrado = datos.GetString(0);
                    ObjetoConsulta.Dirección = datos.GetString(1);
                    ObjetoConsulta.Teléfono = datos.GetString(2);
                    ObjetoConsulta.Email = datos.GetString(3);
                    ObjetoConsulta.Celular = datos.GetString(4);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoConsulta;
        }

        //Método para consultar los datos de la transacción
        internal C_Revisión_Casos_LA DatosTransacción(int Registro_ID)
        {
            Id_RegistroLA = Registro_ID;            
            ObjetoConsulta = new C_Revisión_Casos_LA();
            try
            {
                SqlCommand ConsultaDatosRegistroLA = new SqlCommand("select * from Registro_LA where Id_Registro = '" + Id_RegistroLA + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader registro = ConsultaDatosRegistroLA.ExecuteReader();
                registro.Read();
                Id_OrigenFondos = registro.GetInt32(2);
                Id_Usuario = registro.GetInt32(3);
                ObjetoConsulta.Id_RegistroLA = registro.GetInt32(0);
                ObjetoConsulta.Documento = Convert.ToString(registro.GetInt32(1));
                ObjetoConsulta.Id_OrigenFondos = registro.GetInt32(2);
                ObjetoConsulta.Id_Usuario = registro.GetInt32(3);
                ObjetoConsulta.FechaRegistroOF = Convert.ToString(registro.GetDateTime(4));
                ObjetoConsulta.DetalleOF = registro.GetString(5);
                ObjetoConsulta.ValorOF = Convert.ToString(registro.GetDecimal(6));

                SqlCommand ConsultaDatosOF = new SqlCommand("select Nombre_OF from Origen_Fondos where Id_Origen_Fondos = '" + Id_OrigenFondos + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader OF = ConsultaDatosOF.ExecuteReader();
                OF.Read();
                ObjetoConsulta.NombreOF = OF.GetString(0);

                SqlCommand ConsultaDatosUsuario = new SqlCommand("select nombre from usuarios where id_usuarios = '" + Id_Usuario + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader user = ConsultaDatosUsuario.ExecuteReader();
                user.Read();
                ObjetoConsulta.NombreUsuario = user.GetString(0);

                SqlCommand ConsultaDatosAgencia = new SqlCommand("select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia = (select Id_Agencia from Usuarios where Id_Usuarios = '" + Id_Usuario + "')", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader agencia = ConsultaDatosAgencia.ExecuteReader();
                agencia.Read();
                ObjetoConsulta.AgenciaRegistroOF = agencia.GetString(0);
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoConsulta;
        }

        //Método para consultar los datos de la revisión
        internal C_Revisión_Casos_LA DatosRevisión(int Revisión_ID)
        {
            Id_Revisión = Revisión_ID;
            ObjetoConsulta = new C_Revisión_Casos_LA();
            try
            {
                SqlCommand ConsultaDatosRevisión = new SqlCommand("select * from revisión_casos_LA where id_revisión = '" + Id_Revisión + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader registro = ConsultaDatosRevisión.ExecuteReader();
                registro.Read();
                Id_RegistroLA = registro.GetInt32(1);
                Id_Usuario = registro.GetInt32(2);
                ObjetoConsulta.Id_Revisión = registro.GetInt32(0);
                ObjetoConsulta.Id_RegistroLA = registro.GetInt32(1);
                ObjetoConsulta.Id_Usuario = registro.GetInt32(2);
                ObjetoConsulta.Sí_Adjunta_evidencia = Convert.ToChar(registro.GetString(3));
                ObjetoConsulta.No_Adjunta_evidencia = Convert.ToChar(registro.GetString(4));
                ObjetoConsulta.Sí_Reporta_caso = Convert.ToChar(registro.GetString(5));
                ObjetoConsulta.No_Reporta_caso = Convert.ToChar(registro.GetString(6));
                ObjetoConsulta.Estado_Caso_Abierto = Convert.ToChar(registro.GetString(7));
                ObjetoConsulta.Estado_Caso_Cerrado = Convert.ToChar(registro.GetString(8));
                ObjetoConsulta.Estado_Caso_Pendiente = Convert.ToChar(registro.GetString(9));
                ObjetoConsulta.Fecha = Convert.ToString(registro.GetDateTime(10));
                ObjetoConsulta.Observaciones = registro.GetString(11);

                SqlCommand ConsultaDatosRegistroLA = new SqlCommand("select Fecha, detalleLA, valor_OF, (select Nombre_OF from Origen_Fondos where Origen_Fondos.Id_Origen_Fondos = Registro_LA.Id_Origen_Fondos), (select nombreagencia from Canapro.dbo.agencias where codigoagencia = (select Id_Agencia from Usuarios where Usuarios.Id_Usuarios = Registro_LA.Id_Usuarios)), (select nombre from usuarios where usuarios.id_usuarios = Registro_LA.id_usuarios), Id_nits from Registro_LA where Id_Registro = '" + Id_RegistroLA + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader registroLA = ConsultaDatosRegistroLA.ExecuteReader();
                registroLA.Read();
                ObjetoConsulta.FechaRegistroOF = Convert.ToString(registroLA.GetDateTime(0));
                ObjetoConsulta.DetalleOF = registroLA.GetString(1);
                ObjetoConsulta.ValorOF = Convert.ToString(registroLA.GetDecimal(2));
                ObjetoConsulta.NombreOF = registroLA.GetString(3);
                ObjetoConsulta.AgenciaRegistroOF = registroLA.GetString(4);
                ObjetoConsulta.NombreUsuario = registroLA.GetString(5);
                ObjetoConsulta.Documento = Convert.ToString(registroLA.GetInt32(6));
                Documento = Convert.ToString(registroLA.GetInt32(6));

                SqlCommand ConsultaDatos = new SqlCommand("select nombreintegrado, direccion, telefono1, email, celular  from Canapro.dbo.nits where nit = '" + Documento + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();
                datos.Read();
                ObjetoConsulta.NombreIntegrado = datos.GetString(0);
                ObjetoConsulta.Dirección = datos.GetString(1);
                ObjetoConsulta.Teléfono = datos.GetString(2);
                ObjetoConsulta.Email = datos.GetString(3);
                ObjetoConsulta.Celular = datos.GetString(4);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return ObjetoConsulta;
        }

        //Método para realizar consulta de las revisiones que tengan documento adjunto
        internal void ConsultarRevisiónConDocumentoAdjunto(DataGridView dtgRevisión, string texto)
        {
            try
            {
                if (texto.Equals("Casos con documento adjunto"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select id_revisión as '" + "Id Revisión" + "', id_Registro_LA as '" + "Id Registro" + "', Adjunta_Evidencia_Si as '" + "Doc. Adjunto" + "', Adjunta_Evidencia_No as '" + "Sin Doc. Adjunto" + "', Reportar_Caso_Si as '" + "Reportado" + "', Reportar_Caso_No as '" + "Sin Reportar" + "', Estado_Caso_Abierto as '" + "Estado Abierto" + "', Estado_Caso_Cerrado as '" + "Estado Cerrado" + "', Estado_Caso_Pendiente as '" + "Estado Pendiente" + "', Fecha, Observaciones, (select nombre from usuarios where id_usuarios = Id_Usuario) as '" + "Nombre de Usuario" + "' from Revisión_Casos_LA where Adjunta_Evidencia_Si = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgRevisión.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de las revisiones que no tengan documento adjunto
        internal void ConsultarRevisiónSinDocumentoAdjunto(DataGridView dtgRevisión, string texto)
        {
            try
            {
                if (texto.Equals("Casos sin documento adjunto"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select id_revisión as '" + "Id Revisión" + "', id_Registro_LA as '" + "Id Registro" + "', Adjunta_Evidencia_Si as '" + "Doc. Adjunto" + "', Adjunta_Evidencia_No as '" + "Sin Doc. Adjunto" + "', Reportar_Caso_Si as '" + "Reportado" + "', Reportar_Caso_No as '" + "Sin Reportar" + "', Estado_Caso_Abierto as '" + "Estado Abierto" + "', Estado_Caso_Cerrado as '" + "Estado Cerrado" + "', Estado_Caso_Pendiente as '" + "Estado Pendiente" + "', Fecha, Observaciones, (select nombre from usuarios where id_usuarios = Id_Usuario) as '" + "Nombre de Usuario" + "' from Revisión_Casos_LA where Adjunta_Evidencia_No = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgRevisión.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de las revisiones que esten reportadas
        internal void ConsultarRevisiónReportada(DataGridView dtgRevisión, string texto)
        {
            try
            {
                if (texto.Equals("Casos reportados"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select id_revisión as '" + "Id Revisión" + "', id_Registro_LA as '" + "Id Registro" + "', Adjunta_Evidencia_Si as '" + "Doc. Adjunto" + "', Adjunta_Evidencia_No as '" + "Sin Doc. Adjunto" + "', Reportar_Caso_Si as '" + "Reportado" + "', Reportar_Caso_No as '" + "Sin Reportar" + "', Estado_Caso_Abierto as '" + "Estado Abierto" + "', Estado_Caso_Cerrado as '" + "Estado Cerrado" + "', Estado_Caso_Pendiente as '" + "Estado Pendiente" + "', Fecha, Observaciones, (select nombre from usuarios where id_usuarios = Id_Usuario) as '" + "Nombre de Usuario" + "' from Revisión_Casos_LA where Reportar_Caso_Si = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgRevisión.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de las revisiones que no estén reportadas
        internal void ConsultarRevisiónSinReportar(DataGridView dtgRevisión, string texto)
        {
            try
            {
                if (texto.Equals("Casos sin reportar"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select id_revisión as '" + "Id Revisión" + "', id_Registro_LA as '" + "Id Registro" + "', Adjunta_Evidencia_Si as '" + "Doc. Adjunto" + "', Adjunta_Evidencia_No as '" + "Sin Doc. Adjunto" + "', Reportar_Caso_Si as '" + "Reportado" + "', Reportar_Caso_No as '" + "Sin Reportar" + "', Estado_Caso_Abierto as '" + "Estado Abierto" + "', Estado_Caso_Cerrado as '" + "Estado Cerrado" + "', Estado_Caso_Pendiente as '" + "Estado Pendiente" + "', Fecha, Observaciones , (select nombre from usuarios where id_usuarios = Id_Usuario) as '" + "Nombre de Usuario" + "'from Revisión_Casos_LA where Reportar_Caso_No = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgRevisión.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de las revisiones que estén en estado abierto
        internal void ConsultarRevisiónEstadoAbierto(DataGridView dtgRevisión, string texto)
        {
            try
            {
                if (texto.Equals("Casos en estado abierto"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select id_revisión as '" + "Id Revisión" + "', id_Registro_LA as '" + "Id Registro" + "', Adjunta_Evidencia_Si as '" + "Doc. Adjunto" + "', Adjunta_Evidencia_No as '" + "Sin Doc. Adjunto" + "', Reportar_Caso_Si as '" + "Reportado" + "', Reportar_Caso_No as '" + "Sin Reportar" + "', Estado_Caso_Abierto as '" + "Estado Abierto" + "', Estado_Caso_Cerrado as '" + "Estado Cerrado" + "', Estado_Caso_Pendiente as '" + "Estado Pendiente" + "', Fecha, Observaciones, (select nombre from usuarios where id_usuarios = Id_Usuario) as '" + "Nombre de Usuario" + "' from Revisión_Casos_LA where Estado_Caso_Abierto = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgRevisión.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de las revisiones que estén en estado cerrado
        internal void ConsultarRevisiónEstadoCerrado(DataGridView dtgRevisión, string texto)
        {
            try
            {
                if (texto.Equals("Casos en estado cerrado"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select id_revisión as '" + "Id Revisión" + "', id_Registro_LA as '" + "Id Registro" + "', Adjunta_Evidencia_Si as '" + "Doc. Adjunto" + "', Adjunta_Evidencia_No as '" + "Sin Doc. Adjunto" + "', Reportar_Caso_Si as '" + "Reportado" + "', Reportar_Caso_No as '" + "Sin Reportar" + "', Estado_Caso_Abierto as '" + "Estado Abierto" + "', Estado_Caso_Cerrado as '" + "Estado Cerrado" + "', Estado_Caso_Pendiente as '" + "Estado Pendiente" + "', Fecha, Observaciones, (select nombre from usuarios where id_usuarios = Id_Usuario) as '" + "Nombre de Usuario" + "' from Revisión_Casos_LA where Estado_Caso_Cerrado = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgRevisión.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de las revisiones que no estén en estado pendiente
        internal void ConsultarRevisiónEstadoPendiente(DataGridView dtgRevisión, string texto)
        {
            try
            {
                if (texto.Equals("Casos en estado pendiente"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select id_revisión as '" + "Id Revisión" + "', id_Registro_LA as '" + "Id Registro" + "', Adjunta_Evidencia_Si as '" + "Doc. Adjunto" + "', Adjunta_Evidencia_No as '" + "Sin Doc. Adjunto" + "', Reportar_Caso_Si as '" + "Reportado" + "', Reportar_Caso_No as '" + "Sin Reportar" + "', Estado_Caso_Abierto as '" + "Estado Abierto" + "', Estado_Caso_Cerrado as '" + "Estado Cerrado" + "', Estado_Caso_Pendiente as '" + "Estado Pendiente" + "', Fecha, Observaciones, (select nombre from usuarios where id_usuarios = Id_Usuario) as '" + "Nombre de Usuario" + "' from Revisión_Casos_LA where Estado_Caso_Pendiente = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgRevisión.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de las revisiones por mes
        internal void ConsultarRevisiónPorFecha(DataGridView dtgRevisión, string FechaDesde, string FechaHasta)
        {
            try
            {
                ConsultaSQL = "select id_revisión as '" + "Id Revisión" + "', id_Registro_LA as '" + "Id Registro" + "', Adjunta_Evidencia_Si as '" + "Doc. Adjunto" + "', Adjunta_Evidencia_No as '" + "Sin Doc. Adjunto" + "', Reportar_Caso_Si as '" + "Reportado" + "', Reportar_Caso_No as '" + "Sin Reportar" + "', Estado_Caso_Abierto as '" + "Estado Abierto" + "', Estado_Caso_Cerrado as '" + "Estado Cerrado" + "', Estado_Caso_Pendiente as '" + "Estado Pendiente" + "', Fecha, Observaciones, (select nombre from usuarios where id_usuarios = Id_Usuario) as '" + "Nombre de Usuario" + "' from Revisión_Casos_LA where Fecha between '" + FechaDesde +"' and '" + FechaHasta +"'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgRevisión.DataSource = tabla;
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
