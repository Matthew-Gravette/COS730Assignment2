namespace COS730.Backend.Application;
using COS730.Backend.Domain.Enum;

public class NotificationService
{
    public void Notify(SubmissionOutcome outcome)
    {
        Console.WriteLine(outcome.ToString());
        Console.WriteLine("End: " + DateTime.Now.ToString("HH:mm:ss"));
    }
}