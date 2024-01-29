using Dapper;
using ProyectoFinal.Models;
using System.Data.SqlClient;

namespace ProyectoFinal.Rules
{
    public class PublicacionRule
    {
        public Publicacion GetOnePostRandom()
        {
            var connectionString = @"Server=DESKTOP-119RNS3\SQLEXPRESS;Database=BlogDataBase;Trusted_Connection=True"; // Luego va a appsettings.json

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
