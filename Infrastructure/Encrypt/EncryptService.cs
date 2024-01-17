using System.Security.Cryptography;
using System.Text;

namespace TaskManager.Infrastructure.EncryptService;
public class EncryptService : IEncryptService
{
    public string EncryptData(string token)
    {
        MD5 md5Hash = MD5.Create();
        // Converter a String para array de bytes, que é como a biblioteca trabalha.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(token));

        // Cria-se um StringBuilder para recompôr a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop para formatar cada byte como uma String em hexadecimal
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }
}