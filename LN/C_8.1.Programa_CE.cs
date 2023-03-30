using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Programa
    {
        public int CódigoPrograma { get; set; }
        public string Nombre { get; set; }
        public int Duración { get; set; }
        public string Descripción { get; set; }
        public int CódigoEstado { get; set; }
        internal C_Programa ObjetoPrograma { get; set; }
        public string NombreEstado { get; set; }
        public string ConsultaSQL { get; set; }

        //Método para validar que los registros del Programa no se repitan en la base de datos
        internal Boolean ValidarExistenciaPrograma(int CódigoPrograma)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from WsEdPrograma where codigoPrograma = '" + CódigoPrograma + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros del Programa en la base de datos 
        internal void GuardarPrograma()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_Codigo = new SqlCommand("select max(codigoPrograma) + 1 from WsEdPrograma", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader codigoNuevo = Recibir_Codigo.ExecuteReader();
                    codigoNuevo.Read();
                    CódigoPrograma = codigoNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    CódigoPrograma = 1;
                    //throw;
                }
                SqlCommand GuardarPrograma = new SqlCommand("insert into WsEdPrograma  values ('" + CódigoPrograma + "', '" + Nombre + "','" + Duración + "','" + Descripción + "','" + CódigoEstado + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarPrograma.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El programa se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros del Programa en la base de datos 
        internal void EliminarPrograma(int Registro_Código)
        {
            try
            {
                CódigoPrograma = Registro_Código;
                SqlCommand EliminarPrograma = new SqlCommand("delete from WsEdPrograma where codigoPrograma = '" + CódigoPrograma + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarPrograma.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El programa se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros del Programa en la base de datos 
        internal void ModificarPrograma()
        {
            try
            {
                SqlCommand ModificarPrograma = new SqlCommand("update WsEdPrograma set codigoPrograma = '" + CódigoPrograma + "', nombrePrograma = '" + Nombre + "', duracionPrograma = '" + Duración + "', descripcionPrograma = '" + Descripción + "', codigoEstado = '" + CódigoEstado + "'where codigoPrograma = " + CódigoPrograma, LN.C_Conexión.ConexiónCanapro());
                ModificarPrograma.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El programa se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el código del Programa
        public int Cargar_CódigoPrograma()
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select max(codigoPrograma) + 1 from WsEdPrograma", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Código_Nuevo = consulta.ExecuteReader();
                Código_Nuevo.Read();
                CódigoPrograma = Código_Nuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoPrograma = 1;
                //throw;
            }
            return CódigoPrograma;
        }

        //Método para consultar los datos del programa y almacenarlos en un objeto
        internal C_Programa CargarDatosRegistroPrograma(int Código_Programa)
        {
            ObjetoPrograma = new C_Programa();
            try
            {
                CódigoPrograma = Código_Programa;
                SqlCommand ConsultaDatosPrograma = new SqlCommand("select * from WsEdPrograma where codigoPrograma = '" + CódigoPrograma + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosPrograma.ExecuteReader();
                datos.Read();
                ObjetoPrograma.CódigoPrograma = datos.GetInt32(0);
                ObjetoPrograma.Nombre = datos.GetString(1);
                ObjetoPrograma.Duración = datos.GetInt32(2);
                ObjetoPrograma.Descripción = datos.GetString(3);
                ObjetoPrograma.CódigoEstado = datos.GetInt32(4);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoPrograma;
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
        internal string DatosER(int cP)
        {
            try
            {
                SqlCommand ConsultaCódigo = new SqlCommand("select ltrim(rtrim(codigoEstado)) from WsEdPrograma where codigoPrograma = '"+ cP +"'", LN.C_Conexión.ConexiónCanapro());
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

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dgvPrograma)
        {
            try
            {
                ConsultaSQL = "select codigoPrograma as '" + "Código" + "', nombrePrograma as'" + "Nombre" + "', duracionPrograma as'" + "Duración" + "', descripcionPrograma as'" + "Descripción" + "', codigoEstado as'" + "Código Estado" + "' from WsEdPrograma";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvPrograma.DataSource = tabla;
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
