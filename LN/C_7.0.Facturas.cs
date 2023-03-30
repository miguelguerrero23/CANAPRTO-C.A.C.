using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Facturas
    {

        //Definición y encapsulamiento de variables
        public int Código { get; set; }
        public int Id_Factura { get; set; }
        public int Id_Usuario { get; set; }
        public string Fecha { get; set; }
        public string Cédula { get; set; }
        public string Nombre { get; set; }
        public string Dirección { get; set; }
        public string Teléfono { get; set; }
        public decimal Total_Factura { get; set; }
        public string TotalFacturaEnLetras { get; set; }
        internal LN.C_Facturas ObjetoFacturas { get; set; }
        public string ConsultaSQL { get; set; }


        //Método para validar que los registros de las facturas no se repitan en la base de datos
        internal Boolean ValidarExistenciaDeFactura(int Factura_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Factura where Id_Factura = '" + Factura_ID + "'", LN.C_Conexión.ConexiónCanapro());
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
            }
        }

        //Método para guardar los registros de las Facturas en la base de datos 
        internal void GuardarRegistroFactura()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Factura) + 1 from Factura", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_Factura = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_Factura = 1;
                    //throw;
                }
                SqlCommand GuardarRegistroFactura = new SqlCommand("insert into Factura values ('" + Id_Factura + "','" + Id_Usuario + "', '" + Cédula + "', '" + Nombre + "', '" + Teléfono + "','" + Dirección + "','" + Fecha + "', '" + Total_Factura + "', '" + TotalFacturaEnLetras + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarRegistroFactura.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro de la factura se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros de las Facturas en la base de datos 
        internal void ModificarRegistroFactura()
        {
            try
            {
                SqlCommand ModificarRegistroFactura = new SqlCommand("update Factura set Id_Factura = '" + Id_Factura + "', Id_Usuarios = '" + Id_Usuario + "', Documento = '" + Cédula + "', Nombre = '" + Nombre + "', Teléfono_Celular = '" + Teléfono + "', Dirección = '" + Dirección + "', Fecha = '" + Fecha + "', Total_Factura = '" + Total_Factura + "', Total_Factura_Letras = '" + TotalFacturaEnLetras + "' where Id_Factura = " + Id_Factura, LN.C_Conexión.ConexiónCanapro());
                ModificarRegistroFactura.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("La factura se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de las Facturas la base de datos 
        internal void EliminarRegistroFactura(int Registro_ID)
        {
            try
            {
                SqlCommand EliminarRegistroFactura = new SqlCommand("delete from Factura where Id_Factura = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarRegistroFactura.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("La factura se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Para eliminar una factura, debe eliminar primero todos los detalles asociados a la factura. En caso de que el error perista, comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el id de la Factura
        public int Código_Registro(int ID)
        {
            try
            {
                SqlCommand Recibir_Id = new SqlCommand("select max(Id_Factura) + 1 from Factura ", C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = Recibir_Id.ExecuteReader();
                Id_Nuevo.Read();
                Código = Id_Nuevo.GetInt32(0);
                C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                Código = 1;
                //throw;
            }
            return Código;
        }

        //Método para consultar el ID del usuario en la base de datos
        internal C_Facturas ConsultarDatosUsuario(string usuario)
        {
            ObjetoFacturas = new C_Facturas();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select id_usuarios from usuarios where usuario = '" + usuario + "'", LN.C_Conexión.ConexiónCanapro()); SqlDataReader user = ConsultaUsuario.ExecuteReader();

                if (user.Read())
                {
                    ObjetoFacturas.Id_Usuario = user.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoFacturas;
        }

        //Método para realizar consulta de las facturas por número de cédula
        internal void ConsultarFacturaPorCédula(DataGridView dtgHistorialFacturas, string cédula)
        {
            try
            {
                ConsultaSQL = "select id_factura as '" + "Id Factura" + "', Documento as '" + "N° Cédula" + "', Nombre as '" + "Nombre Proveedor" + "', Teléfono_Celular as '" + "Teléfono/Celular" + "', Dirección, Fecha, Total_Factura as '" + "Total Factura" + "', Total_Factura_Letras as '" + "Total en Letras" + "' , (select nombre from usuarios where usuarios.Id_Usuarios = factura.Id_Usuarios) as '" + "Nombre de Usuario" + "' from Factura where Documento = '" + cédula + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFacturas.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de las facturas por mes
        internal void ConsultarFacturaPorMes(DataGridView dtgHistorialFacturas, string Mes)
        {
            try
            {
                ConsultaSQL = "select id_factura as '" + "Id Factura" + "', Documento as '" + "N° Cédula" + "', Nombre as '" + "Nombre Proveedor" + "', Teléfono_Celular as '" + "Teléfono/Celular" + "', Dirección, Fecha, Total_Factura as '" + "Total Factura" + "' , Total_Factura_Letras as '" + "Total en Letras" + "', (select nombre from usuarios where usuarios.Id_Usuarios = factura.Id_Usuarios) as '" + "Nombre de Usuario" + "'   from Factura where month(Fecha) = '" + Mes + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFacturas.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para consultar los datos de la factura y almacenarlos en un objeto
        internal C_Facturas CargarDatosFactura(int Factura_Id)
        {
            ObjetoFacturas = new C_Facturas();
            try
            {
                Id_Factura = Factura_Id;
                SqlCommand ConsultaDatosFactura = new SqlCommand("select * from Factura where Id_Factura = '" + Id_Factura + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosFactura.ExecuteReader();               

                if (datos.Read())
                {
                    ObjetoFacturas.Id_Factura = datos.GetInt32(0);
                    ObjetoFacturas.Id_Usuario = datos.GetInt32(1);
                    ObjetoFacturas.Cédula = datos.GetString(2);
                    ObjetoFacturas.Nombre = datos.GetString(3);
                    ObjetoFacturas.Teléfono = datos.GetString(4);
                    ObjetoFacturas.Dirección = datos.GetString(5);
                    ObjetoFacturas.Fecha = Convert.ToString(datos.GetDateTime(6));
                    ObjetoFacturas.Total_Factura = datos.GetDecimal(7);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoFacturas;
        }

        //Método para convertir valor total factura en letras
        public string ConvertirEnLetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        //Método que asigna el texto a la conversión del valor total en número a letras
        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }



    }
}
