using demo_ver1.Share.Dtos;
using demo_ver1.Share.Enums;

namespace demo_ver1.Models.MapView;

public class PathModel : IDisposable
{
    public Guid Id { get; }
    public Guid AreaId { get; }
    public PathMapType PathType { get; }
    public string? Name { get; private set; }
    public string? UniqueName { get; private set; }
    public Guid StartPointId { get; private set; }
    public Guid EndPointId { get; private set; }
    public Guid SupportPoint1Id { get; private set; }
    public Guid SupportPoint2Id { get; private set; }
    public Guid SupportPoint3Id { get; private set; }
    public Guid SupportPoint4Id { get; private set; }
    public PointModel? StartPoint { get; private set; } = null;
    public PointModel? EndPoint { get; private set; } = null;
    public PointModel? SupportPoint1 { get; private set; } = null;
    public PointModel? SupportPoint2 { get; private set; } = null;
    public PointModel? SupportPoint3 { get; private set; } = null;
    public PointModel? SupportPoint4 { get; private set; } = null;

    public bool IsSelected { get; private set; } = false;
    public bool IsChanged { get; private set; } = false;

    public event Action? OnChanged;

    public PathModel(PathMapDto path, PointModel startPoint, PointModel endPoint, PointModel? sp1 = null, PointModel? sp2 = null, PointModel? sp3 = null, PointModel? sp4 = null)
    {
        if (!path.StartPointId.Equals(startPoint.Id)
            || !path.EndPointId.Equals(endPoint.Id)
            || !path.SupportPoint1Id.Equals(sp1?.Id ?? Guid.Empty)
            || !path.SupportPoint2Id.Equals(sp2?.Id ?? Guid.Empty)
            || !path.SupportPoint3Id.Equals(sp3?.Id ?? Guid.Empty)
            || !path.SupportPoint4Id.Equals(sp4?.Id ?? Guid.Empty))
        {
            throw new InvalidOperationException();
        }

        Id = path.Id;
        AreaId = path.AreaId;
        Name = path.Name;
        UniqueName = path.UniqueName;
        PathType = path.PathType;
        StartPointId = path.StartPointId;
        EndPointId = path.EndPointId;
        StartPoint = startPoint;
        EndPoint = endPoint;
        SupportPoint1Id = path.SupportPoint1Id;
        SupportPoint2Id = path.SupportPoint2Id;
        SupportPoint3Id = path.SupportPoint3Id;
        SupportPoint4Id = path.SupportPoint4Id;

        StartPoint.OnChanged += Point_OnChanged;
        EndPoint.OnChanged += Point_OnChanged;

        if (sp1 is not null)
        {
            SupportPoint1 = sp1;
            SupportPoint1.OnChanged += Point_OnChanged;
        }

        if (sp2 is not null)
        {
            SupportPoint2 = sp2;
            SupportPoint2.OnChanged += Point_OnChanged;
        }

        if (sp3 is not null)
        {
            SupportPoint3 = sp3;
            SupportPoint3.OnChanged += Point_OnChanged;
        }

        if (sp4 is not null)
        {
            SupportPoint4 = sp4;
            SupportPoint4.OnChanged += Point_OnChanged;
        }
    }

    private void Point_OnChanged()
    {
        OnChanged?.Invoke();
    }

    public void Select()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            StartPoint?.Active();
            EndPoint?.Active();
            SupportPoint1?.Active();
            SupportPoint2?.Active();
            SupportPoint3?.Active();
            SupportPoint4?.Active();
            OnChanged?.Invoke();
        }
    }

    public void Unselect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            StartPoint?.Deactive();
            EndPoint?.Deactive();
            SupportPoint1?.Deactive();
            SupportPoint2?.Deactive();
            SupportPoint3?.Deactive();
            SupportPoint4?.Deactive();
            OnChanged?.Invoke();
        }
    }

    public void UpdateName(string name, string uname)
    {
        Name = name;
        UniqueName = uname;
        OnChanged?.Invoke();
    }

    public void Dispose()
    {
        if (StartPoint is not null) StartPoint.OnChanged -= Point_OnChanged;
        if (EndPoint is not null) EndPoint.OnChanged -= Point_OnChanged;
        if (SupportPoint1 is not null)
        {
            SupportPoint1.OnChanged -= Point_OnChanged;
        }

        if (SupportPoint2 is not null)
        {
            SupportPoint2.OnChanged -= Point_OnChanged;
        }

        if (SupportPoint3 is not null)
        {
            SupportPoint3.OnChanged -= Point_OnChanged;
        }

        if (SupportPoint4 is not null)
        {
            SupportPoint4.OnChanged -= Point_OnChanged;
        }
    }
}