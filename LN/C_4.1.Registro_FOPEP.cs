using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_
    {
        //Definición de variables y encapsulamiento de variables
        public int Id { get; set; }
        public int Id_Registro { get; set; }
        public int Id_Ciudad { get; set; }
        public int Id_Departamento { get; set; }
        public string TipoDocumento { get; set; }
        public string NúmeroCédula { get; set; }
        public int Id_Usuario { get; set; }
        public int CódigoEntidad { get; set; }
        public string Fecha { get; set; }
        public string ValorTotal { get; set; }
        public string Espacio1 { get; set; }
        public decimal ValorMensual { get; set; }
        public string Espacio2 { get; set; }
        public int Aplicación { get; set; }
        public int Porcentaje { get; set; }
        public int AportePréstamo { get; set; }
        public int NúmeroLibranza { get; set; }
        public string Filler { get; set; }
        public string FechaRegistro { get; set; }
        internal C_ ObjetoFopep { get; set; }
        public string NombreIntegrado { get; set; }
        public string Dirección { get; set; }
        public string Teléfono { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Ciudad { get; set;  }
        public string Departamento { get; set; }
        public string CódigoCiudad { get; set; }
        public string CódigoDepartamento { get; set; }
        public string ConsultaSQL { get; set; }
        public string CodCiudad { get; set; }
        ArrayList lista;
        
        
        //Método para validar que los registros de FOPEP no se repitan en la base de datos
        internal Boolean ValidarExistenciaFOPEP(int Registro_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Registro_FOPEP where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros FOPEP en la base de datos 
        internal void GuardarRegistroFOPEP()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Registro) + 1 from Registro_FOPEP", LN.C_Conexión.ConexiónCanapro());
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
                SqlCommand GuardarRegistroFOPEP = new SqlCommand("insert into Registro_FOPEP  values ('" + Id_Registro + "','" + Id_Usuario + "', '" + TipoDocumento + "','" + NúmeroCédula + "', '" + CódigoEntidad + "', '" + Fecha + "', '" + ValorTotal + "', '" + Espacio1 + "', '" + ValorMensual + "', '" + Espacio2 + "', '" + Aplicación + "', '" + Porcentaje + "', '" + AportePréstamo + "', '" + NúmeroLibranza+ "', '" + Filler + "', '" + FechaRegistro + "', '" + CodCiudad + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarRegistroFOPEP.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para guardar los datos de un asociado nuevo en la base de datos CANAPROAP 
        internal void GuardarDatosAsociado()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id) + 1 from DatosAsociadoFOPEP", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id = 1;
                    //throw;
                }
                SqlCommand GuardarDatosAsociado = new SqlCommand("insert into DatosAsociadoFOPEP  values ('" + Id + "','" + Id_Registro + "', '" + NombreIntegrado + "','" + Dirección + "', '" + Teléfono + "', '" + Celular + "', '" + Email + "', '" + Id_Ciudad + "', '" + Id_Departamento + "')", LN.C_Conexión.ConexiónCanapro());
                    GuardarDatosAsociado.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros FOPEP en la base de datos 
        internal void ModificarRegistroFOPEP()
        {
            try
            {
                SqlCommand ModificarRegistroFOPEP = new SqlCommand("update registro_fopep set Id_Registro = '" + Id_Registro + "', Id_Usuario = '" + Id_Usuario + "', TipoDocumento = '" + TipoDocumento + "', NúmeroCédula = '" + NúmeroCédula + "', CódigoEntidad = '" + CódigoEntidad + "', Fecha = '" + Fecha + "', ValorTotal = '" + ValorTotal + "', EspacioVacío1 = '" + Espacio1 + "', ValorMensual = '" + ValorMensual + "', EspacioVacío2 = '" + Espacio2 + "', Aplicación = '" + Aplicación + "', Porcentaje = '" + Porcentaje + "', AportePréstamo = '" + AportePréstamo + "', NúmeroLibranza = '" + NúmeroLibranza + "', Filler = '" + Filler + "', FechaRegistro = '" + FechaRegistro + "' , CodigoCiudad = '" + CodCiudad + "'where Id_Registro = " + Id_Registro, LN.C_Conexión.ConexiónCanapro());
                ModificarRegistroFOPEP.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El registro se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros FOPEP en la base de datos 
        internal void EliminarRegistroFOPEP(int Registro_ID)
        {
            try
            {
                Id_Registro = Registro_ID;
                SqlCommand EliminarRegistroFOPEP = new SqlCommand("delete from Registro_FOPEP where Id_Registro = '" + Id_Registro + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarRegistroFOPEP.ExecuteNonQuery();
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
                SqlCommand consulta = new SqlCommand("select max(Id_Registro) + 1 from Registro_FOPEP", LN.C_Conexión.ConexiónCanapro());
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

        //Método para llenar el textbox con el id del registro en el Panel Datos Personales en FOPEP
        public int Cargar_IdRegistroDatosAsociado()
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select max(Id) + 1 from DatosAsociadoFOPEP", LN.C_Conexión.ConexiónCanapro());
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
        internal C_ ConsultarDatosUsuario(string usuario)
        {
            ObjetoFopep = new C_();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select id_usuarios from usuarios where usuario = '" + usuario + "'", LN.C_Conexión.ConexiónCanapro()); SqlDataReader user = ConsultaUsuario.ExecuteReader();

                if (user.Read())
                {
                    ObjetoFopep.Id_Usuario = user.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoFopep;
        }

        //Método para consultar la fecha en la base de datos
        internal C_ ConsultarFecha(string Mes)
        {
            ObjetoFopep = new C_();
            try
            {
                SqlCommand ConsultaFecha = new SqlCommand("select valor from Configuración_Fechas where mes = '" + Mes + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaFecha.ExecuteReader();

                if (datos.Read())
                {
                    ObjetoFopep.Fecha = datos.GetString(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoFopep;
        }

        //Método para consultar el nombre del asociado
        internal C_ DatosUsuario(string documento)
        {
            ObjetoFopep = new C_();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(nombreintegrado)), ltrim(rtrim(direccion)), telefono1, ltrim(rtrim(celular)), ltrim(rtrim(email)), ltrim(rtrim(coddepartamento)), ltrim(rtrim(codciudad)) from Canapro.dbo.nits where ltrim(rtrim(nit)) = '" + documento + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    ObjetoFopep.NombreIntegrado = datos.GetString(0);
                    ObjetoFopep.Dirección = datos.GetString(1);
                    ObjetoFopep.Teléfono = datos.GetString(2);
                    ObjetoFopep.Celular = datos.GetString(3);
                    ObjetoFopep.Email = datos.GetString(4);
                    char[] separarDepto = datos.GetString(5).ToCharArray();
                    ObjetoFopep.CódigoDepartamento = Convert.ToString(Convert.ToString(separarDepto[0] + Convert.ToString(separarDepto[1])));
                    char[] separarciudad = datos.GetString(6).ToCharArray();
                    ObjetoFopep.CódigoCiudad = Convert.ToString(Convert.ToString(separarciudad[2] + Convert.ToString(separarciudad[3]) + Convert.ToString(separarciudad[4])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ObjetoFopep;
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

        //Método para realizar consulta de los registros FOPEP por número de cédula
        internal void ConsultarRegistroFOPEPPorCédula(DataGridView dtgHistorialFOPEP, string cédula)
        {
            try
            {
                if (ValidarDocumentoAsociado(cédula).Equals(true))
                {
                    ConsultaSQL = "select TipoDocumento as '" + "Tipo Doc." + "', NúmeroCédula as '" + "Cédula" + "', CódigoEntidad as '" + "Entidad" + "', Fecha as '" + "Año - Mes" + "', ValorTotal as '" + "Valor Total" + "', EspacioVacío1 as '" + "Free" + "', ValorMensual as '" + "Valor Mensual" + "', EspacioVacío2 as '" + "Free" + "' , Aplicación, Porcentaje, AportePréstamo as '" + "Aporte/Préstamo" + "', NúmeroLibranza as '" + "Número Libranza" + "', (select direccion from Canapro.dbo.nits where nit = '" + cédula + "') as '" + "Dirección" + "', (select telefono1 from Canapro.dbo.nits where nit = '" + cédula + "')  as '" + "Teléfono" + "', CodigoCiudad as '" + "Ciudad" + "', (select coddepartamento from Canapro.dbo.nits where nit = '" + cédula + "') as '" + "Departamento" + "', (select celular from Canapro.dbo.nits where nit = '" + cédula + "') as '" + "Celular" + "', (select email from Canapro.dbo.nits where nit = '" + cédula + "') as '" + "Correo Electrónico" + "', Filler, FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Usuario) as '" + "Id Usuario" + "' from registro_fopep where NúmeroCédula = '" + cédula + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgHistorialFOPEP.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }
                else if (ValidarDocumentoAsociado(cédula).Equals(false))
                {
                    ConsultaSQL = "select TipoDocumento as '" + "Tipo Doc." + "', NúmeroCédula as '" + "Cédula" + "', CódigoEntidad as '" + "Entidad" + "', Fecha as '" + "Año - Mes" + "', ValorTotal as '" + "Valor Total" + "', EspacioVacío1 as '" + "Free" + "', ValorMensual as '" + "Valor Mensual" + "', EspacioVacío2 as '" + "Free" + "' , Aplicación, Porcentaje, AportePréstamo as '" + "Aporte/Préstamo" + "', NúmeroLibranza as '" + "Número Libranza" + "', (select dirección from DatosAsociadoFOPEP where Id_RegistroFopep = (select id_registro from registro_Fopep where NúmeroCédula = '" + cédula + "')) as '" + "Dirección" + "', (select teléfono from DatosAsociadoFOPEP where Id_RegistroFopep = (select id_registro from registro_Fopep where NúmeroCédula = '" + cédula + "'))  as '" + "Teléfono" + "', CodigoCiudad as '" + "Ciudad" + "', (select códigodepartamento from DatosAsociadoFOPEP where Id_RegistroFopep = (select id_registro from registro_Fopep where NúmeroCédula = '" + cédula + "')) as '" + "Departamento" + "', (select celular from DatosAsociadoFOPEP where Id_RegistroFopep = (select id_registro from registro_Fopep where NúmeroCédula ='" + cédula + "')) as '" + "Celular" + "', (select email from DatosAsociadoFOPEP where Id_RegistroFopep = (select id_registro from registro_Fopep where NúmeroCédula = '" + cédula + "')) as '" + "Correo Electrónico" + "', Filler, FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Usuario) as '" + "Id Usuario" + "' from registro_fopep where NúmeroCédula = '" + cédula + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgHistorialFOPEP.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }                
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de los registros FOPEP por número de libranza
        internal void ConsultarRegistroFOPEPPorLibranza(DataGridView dtgHistorialFOPEP, string Libranza)
        {
            try
            {
                ConsultaSQL = "select TipoDocumento as '" + "Tipo Doc." + "', NúmeroCédula as '" + "Cédula" + "', CódigoEntidad as '" + "Entidad" + "', Fecha as '" + "Año - Mes" + "', ValorTotal as '" + "Valor Total" + "', EspacioVacío1 as '" + "Free" + "', ValorMensual as '" + "Valor Mensual" + "', EspacioVacío2 as '" + "Free" + "' , Aplicación, Porcentaje, AportePréstamo as '" + "Aporte/Préstamo" + "', NúmeroLibranza as '" + "Número Libranza" + "', (select direccion from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Dirección" + "', (select telefono1 from Canapro.dbo.nits where nit = NúmeroCédula)  as '" + "Teléfono" + "', CodigoCiudad as '" + "Ciudad" + "', (select coddepartamento from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Departamento" + "', (select celular from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Celular" + "', (select email from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Correo Electrónico" + "', Filler, FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Usuario) as '" + "Id Usuario" + "' from registro_fopep where NúmeroLibranza = '" + Libranza + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFOPEP.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de los registros FOPEP por aplicación
        internal void ConsultarRegistroFOPEPPorAplicación(DataGridView dtgHistorialFOPEP, string Aplicación)
        {
            try
            {
                ConsultaSQL = "select TipoDocumento as '" + "Tipo Doc." + "', NúmeroCédula as '" + "Cédula" + "', CódigoEntidad as '" + "Entidad" + "', Fecha as '" + "Año - Mes" + "', ValorTotal as '" + "Valor Total" + "', EspacioVacío1 as '" + "Free" + "', ValorMensual as '" + "Valor Mensual" + "', EspacioVacío2 as '" + "Free" + "' , Aplicación, Porcentaje, AportePréstamo as '" + "Aporte/Préstamo" + "', NúmeroLibranza as '" + "Número Libranza" + "', (select direccion from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Dirección" + "', (select telefono1 from Canapro.dbo.nits where nit = NúmeroCédula)  as '" + "Teléfono" + "', CodigoCiudad as '" + "Ciudad" + "', (select coddepartamento from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Departamento" + "', (select celular from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Celular" + "', (select email from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Correo Electrónico" + "', Filler, FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Usuario) as '" + "Id Usuario" + "' from registro_fopep where Aplicación = '" + Aplicación + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFOPEP.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de los registros FOPEP por Aporte/Préstamo
        internal void ConsultarRegistroFOPEPPorAportePréstamo(DataGridView dtgHistorialFOPEP, string AportePréstamo)
        {
            try
            {
                ConsultaSQL = "select TipoDocumento as '" + "Tipo Doc." + "', NúmeroCédula as '" + "Cédula" + "', CódigoEntidad as '" + "Entidad" + "', Fecha as '" + "Año - Mes" + "', ValorTotal as '" + "Valor Total" + "', EspacioVacío1 as '" + "Free" + "', ValorMensual as '" + "Valor Mensual" + "', EspacioVacío2 as '" + "Free" + "' , Aplicación, Porcentaje, AportePréstamo as '" + "Aporte/Préstamo" + "', NúmeroLibranza as '" + "Número Libranza" + "', (select direccion from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Dirección" + "', (select telefono1 from Canapro.dbo.nits where nit = NúmeroCédula)  as '" + "Teléfono" + "', CodigoCiudad as '" + "Ciudad" + "', (select coddepartamento from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Departamento" + "', (select celular from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Celular" + "', (select email from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Correo Electrónico" + "', Filler, FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Usuario) as '" + "Id Usuario" + "' from registro_fopep where AportePréstamo = '" + AportePréstamo + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFOPEP.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de los registros FOPEP por Usuario
        internal void ConsultarRegistroFOPEPPorUsuario(DataGridView dtgHistorialFOPEP, string Usuario)
        {
            try
            {
                ConsultaSQL = "select TipoDocumento as '" + "Tipo Doc." + "', NúmeroCédula as '" + "Cédula" + "', CódigoEntidad as '" + "Entidad" + "', Fecha as '" + "Año - Mes" + "', ValorTotal as '" + "Valor Total" + "', EspacioVacío1 as '" + "Free" + "', ValorMensual as '" + "Valor Mensual" + "', EspacioVacío2 as '" + "Free" + "' , Aplicación, Porcentaje, AportePréstamo as '" + "Aporte/Préstamo" + "', NúmeroLibranza as '" + "Número Libranza" + "', (select direccion from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Dirección" + "', (select telefono1 from Canapro.dbo.nits where nit = NúmeroCédula)  as '" + "Teléfono" + "', CodigoCiudad as '" + "Ciudad" + "', (select coddepartamento from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Departamento" + "', (select celular from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Celular" + "', (select email from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Correo Electrónico" + "', Filler, FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Usuario) as '" + "Id Usuario" + "' from registro_fopep where Id_Usuario = (Select Id_Usuarios from usuarios where usuario = '" + Usuario + "')";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFOPEP.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Método para realizar consulta de los registros FOPEP por Fecha de Registro
        internal void ConsultarRegistroFOPEPPorFechaRegistro(DataGridView dtgHistorialFOPEP, string FechaDesde, string FechaHasta)
        {
            try
            {
                ConsultaSQL = "select TipoDocumento as '" + "Tipo Doc." + "', NúmeroCédula as '" + "Cédula" + "', CódigoEntidad as '" + "Entidad" + "', Fecha as '" + "Año - Mes" + "', ValorTotal as '" + "Valor Total" + "', EspacioVacío1 as '" + "Free" + "', ValorMensual as '" + "Valor Mensual" + "', EspacioVacío2 as '" + "Free" + "' , Aplicación, Porcentaje, AportePréstamo as '" + "Aporte/Préstamo" + "', NúmeroLibranza as '" + "Número Libranza" + "', (select direccion from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Dirección" + "', (select telefono1 from Canapro.dbo.nits where nit = NúmeroCédula)  as '" + "Teléfono" + "', CodigoCiudad as '" + "Ciudad" + "', (select coddepartamento from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Departamento" + "', (select celular from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Celular" + "', (select email from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Correo Electrónico" + "', Filler, FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Usuario) as '" + "Usuario" + "' from registro_fopep where FechaRegistro between '" + FechaDesde + "' and '" + FechaHasta + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFOPEP.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();                        
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de los registros FOPEP por ValorFecha de Registro
        internal void ConsultarRegistroFOPEPPorValorFecha(DataGridView dtgHistorialFOPEP, string ValorFecha)
        {
            try
            {
                ConsultaSQL = "select TipoDocumento as '" + "Tipo Doc." + "', NúmeroCédula as '" + "Cédula" + "', CódigoEntidad as '" + "Entidad" + "', Fecha as '" + "Año - Mes" + "', ValorTotal as '" + "Valor Total" + "', EspacioVacío1 as '" + "Free" + "', ValorMensual as '" + "Valor Mensual" + "', EspacioVacío2 as '" + "Free" + "' , Aplicación, Porcentaje, AportePréstamo as '" + "Aporte/Préstamo" + "', NúmeroLibranza as '" + "Número Libranza" + "', (select direccion from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Dirección" + "', (select telefono1 from Canapro.dbo.nits where nit = NúmeroCédula)  as '" + "Teléfono" + "', CodigoCiudad as '" + "Ciudad" + "', (select coddepartamento from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Departamento" + "', (select celular from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Celular" + "', (select email from Canapro.dbo.nits where nit = NúmeroCédula) as '" + "Correo Electrónico" + "', Filler, FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_Usuarios = Id_Usuario) as '" + "Usuario" + "' from registro_fopep where Fecha = '" + ValorFecha + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFOPEP.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }


        //Método para consultar los datos del registro FOPEP y almacenarlos en un objeto
        internal C_ CargarDatosRegistroFOPEP(int RegistroFopep)
        {
            ObjetoFopep = new C_();
            try
            {
                Id_Registro = RegistroFopep;
                SqlCommand ConsultaDatosFOPEP = new SqlCommand("select * from registro_fopep where Id_Registro = '" + Id_Registro + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosFOPEP.ExecuteReader();
                datos.Read();
                ObjetoFopep.Id_Registro = datos.GetInt32(0);
                ObjetoFopep.Id_Usuario = datos.GetInt32(1);
                ObjetoFopep.TipoDocumento = datos.GetString(2);
                ObjetoFopep.NúmeroCédula = Convert.ToString(datos.GetInt32(3));
                ObjetoFopep.CódigoEntidad = datos.GetInt32(4);
                ObjetoFopep.Fecha = datos.GetString(5);
                ObjetoFopep.ValorTotal = datos.GetString(6);
                //ObjetoFopep.Espacio1 = datos.GetString(7);
                ObjetoFopep.ValorMensual = datos.GetDecimal(8);
                //ObjetoFopep.Espacio2 = datos.GetString(9);
                ObjetoFopep.Aplicación = datos.GetInt32(10);
                ObjetoFopep.Porcentaje = datos.GetInt32(11);
                ObjetoFopep.AportePréstamo = datos.GetInt32(12);
                ObjetoFopep.NúmeroLibranza = datos.GetInt32(13);
                ObjetoFopep.Filler = datos.GetString(14);
                ObjetoFopep.FechaRegistro = Convert.ToString(datos.GetDateTime(15));
                //ObjetoFopep.CodCiudad = Convert.ToInt32(16);
                NúmeroCédula = Convert.ToString(datos.GetInt32(3));

                if (ValidarDocumentoAsociado(NúmeroCédula).Equals(true))
                {
                    SqlCommand ConsultaDatosAsociado = new SqlCommand("select nombreintegrado, direccion, telefono1, email, celular, codciudad, coddepartamento from Canapro.dbo.nits where nit = '" + NúmeroCédula + "'", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader user = ConsultaDatosAsociado.ExecuteReader();
                    user.Read();
                    ObjetoFopep.NombreIntegrado = user.GetString(0);
                    ObjetoFopep.Dirección = user.GetString(1);
                    ObjetoFopep.Teléfono = user.GetString(2);
                    ObjetoFopep.Email = user.GetString(3);
                    ObjetoFopep.Celular = user.GetString(4);
                    char[] separarDepto = user.GetString(6).ToCharArray();
                    ObjetoFopep.CódigoDepartamento = Convert.ToString(Convert.ToString(separarDepto[0] + Convert.ToString(separarDepto[1])));
                    char[] separarciudad = user.GetString(5).ToCharArray();
                    ObjetoFopep.CódigoCiudad = Convert.ToString(Convert.ToString(separarciudad[2] + Convert.ToString(separarciudad[3]) + Convert.ToString(separarciudad[4])));

                }
                else if (ValidarDocumentoAsociado(NúmeroCédula).Equals(false))
                {
                    SqlCommand ConsultaDatosAsociado = new SqlCommand("select nombreasociado, dirección, teléfono, email, celular, códigociudad, códigodepartamento from DatosAsociadoFOPEP where Id_RegistroFopep = (select id_registro from registro_Fopep where NúmeroCédula = '" + NúmeroCédula + "')", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader user = ConsultaDatosAsociado.ExecuteReader();
                    user.Read();
                    ObjetoFopep.NombreIntegrado = user.GetString(0);
                    ObjetoFopep.Dirección = user.GetString(1);
                    ObjetoFopep.Teléfono = user.GetString(2);
                    ObjetoFopep.Email = user.GetString(3);
                    ObjetoFopep.Celular = user.GetString(4);
                    char[] separarDepto = user.GetString(6).ToCharArray();
                    ObjetoFopep.CódigoDepartamento = Convert.ToString(Convert.ToString(separarDepto[0] + Convert.ToString(separarDepto[1])));
                    char[] separarciudad = user.GetString(5).ToCharArray();
                    ObjetoFopep.CódigoCiudad = Convert.ToString(Convert.ToString(separarciudad[2] + Convert.ToString(separarciudad[3]) + Convert.ToString(separarciudad[4])));

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoFopep ;
        }
               
        //Método que realiza la consulta de las ciudades y los departamentos en la base de datos y las almacena en una lista para llevarlas a un Combobox en el formulario Fopep
        internal System.Collections.ArrayList LlenarComboCiudadDepartamento(System.Collections.ArrayList lista)
        {
            try
            {
                ArrayList listaCodigoDepartamento = new ArrayList();
                ArrayList listaNombreCiudad = new ArrayList();

                SqlCommand consultardepartamentos = new SqlCommand("select ltrim(rtrim(coddepartamento)) from Canapro.dbo.departamentos", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosdepartamento = consultardepartamentos.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datosdepartamento.Read())
                {
                    listaCodigoDepartamento.Add(datosdepartamento.GetString(0));
                }

                foreach (var item in listaCodigoDepartamento)
                {
                    listaNombreCiudad.Clear();
                    SqlCommand consultarciudades = new SqlCommand("select ltrim(rtrim(nombreciudad)) from Canapro.dbo.ciudades where ltrim(rtrim(coddepartamento)) = '" + item + "'order by 1 asc", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader datosciudad = consultarciudades.ExecuteReader();
                    LN.C_Conexión.ConexiónCanapro().Close();

                    while (datosciudad.Read())
                    {
                        listaNombreCiudad.Add(datosciudad.GetString(0));
                    }

                    SqlCommand consultarNombreDepartamentos = new SqlCommand("select ltrim(rtrim(nombredepartamento)) from Canapro.dbo.departamentos where ltrim(rtrim(coddepartamento)) = '" + item + "'", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader datosNombreDepartamento = consultarNombreDepartamentos.ExecuteReader();
                    LN.C_Conexión.ConexiónCanapro().Close();
                    datosNombreDepartamento.Read();

                    foreach (var nombreCiud in listaNombreCiudad)
                    {
                       lista.Add(nombreCiud + " - " + datosNombreDepartamento.GetString(0));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;
        }

        //Método para consultar el código de la ciudad y del departamento
        internal C_ ConsultaCódigos(string CiudadDepartamento)
        {
            ObjetoFopep = new C_();

            string[] vectorSeparar = CiudadDepartamento.Split(' ');

            string ciudad = vectorSeparar[0];
            string departamento = vectorSeparar[2];

            SqlCommand consultarCódigoCiudadDepartamento = new SqlCommand("select ltrim(rtrim(codciudad)), ltrim(rtrim(departamentos.coddepartamento)) from Canapro.dbo.ciudades, Canapro.dbo.departamentos where ltrim(rtrim(nombreciudad)) = '" + ciudad + "' and ltrim(rtrim(nombredepartamento)) = '" + departamento + "'", LN.C_Conexión.ConexiónCanapro());
            SqlDataReader datos = consultarCódigoCiudadDepartamento.ExecuteReader();
            LN.C_Conexión.ConexiónCanapro().Close();
            datos.Read();

            char[] separarNumero = datos.GetString(0).ToCharArray();             
            string códigoCiudad =  Convert.ToString(Convert.ToString(separarNumero[2] + Convert.ToString(separarNumero[3]) + Convert.ToString(separarNumero[4])));
            
            ObjetoFopep.CódigoCiudad = códigoCiudad;            
            ObjetoFopep.CódigoDepartamento = datos.GetString(1);

            return ObjetoFopep;
        }



    }
}
