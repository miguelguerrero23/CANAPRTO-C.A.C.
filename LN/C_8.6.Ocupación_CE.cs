using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Ocupación
    {
        public int CódigoOcupación { get; set; }
        public string Nombre { get; set; }
        public string ConsultaSQL { get; set; }
        internal C_Ocupación ObjetoOcupación { get; set; }

        //Método para validar que los registros de la Ocupación no se repitan en la base de datos
        internal Boolean ValidarExistenciaOcupación(int CódigoOcupación)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from WsEdOcupación where codigoOcupación = '" + CódigoOcupación + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros de la Ocupación en la base de datos 
        internal void GuardarOcupación()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_Codigo = new SqlCommand("select max(codigoOcupacion) + 1 from WsEdOcupacion", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader codigoNuevo = Recibir_Codigo.ExecuteReader();
                    codigoNuevo.Read();
                    CódigoOcupación = codigoNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    CódigoOcupación = 1;
                    //throw;
                }
                SqlCommand GuardarOcupación = new SqlCommand("insert into WsEdOcupacion  values ('" + CódigoOcupación + "', '" + Nombre + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarOcupación.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("La ocupación se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de la Ocupación en la base de datos 
        internal void EliminarOcupación(int Registro_Código)
        {
            try
            {
                CódigoOcupación = Registro_Código;
                SqlCommand EliminarOcupación = new SqlCommand("delete from WsEdOcupacion where codigoOcupacion = '" + CódigoOcupación + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarOcupación.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("La ocupación se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros de la Ocupación en la base de datos 
        internal void ModificarOcupación()
        {
            try
            {
                SqlCommand ModificarOcupación = new SqlCommand("update WsEdOcupacion set codigoOcupacion = '" + CódigoOcupación + "', nombreOcupacion = '" + Nombre + "' where codigoOcupacion = " + CódigoOcupación, LN.C_Conexión.ConexiónCanapro());
                ModificarOcupación.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("La ocupación se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el código de la Ocupación
        public int Cargar_CódigoOcupación()
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select max(codigoOcupacion) + 1 from WsEdOcupacion", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Código_Nuevo = consulta.ExecuteReader();
                Código_Nuevo.Read();
                CódigoOcupación = Código_Nuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoOcupación = 1;
                //throw;
            }
            return CódigoOcupación;
        }

        //Método para consultar los datos de la ocupación y almacenarlos en un objeto
        internal C_Ocupación CargarDatosRegistroOcupación(int Código_Ocupación)
        {
            ObjetoOcupación = new C_Ocupación();
            try
            {
                CódigoOcupación = Código_Ocupación;
                SqlCommand ConsultaDatosOcupación = new SqlCommand("select * from WsEdOcupacion where codigoOcupación = '" + CódigoOcupación + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosOcupación.ExecuteReader();
                datos.Read();
                ObjetoOcupación.CódigoOcupación = datos.GetInt32(0);
                ObjetoOcupación.Nombre = datos.GetString(1);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoOcupación;
        }

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dgvOcupación)
        {
            try
            {
                ConsultaSQL = "select codigoOcupacion as '" + "Código" + "', nombreOcupacion as'" + "Nombre" + "' from WsEdOcupacion";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvOcupación.DataSource = tabla;
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
