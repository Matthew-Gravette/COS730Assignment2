using COS730.Backend.Domain.Entities;
using Microsoft.Data.Sqlite;

namespace COS730.Backend.Infrastructure;

public class ScoreRepository(IConfiguration configuration)
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");
    
    public async Task SaveScore(int submissionId, List<Score> scores)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        await using var transaction = connection.BeginTransaction();

        foreach (Score score in scores)
        {
            var query = connection.CreateCommand();
            query.Transaction = transaction;
            query.CommandText="""
                              INSERT INTO Scores (Submission_Id,Reviewer_Id,Score) 
                              VALUES ($SubmissionId,$ReviewerId,$Score);
                              """;
            query.Parameters.AddWithValue("$SubmissionId", submissionId);
            query.Parameters.AddWithValue("$ReviewerId", score.ReviewerId);
            query.Parameters.AddWithValue("$Score", score.ScoreValue);
            await query.ExecuteNonQueryAsync();
        }
        transaction.Commit();
    } 
    
    public async Task<List<int>> GetScoresForSubmission(int submissionId)
    {
        List<int> scores = new List<int>();
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        var query = connection.CreateCommand();
        query.CommandText=@"SELECT Score
                            FROM Scores 
                            WHERE Submission_Id = $submissionId";
        query.Parameters.AddWithValue("$submissionId", submissionId);
        var result = await query.ExecuteReaderAsync();
        while (result.Read())
        {
            scores.Add(result.GetInt32(0));
        }
        return scores;
    }
}