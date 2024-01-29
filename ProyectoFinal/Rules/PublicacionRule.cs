using Dapper;
using ProyectoFinal.Models;
using System.Data.SqlClient;

namespace ProyectoFinal.Rules
{
    public class PublicacionRule
    {
        private readonly IConfiguration _configuration;
        public PublicacionRule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Devuelve un solo post --> Modificar para que devuelva un post aleatorio <--
        public Publicacion GetOnePostRandom()
        {
            var connectionString = _configuration.GetConnectionString("BlogDataBase");

            using var connection = new SqlConnection(connectionString); // Usa System.Sql
            {
                // Abro la conexion y pido mediante una consulta con Dapper los datos que luego se transforman en el objeto
                connection.Open();
                var post = connection.Query<Publicacion>("SELECT TOP 1 * FROM Publicacion"); // Usando Dapper, devuelve IEnumerable

                return post.First(); // Se coloca First() para que devuelva el primer item de la lista
            }

        }
        // Este método devuelve 4 posts de la BD ordenados del más reciente al mas antiguo
        public List<Publicacion> GetPostsHome()
        {
            var connectionString = _configuration.GetConnectionString("BlogDataBase");

            using var connection = new SqlConnection(connectionString); // Usa System.Sql
            {
                // Abro la conexion y pido mediante una consulta con Dapper los datos que luego se transforman en el objeto
                connection.Open();
                var post = connection.Query<Publicacion>("SELECT TOP 4 * FROM Publicacion ORDER BY Creacion DESC"); // Usando Dapper, devuelve IEnumerable

                return post.ToList(); // Se coloca ToList() para que devuelva una lista en vez de un IEnumerable
            }

        }
    }
}
