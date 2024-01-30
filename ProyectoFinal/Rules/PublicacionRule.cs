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
        // Devuelve un solo post aleatorio:
        public Publicacion GetOnePostRandom()
        {
            var connectionString = _configuration.GetConnectionString("BlogDataBase");

            using var connection = new SqlConnection(connectionString); // Usa System.Sql
            {
                // Abro la conexion y pido mediante una consulta con Dapper los datos que luego se transforman en el objeto
                connection.Open();
                var post = connection.Query<Publicacion>("SELECT TOP 1 * FROM Publicacion ORDER BY NEWID()"); // Usando Dapper, devuelve IEnumerable
                // La consulta anterior devuelve la primera fila de la tabla ordenada de forma aleatoria
                return post.First(); // Se coloca First() para que devuelva el primer item de la lista
            }

        }
        // Este método devuelve 4 posts de la BD ordenados del más reciente al mas antiguo:
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
        // Obtener un post, indicado por su id:
        internal Publicacion GetPostById(int id)
        {
            var connectionString = _configuration.GetConnectionString("BlogDataBase");

            using var connection = new SqlConnection(connectionString); // Usa System.Sql
            {
                // Abro la conexion y pido mediante una consulta con Dapper los datos que luego se transforman en el objeto
                connection.Open();
                var query = "SELECT * FROM Publicacion WHERE Id = @id"; // Consulta de SQL que será enviada
                var post = connection.QueryFirstOrDefault<Publicacion>(query, new { id }); // Usando Dapper, devuelve IEnumerable, el primer parámetro es la consulta y el segundo es la variable

                return post; // Se coloca First() para que devuelva una lista en vez de un IEnumerable
            }
        }
    }
}
