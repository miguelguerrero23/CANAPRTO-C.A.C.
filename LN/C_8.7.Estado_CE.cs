using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Estado
    {
        public int CódigoEstado { get; set; }
        public string Nombre { get; set; }
        public string ConsultaSQL { get; set; }
        internal C_Estado ObjetoEstado { get; set; }

        //Método para validar que los registros del Estado no se repitan en la base de datos
        internal Boolean ValidarExistenciaEstado(string NombreEstado)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from WsEdEstado where nombreEstado = '" + NombreEstado + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros del Estado en la base de datos 
        internal void GuardarEstado()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_Codigo = new SqlCommand("select max(codigoEstado) + 1 from WsEdEstado", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader codigoNuevo = Recibir_Codigo.ExecuteReader();
                    codigoNuevo.Read();
                    CódigoEstado = codigoNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    CódigoEstado = 1;
                    //throw;
                }
                SqlCommand GuardarEstado = new SqlCommand("insert into WsEdEstado  values ('" + CódigoEstado + "', '" + Nombre + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarEstado.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El estado se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros del Estado en la base de datos 
        internal void EliminarEstado(int Registro_Código)
        {
            try
            {
                CódigoEstado = Registro_Código;
                SqlCommand EliminarEstado = new SqlCommand("delete from WsEdEstado where codigoEstado = '" + CódigoEstado + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarEstado.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El estado se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        
        //Método para modificar los registros del Estado en la base de datos 
        internal void ModificarEstado()
        {
            try
            {
                SqlCommand ModificarEstado = new SqlCommand("update WsEdEstado set codigoEstado = '" + CódigoEstado + "', nombreEstado = '" + Nombre + "' where codigoEstado = '"+ CódigoEstado +"'", LN.C_Conexión.ConexiónCanapro());
                ModificarEstado.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El estado se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el código del Estado
        public int Cargar_CódigoEstado()
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select max(codigoEstado) + 1 from WsEdEstado", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Código_Nuevo = consulta.ExecuteReader();
                Código_Nuevo.Read();
                CódigoEstado = Código_Nuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoEstado = 1;
                //throw;
            }
            return CódigoEstado;
        }

        //Método para consultar los datos del estado y almacenarlos en un objeto
        internal C_Estado CargarDatosRegistroEstado(int Código_Estado)
        {
            ObjetoEstado = new C_Estado();
            try
            {
                CódigoEstado = Código_Estado;
                SqlCommand ConsultaDatosEstado = new SqlCommand("select * from WsEdEstado where codigoEstado = '" + CódigoEstado + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosEstado.ExecuteReader();
                datos.Read();
                ObjetoEstado.CódigoEstado = datos.GetInt32(0);
                ObjetoEstado.Nombre = datos.GetString(1);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoEstado;
        }

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dgvEstado)
        {
            try
            {
                ConsultaSQL = "select codigoEstado as '" + "Código" + "', nombreEstado as'" + "Nombre" + "' from WsEdEstado";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvEstado.DataSource = tabla;
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
