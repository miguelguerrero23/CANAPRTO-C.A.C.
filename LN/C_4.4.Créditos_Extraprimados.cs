using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Créditos_Extraprimados
    {
        //Definición y encapsulamiento de variables
        public int Id_Créditos { get; set; }
        public int Id_Usuario { get; set; }
        public string Fecha { get; set; }
        public string Documento { get; set; }
        public string Pagaré { get; set; }
        public int Edad { get; set; }
        public decimal Saldo_Crédito { get; set; }
        public int Porcentaje_Extraprima { get; set; }
        public char Asociado_Activo { get; set; }
        public char Asociado_Inactivo { get; set; }
        public char Crédito_Aprobado { get; set; }
        public char Crédito_Rechazado { get; set; }
        internal C_Créditos_Extraprimados ObjetoConsulta { get; set; }
        public string ConsultaSQL { get; set; }
        //Variables de Usuario
        public int Usuario_ID { get; set; }
        public string NombreUsuario { get; set; }
        public string Usuario { get; set; }


        //Método para validar que los registros de los créditos no se repitan en la base de datos
        internal Boolean ValidarExistenciaCréditos(int Registro_ID)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Créditos_Extraprimados where Id_Créditos = '" + Registro_ID + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para guardar los registros de los créditos extraprimados en la base de datos 
        internal void GuardarCréditosExtraprimados()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Créditos) + 1 from Créditos_Extraprimados", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_Créditos = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_Créditos = 1;
                    //throw;
                }
                SqlCommand GuardarCréditos = new SqlCommand("insert into Créditos_Extraprimados  values ('" + Id_Créditos + "','" + Id_Usuario + "', '" + Documento + "','" + Pagaré + "','" + Edad + "','" + Saldo_Crédito + "', '" + Porcentaje_Extraprima + "', '" + Asociado_Activo + "', '" + Asociado_Inactivo + "', '" + Crédito_Aprobado + "', '" + Crédito_Rechazado + "', '" + Fecha + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarCréditos.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El registro se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros de los creditos extraprimados en la base de datos 
        internal void ModificarCréditosExtraprimados()
        {
            try
            {
                SqlCommand ModificarCréditos = new SqlCommand("update Créditos_Extraprimados set Id_Créditos = '" + Id_Créditos + "', Id_Usuario = '" + Id_Usuario + "', Documento = '" + Documento + "', Pagaré = '" + Pagaré + "', Edad = '" + Edad + "', Saldo_Crédito = '" + Saldo_Crédito + "', Porcentaje_ExtraPrima = '" + Porcentaje_Extraprima + "', Asociado_Activo = '" + Asociado_Activo + "', Asociado_Inactivo = '" + Asociado_Inactivo + "', Crédito_Aprobado = '" + Crédito_Aprobado + "', Crédito_Rechazado = '" + Crédito_Rechazado + "', Fecha = '" + Fecha + "' where Id_Créditos = " + Id_Créditos, LN.C_Conexión.ConexiónCanapro());
                ModificarCréditos.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El registro se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de los creditos extraprimados la base de datos 
        internal void EliminarCréditosExtraprimados(int Registro_ID)
        {
            try
            {
                Id_Créditos = Registro_ID;
                SqlCommand EliminarCréditos = new SqlCommand("delete from Créditos_Extraprimados where Id_Créditos = '" + Id_Créditos + "'", LN.C_Conexión.ConexiónCanapro());
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

        //Método para llenar el textbox con el id del crédito
        public int Código_Registro(int ID)
        {
            try
            {
                SqlCommand Recibir_Id = new SqlCommand("select max(Id_Créditos) + 1 from Créditos_Extraprimados", C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = Recibir_Id.ExecuteReader();
                Id_Nuevo.Read();
                Id_Créditos = Id_Nuevo.GetInt32(0);
                C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                Id_Créditos = 1;
                //throw;
            }
            return Id_Créditos;
        }
        
        //Método para consultar el ID del usuario, nombre y usuario en la base de datos
        internal C_Créditos_Extraprimados ConsultarDatosUsuario(string usuario)
        {
            ObjetoConsulta = new C_Créditos_Extraprimados();
            try
            {
                SqlCommand ConsultaUsuario = new SqlCommand("select id_usuarios, nombre, usuario from usuarios where usuario = '" + usuario + "'", LN.C_Conexión.ConexiónCanapro()); SqlDataReader user = ConsultaUsuario.ExecuteReader();

                if (user.Read())
                {
                    ObjetoConsulta.Usuario_ID = user.GetInt32(0);
                    ObjetoConsulta.NombreUsuario = user.GetString(1);
                    ObjetoConsulta.Usuario = user.GetString(2);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoConsulta;
        }
            
        //Método para realizar consulta de los créditos extraprimados por número de cédula
        internal void ConsultarCréditosExtraprimadosPorCédula(DataGridView dtgRevisión, string cédula)
        {
            try
            {
                Documento = cédula;
                ConsultaSQL = "select Documento as '" + "N° Cédula" + "', Pagaré as '" + "N° Pagaré" + "', Edad as '" + "Edad" + "', Saldo_Crédito as '" + "Saldo Crédito" + "', Porcentaje_extraprima as '" + "(%) Extraprima" + "', Asociado_Activo as '" + "Activo" + "', Asociado_Inactivo as '" + "Inactivo" + "', Crédito_Aprobado as '" + "Aprobado" + "', Crédito_Rechazado as '" + "Rechazado" + "' , Fecha, id_créditos as '" + "Id Registro" + "', (select nombre from usuarios where id_usuarios = id_Usuario) as '" + "Usuario" + "' from Créditos_Extraprimados where documento = '" + Documento + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgRevisión.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de los créditos extraprimados por número de cédula
        internal void ConsultarCréditosExtraprimadosPorPagaré(DataGridView dtgcréditos, string pagaré)
        {
            try
            {
                Pagaré = pagaré;
                ConsultaSQL = "select Documento as '" + "N° Cédula" + "', Pagaré as '" + "N° Pagaré" + "', Edad as '" + "Edad" + "', Saldo_Crédito as '" + "Saldo Crédito" + "', Porcentaje_extraprima as '" + "(%) Extraprima" + "', Asociado_Activo as '" + "Activo" + "', Asociado_Inactivo as '" + "Inactivo" + "', Crédito_Aprobado as '" + "Aprobado" + "', Crédito_Rechazado as '" + "Rechazado" + "' , Fecha, id_créditos as '" + "Id Registro" + "', (select nombre from usuarios where id_usuarios = id_Usuario) as '" + "Usuario" + "' from Créditos_Extraprimados where pagaré = '" + Pagaré + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgcréditos.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        
        //Método para realizar consulta de los créditos extraprimados por asociado activo
        internal void ConsultarCréditosExtraprimadosPorAsociadoActivo(DataGridView dtgcréditos, string texto)
        {
            try
            {
                if (texto.Equals("Activo"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select Documento as '" + "N° Cédula" + "', Pagaré as '" + "N° Pagaré" + "', Edad as '" + "Edad" + "', Saldo_Crédito as '" + "Saldo Crédito" + "', Porcentaje_extraprima as '" + "(%) Extraprima" + "', Asociado_Activo as '" + "Activo" + "', Asociado_Inactivo as '" + "Inactivo" + "', Crédito_Aprobado as '" + "Aprobado" + "', Crédito_Rechazado as '" + "Rechazado" + "' , Fecha, id_créditos as '" + "Id Registro" + "', (select nombre from usuarios where id_usuarios = id_Usuario) as '" + "Usuario" + "' from Créditos_Extraprimados where Asociado_Activo = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgcréditos.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de los créditos extraprimados por asociado inactivo
        internal void ConsultarCréditosExtraprimadosPorAsociadoInactivo(DataGridView dtgcréditos, string texto)
        {
            try
            {
                if (texto.Equals("Inactivo"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select Documento as '" + "N° Cédula" + "', Pagaré as '" + "N° Pagaré" + "', Edad as '" + "Edad" + "', Saldo_Crédito as '" + "Saldo Crédito" + "', Porcentaje_extraprima as '" + "(%) Extraprima" + "', Asociado_Activo as '" + "Activo" + "', Asociado_Inactivo as '" + "Inactivo" + "', Crédito_Aprobado as '" + "Aprobado" + "', Crédito_Rechazado as '" + "Rechazado" + "' , Fecha, id_créditos as '" + "Id Registro" + "', (select nombre from usuarios where id_usuarios = id_Usuario) as '" + "Usuario" + "' from Créditos_Extraprimados where Asociado_Inactivo = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgcréditos.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de los créditos extraprimados por crédito aprobado
        internal void ConsultarCréditosExtraprimadosPorCréditoAprobado(DataGridView dtgcréditos, string texto)
        {
            try
            {
                if (texto.Equals("Aprobado"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select Documento as '" + "N° Cédula" + "', Pagaré as '" + "N° Pagaré" + "', Edad as '" + "Edad" + "', Saldo_Crédito as '" + "Saldo Crédito" + "', Porcentaje_extraprima as '" + "(%) Extraprima" + "', Asociado_Activo as '" + "Activo" + "', Asociado_Inactivo as '" + "Inactivo" + "', Crédito_Aprobado as '" + "Aprobado" + "', Crédito_Rechazado as '" + "Rechazado" + "' , Fecha, id_créditos as '" + "Id Registro" + "', (select nombre from usuarios where id_usuarios = id_Usuario) as '" + "Usuario" + "' from Créditos_Extraprimados where Crédito_Aprobado = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgcréditos.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de los créditos extraprimados por crédito rechazado
        internal void ConsultarCréditosExtraprimadosPorCréditoRechazado(DataGridView dtgcréditos, string texto)
        {
            try
            {
                if (texto.Equals("Rechazado"))
                {
                    char carácter = 'X';
                    ConsultaSQL = "select Documento as '" + "N° Cédula" + "', Pagaré as '" + "N° Pagaré" + "', Edad as '" + "Edad" + "', Saldo_Crédito as '" + "Saldo Crédito" + "', Porcentaje_extraprima as '" + "(%) Extraprima" + "', Asociado_Activo as '" + "Activo" + "', Asociado_Inactivo as '" + "Inactivo" + "', Crédito_Aprobado as '" + "Aprobado" + "', Crédito_Rechazado as '" + "Rechazado" + "' , Fecha, id_créditos as '" + "Id Registro" + "', (select nombre from usuarios where id_usuarios = id_Usuario) as '" + "Usuario" + "' from Créditos_Extraprimados where Crédito_Rechazado = '" + carácter + "'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dtgcréditos.DataSource = tabla;
                    LN.C_Conexión.ConexiónCanapro().Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        ////Método para realizar consulta de los créditos extraprimados por Mes
        internal void ConsultarCréditosExtraprimadosPorFecha(DataGridView dtgRevisión, string FechaDesde, string FechaHasta)
        {
            try
            {
                ConsultaSQL = "select Documento as '" + "N° Cédula" + "', Pagaré as '" + "N° Pagaré" + "', Edad as '" + "Edad" + "', Saldo_Crédito as '" + "Saldo Crédito" + "', Porcentaje_extraprima as '" + "(%) Extraprima" + "', Asociado_Activo as '" + "Activo" + "', Asociado_Inactivo as '" + "Inactivo" + "', Crédito_Aprobado as '" + "Aprobado" + "', Crédito_Rechazado as '" + "Rechazado" + "' , Fecha, id_créditos as '" + "Id Registro" + "', (select nombre from usuarios where id_usuarios = id_Usuario) as '" + "Usuario" + "' from Créditos_Extraprimados where Fecha between '" + FechaDesde + "' and '" + FechaHasta + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgRevisión.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para consultar los datos de un crédito extraprimado
        internal C_Créditos_Extraprimados DatosCréditosExtraprimados(int Crédito_ID)
        {
            Id_Créditos = Crédito_ID;
            ObjetoConsulta = new C_Créditos_Extraprimados();
            try
            {
                SqlCommand ConsultaDatosCréditos = new SqlCommand("select documento, pagaré, edad, Saldo_Crédito, Porcentaje_Extraprima, Asociado_Activo, Asociado_Inactivo, Crédito_Aprobado, Crédito_Rechazado, Fecha, Id_usuario from Créditos_Extraprimados where Id_créditos = '" + Id_Créditos + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader crédito = ConsultaDatosCréditos.ExecuteReader();
                crédito.Read();
                ObjetoConsulta.Pagaré = crédito.GetString(1);
                ObjetoConsulta.Edad = crédito.GetInt32(2);
                ObjetoConsulta.Saldo_Crédito = crédito.GetDecimal(3);
                ObjetoConsulta.Porcentaje_Extraprima = crédito.GetInt32(2);
                ObjetoConsulta.Asociado_Activo = Convert.ToChar(crédito.GetString(5));
                ObjetoConsulta.Asociado_Inactivo = Convert.ToChar(crédito.GetString(6));
                ObjetoConsulta.Crédito_Aprobado = Convert.ToChar(crédito.GetString(7));
                ObjetoConsulta.Crédito_Rechazado = Convert.ToChar(crédito.GetString(8));
                ObjetoConsulta.Fecha = Convert.ToString(crédito.GetDateTime(9));
                ObjetoConsulta.Id_Usuario = Convert.ToInt32(crédito.GetInt32(10));
                Usuario_ID = Convert.ToInt32(crédito.GetInt32(10));

                SqlCommand ConsultaDatosUsuario = new SqlCommand("select nombre, usuario from usuarios where id_usuarios = '" + Usuario_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader usuario = ConsultaDatosUsuario.ExecuteReader();
                usuario.Read();
                ObjetoConsulta.NombreUsuario = usuario.GetString(0);
                ObjetoConsulta.Usuario = usuario.GetString(1);
                ObjetoConsulta.Documento = crédito.GetString(0);
            }
            catch (Exception)
            {
                throw;
            }
            return ObjetoConsulta;
        }
    }
}
