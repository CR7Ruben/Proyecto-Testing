using gimnasio.Models;
using System.Data.SqlClient;
using System.Data;

namespace gimnasio.Datos
{
    public class viewDetallesClienteDatos
    {

        public viewDetallesClienteModel consultar(int idCliente)
        {

            var oViewDetallesCliente = new viewDetallesClienteModel();

            var cn = new conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("viewDetallesCliente", conexion);
                cmd.Parameters.AddWithValue("idCliente", idCliente);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        oViewDetallesCliente.idCliente = Convert.ToInt32(dr["idCliente"]);
                        oViewDetallesCliente.nombre = dr["nombre"].ToString();
                        oViewDetallesCliente.apellido = dr["apellido"].ToString();
                        oViewDetallesCliente.numTel = Convert.ToInt64(dr["numTel"]);
                        oViewDetallesCliente.observaciones = dr["observaciones"].ToString();
                        oViewDetallesCliente.fotoUrl = dr["fotoUrl"].ToString();
                        oViewDetallesCliente.membresia = dr["membresia"].ToString();
                        oViewDetallesCliente.fechaFin = Convert.ToDateTime(dr["fechaFin"]);

                    }

                }

            }

            return oViewDetallesCliente;

        }

    }
}
