using COS730.Backend.Entites;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Data.Sqlite;
using System.Configuration;
using COS730.Backend.Services;


namespace COS730.Backend.Controllers;

[ApiController]
[Route("api/submission")]
public class SubmissionController : ControllerBase
{
    private readonly ReviewManager _reviewManager;
    private readonly EvalutionManager _evalutionManager;
    private readonly Database _database;
    
    public SubmissionController(ReviewManager reviewManager, EvalutionManager evalutionManager,Database database)
    {
        _reviewManager = reviewManager;
        _evalutionManager = evalutionManager;
        _database = database;
    }
    
    
    [HttpPost("review")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> submit(
    [FromBody] submissionDTO submission 
    )
    {
        var response = new Validator().validateFormat(submission.Data);
        
        if (response == "invalid")
            return UnprocessableEntity(response);

        await Database.saveSubmission(submission.Data);

        List<Reviewer> reviewers = await _reviewManager.getAvailableReviews();

        foreach (var reviewer in reviewers)
        {
            reviewer.AssignReview();
        }

        await _evalutionManager.startEvalution(reviewers);
        
        return Ok(response);
    }
}

public record submissionDTO(
    string Data
);