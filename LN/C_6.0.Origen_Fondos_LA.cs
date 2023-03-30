using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CANAPRO.LN
{
    class C_Origen_Fondos_LA
    {
        //Definición y encapsulamiento de variables
        String ConsultaSQL { get; set; }       
        public int Código { get; set; }
        public int Id_OF { get; set; }
        public string Nombre_OF { get; set; }
        public string User { get; set; }
        internal C_Origen_Fondos_LA ObjetoOrigenFondos { get; set; }

        //Método para validar que los registros no se repitan en la base de datos 
        internal Boolean ValidarExistenciaOrigenFondos(string Nombre)
        {
            try
            {
                SqlCommand validar = new SqlCommand("select * from Origen_Fondos where Nombre_OF = '" + Nombre + "'", LN.C_Conexión.ConexiónCanapro());
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
        internal void GuardarOrigenFondos()
        {
            try
            {
                try
                {
                    SqlCommand Recibir_ID = new SqlCommand("select max(Id_Origen_Fondos) + 1 from Origen_Fondos", LN.C_Conexión.ConexiónCanapro());
                    SqlDataReader idNuevo = Recibir_ID.ExecuteReader();
                    idNuevo.Read();
                    Id_OF = idNuevo.GetInt32(0);
                    C_Conexión.ConexiónCanapro().Close();
                }
                catch (Exception)
                {
                    Id_OF = 1;
                    //throw;
                }
                SqlCommand GuardarOrigenFondos = new SqlCommand("insert into Origen_Fondos values ('" + Id_OF + "','" + Nombre_OF + "')", LN.C_Conexión.ConexiónCanapro());
                GuardarOrigenFondos.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El origen de fondos se ha guardado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para modificar los registros de las inconsistencias en la base de datos 
        internal void ModificarOrigenFondos()
        {
            try
            {
                SqlCommand ModificarOrigenFondos = new SqlCommand("update Origen_Fondos set Id_Origen_Fondos = '" + Id_OF + "', Nombre_OF = '" + Nombre_OF + "' where Id_Origen_Fondos = " + Id_OF, LN.C_Conexión.ConexiónCanapro());
                ModificarOrigenFondos.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro();
                MessageBox.Show("El origen de fondos se ha actualizado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para eliminar los registros de las inconsistencias en la base de datos
        internal void EliminarOrigenFondos(int OrigenFondos_ID)
        {
            try
            {
                SqlCommand EliminarOrigenFondos = new SqlCommand("delete from Origen_Fondos where Id_Origen_Fondos = '" + OrigenFondos_ID + "'", LN.C_Conexión.ConexiónCanapro());
                EliminarOrigenFondos.ExecuteNonQuery();
                C_Conexión.ConexiónCanapro().Close();
                MessageBox.Show("El origen de fondos se ha eliminado correctamente", "¡Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método que realiza la consulta para actualizar la Tabla
        internal void ActualizarTabla(DataGridView dtgOrigenFondos)
        {
            try
            {
                ConsultaSQL = "select Id_Origen_Fondos as '" + "Código" + "', Nombre_OF as'" + "Nombre de Origen de los Fondos" + "' from Origen_Fondos";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgOrigenFondos.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para llenar el textbox con el código del origen de fondos
        public int LlenarCódigoOrigenFondos(int Código)
        {
            try
            {
                SqlCommand Recibir_Id_Usuario = new SqlCommand("select max(Id_Origen_Fondos) + 1 from Origen_Fondos", C_Conexión.ConexiónCanapro());
                SqlDataReader Id_Nuevo = Recibir_Id_Usuario.ExecuteReader();
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

        //Método para consultar la información de las inconsistencias y almacenarla en un objeto para cargarla en el formulario Origen de Fondos LA
        internal C_Origen_Fondos_LA MostrarOrigen_De_Fondos(int OrigenFondos_ID)
        {
            try
            {
                ObjetoOrigenFondos = new C_Origen_Fondos_LA();
                SqlCommand Ver_OrigenFondos = new SqlCommand("select * from Origen_Fondos where Id_Origen_Fondos = '" + OrigenFondos_ID + "'", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader OrigenFondos = Ver_OrigenFondos.ExecuteReader();
                if (OrigenFondos.Read())
                {
                    ObjetoOrigenFondos.Id_OF = OrigenFondos.GetInt32(0);
                    ObjetoOrigenFondos.Nombre_OF = OrigenFondos.GetString(1);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return ObjetoOrigenFondos;
        }

        //Método que realiza la consulta de las inconsistencias en la base de datos y las almacena en una lista para llevarlas a un Combobox 
        internal System.Collections.ArrayList LlenarComboInconsistencias(System.Collections.ArrayList lista)
        {
            try
            {
                string Inconsistencias;
                SqlCommand consultar = new SqlCommand("select Nombre_OF from Origen_Fondos", LN.C_Conexión.ConexiónCanapro());
                SqlDataReader datos = consultar.ExecuteReader();
                LN.C_Conexión.ConexiónCanapro().Close();

                while (datos.Read() == true)
                {
                    Inconsistencias = datos.GetString(0);
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

        //Método para realizar consulta de una inconsistencia por nombre de usuario en la BD
        internal void ConsultarInconsistenciasPorNombreDeUsuario(DataGridView dtgHistorialInconsistencias, string Consulta_Nombre, string usuario)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', Id_Nits as '" + "Documento" + "', (select Nombre_OF from Origen_Fondos where Origen_Fondos.Id_Origen_Fondos = Registro_LA.Id_Origen_Fondos) as '" + "Origen de Fondos" + "', Fecha as '" + "Fecha de Transacción" + "', DetalleLA as '" + "Detalle" + "', Valor_OF as '" + "Valor" + "', Ruta_Archivo as '" + "Ruta de Archivo" + "',Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_LA.Id_Usuarios) as '" + "Nombre de Usuario" + "' from Registro_LA where Id_Usuarios = (select Id_Usuarios from usuarios where Nombre = '" + Consulta_Nombre + "' and Id_Usuarios = (select Id_Usuarios from usuarios where Usuario = '" + usuario + "'))";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }        

        //Método para realizar consulta de una inconsistencia por usuario en la BD
        internal void ConsultarInconsistenciasPorUsuario(DataGridView dtgHistorialInconsistencias, string Consulta_Usuario, string usuario)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', Id_Nits as '" + "Documento" + "', (select Nombre_OF from Origen_Fondos where Origen_Fondos.Id_Origen_Fondos = Registro_LA.Id_Origen_Fondos) as '" + "Origen de Fondos" + "', Fecha as '" + "Fecha de Transacción" + "', DetalleLA as '" + "Detalle" + "', Valor_OF as '" + "Valor" + "', Ruta_Archivo as '" + "Ruta de Archivo" + "',Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_LA.Id_Usuarios) as '" + "Nombre de Usuario" + "' from Registro_LA where Id_Usuarios = (select Id_Usuarios from usuarios where usuario = '" + Consulta_Usuario + "' and Id_Usuarios = (select Id_Usuarios from usuarios where Usuario = '" + usuario + "'))";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de una inconsistencia por tipo de inconsistencia en la BD
        internal void ConsultarInconsistenciasPorOrigenDeFondos(DataGridView dtgHistorialInconsistencias, string Consulta_Origen_Fondos, string usuario)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', Id_Nits as '" + "Documento" + "', (select Nombre_OF from Origen_Fondos where Origen_Fondos.Id_Origen_Fondos = Registro_LA.Id_Origen_Fondos) as '" + "Origen de Fondos" + "', Fecha as '" + "Fecha de Transacción" + "', DetalleLA as '" + "Detalle" + "', Valor_OF as '" + "Valor" + "', Ruta_Archivo as '" + "Ruta de Archivo" + "',Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_LA.Id_Usuarios) as '" + "Nombre de Usuario" + "' from Registro_LA where Id_Origen_Fondos = (select Id_Origen_Fondos from Origen_Fondos where Nombre_OF = '" + Consulta_Origen_Fondos + "'and Id_Usuarios = (select Id_Usuarios from usuarios where Usuario = '" + usuario + "'))";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de una inconsistencia por fecha en la BD
        internal void ConsultarInconsistenciasPorFecha(DataGridView dtgHistorialInconsistencias, string FechaDesde, string FechaHasta, string usuario)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', Id_Nits as '" + "Documento" + "', (select Nombre_OF from Origen_Fondos where Origen_Fondos.Id_Origen_Fondos = Registro_LA.Id_Origen_Fondos) as '" + "Origen de Fondos" + "', Fecha as '" + "Fecha de Transacción" + "', DetalleLA as '" + "Detalle" + "', Valor_OF as '" + "Valor" + "', Ruta_Archivo as '" + "Ruta de Archivo" + "',Fecha_Registro as '" + "Fecha Registro" + "',  (select nombre from usuarios where usuarios.Id_Usuarios = Registro_LA.Id_Usuarios) as '" + "Nombre de Usuario" + "' from Registro_LA where fecha between '" + FechaDesde + "' and '" + FechaHasta + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Métodos para realizar consulta de lavado de activos para hacerles revisión 

        //Método para realizar consulta de una inconsistencia por nombre de usuario en la BD
        internal void ConsultarInconsistenciasPorNombreDeUsuarioRevision(DataGridView dtgHistorialInconsistencias, string Consulta_Nombre)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', Id_Nits as '" + "Documento" + "', (select Nombre_OF from Origen_Fondos where Origen_Fondos.Id_Origen_Fondos = Registro_LA.Id_Origen_Fondos) as '" + "Origen de Fondos" + "', Fecha as '" + "Fecha de Transacción" + "', DetalleLA as '" + "Detalle" + "', Valor_OF as '" + "Valor" + "', Ruta_Archivo as '" + "Ruta de Archivo" + "',Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_LA.Id_Usuarios) as '" + "Nombre de Usuario" + "' from Registro_LA where Id_Usuarios = (select Id_Usuarios from usuarios where Nombre = '" + Consulta_Nombre + "')";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de una inconsistencia por usuario en la BD
        internal void ConsultarInconsistenciasPorUsuarioRevision(DataGridView dtgHistorialInconsistencias, string Consulta_Usuario)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', Id_Nits as '" + "Documento" + "', (select Nombre_OF from Origen_Fondos where Origen_Fondos.Id_Origen_Fondos = Registro_LA.Id_Origen_Fondos) as '" + "Origen de Fondos" + "', Fecha as '" + "Fecha de Transacción" + "', DetalleLA as '" + "Detalle" + "', Valor_OF as '" + "Valor" + "', Ruta_Archivo as '" + "Ruta de Archivo" + "',Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_LA.Id_Usuarios) as '" + "Nombre de Usuario" + "' from Registro_LA where Id_Usuarios = (select Id_Usuarios from usuarios where usuario = '" + Consulta_Usuario + "')";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de una inconsistencia por tipo de inconsistencia en la BD
        internal void ConsultarInconsistenciasPorOrigenDeFondosRevision(DataGridView dtgHistorialInconsistencias, string Consulta_Origen_Fondos)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', Id_Nits as '" + "Documento" + "', (select Nombre_OF from Origen_Fondos where Origen_Fondos.Id_Origen_Fondos = Registro_LA.Id_Origen_Fondos) as '" + "Origen de Fondos" + "', Fecha as '" + "Fecha de Transacción" + "', DetalleLA as '" + "Detalle" + "', Valor_OF as '" + "Valor" + "', Ruta_Archivo as '" + "Ruta de Archivo" + "',Fecha_Registro as '" + "Fecha Registro" + "', (select nombre from usuarios where usuarios.Id_Usuarios = Registro_LA.Id_Usuarios) as '" + "Nombre de Usuario" + "' from Registro_LA where Id_Origen_Fondos = (select Id_Origen_Fondos from Origen_Fondos where Nombre_OF = '" + Consulta_Origen_Fondos + "')";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
                LN.C_Conexión.ConexiónCanapro().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Se ha presentado un error. Comuniquese con el Departamento de Sistemas", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Método para realizar consulta de una inconsistencia por fecha en la BD
        internal void ConsultarInconsistenciasPorFechaRevision(DataGridView dtgHistorialInconsistencias, string FechaDesde, string FechaHasta)
        {
            try
            {
                ConsultaSQL = "select Id_Registro as '" + "Id Registro" + "', Id_Nits as '" + "Documento" + "', (select Nombre_OF from Origen_Fondos where Origen_Fondos.Id_Origen_Fondos = Registro_LA.Id_Origen_Fondos) as '" + "Origen de Fondos" + "', Fecha as '" + "Fecha de Transacción" + "', DetalleLA as '" + "Detalle" + "', Valor_OF as '" + "Valor" + "', Ruta_Archivo as '" + "Ruta de Archivo" + "',Fecha_Registro as '" + "Fecha Registro" + "',  (select nombre from usuarios where usuarios.Id_Usuarios = Registro_LA.Id_Usuarios) as '" + "Nombre de Usuario" + "' from Registro_LA where fecha between '" + FechaDesde + "' and '" + FechaHasta + "'";
                SqlDataAdapter adaptador = new SqlDataAdapter(ConsultaSQL, LN.C_Conexión.ConexiónCanapro());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dtgHistorialInconsistencias.DataSource = tabla;
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
