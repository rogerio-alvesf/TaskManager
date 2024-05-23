using System.Linq.Expressions;
using System.Text.RegularExpressions;
using TaskManager.Infrastructure.Validators.Intefaces;
using TaskManager.Infrastructure.Validators.Models;

namespace TaskManager.Infrastructure.Validators;

public class ImageValidator : IImageValidator
{
    public ImageType ValidateImageType(string base64ImageString)
    {
        byte[] buffer = Convert.FromBase64String(RemoveBase64MetaData(base64ImageString));

        if (IsPng(buffer))
        {
            return ImageType.PNG;
        }
        else if (IsJpeg(buffer))
        {
            return ImageType.JPEG;
        }

        throw new UnsupportedMediaTypeException("Unsupported media type. Please upload a JPEG or PNG image.");
    }

    public SizeImage ValidateImageSize(string base64ImageString)
    {
        var typeImage = ValidateImageType(base64ImageString);
        byte[] buffer = Convert.FromBase64String(RemoveBase64MetaData(base64ImageString));
        int sizeImageContent = buffer.Length;

        int minimumSize = 0;
        int maximumSize = 0;

        if (typeImage == ImageType.JPEG)
        {
            minimumSize = 500; // Defina o tamanho mínimo da imagem JPEG em bytes
            maximumSize = 10 * 1024 * 1024; // Defina o tamanho máximo da imagem JPEG em bytes (10 MB)
        }

        if (typeImage == ImageType.PNG)
        {
            minimumSize = 1000; // Defina o tamanho mínimo da imagem PNG em bytes
            maximumSize = 5 * 1024 * 1024; // Defina o tamanho máximo da imagem PNG em bytes (5 MB)
        }

        var sizeImage = new SizeImage
        {
            MinimumSize = minimumSize,
            MaximumSize = maximumSize,
            SizeImageContent = sizeImageContent
        };

        if (sizeImageContent < minimumSize || sizeImageContent > maximumSize)
        {
            throw new UnprocessableEntityException("Unprocessable entity. The image size or dimensions are not within the allowed limits.");
        }

        return sizeImage;
    }

    public bool ValidateImageUpload(string image)
    {
        var resultado = ValidateImageSize(image);

        return resultado != null ? true : false;
    }

    private bool IsPng(byte[] buffer)
    {
        return buffer.Length > 8 &&
               buffer[0] == 137 && buffer[1] == 80 && buffer[2] == 78 && buffer[3] == 71 &&
               buffer[4] == 13 && buffer[5] == 10 && buffer[6] == 26 && buffer[7] == 10;
    }
    private bool IsJpeg(byte[] buffer)
    {
        return buffer.Length > 2 && buffer[0] == 255 && buffer[1] == 216 && buffer[2] == 255;
    }
    private string RemoveBase64MetaData(string base64String)
    {
        // remove metadata of base64 (prefix "data:image/jpeg;base64," or "data:image/png;base64,")
        return Regex.Replace(base64String, @"^data:image\/(jpeg|png);base64,", string.Empty);
    }
}