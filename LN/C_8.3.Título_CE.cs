using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Título
    {
        public int CódigoTítulo { get; set; } 
        public string Nombre { get; set; }
        public string Descripción { get; set; }
        internal LN.C_Título ObjetoTítulo { get; set; }
        public string ConsultaSQL { get; set; }

        //Método para validar que los registros del Título no se repitan en la base de datos
        internal Boolean ValidarExistenciaTítulo(int CódigoTítulo)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from WsEdTitulo where codigoTitulo = '" + CódigoTítulo + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros del Título en la base de datos 
        internal void GuardarTítulo()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_Codigo = new SqlCommand("select max(codigoTitulo) + 1 from WsEdTitulo", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader codigoNuevo = Recibir_Codigo.ExecuteReader();
                    codigoNuevo.Read();
                    CódigoTítulo = codigoNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    CódigoTítulo = 1;
                    //throw;
                }
                SqlCommand GuardarTítulo = new SqlCommand("insert into WsEdTitulo  values ('" + CódigoTítulo + "', '" + Nombre + "','" + Descripción + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarTítulo.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El título se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros del Título en la base de datos 
        internal void EliminarTítulo(int Registro_Código)
        {
            try
            {
                CódigoTítulo = Registro_Código;
                SqlCommand EliminarTítulo = new SqlCommand("delete from WsEdTitulo where codigoTitulo = '" + CódigoTítulo + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarTítulo.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El título se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros del Título en la base de datos 
        internal void ModificarTítulo()
        {
            try
            {
                SqlCommand ModificarTítulo = new SqlCommand("update WsEdTitulo set codigoTitulo = '" + CódigoTítulo + "', nombreTitulo = '" + Nombre + "', descripcionTitulo = '" + Descripción+ "' where codigoTitulo = " + CódigoTítulo, LN.C_Conexión.ConexiónCanapro());
                ModificarTítulo.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El título se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el código del Título
        public int Cargar_CódigoTítulo()
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select max(codigoTitulo) + 1 from WsEdTitulo", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Código_Nuevo = consulta.ExecuteReader();
                Código_Nuevo.Read();
                CódigoTítulo = Código_Nuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoTítulo = 1;
                //throw;
            }
            return CódigoTítulo;
        }

        //Método para consultar los datos del título y almacenarlos en un objeto
        internal C_Título CargarDatosRegistroTítulo(int Código_Título)
        {
            ObjetoTítulo = new C_Título();
            try
            {
                CódigoTítulo = Código_Título;
                SqlCommand ConsultaDatosTítulo = new SqlCommand("select * from WsEdTitulo where codigoTitulo = '" + CódigoTítulo + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosTítulo.ExecuteReader();
                datos.Read();
                ObjetoTítulo.CódigoTítulo = datos.GetInt32(0);
                ObjetoTítulo.Nombre = datos.GetString(1);
                ObjetoTítulo.Descripción = datos.GetString(2);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoTítulo;
        }

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dtgTítulo)
        {
            try
            {
                ConsultaSQL = "select codigoTitulo as '" + "Código" + "', nombreTítulo as'" + "Nombre" + "', descripcionTitulo as'" + "Descripción" + "' from WsEdTitulo";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgTítulo.DataSource = tabla;
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
