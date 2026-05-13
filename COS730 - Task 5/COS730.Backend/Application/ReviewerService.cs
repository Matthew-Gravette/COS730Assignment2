using COS730.Backend.Domain.Entities;
using COS730.Backend.Infrastructure;

namespace COS730.Backend.Application;

public class ReviewerService(
    ScoreRepository scoreRepository,
    EvaluationService evaluationService) {
    public void Assign(List<Reviewer> reviewers)
    {
        foreach (Reviewer reviewer in reviewers)
        {
            reviewer.AssignReview();
        }
    }
    
    public async Task GetReviewerScores(List<Reviewer> reviewers, int submissionId)
    {
        List<Score> scores = new List<Score>();
        foreach (Reviewer reviewer in reviewers)
        {
          scores.Add(new Score(reviewer.Id,reviewer.SubmitScore()));
        }
        await scoreRepository.SaveScore(submissionId,scores);
        
        await evaluationService.StartEvalution(submissionId);
    }
}