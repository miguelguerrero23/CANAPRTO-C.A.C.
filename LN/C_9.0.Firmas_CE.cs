using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Firmas
    {
        public int CodigoFirma { get; set; }
        public long IdPersona { get; set; }
        public string nombre { get; set; }
        public string roll { get; set; }
        public string ConsultaSQL { get; set; }
        internal C_Firmas ObjetoFirma { get; set; }

        //Método para validar que los registros del Estado no se repitan en la base de datos
        internal Boolean ValidarFirma(int IdPersona)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from WsEdFirma where IdPersona = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros de Firma en la base de datos 
        internal void GuardarFirma()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_Codigo = new SqlCommand("select max(CodigoFirma) + 1 from WsEdFirma", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader codigoNuevo = Recibir_Codigo.ExecuteReader();
                    codigoNuevo.Read();
                    CodigoFirma = codigoNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    CodigoFirma = 1;
                    //throw;
                }
                SqlCommand GuardarFirma = new SqlCommand("insert into WsEdFirma values ('" + CodigoFirma + "','" + IdPersona + "','" + nombre + "','" + roll + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarFirma.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("La firma se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de Firma en la base de datos 
        internal void EliminarFirma(int Registro_Código)
        {
            try
            {
                CodigoFirma = Registro_Código;
                SqlCommand EliminarFirma = new SqlCommand("delete from WsEdFirma where codigoFirma = '" + CodigoFirma + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarFirma.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro de la firma se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el código del Programa
        public int Cargar_CódigoFirma()
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select max(CodigoFirma) + 1 from WsEdFirma", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Código_Nuevo = consulta.ExecuteReader();
                Código_Nuevo.Read();
                CodigoFirma = Código_Nuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CodigoFirma = 1;
                //throw;
            }
            return CodigoFirma;
        }

        //Método para consultar los datos del estado y almacenarlos en un objeto
        internal C_Firmas CargarDatosRegistroFirma(int Registro_Código)
        {
            ObjetoFirma = new C_Firmas();
            try
            {
                IdPersona = Registro_Código;
                SqlCommand ConsultaDatosFirmas = new SqlCommand("select * from WsEdFirma where IdPersona = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosFirmas.ExecuteReader();
                datos.Read();
                ObjetoFirma.CodigoFirma = datos.GetInt32(0);
                ObjetoFirma.IdPersona = IdPersona;
                ObjetoFirma.roll = datos.GetString(2);
                ObjetoFirma.nombre = datos.GetString(3);

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoFirma;
        }

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dgvFirmas)
        {
            try
            {
                ConsultaSQL = "select CodigoFirma as '" + "Codigo" + "', IdPersona as '" + "No. Documento" + "', nombre as'" + "Nombre" + "', roll as'" + "Tipo de Rol" + "' from WsEdFirma";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvFirmas.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        
        //Método para consultar el nombre de la persona
        internal C_Firmas Datos(long IdPersona)
        {
            ObjetoFirma = new C_Firmas();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(nombre)), ltrim(rtrim(roll)) from WsEdFirma where ltrim(rtrim(IdPersona)) = '" + IdPersona + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    //ObjetoFirma.CodigoFirma = datos.GetInt32(0);
                    //ObjetoFirma.IdPersona = datos.GetInt32(1);
                    ObjetoFirma.nombre = datos.GetString(0);
                    ObjetoFirma.roll = datos.GetString(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ObjetoFirma;
        }
    }
}