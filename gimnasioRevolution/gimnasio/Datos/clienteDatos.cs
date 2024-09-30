using System.Data.SqlClient;
using System.Data;
using gimnasio.Models;

namespace gimnasio.Datos
{

    public class clienteDatos
    {

        public List<clienteModel> listar()
        {

            var oLista = new List<clienteModel>();

            var cn = new conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("viewCliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        oLista.Add(new clienteModel()
                        {

                            idCliente = Convert.ToInt32(dr["idCliente"]),
                            nombre = dr["nombre"].ToString(),
                            apellido = dr["apellido"].ToString(),
                            numTel = Convert.ToInt64(dr["numTel"]),
                            observaciones = dr["observaciones"].ToString(),
                            fotoUrl = dr["fotoUrl"].ToString()

                        });

                    }

                }

            }

            return oLista;

        }

        public clienteModel consultar(int idCliente)
        {

            var oCliente = new clienteModel();

            var cn = new conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("consultarCliente", conexion);
                cmd.Parameters.AddWithValue("idCliente", idCliente);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        oCliente.idCliente = Convert.ToInt32(dr["idCliente"]);
                        oCliente.nombre = dr["nombre"].ToString();
                        oCliente.apellido = dr["apellido"].ToString();
                        oCliente.numTel = Convert.ToInt64(dr["numTel"]);
                        oCliente.observaciones = dr["observaciones"].ToString();
                        oCliente.fotoUrl = dr["fotoUrl"].ToString();

                    }

                }

            }

            return oCliente;

        }

        public bool insertar(clienteModel ocliente)
        {

            bool rpta;

            try
            {

                var cn = new conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("insertarCliente", conexion);
                    cmd.Parameters.AddWithValue("nombre", ocliente.nombre);
                    cmd.Parameters.AddWithValue("apellido", ocliente.apellido);
                    cmd.Parameters.AddWithValue("numTel", ocliente.numTel);
                    cmd.Parameters.AddWithValue("observaciones", ocliente.observaciones);
                    cmd.Parameters.AddWithValue("fotoUrl", ocliente.fotoUrl);
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

        public bool modificar(clienteModel ocliente)
        {

            bool rpta;

            try
            {

                var cn = new conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("modificarCliente", conexion);
                    cmd.Parameters.AddWithValue("idClienteIngresado", ocliente.idCliente);
                    cmd.Parameters.AddWithValue("nombre", ocliente.nombre);
                    cmd.Parameters.AddWithValue("apellido", ocliente.apellido);
                    cmd.Parameters.AddWithValue("numTel", ocliente.numTel);
                    cmd.Parameters.AddWithValue("observaciones", ocliente.observaciones);
                    cmd.Parameters.AddWithValue("fotoUrl", ocliente.fotoUrl);
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

        public bool eliminar(int idCliente)
        {

            bool rpta;

            try
            {

                var cn = new conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("eliminarCliente", conexion);
                    cmd.Parameters.AddWithValue("idCliente", idCliente);
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

    }

}
