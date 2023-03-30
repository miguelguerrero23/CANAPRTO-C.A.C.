using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Formador
    {
        public int CódigoFormador { get; set; }
        public long IdPersona { get; set; }
        public string NombreF { get; set; }
        public int CódigoOcupación { get; set; }
        public int CódigoTítulo { get; set; }
        public int Experiencia { get; set; }
        public int CódigoEstado { get; set; }
        public string NombreEstado { get; set; }
        public string NombreOcupación { get; set; }
        public string NombreTítulo { get; set; }
        public string ConsultaSQL { get; set; }
        internal C_Formador ObjetoFormador { get; set; }

        //Método para validar que los registros del Formador no se repitan en la base de datos
        internal Boolean ValidarExistenciaFormador(decimal IdPersona)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from WsEdFormador where idPersona = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros del Formador en la base de datos 
        internal void GuardarFormador()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_Codigo = new SqlCommand("select max(codigoFormador) + 1 from WsEdFormador", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader codigoNuevo = Recibir_Codigo.ExecuteReader();
                    codigoNuevo.Read();
                    CódigoFormador = codigoNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    CódigoFormador = 1;
                    //throw;
                }
                SqlCommand GuardarFormador = new SqlCommand("insert into WsEdFormador  values ('" + CódigoFormador + "', '" + IdPersona + "','" + CódigoOcupación + "', '" + CódigoTítulo + "', '" + Experiencia + "', '" + CódigoEstado + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarFormador.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El formador se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros del Formador en la base de datos 
        internal void EliminarFormador(int Registro_Código)
        {
            try
            {
                CódigoFormador = Registro_Código;
                SqlCommand EliminarFormador = new SqlCommand("delete from WsEdFormador where codigoFormador = '" + CódigoFormador + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarFormador.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El formador se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros del Formador en la base de datos 
        internal void ModificarFormador()
        {
            try
            {
                SqlCommand ModificarFormador = new SqlCommand("update WsEdFormador set codigoFormador = '" + CódigoFormador + "', idPersona = '" + IdPersona + "',  codigoOcupacion = '" + CódigoOcupación + "', codigoTitulo = '" + CódigoTítulo + "', experiencia = '" + Experiencia + "', codigoEstado = '" + CódigoEstado + "' where codigoFormador = " + CódigoFormador, LN.C_Conexión.ConexiónCanapro());
                ModificarFormador.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El formador se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el código del Formador
        public int Cargar_CódigoFormador()
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select max(codigoFormador) + 1 from WsEdFormador", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Código_Nuevo = consulta.ExecuteReader();
                Código_Nuevo.Read();
                CódigoFormador = Código_Nuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoFormador = 1;
                //throw;
            }
            return CódigoFormador;
        }

        //Método para consultar los datos del formador y almacenarlos en un objeto
        internal C_Formador CargarDatosRegistroFormador(int códigoF)
        {
            ObjetoFormador = new C_Formador();
            try
            {
                CódigoFormador = códigoF;
                SqlCommand ConsultaDatosFormador = new SqlCommand("select * from WsEdFormador where codigoFormador = '" + códigoF + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosFormador.ExecuteReader();
                datos.Read();
                ObjetoFormador.CódigoFormador = datos.GetInt32(0);
                ObjetoFormador.IdPersona = datos.GetInt64(1);
                ObjetoFormador.CódigoOcupación = datos.GetInt32(2);
                ObjetoFormador.CódigoTítulo = datos.GetInt32(3);
                ObjetoFormador.Experiencia = datos.GetInt32(4);
                ObjetoFormador.CódigoEstado = datos.GetInt32(5);

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoFormador;
        }

        //Método para validar que el id de la Persona existe en la base de datos
        internal Boolean ValidarEstadoPersona(long Id_Persona)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Canapro.dbo.nits where nit = '" + Id_Persona + "' and estado = 'A'", LN.C_Conexión.ConexiónCanapro());
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
        
        //Método para validar que el id de la Persona existe en la base de datos
        internal Boolean ValidarAsociado(long Id_Persona)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Canapro.dbo.nits where nit = '" + Id_Persona + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para consultar el nombre de la persona
        internal string Datos(decimal IdPersona)
        {
            ObjetoFormador = new C_Formador();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(nombre)) from WsEdPersona where ltrim(rtrim(idPersona)) = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    NombreF = datos.GetString(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return NombreF;
        }

        //Método que realiza la consulta del estado en la base de datos y las almacena en una lista para llevarlas a un Combobox en el formulario Persona
        internal System.Collections.ArrayList LlenarComboEstado(System.Collections.ArrayList lista)
        {
            try
            {
                ArrayList listaNombreEstado = new ArrayList();

                SqlCommand consultarEstado = new SqlCommand("select ltrim(rtrim(nombreEstado)) from WsEdEstado order by 1 asc", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosestado = consultarEstado.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datosestado.Read())
                {
                    listaNombreEstado.Add(datosestado.GetString(0));
                }

                foreach (var nombreEstado in listaNombreEstado)
                {
                    lista.Add(nombreEstado);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;
        }

        //Método que realiza la consulta del título en la base de datos y las almacena en una lista para llevarlas a un Combobox en el formulario Formador
        internal System.Collections.ArrayList LlenarComboTítulo(System.Collections.ArrayList lista)
        {
            try
            {
                ArrayList listaNombreTítulo = new ArrayList();

                SqlCommand consultarTítulo = new SqlCommand("select ltrim(rtrim(nombreTitulo)) from WsEdTitulo order by 1 asc", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosTítulo = consultarTítulo.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datosTítulo.Read())
                {
                    listaNombreTítulo.Add(datosTítulo.GetString(0));
                }

                foreach (var nombreTítulo in listaNombreTítulo)
                {
                    lista.Add(nombreTítulo);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;
        }

        //Método que realiza la consulta de la ocupación en la base de datos y las almacena en una lista para llevarlas a un Combobox en el formulario Formador
        internal System.Collections.ArrayList LlenarComboOcupación(System.Collections.ArrayList lista)
        {
            try
            {
                ArrayList listaNombreOcupación = new ArrayList();

                SqlCommand consultarOcupación = new SqlCommand("select ltrim(rtrim(nombreOcupacion)) from WsEdOcupacion order by 1 asc", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosOcupación = consultarOcupación.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datosOcupación.Read())
                {
                    listaNombreOcupación.Add(datosOcupación.GetString(0));
                }

                foreach (var nombreOcupación in listaNombreOcupación)
                {
                    lista.Add(nombreOcupación);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;
        }

        //Método para consultar el código del estado
        internal int DatosE(string estado)
        {
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(codigoEstado)) from WsEdEstado where ltrim(rtrim(nombreEstado)) = '" + estado + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    CódigoEstado = Convert.ToInt32(datos.GetString(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return CódigoEstado;
        }

        //Método para consultar el código del título
        internal int DatosT(string título)
        {
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(codigoTitulo)) from WsEdTitulo where ltrim(rtrim(nombreTitulo)) = '" + título + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    CódigoTítulo = Convert.ToInt32(datos.GetString(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return CódigoTítulo
;
        }

        //Método para consultar el código del estado
        internal int DatosO(string ocupación)
        {
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(codigoOcupacion)) from WsEdOcupacion where ltrim(rtrim(nombreOcupacion)) = '" + ocupación + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    CódigoOcupación = Convert.ToInt32(datos.GetString(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return CódigoOcupación;
        }

        //Método para consultar el nombre del estado
        internal string DatosER(int cf)
        {
            try
            {
                SqlCommand ConsultaCódigo = new SqlCommand("select ltrim(rtrim(codigoEstado)) from WsEdFormador where codigoFormador = '" + cf + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosC = ConsultaCódigo.ExecuteReader();
                if (datosC.Read() == true)
                {
                    int código = Convert.ToInt32(datosC.GetString(0));



                    SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(nombreEstado)) from WsEdEstado where ltrim(rtrim(codigoEstado)) = '" + código + "'", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader datos = ConsultaDatos.ExecuteReader();

                    if (datos.Read() == true)
                    {
                        NombreEstado = datos.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return NombreEstado;
        }

        //Método para consultar el nombre del título
        internal string DatosTR(int cf)
        {
            try
            {
                SqlCommand ConsultaCódigo = new SqlCommand("select ltrim(rtrim(codigoTitulo)) from WsEdFormador where codigoFormador = '" + cf + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosC = ConsultaCódigo.ExecuteReader();
                if (datosC.Read() == true)
                {
                    int código = Convert.ToInt32(datosC.GetString(0));



                    SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(nombreTitulo)) from WsEdTitulo where ltrim(rtrim(codigoTitulo)) = '" + código + "'", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader datos = ConsultaDatos.ExecuteReader();

                    if (datos.Read() == true)
                    {
                        NombreTítulo = datos.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return NombreTítulo
;
        }

        //Método para consultar el nombre de la ocupación
        internal string DatosOR(int cf)
        {
            try
            {
                SqlCommand ConsultaCódigo = new SqlCommand("select ltrim(rtrim(codigoOcupación)) from WsEdFormador where codigoFormador = '" + cf + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosC = ConsultaCódigo.ExecuteReader();
                if (datosC.Read() == true)
                {
                    int código = Convert.ToInt32(datosC.GetString(0));



                    SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(nombreOcupacion)) from WsEdOcupacion where ltrim(rtrim(codigoOcupacion)) = '" + código + "'", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader datos = ConsultaDatos.ExecuteReader();

                    if (datos.Read() == true)
                    {
                        NombreOcupación = datos.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return NombreOcupación;
        }

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dgvFormador)
        {
            try
            {
                ConsultaSQL = "select codigoFormador as '" + "Código" + "', idPersona as'" + "Documento" + "', (select nombreOcupacion from WsEdOcupacion where WsEdOcupacion.codigoOcupacion = WsEdFormador.codigoOcupacion) as'" + "Ocupación" + "', (select nombreTitulo from WsEdTitulo where WsEdTitulo.codigoTitulo = WsEdFormador.codigoTitulo) as'" + "Título" + "', experiencia as'" + "Experiencia" + "', (select nombreEstado from WsEdEstado where WsEdEstado.codigoEstado = WsEdFormador.codigoEstado) as'" + "Estado" + "'  from WsEdFormador ";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvFormador.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        internal void ActualizarTablas(DataGridView dgvCertificacionesF)
        {
            try
            {
                ConsultaSQL = "select codigoFormador as '" + "Código" + "', idPersona as'" + "Documento" + "', (select nombreOcupacion from WsEdOcupacion where WsEdOcupacion.codigoOcupacion = WsEdFormador.codigoOcupacion) as'" + "Ocupación" + "', (select nombreTitulo from WsEdTitulo where WsEdTitulo.codigoTitulo = WsEdFormador.codigoTitulo) as'" + "Título" + "', experiencia as'" + "Experiencia" + "', (select nombreEstado from WsEdEstado where WsEdEstado.codigoEstado = WsEdFormador.codigoEstado) as'" + "Estado" + "'  from WsEdFormador ";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvCertificacionesF.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        internal void ConsultarCapacitacionesPorCédulaF(DataGridView dgvCertificacionesF, long cédula)
        {
            try
            {
                if (ValidarExistenciaFormador(cédula).Equals(true))
                {
                    ConsultaSQL = "select C.codigoFormador as 'Código Formador', (select nombrePrograma from WsEdPrograma P where P.codigoPrograma = C.codigoPrograma) as 'Programa', (select nombreciudad from Canapro.dbo.ciudades where codciudad = C.Ciudad) as 'Ciudad', (select nombredepartamento from Canapro.dbo.departamentos where coddepartamento = C.Departamento) as 'Departamento', direccion as Dirección, (select nombreMetodologia from WsEdMetodologia M where M.codigoMetodología = C.codigoMetodologia) as 'Metodolgía', fechaInicio as 'Fecha inicio', fechaFin as 'Fecha Fin', intensidad as Intensidad, clausurada as 'Clausurada', (select idPersona from WsEdFormador F where F.codigoFormador = C.codigoFormador)as 'Formador' from WsEdCapacitación C, WsEdRegistroParticipantes R where C.codigoCapacitacion = R.codigoCapacitacion and R.idPersona = '" + cédula + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());

                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvCertificacionesF.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
