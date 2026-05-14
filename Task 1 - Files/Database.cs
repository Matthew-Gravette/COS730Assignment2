using COS730.Backend.Entites;
using Microsoft.Data.Sqlite;

namespace COS730.Backend.Services;

public class Database
{
        private static string? _connectionString;
        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

    public static async Task<int> saveSubmission(string data)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        DateTime CreatedAt = DateTime.UtcNow;
        
        var query = connection.CreateCommand();
        query.CommandText=@"INSERT INTO Submissions  (Data , CreatedAt) VALUES ($Data, $CreatedAt);";
        query.Parameters.AddWithValue("$Data", data);
        query.Parameters.AddWithValue("$CreatedAt", CreatedAt);
        var confirmation = await query.ExecuteNonQueryAsync();
        return confirmation;
    }
    
    public async Task<List<Reviewer>> fetchReviewers()
    {
        List<Reviewer> reviewers = new List<Reviewer>();
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        var query = connection.CreateCommand();
        query.CommandText=@"SELECT * FROM Reviewers";
        var result = await query.ExecuteReaderAsync();
        while (result.Read())
        {
            reviewers.Add(new Reviewer(result.GetString(1), result.GetInt32(2), result.GetInt32(3) == 1));
        }
        return reviewers;
    }
    
    public async Task saveScore(int score)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        var query = connection.CreateCommand();
        query.CommandText=@"INSERT INTO Scores (Score) VALUES ($Score);";
        query.Parameters.AddWithValue("$Score", score);
        await query.ExecuteNonQueryAsync();
    } 
}