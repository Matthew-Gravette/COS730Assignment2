using COS730.Backend.Domain.Entities;
using COS730.Backend.Infrastructure;

namespace COS730.Backend.Application;

public class SubmissionService(
    SubmissionRepository submissionRepository, 
    ValidationService validationService,
    ReviewerRepository reviewerRepository,
    ReviewerService reviewerService)
{
    public async Task ProcessSubmission(string submissionData)
    {
        String response = validationService.Validate(submissionData);
        if (response == "Invalid")
            throw new ArgumentException("Invalid submission");
        
        var submissionId = await submissionRepository.SaveSubmissions(submissionData);
        List<Reviewer> reviewers = await reviewerRepository.FetchReviewers();
        
        reviewerService.Assign(reviewers);
        
        await reviewerService.GetReviewerScores(reviewers,submissionId);
    }
}