using System.Data.SqlClient;
using System.Data;
using gimnasio.Models;

namespace gimnasio.Datos
{
    public class viewClienteDatos
    {

        public List<viewClienteModel> listar()
        {

            var oLista = new List<viewClienteModel>();

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

                        oLista.Add(new viewClienteModel()
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

    }
}
