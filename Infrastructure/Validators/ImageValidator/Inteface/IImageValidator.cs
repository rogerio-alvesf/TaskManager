using TaskManager.Infrastructure.Validators.Models;

namespace TaskManager.Infrastructure.Validators.Intefaces;

public interface IImageValidator
{
    ImageType ValidateImageType(string base64ImageString);
    SizeImage ValidateImageSize(string image);
    bool ValidateImageUpload(string image);
}