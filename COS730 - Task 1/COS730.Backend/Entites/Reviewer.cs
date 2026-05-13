namespace COS730.Backend.Entites;

public class Reviewer
{
    public string Name;
    public int CurrentAmountOfReviews;
    public bool Capacity;

    public Reviewer(string name, int currentAmountOfReviews, bool capacity)
    {
        Name = name;
        CurrentAmountOfReviews = currentAmountOfReviews;
        Capacity = capacity;
    }

    public void AssignReview(){
        CurrentAmountOfReviews++;
    }
    
    public int SubmitScore()
    {
        var score = new Random();
        return score.Next(4, 10);
    }
}
