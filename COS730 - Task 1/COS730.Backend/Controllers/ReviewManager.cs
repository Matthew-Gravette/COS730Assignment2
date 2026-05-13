using COS730.Backend.Entites;
using COS730.Backend.Services;
using Microsoft.Data.Sqlite;

namespace COS730.Backend.Controllers;

public class ReviewManager
{
    private readonly string? _connectionString;
    private Database _database;
    public ReviewManager(IConfiguration configuration, Database database)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _database = database;
        
    }

    public async  Task<List<Reviewer>> getAvailableReviews()
    {
        List<Reviewer> reviewers = new List<Reviewer>();
        reviewers = await _database.fetchReviewers();
        filterConflicts(reviewers);
        filterWorkload(reviewers);
        return reviewers;
    }

    private static void filterConflicts(List<Reviewer> reviewerList)
    {
        foreach (var reviewer in reviewerList.ToList())
        {
            if (!reviewer.Capacity)
            {
                reviewerList.Remove(reviewer);
            }
        }
    }
    
    private static void filterWorkload(List<Reviewer> reviewerList)
    {
        foreach (var reviewer in reviewerList.ToList())
        {
            if (reviewer.CurrentAmountOfReviews > 10)
            {
                reviewerList.Remove(reviewer);
            }
        }
    }
}