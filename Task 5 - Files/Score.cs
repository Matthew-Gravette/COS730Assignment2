namespace COS730.Backend.Domain.Entities;

public class Score(int reviewerId, int scoreValue)
{
    public int ScoreValue { get; set; } = scoreValue;
    public int ReviewerId { get; set; } =  reviewerId;
}