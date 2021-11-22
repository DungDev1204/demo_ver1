namespace demo_ver1.Services.MapView;

public class MapCommon
{
    public string? MapName { get; private set; }
    public string? MapUniqueName { get; private set; }
    public Guid MapId { get; private set; }
    public string? MapImageFile { get; private set; }
    public short ImageWidth { get; private set; }
    public short ImageHeight { get; private set; }
    public string? ImageUrl { get; private set; }
    public double OriginX { get; private set; }
    public double OriginY { get; private set; }
    public double Resolution { get; private set; } = 1;

    public event Action? OnNameChanged;
    public event Action? OnImageChanged;
    public event Action? OnParameterChanged;

    public void UpdateImage(string imgFile, short imgWidth, short imgHeight, string imgUrl)
    {
        MapImageFile = imgFile;
        ImageWidth = imgWidth;
        ImageHeight = imgHeight;
        ImageUrl = imgUrl;
        OnImageChanged?.Invoke();
    }

    public void UpdateName(Guid id, string name, string uname)
    {
        MapId = id;
        MapName = name;
        MapUniqueName = uname;
        OnNameChanged?.Invoke();
    }

    public void UpdateParameter(double originX, double originY, double resolution)
    {
        OriginX = originX;
        OriginY = originY;
        Resolution = resolution;
        OnParameterChanged?.Invoke();
    }
}