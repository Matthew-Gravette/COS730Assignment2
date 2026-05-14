using COS730.Backend.Application;
using COS730.Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace COS730.Backend.API;

[ApiController]
[Route("api/submission")]
public class SubmissionController : ControllerBase
{
    private readonly SubmissionService _SubmissionService;
    
    public SubmissionController(SubmissionService submissionService)
    {
        _SubmissionService = submissionService;
    }
    
    
    [HttpPost("review")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Submit(
    [FromBody] SubmissionDto submission 
    )
    {
        try
        {
            await _SubmissionService.ProcessSubmission(submission.Data);
            return Ok();
        }
        catch (ArgumentException)
        {
            return UnprocessableEntity("Invalid Format for submission");
        }
    }
}

public record SubmissionDto(
    string Data
);