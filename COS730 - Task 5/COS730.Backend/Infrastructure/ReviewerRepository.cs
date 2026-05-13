using COS730.Backend.Domain.Entities;
using Microsoft.Data.Sqlite;

namespace COS730.Backend.Infrastructure;


public class ReviewerRepository(IConfiguration configuration)
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");
    private readonly int _maxCapacity = 10;
    
    
    
    public async Task<List<Reviewer>> FetchReviewers()
    {
        List<Reviewer> reviewers = new List<Reviewer>();
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        var query = connection.CreateCommand();
        query.CommandText=@"SELECT * 
                            FROM Reviewers 
                            WHERE Available = true and CurrentAmountOfReviews <= $Capacity";
        query.Parameters.AddWithValue("$Capacity", _maxCapacity);
        var result = await query.ExecuteReaderAsync();
        while (result.Read())
        {
            reviewers.Add(new Reviewer(result.GetInt32(0), result.GetString(1), result.GetInt32(2), result.GetInt32(3) == 1));
        }
        return reviewers;
        
    }
}