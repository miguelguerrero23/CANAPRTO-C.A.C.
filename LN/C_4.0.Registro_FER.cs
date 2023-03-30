using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Registro_FER
    {
        //Definición de varibales y encapsulamiento de variables
        public int Id_Registro { get; set; }
        public int Id_Usuario { get; set; }
        public string NúmeroCédula { get; set; }
        public int Nit { get; set; } 
        public char DígitoVerificación { get; set; }
        public string CódigoCanapro { get; set; }
        public string TipoNovedad { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public decimal ValorMensual { get; set; }
        public decimal ValorTotal { get; set; }
        public string Fecha { get; set; }
        internal C_Registro_FER ObjetoConsulta { get; set; }
        public string NombreIntegrado { get; set; }
        public string ConsultaSQL { get; set; }
        public string MesNovedad { get; set; }

        //Método para validar que los registros de FER no se repitan en la base de datos
        internal Boolean ValidarExistenciaFER(int Registro_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Registro_FER where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
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
         
        //Método para guardar los registros FER en la base de datos 
        internal void GuardarRegistroFER()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Registro) + 1 from Registro_FER", LN.C_Conexión.ConexiónCanapro());
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
                SqlCommand GuardarRegistroFER = new SqlCommand("insert into Registro_FER  values ('" + Id_Registro + "','" + Id_Usuario + "', '" + NúmeroCédula + "','" + Nit + "','" + DígitoVerificación + "','" + CódigoCanapro + "', '" + TipoNovedad + "', '" + FechaInicio + "', '" + FechaFin + "', '" + ValorMensual + "', '" + ValorTotal + "', '" + Fecha + "','" + MesNovedad + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarRegistroFER.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros FER en la base de datos 
        internal void ModificarRegistroFER()
        {
            try
            {
                SqlCommand ModificarRegistroFER = new SqlCommand("update Registro_FER set Id_Registro = '" + Id_Registro + "', Id_Usuario = '" + Id_Usuario + "', NúmeroCédula = '" + NúmeroCédula + "', NIT = '" + Nit + "', DígitoVerificación = '" + DígitoVerificación + "', CódigoCanapro = '" + CódigoCanapro + "', TipoNovedad = '" + TipoNovedad + "', FechaInicio = '" + FechaInicio + "', FechaFin = '" + FechaFin + "', ValorMensual = '" + ValorMensual + "', ValorTotal = '" + ValorTotal + "', FechaRegistro = '" + Fecha + "', MesNovedad = '" + MesNovedad + "' where Id_Registro = " + Id_Registro, LN.C_Conexión.ConexiónCanapro());
                ModificarRegistroFER.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El registro se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros FER en la base de datos 
        internal void EliminarRegistroFER(int Registro_ID)
        {
            try
            {
                Id_Registro = Registro_ID;
                SqlCommand EliminarRegistroFER = new SqlCommand("delete from Registro_FER where Id_Registro = '" + Id_Registro + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarRegistroFER.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el id del registro
        public int Cargar_IdRegistro()
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select max(Id_Registro) + 1 from Registro_FER", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = consulta.ExecuteReader();
                Id_Nuevo.Read();
                Id_Registro = Id_Nuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                Id_Registro = 1;
                //throw;
            }
            return Id_Registro;
        }

        //Método para consultar el ID del usuario en la base de datos
        internal C_Registro_FER ConsultarDatosUsuario(string usuario)
        {
            ObjetoConsulta = new C_Registro_FER();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select id_usuarios from usuarios where usuario = '" + usuario + "'", LN.C_Conexión.ConexiónCanapro()); SqlDataReader user = ConsultaUsuario.ExecuteReader();

                if (user.Read())
                {
                    ObjetoConsulta.Id_Usuario = user.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoConsulta;
        }

        //Método para consultar la fecha de ingreso y retiro en la base de datos
        internal C_Registro_FER ConsultarFechas(string Mes)
        {
            ObjetoConsulta = new C_Registro_FER();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select Fecha_Ingreso, Fecha_Retiro from Configuración_Fechas where mes = '" + Mes + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaUsuario.ExecuteReader();
                datos.Read();
                ObjetoConsulta.FechaInicio = Convert.ToString(datos.GetDateTime(0));
                ObjetoConsulta.FechaFin = Convert.ToString(datos.GetDateTime(1));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ObjetoConsulta;
        }

        //Método para consultar el nombre del usuario
        internal C_Registro_FER DatosUsuario(string documento)
        {
            ObjetoConsulta = new C_Registro_FER();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(nombreintegrado)) from Canapro.dbo.nits where nit = '" + documento + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    ObjetoConsulta.NombreIntegrado = datos.GetString(0);
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
                
        //Método para realizar consulta de los registros FER por número de cédula
        internal void ConsultarRegistroFERPorCédula(DataGridView dtgHistorialFER, string cédula)
        {
            try
            {
                ConsultaSQL = "select NIT, DígitoVerificación as '" + "Dígito Verificación" + "', TipoNovedad as '" + "Tipo Novedad" + "', NúmeroCédula as '" + "N° Cédula" + "', CódigoCanapro as '" + "Código Canapro" + "', MesNovedad as '" + "Mes Novedad" + "', FechaInicio as '" + "Fecha Inicio" + "', FechaFin as '" + "Fecha Fin" + "', ValorMensual as '" + "Valor Mensual" + "', ValorTotal as '" + "Valor Total" + "', id_registro as '" + "Id Registro" + "', FechaRegistro, (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_FER where NúmeroCédula = '" + cédula + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFER.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar cconsulta de los registros FER  por código de canapro
        internal void ConsultarRegistroFERPorCódigo(DataGridView dtgHistorialFER, string código)
        {
            try
            {
                ConsultaSQL = "select NIT, DígitoVerificación as '" + "Dígito Verificación" + "', TipoNovedad as '" + "Tipo Novedad" + "', NúmeroCédula as '" + "N° Cédula" + "', CódigoCanapro as '" + "Código Canapro" + "', MesNovedad as '" + "Mes Novedad" + "', FechaInicio as '" + "Fecha Inicio" + "', FechaFin as '" + "Fecha Fin" + "', ValorMensual as '" + "Valor Mensual" + "', ValorTotal as '" + "Valor Total" + "', id_registro as '" + "Id Registro" + "', FechaRegistro, (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_FER where CódigoCanapro = '" + código + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFER.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar cconsulta de los registros FER  por usuario
        internal void ConsultarRegistroFERPorUsuario(DataGridView dtgHistorialFER, string usuario)
        {
            try
            {
                ConsultaSQL = "select NIT, DígitoVerificación as '" + "Dígito Verificación" + "', TipoNovedad as '" + "Tipo Novedad" + "', NúmeroCédula as '" + "N° Cédula" + "', CódigoCanapro as '" + "Código Canapro" + "', MesNovedad as '" + "Mes Novedad" + "', FechaInicio as '" + "Fecha Inicio" + "', FechaFin as '" + "Fecha Fin" + "', ValorMensual as '" + "Valor Mensual" + "', ValorTotal as '" + "Valor Total" + "', id_registro as '" + "Id Registro" + "', FechaRegistro, (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_FER where Id_Usuario = (select id_usuarios from usuarios where usuario = '" + usuario + "')";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFER.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar cconsulta de los registros FER  tipo de novedad
        internal void ConsultarRegistroFERPorTipoDeNovedad(DataGridView dtgHistorialFER, string TipoNovedad)
        {
            try
            {
                ConsultaSQL = "select NIT, DígitoVerificación as '" + "Dígito Verificación" + "', TipoNovedad as '" + "Tipo Novedad" + "', NúmeroCédula as '" + "N° Cédula" + "', CódigoCanapro as '" + "Código Canapro" + "', MesNovedad as '" + "Mes Novedad" + "', FechaInicio as '" + "Fecha Inicio" + "', FechaFin as '" + "Fecha Fin" + "', ValorMensual as '" + "Valor Mensual" + "', ValorTotal as '" + "Valor Total" + "', id_registro as '" + "Id Registro" + "', FechaRegistro, (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_FER where TipoNovedad = '" + TipoNovedad + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFER.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar cconsulta de los registros FER  por intervalo de fecha
        internal void ConsultarRegistroFERPorFecha(DataGridView dtgHistorialFER, string FechaDesde, string FechaHasta)
        {
            try
            {
                ConsultaSQL = "select NIT, DígitoVerificación as '" + "Dígito Verificación" + "', TipoNovedad as '" + "Tipo Novedad" + "', NúmeroCédula as '" + "N° Cédula" + "', CódigoCanapro as '" + "Código Canapro" + "', MesNovedad as '" + "Mes Novedad" + "', FechaInicio as '" + "Fecha Inicio" + "', FechaFin as '" + "Fecha Fin" + "', ValorMensual as '" + "Valor Mensual" + "', ValorTotal as '" + "Valor Total" + "', id_registro as '" + "Id Registro" + "', FechaRegistro, (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_FER where FechaRegistro between '" + FechaDesde + "' and '" + FechaHasta + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFER.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar cconsulta de los registros FER  por intervalo Mes de registro de novedad
        internal void ConsultarRegistroFERPorMesNovedad(DataGridView dtgHistorialFER, string Mesnovedad)
        {
            try
            {
                ConsultaSQL = "select NIT, DígitoVerificación as '" + "Dígito Verificación" + "', TipoNovedad as '" + "Tipo Novedad" + "', NúmeroCédula as '" + "N° Cédula" + "', CódigoCanapro as '" + "Código Canapro" + "', MesNovedad as '" + "Mes Novedad" + "', FechaInicio as '" + "Fecha Inicio" + "', FechaFin as '" + "Fecha Fin" + "', ValorMensual as '" + "Valor Mensual" + "', ValorTotal as '" + "Valor Total" + "', id_registro as '" + "Id Registro" + "', FechaRegistro, (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_FER where MesNovedad = '" + Mesnovedad + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFER.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }


        //Método para consultar los datos del registro fer y almacenarlos en un objeto
        internal C_Registro_FER CargarDatosRegistroFER(int RegistroFER)
        {
            ObjetoConsulta = new C_Registro_FER();
            try
            {
                Id_Registro = RegistroFER;
                SqlCommand ConsultaDatosFER = new SqlCommand("select * from Registro_FER where Id_Registro = '" + Id_Registro + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosFER.ExecuteReader();
                datos.Read();
                ObjetoConsulta.Id_Registro = datos.GetInt32(0);
                ObjetoConsulta.Id_Usuario = datos.GetInt32(1);
                ObjetoConsulta.NúmeroCédula = Convert.ToString(datos.GetInt32(2));
                ObjetoConsulta.Nit = datos.GetInt32(3);
                ObjetoConsulta.DígitoVerificación = Convert.ToChar(datos.GetString(4));
                ObjetoConsulta.CódigoCanapro = datos.GetString(5);
                ObjetoConsulta.TipoNovedad = datos.GetString(6);
                ObjetoConsulta.FechaInicio = Convert.ToString(datos.GetDateTime(7));
                ObjetoConsulta.FechaFin = Convert.ToString(datos.GetDateTime(8));
                ObjetoConsulta.ValorMensual = datos.GetDecimal(9);
                ObjetoConsulta.ValorTotal = datos.GetDecimal(10);
                ObjetoConsulta.Fecha = Convert.ToString(datos.GetDateTime(11));
                NúmeroCédula = Convert.ToString(datos.GetInt32(2));
                TipoNovedad = datos.GetString(6);

                SqlCommand ConsultaDatosUsuario = new SqlCommand("select ltrim(rtrim(nombreintegrado)) from Canapro.dbo.nits where nit = '" + NúmeroCédula + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader user = ConsultaDatosUsuario.ExecuteReader();
                user.Read();
                ObjetoConsulta.NombreIntegrado = user.GetString(0);

                if (TipoNovedad.Equals("I"))
                {
                    string FechaI = ObjetoConsulta.FechaInicio;
                    string[] SepararFechaInicio = FechaI.Split(' ');
                    FechaInicio = SepararFechaInicio[0];
                    string fecha = SepararFechaInicio[0];
                    string[] FECHA = fecha.Split('/');
                    string día = FECHA[0];
                    string mes = FECHA[1];
                    string año = FECHA[2];
                    if (mes.Equals("01"))
                    {
                        ObjetoConsulta.MesNovedad = "Enero";
                    }
                    else if (mes.Equals("02"))
                    {
                        ObjetoConsulta.MesNovedad = "Febrero";
                    }
                    else if (mes.Equals("03"))
                    {
                        ObjetoConsulta.MesNovedad = "Marzo";
                    }
                    else if (mes.Equals("04"))
                    {
                        ObjetoConsulta.MesNovedad = "Abril";
                    }
                    else if (mes.Equals("05"))
                    {
                        ObjetoConsulta.MesNovedad = "Mayo";
                    }
                    else if (mes.Equals("06"))
                    {
                        ObjetoConsulta.MesNovedad = "Junio";
                    }
                    else if (mes.Equals("07"))
                    {
                        ObjetoConsulta.MesNovedad = "Julio";
                    }
                    else if (mes.Equals("08"))
                    {
                        ObjetoConsulta.MesNovedad = "Agosto";
                    }
                    else if (mes.Equals("09"))
                    {
                        ObjetoConsulta.MesNovedad = "Septiembre";
                    }
                    else if (mes.Equals("10"))
                    {
                        ObjetoConsulta.MesNovedad = "Octubre";
                    }
                    else if (mes.Equals("11"))
                    {
                        ObjetoConsulta.MesNovedad = "Noviembre";
                    }
                    else if (mes.Equals("12"))
                    {
                        ObjetoConsulta.MesNovedad = "Diciembre";
                    }
                    //FechaInicio = Convert.ToString(año + "-" + mes + "- " + día);
                    //SqlCommand ConsultarMes = new SqlCommand("select mes from configuración_fechas where fecha_ingreso = '" + FechaInicio + "'", LN.C_Conexión.ConexiónCanapro());
                    //SqlDataReader reader = ConsultarMes.ExecuteReader();
                    //reader.Read();
                    //ObjetoConsulta.MesNovedad = reader.GetString(0);
                }
                if (TipoNovedad.Equals("R"))
                {
                    string FechaI = ObjetoConsulta.FechaInicio;
                    string[] SepararFechaInicio = FechaI.Split(' ');
                    FechaInicio = SepararFechaInicio[0];
                    string fecha = SepararFechaInicio[0];
                    string[] FECHA = fecha.Split('/');
                    string día = FECHA[0];
                    string mes = FECHA[1];
                    string año = FECHA[2];
                    if (mes.Equals("01"))
                    {
                        ObjetoConsulta.MesNovedad = "Enero";
                    }
                    else if (mes.Equals("02"))
                    {
                        ObjetoConsulta.MesNovedad = "Febrero";
                    }
                    else if (mes.Equals("03"))
                    {
                        ObjetoConsulta.MesNovedad = "Marzo";
                    }
                    else if (mes.Equals("04"))
                    {
                        ObjetoConsulta.MesNovedad = "Abril";
                    }
                    else if (mes.Equals("05"))
                    {
                        ObjetoConsulta.MesNovedad = "Mayo";
                    }
                    else if (mes.Equals("06"))
                    {
                        ObjetoConsulta.MesNovedad = "Junio";
                    }
                    else if (mes.Equals("07"))
                    {
                        ObjetoConsulta.MesNovedad = "Julio";
                    }
                    else if (mes.Equals("08"))
                    {
                        ObjetoConsulta.MesNovedad = "Agosto";
                    }
                    else if (mes.Equals("09"))
                    {
                        ObjetoConsulta.MesNovedad = "Septiembre";
                    }
                    else if (mes.Equals("10"))
                    {
                        ObjetoConsulta.MesNovedad = "Octubre";
                    }
                    else if (mes.Equals("11"))
                    {
                        ObjetoConsulta.MesNovedad = "Noviembre";
                    }
                    else if (mes.Equals("12"))
                    {
                        ObjetoConsulta.MesNovedad = "Diciembre";
                    }
                    //FechaInicio = Convert.ToString(año + "-" + mes + "- " + día);
                    //SqlCommand ConsultarMes = new SqlCommand("select mes from configuración_fechas where fecha_retiro = '" + FechaInicio + "'", LN.C_Conexión.ConexiónCanapro());
                    //SqlDataReader reader = ConsultarMes.ExecuteReader();
                    //reader.Read();
                    //ObjetoConsulta.MesNovedad = reader.GetString(0);
                }              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ObjetoConsulta;
        }

    }
}
