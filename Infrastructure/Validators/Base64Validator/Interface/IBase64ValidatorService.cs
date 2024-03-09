namespace TaskManager.Infrastructure.Validators.Intefaces;

public interface IBase64ValidatorService
{
    public bool IsBase64String(string base64string);
}
