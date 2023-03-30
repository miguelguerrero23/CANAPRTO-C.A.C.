using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Metodología
    {
        public int CódigoMetodología { get; set; }
        public string Nombre { get; set; }
        public string ConsultaSQL { get; set; }
        internal LN.C_Metodología ObjetoMetodología { get; set; }

        //Método para validar que los registros de Metodología no se repitan en la base de datos
        internal Boolean ValidarExistenciaMetodología(string NombreMetodología)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from WsEdMetodologia where nombreMetodologia = '" + NombreMetodología + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros de Metodología en la base de datos 
        internal void GuardarMetodología()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_Codigo = new SqlCommand("select max(codigoMetodologia) + 1 from WsEdMetodologia", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader codigoNuevo = Recibir_Codigo.ExecuteReader();
                    codigoNuevo.Read();
                    CódigoMetodología = codigoNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    CódigoMetodología = 1;
                    //throw;
                }
                SqlCommand GuardarMetodología = new SqlCommand("insert into WsEdMetodologia  values ('" + CódigoMetodología + "', ltrim(rtrim('" + Nombre + "')))", LN.C_Conexión.ConexiónCanapro());
                GuardarMetodología.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("La metodología se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de Metodología en la base de datos 
        internal void EliminarMetodología(int Registro_Código)
        {
            try
            {
                CódigoMetodología = Registro_Código;
                SqlCommand EliminarMetodología = new SqlCommand("delete from WsEdMetodologia where codigoMetodologia = '" + CódigoMetodología + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarMetodología.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("La metodología se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros de Metodología en la base de datos 
        internal void ModificarMetodología()
        {
            try
            {
                SqlCommand ModificarMetodología = new SqlCommand("update WsEdMetodologia set codigoMetodologia = '" + CódigoMetodología + "', nombreMetodologia = ltrim(rtrim('" + Nombre + "')) where codigoMetodología = '"+ CódigoMetodología +"'", LN.C_Conexión.ConexiónCanapro());
                ModificarMetodología.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("La metodología se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el código de la Metodología
        public int Cargar_CódigoMetodología()
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select max(codigoMetodologia) + 1 from WsEdMetodologia", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Código_Nuevo = consulta.ExecuteReader();
                Código_Nuevo.Read();
                CódigoMetodología = Código_Nuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoMetodología = 1;
                //throw;
            }
            return CódigoMetodología;
        }

        //Método para consultar los datos de la metodología y almacenarlos en un objeto
        internal C_Metodología CargarDatosRegistroMetodología(int Código_Metodología)
        {
            ObjetoMetodología = new C_Metodología();
            try
            {
                CódigoMetodología = Código_Metodología;
                SqlCommand ConsultaDatosMetodología = new SqlCommand("select * from WsEdMetodología where codigoMetodología = '" + CódigoMetodología + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosMetodología.ExecuteReader();
                datos.Read();
                ObjetoMetodología.CódigoMetodología = datos.GetInt32(0);
                ObjetoMetodología.Nombre = datos.GetString(1);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoMetodología;
        }

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dgvMetodología)
        {
            try
            {
                ConsultaSQL = "select codigoMetodologia as '" + "Código" + "', nombreMetodología as'" + "Nombre" + "' from WsEdMetodologia";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvMetodología.DataSource = tabla;
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
