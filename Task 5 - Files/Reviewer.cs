namespace COS730.Backend.Domain.Entities;

public class Reviewer(int id, string name, int currentAmountOfReviews, bool available)
{
    public string Name { get; private set; } = name;
    public int Id { get; private set; } = id;
    public int CurrentAmountOfReviews { get; private set; } = currentAmountOfReviews;
    public bool Available { get; private set; } = available;

    public void AssignReview(){
        CurrentAmountOfReviews++;
    }
    
    public int SubmitScore()
    {
        var score = new Random();
        return score.Next(4, 10);
    }
}