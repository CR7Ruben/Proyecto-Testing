using System.Data.SqlClient;

namespace gimnasio.Datos
{

    public class conexion
    {

        private string cadenaSQL = string.Empty;

        public conexion()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            cadenaSQL = builder.GetSection("ConnectionString:CadenaSQL").Value;

        }

        public string getCadenaSQL()
        {

            return cadenaSQL;

        }

    }

}

