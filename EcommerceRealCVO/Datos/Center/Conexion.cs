
namespace EcommerceRealCVO.Datos.Center
{
    public class Conexion
    {
        private string connSQL = string.Empty;

        public Conexion()
        {


            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            connSQL = builder.GetSection("ConnectionStrings:ConexionSQL").Value;
        }

        public string getConnSQL()
        {
            return connSQL;
        }


    }
}
