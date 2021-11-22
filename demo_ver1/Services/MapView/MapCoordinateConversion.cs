namespace demo_ver1.Services.MapView;
public class MapCoordinateConversion
{
    private double SupportPixelOriginX { get; set; }
    private double SupportPixelOriginY { get; set; }
    private double SupportPositionOriginX { get; set; }
    private double SupportPositionOriginY { get; set; }
    private double Resolution { get; set; }

    public event Action? OnChanged;
    private readonly MapCommon MapCommon;

    public MapCoordinateConversion(MapCommon map)
    {
        MapCommon = map;
        MapCommon.OnParameterChanged += MapCommon_OnChanged;
        MapCommon.OnImageChanged += MapCommon_OnChanged;
        MapCommon_OnChanged();
    }

    private void MapCommon_OnChanged()
    {
        SupportPixelOriginX = -MapCommon.OriginX / MapCommon.Resolution;
        SupportPixelOriginY = MapCommon.ImageHeight + MapCommon.OriginY / MapCommon.Resolution;
        SupportPositionOriginX = MapCommon.OriginX;
        SupportPositionOriginY = MapCommon.ImageHeight * MapCommon.Resolution + MapCommon.OriginY;
        Resolution = MapCommon.Resolution;
        OnChanged?.Invoke();
    }

    public (double, double) TranslateFromPixcelToPosition(double px, double py)
        => (px * Resolution + SupportPositionOriginX, SupportPositionOriginY - py * Resolution);

    public (double, double) TranslateFromPositionToPixcel(double x, double y)
        => (x / Resolution + SupportPixelOriginX, SupportPixelOriginY - y / Resolution);
}