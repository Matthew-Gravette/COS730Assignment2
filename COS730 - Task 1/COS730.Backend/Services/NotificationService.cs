namespace COS730.Backend.Services;

public class NotificationService
{
    private static string result;

    public void notifyAcceptance()
    {
        result = "Accepted";
        sendNotification();
    }

    public void notifyReject()
    {
        result = "Rejected";
        sendNotification();
    }

    public void notifyRevision()
    {
        result = "Revision";
        sendNotification();
    }

    public void sendNotification()
    {
        Console.WriteLine(result);
    }
}