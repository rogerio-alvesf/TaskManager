namespace TaskManager.Infrastructure.Validators.Models;

public class OutImageDetails
{
    public ImageType Type { get; set; }
    public int MinimumSize { get; set; }
    public int MaximumSize { get; set; }
    public SizeImage Size { get; set; }
}

public enum ImageType
{
    JPEG,
    PNG,
    UNKNOWN
}

public class SizeImage
{
    public int MinimumSize { get; set; }
    public int MaximumSize { get; set; }
    public int SizeImageContent { get; set; }
}