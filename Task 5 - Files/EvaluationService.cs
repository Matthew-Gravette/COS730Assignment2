using COS730.Backend.Domain.Entities;
using COS730.Backend.Domain.Enum;
using COS730.Backend.Infrastructure;

namespace COS730.Backend.Application;

public class EvaluationService(
    ScoreRepository scoreRepository,
    NotificationService notificationService)
{
    private List<int> _scores = new List<int>();
    private double _average;
    public async Task StartEvalution(int submissionId)
    {
        _scores = await scoreRepository.GetScoresForSubmission(submissionId);
        
        _average = CalculateAverage();
        var consensus = CheckConsensus();
        var result = ApplyResults(consensus);
        
        notificationService.Notify(result);
    }
    
    private double CalculateAverage()
    {
        return _scores.Average();
    }
    
    private bool CheckConsensus()
    {
        foreach (var score in _scores)
        {
            if (_average + 2 < score || _average - 2 > score)
            {
                return false;
            }
        }
        return true;
    }

    private SubmissionOutcome ApplyResults(bool consensus)
    {
        if (_average <= 5)
        {
            return SubmissionOutcome.Rejected;
        }
        return consensus ? SubmissionOutcome.Accepted : SubmissionOutcome.Revision;
    }
}