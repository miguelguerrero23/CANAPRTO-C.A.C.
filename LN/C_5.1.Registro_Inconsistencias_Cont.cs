using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Registro_Inconsistencias_Cont
    {

        //Definición y encapsulamiento de variables
        public int Código { get; set; }
        public int Id_Registro { get; set; }
        public int Id_Inconsistencia { get; set; }
        public int Id_Usuario { get; set; }
        public string Fecha { get; set; }
        public string Comprobante_correcto { get; set; }
        public string Comprobante_errado { get; set; }
        public string Cédula_Tercero { get; set; }
        public decimal Ajuste_valor_correcto { get; set; }
        public decimal Ajuste_valor_errado { get; set; }
        public int Cuenta_correcta { get; set; }
        public int Cuenta_errada { get; set; }
        public decimal Tarifa_correcta { get; set; }
        public decimal Tarifa_errada { get; set; }
        public int Tercero_correcto { get; set; }
        public int Tercero_errado { get; set; }
        public char RteFuente { get; set; }
        public char RteIVA { get; set; }
        public char RteICA { get; set; }
        public char Iva_retenido { get; set; }
        public char Nota_contable { get; set; }
        public char Cdat { get; set; }
        public char Factura_equivalente { get; set; }
        public char Cuenta_cobro { get; set; }
        public char Factura { get; set; }
        public char Sí_aplica_ajuste { get; set; }
        public char No_aplica_ajuste { get; set; }
        public string Acción_correctiva { get; set; }
        internal C_Registro_Inconsistencias_Cont ObjetoContabilidad { get; set; }
        public string BuscarUsuario { get; set; }
        public int CódigoUsuario { get; set; }
        public int CódigoInconsistencia { get; set; }
        public string BuscarAgencia { get; set; }
        public string Usuario { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreInconsistencia { get; set; }
        public string Fecha_Registro { get; set; }
        public int Id_Responsable { get; set; } 

        //Método para validar que los registros de las inconsistencias no se repitan en la base de datos
        internal Boolean ValidarExistenciaRegistroInconsistenciaContable(int Registro_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Registro_Inconsistencias_Contables where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros de las inconsistencias en la base de datos 
        internal void GuardarRegistroInconsistenciasContables()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Registro) + 1 from Registro_Inconsistencias_Contables", LN.C_Conexión.ConexiónCanapro());
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
                SqlCommand GuardarRegistroInconsistenciaContable = new SqlCommand("insert into Registro_Inconsistencias_Contables values ('" + Id_Registro + "','" + Id_Inconsistencia + "', '" + Id_Usuario + "', '" + Fecha + "','" + Comprobante_correcto + "','" + Comprobante_errado + "','" + Cédula_Tercero + "','" + Ajuste_valor_correcto +"','" + Ajuste_valor_errado +"', '" + Cuenta_correcta +"','" + Cuenta_errada +"','" + Tarifa_correcta +"','" + Tarifa_errada +"','" + Tercero_correcto +"','" + Tercero_errado + "','" + RteFuente +"','" + RteIVA +"','" + RteICA +"','" + Iva_retenido +"','" + Nota_contable +"','" + Cdat +"','" + Factura_equivalente +"','" + Cuenta_cobro +"','" + Factura + "','" + Sí_aplica_ajuste +"','" + No_aplica_ajuste +"','" + Acción_correctiva + "','" + Fecha_Registro + "','" + Id_Responsable
 + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarRegistroInconsistenciaContable.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros de las inconsistencias en la base de datos 
        internal void ModificarRegistroInconsistenciasContables()
        {
            try
            {
                SqlCommand ModificarRegistroInconsistenciaContable = new SqlCommand("update Registro_Inconsistencias_Contables set Id_Registro = '" + Id_Registro + "', Id_Inconsistencia = '" + Id_Inconsistencia + "', Id_Usuarios = '" + Id_Usuario + "', Fecha = '" + Fecha + "', ComprobanteCorrecto = '" + Comprobante_correcto + "', ComprobanteErrado = '" + Comprobante_errado + "',Cédula_Tercero = '" + Cédula_Tercero + "', AjusteValorCorrecto = '" + Ajuste_valor_correcto + "', AjusteValorErrado = '" + Ajuste_valor_errado + "', CuentaCorrecta = '" + Cuenta_correcta + "', CuentaErrada = '" + Cuenta_errada + "', TarifaCorrecta = '" + Tarifa_correcta + "', TarifaErrada = '" + Tarifa_errada + "', TerceroCorrecto = '" + Tercero_correcto + "', TerceroErrado = '" + Tercero_errado + "', RteFuente = '" + RteFuente + "', RteIVA = '" + RteIVA + "', RteICA = '" + RteICA + "' , IvaRetenido = '" + Iva_retenido + "', NotaContable = '" + Nota_contable + "', CDAT = '" + Cdat + "', FacturaEquivalente = '" + Factura_equivalente + "', CuentaDeCobro = '" + Cuenta_cobro + "', Factura = '" + Factura + "', Sí_RealizaAjuste = '" + Sí_aplica_ajuste + "', No_RealizaAjuste = '" + No_aplica_ajuste + "', AcciónCorrectiva = '" + Acción_correctiva + "', Fecha_Registro = '" + Fecha_Registro + "', Id_Responsable = '" + Id_Responsable + "' where Id_Registro = " + Id_Registro, LN.C_Conexión.ConexiónCanapro());
                ModificarRegistroInconsistenciaContable.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El registro se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de las inconsistenciasen la base de datos 
        internal void EliminarRegistroInconsistenciasContables(int Registro_ID)
        {
            try
            {
                SqlCommand EliminarRegistroInconsistenciaContable = new SqlCommand("delete from Registro_Inconsistencias_Contables where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarRegistroInconsistenciaContable.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método que realiza la consulta del nombre completo de los usuarios en la base de datos "CANAPRO" y las almacena en una lista para llevarlas a un Combobox 
        internal System.Collections.ArrayList LlenarComboNombreUsuarios(System.Collections.ArrayList lista)
        {
            try
            {
                string NombreUsuario;
                SqlCommand consultar = new SqlCommand("select Nombre from usuarios", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = consultar.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datos.Read() == true)
                {
                    NombreUsuario = datos.GetString(0);
                    lista.Add(datos.GetString(0));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;
        }

        //Método que realiza la consulta del nombre completo de los usuarios en la base de datos "CANAPRO" y las almacena en una lista para llevarlas a un Combobox 
        internal System.Collections.ArrayList LlenarComboTipoInconsistencias(System.Collections.ArrayList lista)
        {
            try
            {
                string NombreInconsistencia;
                SqlCommand consultar = new SqlCommand("select NombreInconsistenciaContable from Tipo_Inconsistencia_Contable", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = consultar.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datos.Read() == true)
                {
                    NombreInconsistencia = datos.GetString(0);
                    lista.Add(datos.GetString(0));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return lista;
        }

        //Método para llenar el textbox con el id del registro
        public int Código_Registro(int ID)
        {
            try
            {
                SqlCommand Recibir_Id = new SqlCommand("select max(Id_Registro) + 1 from Registro_Inconsistencias_Contables", C_Conexión.ConexiónCanapro());
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
        internal C_Registro_Inconsistencias_Cont ConsultarDatosUsuario(string usuario)
        {
            ObjetoContabilidad = new C_Registro_Inconsistencias_Cont();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select id_usuarios from usuarios where usuario = '" + usuario + "'", LN.C_Conexión.ConexiónCanapro()); SqlDataReader user = ConsultaUsuario.ExecuteReader();

                if (user.Read())
                {
                    ObjetoContabilidad.Id_Responsable = user.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoContabilidad;
        } 

        //Método para llenar el textbox con el usuario cuando se selecciona un nombre de usuario en el formulario de registro de inconsistencias contables
        internal string Usuarios(string Nombre_Usuario)
        {
            try
            {
                SqlCommand Recibir_Usuario = new SqlCommand("select usuario from usuarios where nombre = '" + Nombre_Usuario + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Usuario_Nuevo = Recibir_Usuario.ExecuteReader();
                Usuario_Nuevo.Read();
                BuscarUsuario = Usuario_Nuevo.GetString(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                BuscarUsuario = "";
                //throw;
            }

            return BuscarUsuario;
        }

        //Método para llenar el textbox con el id de la inconsistencia cuando se selecciona un item de inconsistencia en el formulario de registro de inconsistencias contables
        internal int IDUsuario(string Nombre_Usuario)
        {
            try
            {
                SqlCommand Recibir_Id_Usuario = new SqlCommand("select Id_Usuarios from usuarios where Nombre = '" + Nombre_Usuario + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader CódigoNuevo = Recibir_Id_Usuario.ExecuteReader();
                CódigoNuevo.Read();
                CódigoUsuario = CódigoNuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoUsuario = 1;
                //throw;
            }

            return CódigoUsuario;
        }

        //Método para llenar el textbox con nombre de la agencia cuando se selecciona un nombre de usuario en el formulario de registro de inconsistencias contables
        internal string NombreAgencia(string Nombre_Usuario)
        {
            try
            {
                SqlCommand Recibir_Id_Usuario = new SqlCommand("select ltrim(rtrim(nombreagencia)) from Canapro.dbo.agencias where codigoagencia =(select Id_Agencia from usuarios where Nombre = '" + Nombre_Usuario + "')", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader CódigoNuevo = Recibir_Id_Usuario.ExecuteReader();
                CódigoNuevo.Read();
                BuscarAgencia = CódigoNuevo.GetString(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                BuscarAgencia = "";
                //throw;
            }

            return BuscarAgencia;
        }

        //Método para llenar el textbox con el id de la inconsistencia cuando se selecciona un item de inconsistencia en el formulario de registro de control interno
        internal int Id_Inconsistencias(string Inconsistencia)
        {
            try
            {
                SqlCommand Recibir_Id_Inconsistencia = new SqlCommand("select Id_TipoInconsistenciaContable from Tipo_Inconsistencia_Contable where NombreInconsistenciaContable = '" + Inconsistencia + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader CódigoNuevo = Recibir_Id_Inconsistencia.ExecuteReader();
                CódigoNuevo.Read();
                CódigoInconsistencia = CódigoNuevo.GetInt32(0);
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                CódigoInconsistencia = 1;
                //throw;
            }

            return CódigoInconsistencia;
        }

        //Método que realiza consulta a la tabla Registro de Inconsistencias y almacena la información en un objeto para luego cargarla en el formulario de registro y permitir modificar y eliminar los registros
        internal C_Registro_Inconsistencias_Cont MostrarInconsistencias(int Registro_ID)
        {
            try
            {
                ObjetoContabilidad = new C_Registro_Inconsistencias_Cont();
                SqlCommand Ver_Inconsistencias = new SqlCommand("select * from Registro_Inconsistencias_Contables where Id_Registro = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader inconsistencia = Ver_Inconsistencias.ExecuteReader();
                inconsistencia.Read();
                int Usuario_ID = inconsistencia.GetInt32(2); 
                int Inconsistencia_ID = inconsistencia.GetInt32(1);
                ObjetoContabilidad.Id_Registro = inconsistencia.GetInt32(0);
                ObjetoContabilidad.Id_Inconsistencia = inconsistencia.GetInt32(1);
                ObjetoContabilidad.Id_Usuario = inconsistencia.GetInt32(2);
                ObjetoContabilidad.Fecha = Convert.ToString(inconsistencia.GetDateTime(3));
                ObjetoContabilidad.Comprobante_correcto = inconsistencia.GetString(4);
                ObjetoContabilidad.Comprobante_errado = inconsistencia.GetString(5);
                ObjetoContabilidad.Cédula_Tercero = inconsistencia.GetString(6);
                ObjetoContabilidad.Ajuste_valor_correcto = inconsistencia.GetDecimal(7);
                ObjetoContabilidad.Ajuste_valor_errado = inconsistencia.GetDecimal(8);
                ObjetoContabilidad.Cuenta_correcta = inconsistencia.GetInt32(9);
                ObjetoContabilidad.Cuenta_errada = inconsistencia.GetInt32(10);
                ObjetoContabilidad.Tarifa_correcta = inconsistencia.GetDecimal(11);
                ObjetoContabilidad.Tarifa_errada = inconsistencia.GetDecimal(12);
                ObjetoContabilidad.Tercero_correcto = inconsistencia.GetInt32(13);
                ObjetoContabilidad.Tercero_errado = inconsistencia.GetInt32(14);
                ObjetoContabilidad.RteFuente = Convert.ToChar(inconsistencia.GetString(15));
                ObjetoContabilidad.RteIVA = Convert.ToChar(inconsistencia.GetString(16));
                ObjetoContabilidad.RteICA = Convert.ToChar(inconsistencia.GetString(17));
                ObjetoContabilidad.Iva_retenido = Convert.ToChar(inconsistencia.GetString(18));
                ObjetoContabilidad.Nota_contable = Convert.ToChar(inconsistencia.GetString(19));
                ObjetoContabilidad.Cdat = Convert.ToChar(inconsistencia.GetString(20));
                ObjetoContabilidad.Factura_equivalente = Convert.ToChar(inconsistencia.GetString(21));
                ObjetoContabilidad.Cuenta_cobro = Convert.ToChar(inconsistencia.GetString(22));
                ObjetoContabilidad.Factura = Convert.ToChar(inconsistencia.GetString(23));
                ObjetoContabilidad.Sí_aplica_ajuste = Convert.ToChar(inconsistencia.GetString(24));
                ObjetoContabilidad.No_aplica_ajuste = Convert.ToChar(inconsistencia.GetString(25));
                ObjetoContabilidad.Acción_correctiva = inconsistencia.GetString(26);
                ObjetoContabilidad.Fecha_Registro = Convert.ToString(inconsistencia.GetDateTime(27));
                ObjetoContabilidad.Id_Responsable = Convert.ToInt32(inconsistencia.GetInt32(28));
                SqlCommand Ver_Usuarios = new SqlCommand("select usuario, Nombre from usuarios where Id_Usuarios = '" + Usuario_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader usuario = Ver_Usuarios.ExecuteReader();
                usuario.Read();
                ObjetoContabilidad.Usuario = Convert.ToString(usuario.GetString(0));
                ObjetoContabilidad.NombreUsuario = Convert.ToString(usuario.GetString(1));
                SqlCommand Ver_TipoInconsistencia = new SqlCommand("select NombreInconsistenciaContable from Tipo_Inconsistencia_Contable where Id_TipoInconsistenciaContable = '" + Inconsistencia_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader Tipo = Ver_TipoInconsistencia.ExecuteReader();
                Tipo.Read();
                ObjetoContabilidad.NombreInconsistencia = Convert.ToString(Tipo.GetString(0));                
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoContabilidad;
        }

    }
}
