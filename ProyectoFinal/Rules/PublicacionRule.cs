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
        public Publicacion GetOnePostRandom()
        {
            var connectionString = _configuration.GetConnectionString("BlogDataBase");

            using var connection = new SqlConnection(connectionString); // Usa System.Sql
            {
                // Abro la conexion y pido mediante una consulta con Dapper los datos que luego se transforman en el objeto
                connection.Open();
                var post = connection.Query<Publicacion>("SELECT TOP 1 * FROM Publicacion"); // Usando Dapper, devuelve lista IEnumerable

                return post.First(); // Se coloca First() para que devuelva el primer item de la lista
            }

        }
    }
}
