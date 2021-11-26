namespace demo_ver1.Share.Dtos;

public class AreaMapDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? UniqueName { get; set; }
    public string? MapImageFile { get; set; }
    public string? MapImageUrl { get; set; }
    public short MapImageWidth { get; set; }
    public short MapImageHeight { get; set; }
    public double MapOriginX { get; set; }
    public double MapOriginY { get; set; }
    public double MapResolution { get; set; }
}
