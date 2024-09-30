using System.Data.SqlClient;
using System.Data;
using gimnasio.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gimnasio.Datos
{

    public class clientePorMembresiaDatos
    {

        public List<clientePorMembresiaModel> Listar()
        {
            var oLista = new List<clientePorMembresiaModel>();

            var cn = new conexion();

            using (var conn = new SqlConnection(cn.getCadenaSQL()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("clienteMembresia", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new clientePorMembresiaModel()
                        {

                            idCliente = Convert.ToInt32(dr["idCliente"]),
                            nombre = dr["nombre"].ToString(),
                            apellido = dr["apellido"].ToString(),
                            membresia = dr["membresia"].ToString(),
                            fechaFin = Convert.ToDateTime(dr["fechaFin"])

                        });
                    }

                }
            }
            return oLista;
        }

        public List<clientePorMembresiaModel> ListarPorMembresia(int idMembresia)
        {

            var oLista = new List<clientePorMembresiaModel>();

            var cn = new conexion();

            using (var conn = new SqlConnection(cn.getCadenaSQL()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("clientePorMembresia", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idMembresia", idMembresia);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new clientePorMembresiaModel()
                        {

                            idCliente = Convert.ToInt32(dr["idCliente"]),
                            nombre = dr["nombre"].ToString(),
                            apellido = dr["apellido"].ToString(),
                            membresia = dr["membresia"].ToString(),
                            fechaFin = Convert.ToDateTime(dr["fechaFin"])

                        });
                    }

                }
            }
            return oLista;
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
