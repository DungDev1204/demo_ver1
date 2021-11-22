using AntDesign.JsInterop;
namespace demo_ver1.Services.MapView;
public class MapViewer
{
    public string? MapUniqueName { get; private set; }
    public Guid MapId { get; private set; }
    public double CursorPossitionX { get; private set; }
    public double CursorPossitionY { get; private set; }
    public double Left { get; private set; } = 0;
    public double Top { get; private set; } = 0;
    public double Scale { get; private set; } = 1.0;
    public bool ShiftKey { get; private set; }

    public event Action? OnViewUpdated;
    public event Action? OnScaled;
    public event Action? OnMouseMoved;

    private readonly MapCommon MapCommon;
    private readonly MapCoordinateConversion MapCoordinateConversion;
    private readonly DomRect ContainerRect = new();

    public MapViewer(MapCommon map, MapCoordinateConversion mapConversion)
    {
        MapCommon = map;
        MapCoordinateConversion = mapConversion;
    }

    public void UpdateContainerRect(DomRect? rect = null)
    {
        if (rect is null)
        {
            ContainerRect.Width = 0;
            ContainerRect.Height = 0;
            ContainerRect.X = 0;
            ContainerRect.Y = 0;
            ContainerRect.Top = 0;
            ContainerRect.Right = 0;
            ContainerRect.Bottom = 0;
            ContainerRect.Left = 0;
        }
        else
        {
            ContainerRect.Width = rect.Width;
            ContainerRect.Height = rect.Height;
            ContainerRect.X = rect.X;
            ContainerRect.Y = rect.Y;
            ContainerRect.Top = rect.Top;
            ContainerRect.Right = rect.Right;
            ContainerRect.Bottom = rect.Bottom;
            ContainerRect.Left = rect.Left;
        }
    }

    public void FitContainer()
    {
        if (MapCommon.ImageWidth == 0
            || MapCommon.ImageHeight == 0
            || ContainerRect is null
            || ContainerRect.Width == 0
            || ContainerRect.Height == 0) return;

        double xScale = (double)ContainerRect.Width / MapCommon.ImageWidth;
        double yScale = (double)ContainerRect.Height / MapCommon.ImageHeight;

        Scale = xScale < yScale ? xScale : yScale;
        Left = (double)(ContainerRect.Width - MapCommon.ImageWidth) / 2;
        Top = (double)(ContainerRect.Height - MapCommon.ImageHeight) / 2;
        OnScaled?.Invoke();
    }

    public void UpdateShiftKeyState(bool state) => ShiftKey = state;

    public void Move(double moveX, double moveY)
    {
        Left += moveX;
        Top += moveY;
        OnViewUpdated?.Invoke();
    }

    public void ZoomIn(double offsetX, double offsetY)
    {
        double scaleChange = Scale < 5 ? 0.5 : 1;
        Left -= (offsetX - MapCommon.ImageWidth / 2) * scaleChange;
        Top -= (offsetY - MapCommon.ImageHeight / 2) * scaleChange;
        Scale += scaleChange;
        OnScaled?.Invoke();
    }

    public void ZoomOut(double offsetX, double offsetY)
    {
        if (Scale <= 0.3) return;
        double scaleChange = Scale > 3 ? -0.5 : -0.1;
        Left -= (offsetX - MapCommon.ImageWidth / 2) * scaleChange;
        Top -= (offsetY - MapCommon.ImageHeight / 2) * scaleChange;
        Scale += scaleChange;
        OnScaled?.Invoke();
    }

    public void UpdateCursorPosition(double clientX, double clientY)
    {
        var px = (clientX - (double)ContainerRect.X - Left - MapCommon.ImageWidth * (1 - Scale) / 2) / Scale;
        var py = (clientY - (double)ContainerRect.Y - Top - MapCommon.ImageHeight * (1 - Scale) / 2) / Scale;

        (CursorPossitionX, CursorPossitionY) = MapCoordinateConversion.TranslateFromPixcelToPosition(px, py);
        OnMouseMoved?.Invoke();
    }
}
