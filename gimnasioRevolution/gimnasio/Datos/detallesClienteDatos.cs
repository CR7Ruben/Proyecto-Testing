using gimnasio.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gimnasio.Datos
{

    public class detallesClienteDatos
    {

        public bool insertar(detallesClienteModel odetallesCliente)
        {

            bool rpta;

            try
            {

                var cn = new conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("insertarDetallesCliente", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idCliente", odetallesCliente.idCliente);
                    cmd.Parameters.AddWithValue("idMembresia", odetallesCliente.idMembresia);
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


        public bool modificar(detallesClienteModel odetallesCliente)
        {

            bool rpta;

            try
            {

                var cn = new conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("modificarDetallesCliente", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idCliente", odetallesCliente.idCliente);
                    cmd.Parameters.AddWithValue("idMembresia", odetallesCliente.idMembresia);
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

        public detallesClienteModel consultar(int idCliente)
        {

            var oCliente = new detallesClienteModel();

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

                    }

                }

            }

            return oCliente;

        }

    }

}
