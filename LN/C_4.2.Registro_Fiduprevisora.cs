using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Registro_Fiduprevisora
    {

        //Definición y encapsulamiento de variables
        public int Id_Registro { get; set; }
        public int Id_Usuario { get; set; }
        public string NúmeroCédula { get; set; }
        public string Número_Autorización { get; set; }
        public string Fecha_Autorización { get; set; }
        public decimal Valor_Total { get; set; }
        public decimal Valor_Mensual { get; set; }
        public int Porcentaje { get; set; }
        public int Número_Cuotas { get; set; }
        public string Fecha_Descuento { get; set; }
        public string Nit_CANAPRO { get; set; }
        public int Concepto { get; set; }
        public int Novedad { get; set; }
        public string Fecha { get; set; }
        public string NombreIntegrado { get; set; }
        internal LN.C_Registro_Fiduprevisora ObjetoPrevisora { get; set; }
        public string ConsultaSQL { get; set; }

        //Método para validar que los registros de PREVISORA no se repitan en la base de datos
        internal Boolean ValidarExistenciaPREVISORA(int Registro_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Registro_Fiduprevisora where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros PREVISORA en la base de datos 
        internal void GuardarRegistroPREVISORA()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Registro) + 1 from Registro_Fiduprevisora", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_Registro = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_Registro = 1;
                    //throw;
                }
                SqlCommand GuardarRegistroPREVISORA = new SqlCommand("insert into Registro_Fiduprevisora  values ('" + Id_Registro + "','" + Id_Usuario + "', '" + NúmeroCédula + "','" + Número_Autorización + "','" + Fecha_Autorización + "','" + Valor_Total + "', '" + Valor_Mensual + "', '" + Porcentaje + "', '" + Número_Cuotas + "', '" + Fecha_Descuento + "', '" + Nit_CANAPRO + "', '" + Concepto + "', '" + Novedad + "', '" + Fecha + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarRegistroPREVISORA.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros PREVISORA en la base de datos 
        internal void ModificarRegistroPREVISORA()
        {
            try
            {
                SqlCommand ModificarRegistroPREVISORA = new SqlCommand("update Registro_Fiduprevisora set Id_Registro = '" + Id_Registro + "', Id_Usuario = '" + Id_Usuario + "', Cédula = '" + NúmeroCédula + "', NúmeroAutorización = '" + Número_Autorización + "', FechaAutorización = '" + Fecha_Autorización + "', ValorTotal = '" + Valor_Total + "', ValorMensual = '" + Valor_Mensual + "', Porcentaje = '" + Porcentaje + "', NúmeroCuotas = '" + Número_Cuotas + "', FechaDescuento = '" + Fecha_Descuento + "', NITCanapro = '" + Nit_CANAPRO + "', Concepto = '" + Concepto + "', CódigoNovedad = '" + Novedad + "', FechaRegistro = '" + Fecha + "' where Id_Registro = " + Id_Registro, LN.C_Conexión.ConexiónCanapro());
                ModificarRegistroPREVISORA.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El registro se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros PREVISORA en la base de datos 
        internal void EliminarRegistroPREVISORA(int Registro_ID)
        {
            try
            {
                Id_Registro = Registro_ID;
                SqlCommand EliminarRegistroPREVISORA = new SqlCommand("delete from Registro_Fiduprevisora where Id_Registro = '" + Id_Registro + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarRegistroPREVISORA.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el id del registro
        public int Cargar_IdRegistro()
        {
            try
            {
                SqlCommand consulta = new SqlCommand("select max(Id_Registro) + 1 from Registro_Fiduprevisora", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = consulta.ExecuteReader();
                Id_Nuevo.Read();
                Id_Registro = Id_Nuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                Id_Registro = 1;
                //throw;
            }
            return Id_Registro;
        }

        //Método para consultar el nombre del asociado
        internal C_Registro_Fiduprevisora DatosUsuario(string documento)
        {
            ObjetoPrevisora = new C_Registro_Fiduprevisora();
            try
            {
                SqlCommand ConsultaDatos = new SqlCommand("select ltrim(rtrim(nombreintegrado)) from Canapro.dbo.nits where nit = '" + documento + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatos.ExecuteReader();

                if (datos.Read() == true)
                {
                    ObjetoPrevisora.NombreIntegrado = datos.GetString(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoPrevisora;
        }

        //Método para validar que el documento del asociado existe en la base de datos
        internal Boolean ValidarDocumentoAsociado(string documento)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Canapro.dbo.nits where nit = '" + documento + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para consultar la fecha en la base de datos
        internal C_Registro_Fiduprevisora ConsultarFechas(string Mes)
        {
            ObjetoPrevisora = new C_Registro_Fiduprevisora();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select Fecha from Configuración_Fechas where mes = '" + Mes + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaUsuario.ExecuteReader();

                if (datos.Read())
                {
                    ObjetoPrevisora.Fecha = Convert.ToString(datos.GetDateTime(0));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoPrevisora;
        }

        //Método para consultar el ID del usuario en la base de datos
        internal C_Registro_Fiduprevisora ConsultarDatosUsuario(string usuario)
        {
            ObjetoPrevisora = new C_Registro_Fiduprevisora();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select id_usuarios from usuarios where usuario = '" + usuario + "'", LN.C_Conexión.ConexiónCanapro()); SqlDataReader user = ConsultaUsuario.ExecuteReader();

                if (user.Read())
                {
                    ObjetoPrevisora.Id_Usuario = user.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoPrevisora;
        }

        //Método para realizar consulta de los registros PREVISORA por número de cédula
        internal void ConsultarRegistroPREVISORAPorCédula(DataGridView dtgHistorialFiduprevisora, string cédula)
        {
            try
            {
                ConsultaSQL = "select Cédula, NúmeroAutorización as '" + "Número Libranza" + "', FechaAutorización as '" + "Fecha Autorización" + "', ValorTotal as '" + "Valor Total" + "', ValorMensual as '" + "Valor Mensual" + "', Porcentaje, NúmeroCuotas as '" + "Número Cuotas" + "', FechaDescuento as '" + "Fecha Efectividad" + "', NitCanapro as '" + "NIT" + "', Concepto, CódigoNovedad as '" + "Código Novedad" + "', FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro"  + "', (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_Fiduprevisora where Cédula = '" + cédula + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFiduprevisora.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de los registros PREVISORA por número de autorización
        internal void ConsultarRegistroPREVISORAPorNúmeroAutorización(DataGridView dtgHistorialFiduprevisora, string autorización)
        {
            try
            {
                ConsultaSQL = "select Cédula, NúmeroAutorización as '" + "Número Libranza" + "', FechaAutorización as '" + "Fecha Autorización" + "', ValorTotal as '" + "Valor Total" + "', ValorMensual as '" + "Valor Mensual" + "', Porcentaje, NúmeroCuotas as '" + "Número Cuotas" + "', FechaDescuento as '" + "Fecha Efectividad" + "', NitCanapro as '" + "NIT" + "', Concepto, CódigoNovedad as '" + "Código Novedad" + "', FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_Fiduprevisora where NúmeroAutorización = '" + autorización  + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFiduprevisora.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar cconsulta de los registros PREVISORA  por mes de autorización
        internal void ConsultarRegistroPREVISORAPorFechaAutorización(DataGridView dtgHistorialFiduprevisora, string FechaAutorización)
        {
            try
            {
                ConsultaSQL = "select Cédula, NúmeroAutorización as '" + "Número Libranza" + "', FechaAutorización as '" + "Fecha Autorización" + "', ValorTotal as '" + "Valor Total" + "', ValorMensual as '" + "Valor Mensual" + "', Porcentaje, NúmeroCuotas as '" + "Número Cuotas" + "', FechaDescuento as '" + "Fecha Efectividad" + "', NitCanapro as '" + "NIT" + "', Concepto, CódigoNovedad as '" + "Código Novedad" + "', FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_Fiduprevisora where Month(FechaAutorización) = '" + FechaAutorización + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFiduprevisora.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar cconsulta de los registros PREVISORA  por mes de efectividad
        internal void ConsultarRegistroPREVISORAPorFechaEfectividad(DataGridView dtgHistorialFiduprevisora, string FechaEfectividad)
        {
            try
            {
                ConsultaSQL = "select Cédula, NúmeroAutorización as '" + "Número Libranza" + "', FechaAutorización as '" + "Fecha Autorización" + "', ValorTotal as '" + "Valor Total" + "', ValorMensual as '" + "Valor Mensual" + "', Porcentaje, NúmeroCuotas as '" + "Número Cuotas" + "', FechaDescuento as '" + "Fecha Efectividad" + "', NitCanapro as '" + "NIT" + "', Concepto, CódigoNovedad as '" + "Código Novedad" + "', FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_Fiduprevisora where Month(FechaDescuento) = '" + FechaEfectividad + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFiduprevisora.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar cconsulta de los registros PREVISORA  por concepto
        internal void ConsultarRegistroPREVISORAPorConcepto(DataGridView dtgHistorialFiduprevisora, string Concepto)
        {
            try
            {
                ConsultaSQL = "select Cédula, NúmeroAutorización as '" + "Número Libranza" + "', FechaAutorización as '" + "Fecha Autorización" + "', ValorTotal as '" + "Valor Total" + "', ValorMensual as '" + "Valor Mensual" + "', Porcentaje, NúmeroCuotas as '" + "Número Cuotas" + "', FechaDescuento as '" + "Fecha Efectividad" + "', NitCanapro as '" + "NIT" + "', Concepto, CódigoNovedad as '" + "Código Novedad" + "', FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_Fiduprevisora where Concepto = '" + Concepto + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFiduprevisora.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar cconsulta de los registros PREVISORA  por usuario
        internal void ConsultarRegistroPREVISORAPorUsuario(DataGridView dtgHistorialFiduprevisora, string Usuario)
        {
            try
            {
                ConsultaSQL = "select Cédula, NúmeroAutorización as '" + "Número Libranza" + "', FechaAutorización as '" + "Fecha Autorización" + "', ValorTotal as '" + "Valor Total" + "', ValorMensual as '" + "Valor Mensual" + "', Porcentaje, NúmeroCuotas as '" + "Número Cuotas" + "', FechaDescuento as '" + "Fecha Efectividad" + "', NitCanapro as '" + "NIT" + "', Concepto, CódigoNovedad as '" + "Código Novedad" + "', FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_Fiduprevisora where Id_Usuario = (select id_usuarios from usuarios where usuario = '" + Usuario + "')";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFiduprevisora.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar cconsulta de los registros PREVISORA  por mes de registro
        internal void ConsultarRegistroPREVISORAPorFechaRegistro(DataGridView dtgHistorialFiduprevisora, string FechaDesde, string FechaHasta)
        {
            try
            {
                ConsultaSQL = "select Cédula, NúmeroAutorización as '" + "Número Libranza" + "', FechaAutorización as '" + "Fecha Autorización" + "', ValorTotal as '" + "Valor Total" + "', ValorMensual as '" + "Valor Mensual" + "', Porcentaje, NúmeroCuotas as '" + "Número Cuotas" + "', FechaDescuento as '" + "Fecha Efectividad" + "', NitCanapro as '" + "NIT" + "', Concepto, CódigoNovedad as '" + "Código Novedad" + "', FechaRegistro as '" + "Fecha Registro" + "', Id_Registro as '" + "Id Registro" + "', (select nombre from usuarios where Id_usuarios = Id_Usuario) as '" + "Usuario" + "' from Registro_Fiduprevisora where FechaRegistro between '" + FechaDesde + "' and '" + FechaHasta + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialFiduprevisora.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para consultar los datos del registro fiduprevisora y almacenarlos en un objeto
        internal C_Registro_Fiduprevisora CargarDatosRegistroFiduprevisora(int RegistroFiduprevisora)
        {
            ObjetoPrevisora = new C_Registro_Fiduprevisora();
            try
            {
                Id_Registro = RegistroFiduprevisora;
                SqlCommand ConsultaDatosFER = new SqlCommand("select * from Registro_Fiduprevisora where Id_Registro = '" + Id_Registro + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = ConsultaDatosFER.ExecuteReader();
                datos.Read();
                ObjetoPrevisora.Id_Registro = datos.GetInt32(0);
                ObjetoPrevisora.Id_Usuario = datos.GetInt32(1);
                ObjetoPrevisora.NúmeroCédula = Convert.ToString(datos.GetInt32(2));
                ObjetoPrevisora.Número_Autorización = datos.GetString(3);
                ObjetoPrevisora.Fecha_Autorización = Convert.ToString(datos.GetDateTime(4));
                ObjetoPrevisora.Valor_Total = datos.GetDecimal(5);
                ObjetoPrevisora.Valor_Mensual = datos.GetDecimal(6);
                ObjetoPrevisora.Porcentaje = datos.GetInt32(7);
                ObjetoPrevisora.Número_Cuotas = datos.GetInt32(8);
                ObjetoPrevisora.Fecha_Descuento = Convert.ToString(datos.GetDateTime(9));
                ObjetoPrevisora.Nit_CANAPRO = datos.GetString(10);
                ObjetoPrevisora.Concepto = datos.GetInt32(11);
                ObjetoPrevisora.Novedad = datos.GetInt32(12);
                ObjetoPrevisora.Fecha = Convert.ToString(datos.GetDateTime(13));

                SqlCommand ConsultaDatosUsuario = new SqlCommand("select ltrim(rtrim(nombreintegrado)) from Canapro.dbo.nits where nit = '" + ObjetoPrevisora.NúmeroCédula + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader user = ConsultaDatosUsuario.ExecuteReader();
                user.Read();
                ObjetoPrevisora.NombreIntegrado = user.GetString(0);
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoPrevisora;
        }

    }
}
