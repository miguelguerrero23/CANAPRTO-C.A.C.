using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Registro_Participantes
    {
        public long IdPersona { get; set; }
        public string NombrePersona { get; set; }
        public string Horas { get; set; }
        public int CódigoCapacitación { get; set; }
        public int CódigoPrograma { get; set; }
        public int CódigoRegistro { get; set; }
        public string FechaCulminacion { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string ConsultaSQL { get; set; }
        internal C_Registro_Participantes ObjetoRP { get; set; }
        internal C_Capacitación ObjetoC { get; set; }
        internal C_Formador ObjetoF { get; set; }
        ArrayList lista;

        //Método para validar que un participante no repita un programa en la base de datos
        /* internal Boolean ValidarPersonayPrograma(long idPersona, int códigoPrograma)
         {
             try
             {
                 SqlCommand validar = new SqlCommand("select * from WsEdRegistroParticipantes where idPersona = '" + idPersona + "' and codigoPrograma = '" + códigoPrograma + "'", LN.C_Conexión.ConexiónCanapro());
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
         }*/

        //Método para guardar los registros de participantes en la base de datos 
        internal void GuardarPersonaC()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_Codigo = new SqlCommand("select max(codigoRegistro) + 1 from WsEdRegistroParticipantes", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader codigoNuevo = Recibir_Codigo.ExecuteReader();
                    codigoNuevo.Read();
                    CódigoRegistro = codigoNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    CódigoRegistro = 1;
                    //throw;
                }
                SqlCommand GuardarPersonaC = new SqlCommand("insert into WsEdRegistroParticipantes values ('" + CódigoRegistro + "','" + IdPersona + "', '" + CódigoCapacitación + "','" + FechaCulminacion + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarPersonaC.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El participante se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros del participante en la base de datos 
        internal void EliminarPersonaC(int Registro_Código)
        {
            try
            {
                IdPersona = Registro_Código;
                SqlCommand EliminarPersonaC = new SqlCommand("delete from WsEdRegistroParticipantes where idPersona = '" + IdPersona + "' and codigoCapacitacion = '" + CódigoCapacitación + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarPersonaC.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El participante se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para consultar los datos del participante y almacenarlos en un objeto
        internal C_Registro_Participantes CargarDatosRegistroParticipante(long id_Persona)
        {
            ObjetoRP = new C_Registro_Participantes();
            try
            {
                IdPersona = id_Persona;
                SqlCommand ConsultaDatosParticipante = new SqlCommand("(select idPersona, nombre from WsEdPersona where idPersona = '" + IdPersona + "')", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosParticipante.ExecuteReader();
                datos.Read();
                ObjetoRP.IdPersona = datos.GetInt64(0);
                ObjetoRP.NombrePersona = datos.GetString(1);

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoRP;
        }

        //Método que realiza la consulta de la capacitación en la base de datos y las almacena en una lista para llevarlas a un Combobox en el formulario Registro Participantes
        internal System.Collections.ArrayList LlenarComboCapacitación(System.Collections.ArrayList lista)
        {
            try
            {
                ArrayList listaCapacitación = new ArrayList();
                ArrayList listaPrograma = new ArrayList();
                ArrayList listaFecha = new ArrayList();

                SqlCommand consultarDatosC = new SqlCommand("select CONVERT(varchar(12), C.codigoCapacitacion) + '-' + (select P.nombrePrograma from WsEdPrograma P where P.codigoPrograma = CONVERT(varchar(12), C.codigoPrograma)) + '-' + CONVERT(varchar(100), C.fechaFin) from WsEdCapacitacion C order by C.codigoCapacitacion asc", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosC = consultarDatosC.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datosC.Read())
                {
                    listaCapacitación.Add(datosC.GetString(0));
                }
                foreach (var datos in listaCapacitación)
                {
                    lista.Add(datos);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;
        }

        //Método para validar que el id de la Persona existe en la base de datos
        internal Boolean ValidarIdPersona(long Id_Persona)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from WsEdPersona where idPersona = '" + Id_Persona + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = validar.ExecuteReader();
                if (datos.Read() == true)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                throw;
            }
        }

        //Método para validar que el documento del asociado existe en la base de datos
        internal Boolean ValidarDocumentoAsociado(string documento)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select nit, nombreintegrado from Canapro.dbo.nits where nit = '" + documento + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para validar que el estado de la Persona en la base de datos
        internal Boolean ValidarEstadoPersona(long Id_Persona)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from WsEdPersona where idPersona = '" + Id_Persona + "' and codigoEstado = 1", LN.C_Conexión.ConexiónCanapro());
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
        internal C_Registro_Participantes Datos(long IdPersona)
        {
            ObjetoRP = new C_Registro_Participantes();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(nombre)) from WsEdPersona where ltrim(rtrim(idPersona)) = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    ObjetoRP.NombrePersona = datos.GetString(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ObjetoRP;
        }

        //Método para consultar el nombre del asociado
        internal C_Registro_Participantes DatosA(long IdPersona)
        {
            ObjetoRP = new C_Registro_Participantes();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(nombreintegrado)) from Canapro.dbo.nits where nit = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    ObjetoRP.NombrePersona = datos.GetString(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ObjetoRP;
        }

        //Método para realizar consulta de las capacitaciones por número de cédula
        internal void ConsultarCapacitacionesPorCédula(DataGridView dgvRegistroParticipantes,  long cédula)
        {
            try
            {
                if (ValidarIdPersona(cédula).Equals(true))
                {

                    ConsultaSQL = "select C.codigoCapacitacion as 'Código Capacitación', (select nombrePrograma from WsEdPrograma P where P.codigoPrograma = C.codigoPrograma) as 'Programa', (select nombreciudad from Canapro.dbo.ciudades where codciudad = C.Ciudad) as 'Ciudad', (select nombredepartamento from Canapro.dbo.departamentos where coddepartamento = C.Departamento) as 'Departamento', direccion as 'Dirección', (select nombreMetodologia from WsEdMetodologia M where M.codigoMetodología = C.codigoMetodologia) as 'Metodolgía', fechaInicio as 'Fecha Inicio', fechaFin as 'Fecha Fin', intensidad as 'Intensidad', clausurada as 'Clausurada', (select idPersona from WsEdFormador F where F.codigoFormador = C.codigoFormador)as 'Formador' from WsEdCapacitación C, WsEdRegistroParticipantes R where C.codigoCapacitacion = R.codigoCapacitacion and R.idPersona = '" + cédula + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvRegistroParticipantes.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        internal void ConsultarCapacitacionesPorFechas(DataGridView dgvRegistroParticipantes, long cédula, string FechaDesde, string FechaHasta)
        {
            try
            {
                if (ValidarIdPersona(cédula).Equals(true))
                {
                    ConsultaSQL = "select C.codigoCapacitacion as 'Código Capacitación', (select nombrePrograma from WsEdPrograma P where P.codigoPrograma = C.codigoPrograma) as 'Programa', (select nombreciudad from Canapro.dbo.ciudades where codciudad = C.Ciudad) as 'Ciudad', (select nombredepartamento from Canapro.dbo.departamentos where coddepartamento = C.Departamento) as 'Departamento', direccion as 'Dirección', (select nombreMetodologia from WsEdMetodologia M where M.codigoMetodología = C.codigoMetodologia) as 'Metodolgía', fechaInicio as 'Fecha Inicio', fechaFin as 'Fecha Fin', intensidad as 'Intensidad', clausurada as 'Clausurada', (select idPersona from WsEdFormador F where F.codigoFormador = C.codigoFormador)as 'Formador' from WsEdCapacitación C, WsEdRegistroParticipantes R where C.codigoCapacitacion = R.codigoCapacitacion and R.idPersona = '" + cédula + "' from WsEdCapacitacion where fechaInicio between '" + FechaDesde + "' and '" + FechaHasta + "'";
                    //ConsultaSQL = "select C.codigoCapacitacion as '" + "Código Capacitación" + "', (select nombrePrograma from WsEdPrograma P where P.codigoPrograma = C.codigoPrograma) as '" + "Programa" + "', (select nombreciudad from Canapro.dbo.ciudades where codciudad = C.Ciudad) as '" + "Ciudad" + "', (select nombredepartamento from Canapro.dbo.departamentos where coddepartamento = C.Departamento) as '" + "Departamento" + "', direccion as '" + "Dirección" + "', (select nombreMetodologia from WsEdMetodologia M where M.codigoMetodología = C.codigoMetodologia) as '" + "Metodolgía" + "', fechaInicio as '" + "Fecha Inicio" + "' , fechaFin as '" + "Fecha Fin" + "', intensidad as '" + "Intensidad" + "', clausurada as '" + "Clausurada" + "', (select idPersona from WsEdFormador F where F.codigoFormador = C.codigoFormador)as '" + "Formador" + "' from WsEdCapacitacion where fechaInicio between '" + FechaDesde + "' and '" + FechaHasta + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvRegistroParticipantes.DataSource = tabla;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public String TotalHoras(long cédula)
        {
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select sum (intensidad) from WsEdCapacitacion C, WsEdRegistroParticipantes R where C.codigoCapacitacion = R.codigoCapacitacion and R.idPersona = '"+ cédula + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    Horas = Convert.ToString(datos.GetInt32(0));

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return Horas;
        }

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dgvRegistroParticipantes, int códigoCap)
        {
            try
            {
                ConsultaSQL = "select P.idPersona as '" + "No. Documento" + "', P.nombre as'" + "Nombre" + "', P.direccion as'" + "Dirección" + "', P.telefono as'" + "Teléfono" + "', P.celular as'" + "Celular" + "', P.correoElectronico as'" + "Correo Electrónico" + "' from WsEdPersona P, WsEdRegistroParticipantes R where P.idPersona = R.idPersona and R.codigoCapacitacion = '" + códigoCap + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvRegistroParticipantes.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para consultar el código de la capacitación
        internal int ConsultaCódigo(string Capacitación)
        {
            ObjetoRP = new C_Registro_Participantes();

            string[] vectorSeparar = Capacitación.Split('-');

            string capacitación = vectorSeparar[0];

            CódigoCapacitación = Convert.ToInt32(capacitación);

            return CódigoCapacitación;
        }

        //Método para consultar el código del programa de la capacitación
        internal int ConsultaCódigoP(string Programa)
        {
            ObjetoRP = new C_Registro_Participantes();

            string[] vectorSeparar = Programa.Split('-');

            string programa = vectorSeparar[1];

            string NombrePrograma = programa;

            SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(codigoPrograma)) from WsEdPrograma where ltrim(rtrim(nombrePrograma)) = ltrim(rtrim('" + NombrePrograma + "'))", LN.C_Conexión.ConexiónCanapro());
            SqlDataReader datos = ConsultaDatos.ExecuteReader();

            if (datos.Read() == true)
            {
                CódigoPrograma = Convert.ToInt32(datos.GetString(0));
            }
            return CódigoPrograma;
        }

        //Método para consultar la fecha de la capacitación
        internal string ConsultaFecha(string Fecha)
        {
            ObjetoRP = new C_Registro_Participantes();

            string[] vectorSeparar = Fecha.Split('-');

            string fecha = vectorSeparar[2];

            string FechaFin = fecha;

            return FechaFin;
        }

        internal void FiltrarFechas(string fechaInicio)
        {
            ConsultaSQL = "select fechaInicio from WsEdCpacitacion";
            SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
            adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
            adaptador.SelectCommand.Parameters.Add("@fechaDesde", SqlDbType.DateTime).Value = FechaDesde;
            adaptador.SelectCommand.Parameters.Add("@fechaHasta", SqlDbType.DateTime).Value = FechaHasta;
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
        }

    }
}