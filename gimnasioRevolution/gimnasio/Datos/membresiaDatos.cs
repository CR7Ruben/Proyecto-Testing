using gimnasio.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gimnasio.Datos
{
    public class membresiaDatos
    {

        public List<membresiaModel> listarMembresias()
        {

            var oLista = new List<membresiaModel>();

            var cn = new conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("listarMembresias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        oLista.Add(new membresiaModel()
                        {

                            idMembresia = Convert.ToInt32(dr["idMembresia"]),
                            membresia = dr["membresia"].ToString(),
                            duracion = Convert.ToInt32(dr["duracion"]),
                            precio = Convert.ToInt32(dr["precio"]),

                        });

                    }

                }

            }

            return oLista;

        }
        public bool agregar(membresiaModel omembresia)
        {

            bool rpta;

            try
            {

                var cn = new conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("agregarMembresia", conexion);
                    cmd.Parameters.AddWithValue("membresia", omembresia.membresia);
                    cmd.Parameters.AddWithValue("duracion", omembresia.duracion);
                    cmd.Parameters.AddWithValue("precio", omembresia.precio);
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

        public membresiaModel consultar(int idMembresia)
        {

            var oMembresia = new membresiaModel();

            var cn = new conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("consultarMembresia", conexion);
                cmd.Parameters.AddWithValue("idMembresia", idMembresia);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        oMembresia.idMembresia = Convert.ToInt32(dr["idMembresia"]);
                        oMembresia.membresia = dr["membresia"].ToString();
                        oMembresia.duracion = Convert.ToInt32(dr["duracion"]);
                        oMembresia.precio = Convert.ToInt32(dr["precio"]);

                    }

                }

            }

            return oMembresia;

        }

        public bool modificar(membresiaModel omembresia)
        {

            bool rpta;

            try
            {

                var cn = new conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("modificarMembresia", conexion);
                    cmd.Parameters.AddWithValue("idMembresia", omembresia.idMembresia);
                    cmd.Parameters.AddWithValue("membresia", omembresia.membresia);
                    cmd.Parameters.AddWithValue("duracion", omembresia.duracion);
                    cmd.Parameters.AddWithValue("precio", omembresia.precio);
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
        public bool eliminar(int idMembresia)
        {

            bool rpta;

            try
            {

                var cn = new conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("eliminarMembresia", conexion);
                    cmd.Parameters.AddWithValue("idMembresia", idMembresia);
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

        public List<SelectListItem> ListarMembresias()
        {
            var oLista = new List<SelectListItem>();

            var cn = new conexion();

            using (var conn = new SqlConnection(cn.getCadenaSQL()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dropListMembresia", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new SelectListItem()
                        {
                            Value = dr["idMembresia"].ToString(),
                            Text = dr["membresia"].ToString()
                        });
                    }
                }
            }

            return oLista;
        }

    }
}
