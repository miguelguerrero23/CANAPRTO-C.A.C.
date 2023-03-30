using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Detalle_Factura
    {

        //Definición y encapsulamiento de variables
        public int Id_Detalle { get; set; }
        public int Id_Factura { get; set; }
        public int Cantidad { get; set; }
        public string Concepto { get; set; }
        public decimal Valor_unitario { get; set; }
        public decimal Valor_total { get; set; }
        public string Valor_letras { get; set; }
        public int Impuesto { get; set; }
        public string ConsultaSQL { get; set; }
        internal C_Detalle_Factura ObjetoDetalle { get; set; }

        //Método para validar que los registros del detalle de la factura no se repita en la base de datos
        internal Boolean ValidarExistenciaDetalleFactura(int Detalle_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Detalle_Factura where Id_Detalle = '" + Detalle_ID + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros del detalle de la factura en la base de datos 
        internal void GuardarRegistroDetalleFactura()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Detalle) + 1 from Detalle_Factura", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_Detalle = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_Detalle = 1;
                    //throw;
                }
                SqlCommand GuardarRegistroDetalleFactura = new SqlCommand("insert into Detalle_Factura values ('" + Id_Detalle + "','" + Id_Factura + "', '" + Cantidad + "', '" + Concepto + "', '" + Valor_unitario + "','" + Impuesto + "','" + Valor_total + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarRegistroDetalleFactura   .ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros del detalle de la factura en la base de datos 
        internal void ModificarRegistroDetalleFactura()
        {
            try
            {
                SqlCommand ModificarRegistroDetalleFactura = new SqlCommand("update Detalle_Factura set Id_Detalle = '" + Id_Detalle + "', Id_Factura = '" + Id_Factura + "', Cantidad = '" + Cantidad + "', Concepto = '" + Concepto + "', Valor_Unitario = '" + Valor_unitario + "', Impuesto = '" + Impuesto + "', Valor_total = '" + Valor_total + "' where Id_Detalle = " + Id_Detalle, LN.C_Conexión.ConexiónCanapro());
                ModificarRegistroDetalleFactura .ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El registro se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros  del detalle de la factura la base de datos 
        internal void EliminarRegistroDetalleFactura(int Detalle_ID)
        {
            try
            {
                SqlCommand EliminarRegistroFactura = new SqlCommand("delete from Detalle_Factura where Id_Detalle = '" + Detalle_ID + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarRegistroFactura.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método que realiza la consulta para actualizar la Tabla con el detalle factura
        internal void ActualizarTabla(DataGridView dtgDetalleFactura, int Factura_ID)
        {
            try
            {
                ConsultaSQL = "select Id_detalle as '" + "Id Detalle" + "', Cantidad, Concepto, Valor_Unitario as '" + "Valor Unitario" + "', Impuesto, Valor_Total as '" + "Valor Total" + "' from Detalle_Factura where Id_Factura = '" + Factura_ID + "' order by 1 asc";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgDetalleFactura.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el id del detalle de la Factura
        public int Código_Registro(int ID)
        {
            try
            {
                SqlCommand Recibir_Id = new SqlCommand("select max(Id_Detalle) + 1 from Detalle_Factura ", C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = Recibir_Id.ExecuteReader();
                Id_Nuevo.Read();
                Id_Detalle = Id_Nuevo.GetInt32(0);
                C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                Id_Detalle = 1;
                //throw;
            }
            return Id_Detalle;
        }

        //Método para consultar la información del detalle de la factura y almacenarla en un objeto para cargarla en el formulario factura
        internal C_Detalle_Factura ConsultarDetalleFactura(int Detalle_ID)
        {
            try
            {
                ObjetoDetalle = new C_Detalle_Factura();
                SqlCommand Ver_DetalleFactura = new SqlCommand("select * from Detalle_Factura where Id_Detalle = '" + Detalle_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = Ver_DetalleFactura.ExecuteReader();
                if (datos.Read())
                {
                    ObjetoDetalle.Id_Detalle = datos.GetInt32(0);
                    ObjetoDetalle.Id_Factura = datos.GetInt32(1);
                    ObjetoDetalle.Cantidad = datos.GetInt32(2);
                    ObjetoDetalle.Concepto = datos.GetString(3);
                    ObjetoDetalle.Valor_unitario = datos.GetDecimal(4);
                    ObjetoDetalle.Impuesto = datos.GetInt32(5);
                    ObjetoDetalle.Valor_total = datos.GetDecimal(6);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoDetalle;
        }

        //Método para modificar los registros del detalle de la factura en la base de datos 
        internal void ActualizarTotalFactura(decimal Total, int Factura_ID)
        {
            try
            {
                LN.C_Facturas facturas = new C_Facturas();
                Valor_letras = facturas.ConvertirEnLetras(Convert.ToString(Total));
                SqlCommand ActualizarTotalFactura = new SqlCommand("update Factura set Total_Factura = '" + Total + "', Total_Factura_Letras = '" + Valor_letras + "' where Id_Factura = '" + Factura_ID + "'", LN.C_Conexión.ConexiónCanapro());
                ActualizarTotalFactura.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
