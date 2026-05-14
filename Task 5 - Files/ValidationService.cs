namespace COS730.Backend.Application;

public class ValidationService
{
    public string Validate(String? data)
    {
        if (data == null)
        {
            return("Invalid");
        }
        if (data.Length == 0)
        {
            return("Invalid");
        }
        return("Valid");
    }
}