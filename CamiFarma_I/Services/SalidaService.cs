using System.Data;
using Microsoft.Data.SqlClient;
using CamiFarma_I.Models;

namespace CamiFarma_I.Services
{
    public class SalidaService
    {
        private readonly string _connectionString;

        public SalidaService(IConfiguration configuration)
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

        // Registrar salida
        public void RegistrarSalida(int productoId, int cantidad)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_RegistrarSalida", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductoId", productoId);
                command.Parameters.AddWithValue("@Cantidad", cantidad);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Listar salidas del día
        public List<SalidaReporte> ListarSalidasDiarias()
        {
            var lista = new List<SalidaReporte>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_SalidasDiarias", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new SalidaReporte
                        {
                            NombreProducto = reader["Nombre"].ToString(),
                            Cantidad = Convert.ToInt32(reader["TotalSalidas"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"])
                        });
                    }
                }
            }

            return lista;
        }

        // Listar salidas por rango
        public List<SalidaReporte> ListarSalidasPorRango(DateTime inicio, DateTime fin)
        {
            var lista = new List<SalidaReporte>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("USP_SalidasPorRango", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FechaInicio", inicio);
                command.Parameters.AddWithValue("@FechaFin", fin);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new SalidaReporte
                        {
                            NombreProducto = reader["Nombre"].ToString(),
                            Cantidad = Convert.ToInt32(reader["TotalSalidas"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"])
                        });
                    }
                }
            }

            return lista;
        }
    }
}
