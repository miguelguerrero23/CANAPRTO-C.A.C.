using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Persona
    {
        public long IdPersona { get; set; }
        public string Cédula { get; set; }
        public string TipoDocumento { get; set; }
        public int ExpedidaEn { get; set; }
        public string Nombre { get; set; }
        public string FechaRegistro { get; set; } //Automática
        public string Asociado { get; set; }
        public string Género { get; set; }
        public string Dirección { get; set; }
        public string Teléfono { get; set; }
        public int Teléfono1 { get; set; }
        public string Celular { get; set; }
        public long Celular1 { get; set; }
        public string CorreoElectrónico { get; set; }
        public int CódigoEstado { get; set; }
        public string NombreEstado { get; set; }
        public int CódigoCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public int CódigoDepartamento { get; set; } 
        public string NombreDepto { get; set; }
        public string ConsultaSQL { get; set; }
        internal C_Persona ObjetoPersona { get; set; }
        ArrayList lista;

        //Método para validar que los registros de la Persona no se repitan en la base de datos
        internal Boolean ValidarExistenciaPersona(long IdPersona)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from WsEdPersona where idPersona = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros de la Persona en la base de datos 
        internal void GuardarPersona() 
        {
            try
            {

                SqlCommand GuardarPersona = new SqlCommand("insert into WsEdPersona  values ('" + IdPersona + "', '" + TipoDocumento + "', '" + ExpedidaEn + "','" + Nombre + "', '" + FechaRegistro + "', '" + Asociado + "', '" + Género + "', '" + Dirección + "', '" + Teléfono1 + "', '" + Celular1 + "', '" + CorreoElectrónico + "', '" + CódigoDepartamento + "', '" + CódigoCiudad + "', '" + CódigoEstado + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarPersona.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro de la persona se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros de la Persona en la base de datos 
        internal void ModificarPersona()
        {
            try
            {
                SqlCommand ModificarPersona = new SqlCommand("update WsEdPersona set idPersona = '" + IdPersona + "', tipoDocumento = '" + TipoDocumento + "', expedidaEn = '" + ExpedidaEn + "', nombre = '" + Nombre + "', fechaRegistro = '" + FechaRegistro + "', asociado = '" + Asociado + "', genero = '" + Género + "', direccion = '" + Dirección + "', telefono = '" + Teléfono1 + "', celular = '" + Celular1 + "', correoElectronico = '" + CorreoElectrónico + "', Departamento = '" + CódigoDepartamento + "', Ciudad = '" + CódigoCiudad + "', codigoEstado = '" + CódigoEstado + "' where idPersona = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
                ModificarPersona.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("La persona se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de la Persona en la base de datos 
        internal void EliminarPersona(int Registro_Código)
        {
            try
            {
                IdPersona = Registro_Código;
                SqlCommand EliminarPersona = new SqlCommand("delete from WsEdPersona where idPersona = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarPersona.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro de la persona se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para validar que el documento del asociado existe en la base de datos
        internal Boolean ValidarDocumentoAsociado(string documento)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select tipoidentificacion, codciudadcedula, nit, nombreintegrado, coddepartamento, codciudad, direccion, fechaingreso, telefono1, celular, estado, email, relacion from Canapro.dbo.nits where nit = '" + documento + "'", LN.C_Conexión.ConexiónCanapro());
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
        internal C_Persona Datos(long IdPersona)
        {
            ObjetoPersona = new C_Persona();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select tipoDocumento, expedidaEn, nombre, fechaRegistro, asociado, genero, direccion, telefono, celular, correoElectronico, Departamento, Ciudad, codigoEstado from WsEdPersona where ltrim(rtrim(idPersona)) = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    ObjetoPersona.TipoDocumento = datos.GetString(0);
                    ObjetoPersona.ExpedidaEn = datos.GetInt32(1);
                    ObjetoPersona.Nombre = datos.GetString(2);
                    ObjetoPersona.FechaRegistro = Convert.ToString(datos.GetDateTime(3));
                    ObjetoPersona.Asociado = datos.GetString(4);
                    ObjetoPersona.Género = datos.GetString(5);
                    ObjetoPersona.Dirección = datos.GetString(6);
                    ObjetoPersona.Teléfono = Convert.ToString(datos.GetInt32(7));
                    ObjetoPersona.Celular = Convert.ToString(datos.GetInt64(8));
                    ObjetoPersona.CorreoElectrónico = datos.GetString(9);
                    ObjetoPersona.CódigoDepartamento = datos.GetInt32(10);
                    ObjetoPersona.CódigoCiudad = datos.GetInt32(11);
                    ObjetoPersona.CódigoEstado = datos.GetInt32(12);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ObjetoPersona;

        }
        //Método para consultar el nombre del asociado
        internal C_Persona DatosA(string documento)
        {
            ObjetoPersona = new C_Persona();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select tipoidentificacion, codciudadcedula, nombreintegrado, (select sexo from Canapro.dbo.asociados where cedulasociado = '" + documento + "'), coddepartamento, codciudad, direccion, fechaingreso, replace(telefono1, ' ', ''), celular, estado, email from Canapro.dbo.nits where nit = '" + documento + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    ObjetoPersona.TipoDocumento = datos.GetString(0);
                    ObjetoPersona.ExpedidaEn = Convert.ToInt32(datos.GetString(1));
                    ObjetoPersona.Nombre = datos.GetString(2);
                    ObjetoPersona.Género = datos.GetString(3);
                    ObjetoPersona.CódigoDepartamento = Convert.ToInt32(datos.GetString(4));
                    ObjetoPersona.CódigoCiudad = Convert.ToInt32(datos.GetString(5));
                    ObjetoPersona.Dirección = datos.GetString(6);
                    ObjetoPersona.FechaRegistro = Convert.ToString(datos.GetDateTime(7));
                    ObjetoPersona.Teléfono = datos.GetString(8);
                    ObjetoPersona.Celular = datos.GetString(9);
                    ObjetoPersona.NombreEstado = datos.GetString(10);
                    ObjetoPersona.CorreoElectrónico = datos.GetString(11);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ObjetoPersona;
        }


        //Método que realiza la consulta de las ciudades en la base de datos y las almacena en una lista para llevarlas a un Combobox en el formulario Persona
        internal System.Collections.ArrayList LlenarComboCiudad(System.Collections.ArrayList lista, string Depto)
        {
            try
            {
                ArrayList listaCodigoDepartamento = new ArrayList();
                ArrayList listaNombreCiudad = new ArrayList();

                SqlCommand consultardepartamentos = new SqlCommand("select ltrim(rtrim(coddepartamento)) from Canapro.dbo.departamentos where ltrim(rtrim(nombredepartamento)) = '" + Depto + "'", LN.C_Conexión.ConexiónCanapro());
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
                    foreach (var nombreCiud in listaNombreCiudad)
                    {
                        lista.Add(nombreCiud);
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

        //Método que realiza la consulta de los departamentos en la base de datos y las almacena en una lista para llevarlas a un Combobox en el formulario Persona
        internal System.Collections.ArrayList LlenarComboDepto(System.Collections.ArrayList lista)
        {
            try
            {
                ArrayList listaNombreDepartamento = new ArrayList();

                SqlCommand consultarNombreDepartamentos = new SqlCommand("select ltrim(rtrim(nombredepartamento)) from Canapro.dbo.departamentos order by 1 asc", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosNombreDepartamento = consultarNombreDepartamentos.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();
                datosNombreDepartamento.Read();

                while (datosNombreDepartamento.Read())
                {
                    listaNombreDepartamento.Add(datosNombreDepartamento.GetString(0));

                }
                foreach (var nombreDepto in listaNombreDepartamento)
                {
                    lista.Add(nombreDepto);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;

        }

        //Método que realiza la consulta de los departamentos en la base de datos y las almacena en una lista para llevarlas a un Combobox en el formulario Persona
        internal System.Collections.ArrayList LlenarComboExpedidaEn(System.Collections.ArrayList lista)
        {
            try
            {
                ArrayList listaNombreCiudad = new ArrayList();

                SqlCommand consultarNombreCiudades = new SqlCommand("select ltrim(rtrim(nombreciudad)) from Canapro.dbo.ciudades order by 1 asc", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosNombreCiudades = consultarNombreCiudades.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();
                datosNombreCiudades.Read();

                while (datosNombreCiudades.Read())
                {
                    listaNombreCiudad.Add(datosNombreCiudades.GetString(0));

                }
                foreach (var nombreCiudad in listaNombreCiudad)
                {
                    lista.Add(nombreCiudad);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;

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

        //Método para consultar el nombre del estado
        internal string DatosER(long id)
        {
            try
            {
                SqlCommand ConsultaCódigo = new SqlCommand("select ltrim(rtrim(codigoEstado)) from WsEdPersona where idPersona = '" + id + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para consultar el nombre del estado
        internal string DatosEL(long id)
        {
            try
            {
                SqlCommand ConsultaEstado = new SqlCommand("select ltrim(rtrim(estado)) from Canapro.dbo.nits where ltrim(rtrim(nit)) = '" + id + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosE = ConsultaEstado.ExecuteReader();
                if (datosE.Read() == true)
                {
                    NombreEstado = datosE.GetString(0);

                    if (NombreEstado == "A")
                    {
                        NombreEstado = "Activo";
                    }
                    else
                    {
                        NombreEstado = "Inactivo";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return NombreEstado;
        }

        //Método para consultar el nombre de la ciudad del asociado
        internal string DatosCiudadA(long id)
        {
            try
            {
                SqlCommand ConsultaCiudad = new SqlCommand("select ltrim(rtrim(codciudad)) from Canapro.dbo.nits where ltrim(rtrim(nit)) = '" + id + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosC = ConsultaCiudad.ExecuteReader();
                if (datosC.Read() == true)
                {
                    CódigoCiudad = Convert.ToInt32(datosC.GetString(0));

                    SqlCommand consultarciudades = new SqlCommand("select ltrim(rtrim(nombreciudad)) from Canapro.dbo.ciudades where ltrim(rtrim(codciudad)) = '" + CódigoCiudad + "'", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader datosciudad = consultarciudades.ExecuteReader();
                    LN.C_Conexión.ConexiónCanapro().Close();

                    if (datosciudad.Read() == true)
                    {
                        NombreCiudad = datosciudad.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return NombreCiudad;
        }

        //Método para consultar el nombre de la ciudad de la persona
        internal string DatosCiudad(long id)
        {
            try
            {
                SqlCommand consultarciudades = new SqlCommand("select ltrim(rtrim(nombreciudad)) from Canapro.dbo.ciudades where ltrim(rtrim(codciudad)) = '" + CódigoCiudad + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosciudad = consultarciudades.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                if (datosciudad.Read() == true)
                {
                    NombreCiudad = datosciudad.GetString(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return NombreCiudad;
        }

        //Método para consultar el código de la ciudad
        internal int DatosC(string ciudad)
        {
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(codciudad)) from Canapro.dbo.ciudades where ltrim(rtrim(nombreciudad)) = '" + ciudad + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    CódigoCiudad = Convert.ToInt32(datos.GetString(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return CódigoCiudad;
        }

        //Método para consultar el nombre de expedidaEn del asociado
        internal string DatosExpedidaEnA(long id)
        {
            try
            {
                SqlCommand ConsultaCiudad = new SqlCommand("select ltrim(rtrim(codciudadcedula)) from Canapro.dbo.nits where ltrim(rtrim(nit)) = '" + id + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosC = ConsultaCiudad.ExecuteReader();
                if (datosC.Read() == true)
                {
                    CódigoCiudad = Convert.ToInt32(datosC.GetString(0));

                    SqlCommand consultarciudades = new SqlCommand("select ltrim(rtrim(nombreciudad)) from Canapro.dbo.ciudades where ltrim(rtrim(codciudad)) = '" + CódigoCiudad + "'", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader datosciudad = consultarciudades.ExecuteReader();
                    LN.C_Conexión.ConexiónCanapro().Close();

                    if (datosciudad.Read() == true)
                    {
                        NombreCiudad = datosciudad.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return NombreCiudad;
        }

        //Método para consultar el nombre de expedidaEn de la persona
        internal string DatosExpedidaEn(long id)
        {
            try
            {
                SqlCommand consultarciudades = new SqlCommand("select ltrim(rtrim(nombreciudad)) from Canapro.dbo.ciudades where ltrim(rtrim(codciudad)) = '" + ExpedidaEn + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosciudad = consultarciudades.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                if (datosciudad.Read() == true)
                {
                    NombreCiudad = datosciudad.GetString(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return NombreCiudad;
        }

        //Método para consultar el nombre del depto del asociados
        internal string DatosDeptoA(long id)
        {
            try
            {
                SqlCommand ConsultaDepto = new SqlCommand("select ltrim(rtrim(coddepartamento)) from Canapro.dbo.nits where ltrim(rtrim(nit)) = '" + id + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosD = ConsultaDepto.ExecuteReader();
                if (datosD.Read() == true)
                {
                    CódigoDepartamento = Convert.ToInt32(datosD.GetString(0));

                    SqlCommand consultardeptos = new SqlCommand("select ltrim(rtrim(nombredepartamento)) from Canapro.dbo.departamentos where ltrim(rtrim(coddepartamento)) = '" + CódigoDepartamento + "'", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader datosdeptos = consultardeptos.ExecuteReader();
                    LN.C_Conexión.ConexiónCanapro().Close();

                    if (datosdeptos.Read() == true)
                    {
                        NombreDepto = datosdeptos.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return NombreDepto;
        }

        //Método para consultar el nombre del depto de la persona
        internal string DatosDepto(long id)
        {
            try
            {
                SqlCommand consultardeptos = new SqlCommand("select ltrim(rtrim(nombredepartamento)) from Canapro.dbo.departamentos where ltrim(rtrim(coddepartamento)) = '" + CódigoDepartamento + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datosdeptos = consultardeptos.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                if (datosdeptos.Read() == true)
                {
                    NombreDepto = datosdeptos.GetString(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return NombreDepto;
        }

        //Método para consultar el código del depto
        internal int DatosD(string depto)
        {
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(coddepartamento)) from Canapro.dbo.departamentos where ltrim(rtrim(nombredepartamento)) = '" + depto + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    CódigoCiudad = Convert.ToInt32(datos.GetString(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return CódigoCiudad;
        }

    }
}
