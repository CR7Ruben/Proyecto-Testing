using gimnasio.Models;
using System.Data.SqlClient;
using System.Data;

namespace gimnasio.Datos
{

    public class administradorDatos
    {

        public bool registrarAdministrador(administradorModel oadministrador)
        {

            bool rpta;

            try
            {

                var cn = new conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("registrarAdministrador", conexion);
                    cmd.Parameters.AddWithValue("usuario", oadministrador.usuario);
                    cmd.Parameters.AddWithValue("contrasena", oadministrador.contrasena);
                    cmd.Parameters.AddWithValue("repetirContrasena", oadministrador.repetirContrasena);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }

                rpta = true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                rpta = false;

            }

            return rpta;

        }

        public int? validacionAdministrador(administradorModel oadministrador)
        {

            /* al final del proceso recibira el id del administrador */
            int? idAdministrador = null;

            try
            {
                var cn = new conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("validacionAdministrador", conexion);
                    cmd.Parameters.AddWithValue("usuario", oadministrador.usuario);
                    cmd.Parameters.AddWithValue("contrasena", oadministrador.contrasena);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {

                        /* si retorna true, significa que hay datos almacenados */
                        if (reader.Read())
                        {

                            /* lee la primera casilla  */
                            idAdministrador = reader.GetInt32(0);

                        }

                    }

                }

            }
            catch (Exception ex)
            {

                string error = ex.Message;

            }

            /* devuelve el id */
            return idAdministrador;

        }

    }

}
