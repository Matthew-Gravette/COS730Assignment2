using Microsoft.Data.Sqlite;

namespace COS730.Backend.Infrastructure;

public class SubmissionRepository(IConfiguration configuration)
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task<int> SaveSubmissions(String data)
    {
        try
        {
            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            
            DateTime CreatedAt = DateTime.UtcNow;
            
            var query = connection.CreateCommand();
            query.CommandText=@"INSERT INTO Submissions  (Data , CreatedAt) VALUES ($Data, $CreatedAt) RETURNING Id;";
            query.Parameters.AddWithValue("$Data", data);
            query.Parameters.AddWithValue("$CreatedAt", CreatedAt);
            var id = await query.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }
        catch (SqliteException e)
        {
            throw new InvalidOperationException("Error saving to db",e);
        }
    }
}