using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Configuración_Fechas
    {

        //Encapsulamiento de varibales;
        public int Id_Fecha { get; set; }
        public string Mes { get; set; }
        public string Valor { get; set; }
        public string Fecha { get; set; }
        public string Fecha_Ingreso { get; set; }
        public string Fecha_Retiro { get; set; }
        public string ConsultaSQL { get; set; }
        internal C_Configuración_Fechas ObjetoFechas { get; set; }
        

        //Método para validar que los registros de las Fechas no se repitan en la base de datos
        internal Boolean ValidarExistenciaFecha(int Registro_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Configuración_Fechas where Id_Fechas = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros de la Configuración de fechas en la base de datos 
        internal void GuardarConfiguraciónFechas()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Fechas) + 1 from Configuración_Fechas", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_Fecha = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_Fecha = 1;
                    //throw;
                }
                SqlCommand GuardarConfiguraciónFechas = new SqlCommand("insert into Configuración_Fechas  values ('" + Id_Fecha + "','" + Mes + "', '" + Valor + "','" + Fecha + "','" + Fecha_Ingreso + "','" + Fecha_Retiro + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarConfiguraciónFechas.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros de la Configuración de fechas en la base de datos
        internal void ModificarConfiguraciónFechas()
        {
            try
            {
                SqlCommand ModificarConfiguraciónFechas = new SqlCommand("update Configuración_Fechas set Id_Fechas = '" + Id_Fecha + "', Mes = '" + Mes + "', Valor = '" + Valor + "', Fecha = '" + Fecha + "', Fecha_Ingreso = '" + Fecha_Ingreso + "', Fecha_Retiro= '" + Fecha_Retiro + "' where Id_Fechas = " + Id_Fecha, LN.C_Conexión.ConexiónCanapro());
                ModificarConfiguraciónFechas.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El registro se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de la Configuración de fechas en la base de datos
        internal void EliminarConfiguraciónFechas(int Registro_ID)
        {
            try
            {
                Id_Fecha = Registro_ID;
                SqlCommand EliminarCréditos = new SqlCommand("delete from Configuración_Fechas where Id_Fechas = '" + Id_Fecha + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarCréditos.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dtgFechas)
        {
            try
            {
                ConsultaSQL = "select Id_Fechas as '" + "Id" + "', Mes, Valor, Fecha, Fecha_Ingreso as'" + "Fecha Ingreso" + "', Fecha_Retiro as '" + "Fecha Retiro" + "' from Configuración_Fechas";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgFechas.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el id del registro
        public int CódigoRegistro(int ID)
        {
            try
            {
                SqlCommand Recibir_Id = new SqlCommand("select max(Id_Fechas) + 1 from Configuración_Fechas", C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = Recibir_Id.ExecuteReader();
                Id_Nuevo.Read();
                Id_Fecha = Id_Nuevo.GetInt32(0);
                C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                Id_Fecha = 1;
                //throw;
            }
            return Id_Fecha;
        }

        //Método para consultar la información de la configuración de fechas y almacenarla en un objeto para cargarla en el formulario configuración de fechas
        internal C_Configuración_Fechas VerConfiguración(int Fecha_ID)
        {
            try
            {
                ObjetoFechas = new C_Configuración_Fechas();
                SqlCommand Ver_configuración_fechas = new SqlCommand("select * from configuración_fechas where Id_fechas = '" + Fecha_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = Ver_configuración_fechas.ExecuteReader();
                if (datos.Read())
                {
                    ObjetoFechas.Id_Fecha = datos.GetInt32(0);
                    ObjetoFechas.Mes = datos.GetString(1);
                    ObjetoFechas.Valor = datos.GetString(2);
                    ObjetoFechas.Fecha = Convert.ToString(datos.GetDateTime(3));
                    ObjetoFechas.Fecha_Ingreso = Convert.ToString(datos.GetDateTime(4));
                    ObjetoFechas.Fecha_Retiro = Convert.ToString(datos.GetDateTime(5));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoFechas;
        }

        //Método para consultar el nombre del mes
        internal C_Configuración_Fechas NombreMes(string valor)
        {
            try
            {
                ObjetoFechas = new C_Configuración_Fechas();
                SqlCommand Ver_configuración_fechas = new SqlCommand("select mes from configuración_fechas where valor = '" + valor + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = Ver_configuración_fechas.ExecuteReader();
                if (datos.Read())
                {
                    ObjetoFechas.Mes = datos.GetString(0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoFechas;
        }

        
    }
}
