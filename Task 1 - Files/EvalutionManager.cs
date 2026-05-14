using COS730.Backend.Entites;
using COS730.Backend.Services;

namespace COS730.Backend.Controllers;

public class EvalutionManager
{
    private static readonly List<int> _scores = new List<int>();
    private static double _average;
    private static bool _consensus;
    private static Database _database;
    private readonly NotificationService _notificationService;

    public EvalutionManager( NotificationService notificationService, Database database)
    {
        _notificationService  = notificationService;
        _database = database;
    }
    public async Task startEvalution(List<Reviewer> reviewers)
    {
        foreach (Reviewer reviewer in reviewers)
        {
            int score = reviewer.SubmitScore();
            _scores.Add(score);
            await _database.saveScore(score);
        }
        _average = calculateAverage();
        _consensus = checkConsensus();
        string result = applyResults();

        switch (result)
        {
            case "accepted":
                _notificationService.notifyAcceptance();
                break;
            case "rejected":
                _notificationService.notifyReject();
                break;
            case "revision":
                _notificationService.notifyRevision();
                break;
        }
    }

    private static double calculateAverage()
    {
        return _scores.Average();
    }
    
    private static bool checkConsensus()
    {
        foreach (int score in _scores)
        {
            if (_average + 1 < score || _average - 1 > score)
            {
                return false;
            }
        }
        return true;
    }

    private static string applyResults()
    {
        if (_average <= 5)
        {
            return "rejected";
        }
        return !_consensus ? "revision" : "accepted";
    }
}