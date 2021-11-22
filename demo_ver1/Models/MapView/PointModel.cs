using demo_ver1.Services.MapView;
using demo_ver1.Share.Dtos;


namespace demo_ver1.Models.MapView;
public class PointModel : IDisposable
{
    public Guid Id { get; }
    public Guid AreaId { get; }
    public bool IsSupport { get; }
    public string? Name { get; private set; }
    public string? UniqueName { get; private set; }
    public double SavedPositionX { get; private set; }
    public double SavedPositionY { get; private set; }
    public double PositionX => LinkedPointModel?.PositionX ?? positionX;
    public double PositionY => LinkedPointModel?.PositionY ?? positionY;
    private double positionX { get; set; }
    private double positionY { get; set; }
    public double PixelX => LinkedPointModel?.PixelX ?? pixelX;
    public double PixelY => LinkedPointModel?.PixelY ?? pixelY;
    private double pixelX { get; set; }
    private double pixelY { get; set; }
    public Guid LinkedPointId { get; private set; }
    public bool IsSupportActive { get; private set; } = false;
    public bool IsSelected { get; private set; } = false;
    public bool IsDataChanged { get; private set; } = false;

    public event Action? OnChanged;
    public event Action<bool>? OnDataChanged;

    private readonly MapCoordinateConversion CoordinateConversion;
    private PointModel? LinkedPointModel { get; set; }

    public PointModel(MapCoordinateConversion conversion, PointMapDto point)
    {
        CoordinateConversion = conversion;
        CoordinateConversion.OnChanged += CoordinateConversion_OnChanged;

        Id = point.Id;
        AreaId = point.AreaId;
        IsSupport = point.IsSupport;
        Name = point.Name;
        UniqueName = point.UniqueName;
        LinkedPointId = point.LinkedPointId;

        if (LinkedPointId.Equals(Guid.Empty))
        {
            UpdatePosition(point.X, point.Y);
        }
    }

    public void LinkTo(PointModel? linkedPoint)
    {
        if (LinkedPointModel is not null)
        {
            LinkedPointModel.OnChanged -= UpdatePositionFromLinkPoint;
        }

        LinkedPointId = Guid.Empty;
        LinkedPointModel = linkedPoint;
        if (LinkedPointModel is not null)
        {
            LinkedPointId = LinkedPointModel.LinkedPointId;
            LinkedPointModel.OnChanged += UpdatePositionFromLinkPoint;
            UpdatePositionFromLinkPoint();
        }
    }

    private void UpdatePositionFromLinkPoint()
    {
        if (LinkedPointModel is not null)
        {
            OnChanged?.Invoke();
        }
    }

    private void CoordinateConversion_OnChanged()
    {
        if (LinkedPointId.Equals(Guid.Empty))
        {
            (pixelX, pixelY) = CoordinateConversion.TranslateFromPositionToPixcel(positionX, positionY);
            OnChanged?.Invoke();
        }
    }

    public void SetPosition(double x, double y)
    {
        if (LinkedPointModel is not null)
        {
            LinkedPointModel.SetPosition(x, y);
        }
        else if (positionX != x || positionY != y)
        {

            positionX = x;
            positionY = y;

            (pixelX, pixelY) = CoordinateConversion.TranslateFromPositionToPixcel(positionX, positionY);
            bool _changed = IsDataChanged;
            IsDataChanged = positionX != SavedPositionX || positionY != SavedPositionY;

            if (_changed != IsDataChanged)
            {
                OnDataChanged?.Invoke(IsDataChanged);
            }
            OnChanged?.Invoke();
        }
    }

    public void ResetPosition()
    {
        if (LinkedPointModel is not null)
        {
            LinkedPointModel.ResetPosition();
        }
        else
        {
            SetPosition(SavedPositionX, SavedPositionY);
        }
    }

    public void UpdateName(string name, string uname)
    {
        Name = name;
        UniqueName = uname;
        OnChanged?.Invoke();
    }

    public void UpdatePosition(double x, double y)
    {
        if (LinkedPointModel is not null)
        {
            LinkedPointModel.UpdatePosition(x, y);
        }
        else
        {
            SavedPositionX = x;
            SavedPositionY = y;
            positionX = SavedPositionX;
            positionY = SavedPositionY;
            (pixelX, pixelY) = CoordinateConversion.TranslateFromPositionToPixcel(positionX, positionY);
            IsDataChanged = false;
            IsDataChanged = positionX != SavedPositionX || positionY != SavedPositionY;
            OnDataChanged?.Invoke(IsDataChanged);
            OnChanged?.Invoke();
        }
    }

    public void Select()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            OnChanged?.Invoke();
        }
    }

    public void Unselect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            OnChanged?.Invoke();
        }
    }

    private static readonly List<PointModel> ActivedPoints = new();
    public void Active()
    {
        if (IsSupport && !IsSupportActive)
        {
            if (!ActivedPoints.Contains(this))
            {
                ActivedPoints.Add(this);
            }
            IsSupportActive = true;
            OnChanged?.Invoke();
        }
    }

    public void Deactive()
    {
        if (IsSupport && IsSupportActive)
        {
            IsSupportActive = false;
            OnChanged?.Invoke();
        }
    }

    public static void DeactiveAll()
    {
        foreach (var model in ActivedPoints)
        {
            model.Deactive();
        }

        ActivedPoints.Clear();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(true);  // Violates rule
    }

    protected virtual void Dispose(bool disposing) => CoordinateConversion.OnChanged -= CoordinateConversion_OnChanged;
}

