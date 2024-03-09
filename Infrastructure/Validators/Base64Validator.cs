using TaskManager.Infrastructure.Validators.Intefaces;

namespace TaskManager.Infrastructure.Validators;

public class Base64ValidatorService : IBase64ValidatorService
{
    public bool IsBase64String(string base64string)
    {
        try
        {
            byte[] bytes = Convert.FromBase64String(base64string);
            
            string decodedString = Convert.ToBase64String(bytes);
            
            return base64string == decodedString;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}