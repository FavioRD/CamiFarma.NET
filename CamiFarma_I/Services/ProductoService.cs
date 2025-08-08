using System.Data;
using Microsoft.Data.SqlClient; 
using CamiFarma_I.Models;

namespace CamiFarma_I.Services
{
    public class ProductoService
    {
        private readonly string _connectionString;

        public ProductoService(IConfiguration configuration)
        {
            // Recuperar variables de entorno
            var sqlServer = Environment.GetEnvironmentVariable("SQL_SERVER");
            var sqlUser = Environment.GetEnvironmentVariable("SQL_USER");
            var sqlPass = Environment.GetEnvironmentVariable("SQL_PASS");

            if (string.IsNullOrEmpty(sqlServer) || string.IsNullOrEmpty(sqlUser) || string.IsNullOrEmpty(sqlPass))
            {
                throw new InvalidOperationException("Las variables de entorno SQL_SERVER, SQL_USER o SQL_PASS no están configuradas.");
            }

            _connectionString = $"Server={sqlServer};Database=FarmaciaDB;User Id={sqlUser};Password={sqlPass};Encrypt=False;TrustServerCertificate=True;";
        }

        public List<Producto> ObtenerTodos()
        {
            var productos = new List<Producto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("USP_ListarProductos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(new Producto
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                FechaExpiracion = Convert.ToDateTime(reader["FechaExpiracion"])
                            });
                        }
                    }
                }
            }

            return productos;
        }
    }
}
