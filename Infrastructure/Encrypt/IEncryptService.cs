namespace TaskManager.Infrastructure.EncryptService;
public interface IEncryptService
{
    string EncryptData(string token);
}