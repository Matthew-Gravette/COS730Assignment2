namespace COS730.Backend.Entites;

public class Validator
{
    public string validateFormat(string? data) {
        if (data == null)
            return "invalid";
        if (data.Length == 0)
            return "invalid";
        return "valid";
    }
}