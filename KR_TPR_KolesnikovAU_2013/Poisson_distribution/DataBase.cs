using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poisson_distribution
{
    internal class DataBase
    {
        async Task CreateTable()
        {
            var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=postgres;";

            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "DROP TABLE IF EXISTS people";
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS people (
                id SERIAL PRIMARY KEY,
                start_time_queue TIMESTAMP,
                finish_time_queue TIMESTAMP,
                start_time_delay TIMESTAMP,
                finish_time_delay TIMESTAMP
            )";
            await cmd.ExecuteNonQueryAsync();

            connection.Close();
        }
        async Task InsertInTable(DateTime? start_time_queue, DateTime? finish_time_queue, DateTime? start_time_delay, DateTime? finish_time_delay)
        {
            var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=postgres;";

            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "INSERT INTO people (start_time_queue, finish_time_queue, start_time_delay, finish_time_delay) " +
                  "VALUES (@StartTimeQueue, @FinishTimeQueue, @StartTimeDelay, @FinishTimeDelay)";

            cmd.Parameters.AddWithValue("@StartTimeQueue", start_time_queue == null ? DBNull.Value : start_time_queue);
            cmd.Parameters.AddWithValue("@FinishTimeQueue", finish_time_queue == null ? DBNull.Value : finish_time_queue);
            cmd.Parameters.AddWithValue("@StartTimeDelay", start_time_delay == null ? DBNull.Value : start_time_delay);
            cmd.Parameters.AddWithValue("@FinishTimeDelay", finish_time_delay == null ? DBNull.Value : finish_time_delay);

            await cmd.ExecuteNonQueryAsync();

            connection.Close();
        }
        async Task InsertInQueue(int id, DateTime? time)
        {
            var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=postgres;";

            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            string formattedTime = time.Value.ToString("yyyy-MM-dd HH:mm:ss");

            cmd.CommandText = $"UPDATE people" +
                $" SET start_time_queue ='{formattedTime}'" +
                $" WHERE id = {id}";

            await cmd.ExecuteNonQueryAsync();

            connection.Close();
        }
        async Task InsertInDelay(int id, DateTime? time)
        {
            var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=postgres;";

            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            string formattedTime = time.Value.ToString("yyyy-MM-dd HH:mm:ss");

            cmd.CommandText = $"UPDATE people" +
                $" SET finish_time_queue ='{formattedTime}'," +
                $" start_time_delay ='{formattedTime}'" +
                $" WHERE id = {id}";

            await cmd.ExecuteNonQueryAsync();

            connection.Close();
        }
        async Task DeleteDelay(int id, DateTime? time)
        {
            var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=postgres;";

            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            string formattedTime = time.Value.ToString("yyyy-MM-dd HH:mm:ss");

            cmd.CommandText = $"UPDATE people" +
                $" SET finish_time_delay ='{formattedTime}'" +
                $" WHERE id = {id}";

            await cmd.ExecuteNonQueryAsync();

            connection.Close();
        }
        async Task Delete(int id) {
            var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=postgres;";

            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "DELETE FROM people WHERE id = @id";
            cmd.Parameters.AddWithValue("id", id);

            await cmd.ExecuteNonQueryAsync();

            connection.Close();
        }
        async Task<int> Count_people() {
            var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=postgres;";

            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT COUNT(*) FROM people";
            int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            connection.Close();

            return count;
        }
        async Task<double> Count_Intensiv() {
            var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=postgres;";

            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT AVG(extract(SECONDS FROM age(finish_time_delay, start_time_delay))) from people";
            decimal averageSeconds = (decimal)await cmd.ExecuteScalarAsync();
            double averageSecondsDouble = Convert.ToDouble(averageSeconds);

            connection.Close();

            return averageSecondsDouble;
        }
        public static async Task StartCreateTable()
        {
            var program = new DataBase();
            await program.CreateTable();
        }
        public static async Task StartInsertInQueue(int id, DateTime? time)
        {
            var program = new DataBase();
            await program.InsertInQueue(id, time);
        }
        public static async Task StartInsertInDelay(int id, DateTime? time)
        {
            var program = new DataBase();
            await program.InsertInDelay(id, time);
        }
        public static async Task StartDeleteDelay(int id, DateTime? time)
        {
            var program = new DataBase();
            await program.DeleteDelay(id, time);
        }
        public static async Task DeleteMan(int id) {
            var program = new DataBase();
            await program.Delete(id);
        }
        public static async Task<int> StartCountPeople() {
            var program = new DataBase();
            return await program.Count_people();
        }
        public static async Task<double> StartCountIntensiv() {
            var program = new DataBase();
            return await program.Count_Intensiv();
        }
        public static async Task DoInsertInTable(DateTime? start_time_queue, DateTime? finish_time_queue, DateTime? start_time_delay, DateTime? finish_time_delay)
        {
            var program = new DataBase();
            await program.InsertInTable(start_time_queue, finish_time_queue, start_time_delay, finish_time_delay);
        }
    }
}
