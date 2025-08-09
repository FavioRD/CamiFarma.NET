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

        // Listar
        public List<Producto> ObtenerTodos()
        {
            var productos = new List<Producto>();

            using (var connection = new SqlConnection(_connectionString))
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

            return productos;
        }

        // Agregar producto
        public void Insertar(Producto producto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("USP_InsertarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    command.Parameters.AddWithValue("@Precio", producto.Precio);
                    command.Parameters.AddWithValue("@Stock", producto.Stock);
                    command.Parameters.AddWithValue("@FechaExpiracion", producto.FechaExpiracion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Editar producto
        public void Editar(Producto producto)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_EditarProducto", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", producto.Id);
                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@Stock", producto.Stock);
                command.Parameters.AddWithValue("@FechaExpiracion", producto.FechaExpiracion);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Eliminar producto
        public void Eliminar(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_EliminarProducto", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Producto ObtenerPorId(int id)
        {
            Producto producto = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Productos WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        producto = new Producto
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = (decimal)reader["Precio"],
                            Stock = (int)reader["Stock"],
                            FechaExpiracion = (DateTime)reader["FechaExpiracion"]
                        };
                    }
                }
            }

            return producto;
        }

    }
}
