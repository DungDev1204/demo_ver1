using demo_ver1.Management.Share.Models;
using demo_ver1.Enums;
using demo_ver1.Models.MapView;
using demo_ver1.Share.Dtos;
using demo_ver1.Share.Enums;

namespace demo_ver1.Services.MapView;

public class AreaMapProvider
{
    public AreaMapDesignCommand DesignCommand { get; private set; } = AreaMapDesignCommand.Default;
    public bool IsSeletedItem => SelectedPoint is not null || SelectedPath is not null;
    public string? SeletedItemName => IsSeletedItem ? (SelectedPoint is null ? SelectedPath?.Name : SelectedPoint.Name) : "";
    public Guid SelectedPointId => SelectedPoint?.Id ?? Guid.Empty;
    public Guid SelectedPathId => SelectedPath?.Id ?? Guid.Empty;
    public int ChangedItemsCount { get; private set; } = 0;
    public IReadOnlyList<PointModel> AreaMapPoints => PointModels.AsReadOnly();
    public IReadOnlyList<PathModel> AreaMapPaths => PathModels.AsReadOnly();
    public event Action? OnUpdatedAreaMapPoints;
    public event Action? OnUpdatedAreaMapPaths;
    public event Action? ChangedItemsCountChanged;
    public event Action? SelectedItemChanged;
    public event Action<AreaMapDesignCommand>? DesignCommandChanged;

    private readonly MapCoordinateConversion CoordinateConversion;
    private List<PathModel> PathModels { get; } = new();
    private List<PointModel> PointModels { get; } = new();
    private PointModel? SelectedPoint { get; set; } = null;
    private PathModel? SelectedPath { get; set; } = null;

    public AreaMapProvider(MapCoordinateConversion conversion)
    {
        CoordinateConversion = conversion;
    }

    public void SetDesignCommand(AreaMapDesignCommand command)
    {
        Console.WriteLine(command);
        DesignCommand = command;
        DesignCommandChanged?.Invoke(DesignCommand);
    }

    public void LoadPoints(IEnumerable<PointMapDto> points)
    {
        PointModels.Clear();
        AddPoints(points);
    }

    public void AddPoints(IEnumerable<PointMapDto> points)
    {
        foreach (var point in points)
        {
            var model = new PointModel(CoordinateConversion, point);

            model.OnDataChanged += isChanged =>
            {
                ChangedItemsCount += isChanged ? 1 : -1;
                ChangedItemsCountChanged?.Invoke();
            };

            PointModels.Add(model);
        }

        foreach (var model in PointModels)
        {
            if (model.IsSupport && !model.LinkedPointId.Equals(Guid.Empty))
            {
                var linkModel = PointModels.FirstOrDefault(m => m.Id.Equals(model.LinkedPointId));
                if (linkModel is not null)
                {
                    model.LinkTo(linkModel);
                }
            }
        }

        OnUpdatedAreaMapPoints?.Invoke();
    }

    public void AddPoint(PointMapDto point)
    {
        var model = new PointModel(CoordinateConversion, point);

        model.OnDataChanged += isChanged =>
        {
            ChangedItemsCount += isChanged ? 1 : -1;
            ChangedItemsCountChanged?.Invoke();
        };
        PointModels.Add(model);

        if (model.IsSupport && !model.LinkedPointId.Equals(Guid.Empty))
        {
            var linkModel = PointModels.FirstOrDefault(m => m.Id.Equals(model.LinkedPointId));
            if (linkModel is not null)
            {
                model.LinkTo(linkModel);
            }
        }

        OnUpdatedAreaMapPoints?.Invoke();
        SelectPoint(model);
    }

    public void DeleteSelectedPoint()
    {
        if (SelectedPoint is null || SelectedPoint.IsSupport) return;

        DeletePoint(SelectedPoint);
    }

    public void DeletePoint(PointModel? deletePoint)
    {
        if (deletePoint is null) return;

        if (deletePoint.LinkedPointId.Equals(Guid.Empty))
        {
            if (!deletePoint.Id.Equals(Guid.Empty))
            {
                foreach (var point in PointModels)
                {
                    if (point.LinkedPointId.Equals(deletePoint.Id))
                    {
                        point.LinkTo(null);
                        point.UpdatePosition(deletePoint.PositionX, deletePoint.PositionY);
                    }
                }
            }
        }
        else
        {
            var linkedPoint = PointModels.FirstOrDefault(p => p.Id.Equals(deletePoint.LinkedPointId));
            foreach (var point in PointModels)
            {
                if (point.LinkedPointId.Equals(deletePoint.Id))
                {
                    point.LinkTo(linkedPoint);
                }
            }
        }

        PointModels.Remove(deletePoint);
        if (deletePoint.Equals(SelectedPoint))
        {
            SelectPoint(null);
        }
        OnUpdatedAreaMapPoints?.Invoke();
    }

    public void DeleteSelectedPath()
    {
        if (SelectedPath is null) return;
        DeletePath(SelectedPath);
    }

    public void DeletePath(PathModel deletePath)
    {
        if (deletePath is null) return;

        DeletePoint(deletePath.StartPoint);
        DeletePoint(deletePath.EndPoint);

        if (deletePath.SupportPoint1 is not null) DeletePoint(deletePath.SupportPoint1);
        if (deletePath.SupportPoint2 is not null) DeletePoint(deletePath.SupportPoint2);
        if (deletePath.SupportPoint3 is not null) DeletePoint(deletePath.SupportPoint3);
        if (deletePath.SupportPoint4 is not null) DeletePoint(deletePath.SupportPoint4);

        PathModels.Remove(deletePath);
        if (deletePath.Equals(SelectedPath))
        {
            SelectPath(null);
        }
        OnUpdatedAreaMapPaths?.Invoke();
    }

    public void LoadPaths(IEnumerable<PathMapDto> paths)
    {
        PathModels.Clear();
        foreach (var path in paths)
        {
            var startPoint = PointModels.FirstOrDefault(p => p.Id.Equals(path.StartPointId));
            var endPoint = PointModels.FirstOrDefault(p => p.Id.Equals(path.EndPointId));
            if (startPoint is null || endPoint is null) break;

            PointModel? pointSP1 = null;
            if (!path.SupportPoint1Id.Equals(Guid.Empty))
            {
                pointSP1 = PointModels.FirstOrDefault(p => p.Id.Equals(path.SupportPoint1Id));
                if (pointSP1 is null) break;
            }

            PointModel? pointSP2 = null;
            if (!path.SupportPoint2Id.Equals(Guid.Empty))
            {
                pointSP2 = PointModels.FirstOrDefault(p => p.Id.Equals(path.SupportPoint2Id));
                if (pointSP2 is null) break;
            }

            PointModel? pointSP3 = null;
            if (!path.SupportPoint3Id.Equals(Guid.Empty))
            {
                pointSP3 = PointModels.FirstOrDefault(p => p.Id.Equals(path.SupportPoint3Id));
                if (pointSP3 is null) break;
            }

            PointModel? pointSP4 = null;
            if (!path.SupportPoint4Id.Equals(Guid.Empty))
            {
                pointSP4 = PointModels.FirstOrDefault(p => p.Id.Equals(path.SupportPoint4Id));
                if (pointSP4 is null) break;
            }

            PathModels.Add(new PathModel(path, startPoint, endPoint, pointSP1, pointSP2, pointSP3, pointSP4));
        }

        OnUpdatedAreaMapPaths?.Invoke();
    }

    public void CreateLine(double startX, double startY)
    {
        var startPoint = new PointModel(CoordinateConversion, new PointMapDto()
        {
            IsSupport = true,
            X = startX,
            Y = startY,
        });

        var endPoint = new PointModel(CoordinateConversion, new PointMapDto()
        {
            IsSupport = true,
            X = startX,
            Y = startY,
        });


        PointModels.Add(startPoint);
        PointModels.Add(endPoint);

        var lineModel = new PathModel(new PathMapDto()
        {
            PathType = PathMapType.Line,
        }, startPoint, endPoint);

        PathModels.Add(lineModel);

        SelectPath(lineModel);
        SelectPoint(endPoint);

        OnUpdatedAreaMapPaths?.Invoke();
        OnUpdatedAreaMapPoints?.Invoke();
    }

    public void CreateQuadraticCurveto(double startX, double startY)
    {
        var startPoint = new PointModel(CoordinateConversion, new PointMapDto()
        {
            IsSupport = true,
            X = startX,
            Y = startY,
        });

        var endPoint = new PointModel(CoordinateConversion, new PointMapDto()
        {
            IsSupport = true,
            X = startX,
            Y = startY,
        });

        var controlPoint = new PointModel(CoordinateConversion, new PointMapDto()
        {
            IsSupport = true,
            X = startX,
            Y = startY,
        });


        PointModels.Add(startPoint);
        PointModels.Add(endPoint);
        PointModels.Add(controlPoint);

        var model = new PathModel(new PathMapDto()
        {
            PathType = PathMapType.QuadraticCurveto,
        }, startPoint, endPoint, controlPoint);

        PathModels.Add(model);

        SelectPath(model);
        SelectPoint(endPoint);

        OnUpdatedAreaMapPaths?.Invoke();
        OnUpdatedAreaMapPoints?.Invoke();
    }

    public void AddPath(PathMapDto path)
    {
        var startPoint = PointModels.FirstOrDefault(p => p.Id.Equals(path.StartPointId));
        var endPoint = PointModels.FirstOrDefault(p => p.Id.Equals(path.EndPointId));
        if (startPoint is null || endPoint is null) return;

        PointModel? pointSP1 = null;
        if (!path.SupportPoint1Id.Equals(Guid.Empty))
        {
            pointSP1 = PointModels.FirstOrDefault(p => p.Id.Equals(path.SupportPoint1Id));
            if (pointSP1 is null) return;
        }

        PointModel? pointSP2 = null;
        if (!path.SupportPoint2Id.Equals(Guid.Empty))
        {
            pointSP2 = PointModels.FirstOrDefault(p => p.Id.Equals(path.SupportPoint2Id));
            if (pointSP2 is null) return;
        }

        PointModel? pointSP3 = null;
        if (!path.SupportPoint3Id.Equals(Guid.Empty))
        {
            pointSP3 = PointModels.FirstOrDefault(p => p.Id.Equals(path.SupportPoint3Id));
            if (pointSP3 is null) return;
        }

        PointModel? pointSP4 = null;
        if (!path.SupportPoint4Id.Equals(Guid.Empty))
        {
            pointSP4 = PointModels.FirstOrDefault(p => p.Id.Equals(path.SupportPoint4Id));
            if (pointSP4 is null) return;
        }

        var model = new PathModel(path, startPoint, endPoint, pointSP1, pointSP2, pointSP3, pointSP4);
        PathModels.Add(model);
        OnUpdatedAreaMapPaths?.Invoke();
        SelectPath(model);
    }

    public PointMapDto? GetSelectedPoint()
    {
        return SelectedPoint is null ? null : new PointMapDto()
        {
            Id = SelectedPoint.Id,
            AreaId = SelectedPoint.AreaId,
            IsSupport = false,
            Name = SelectedPoint.Name,
            UniqueName = SelectedPoint.UniqueName,
            X = SelectedPoint.PositionX,
            Y = SelectedPoint.PositionY,
            LinkedPointId = SelectedPoint.LinkedPointId,
        };
    }

    public (bool, double, double) GetPointPosition(Guid id)
    {
        if (id.Equals(Guid.Empty)) return (true, 0, 0);
        var point = PointModels.FirstOrDefault(p => p.Id.Equals(id));
        return point is null ? (true, 0, 0) : (false, point.PositionX, point.PositionY);
    }

    public void SelectPoint(PointModel? model)
    {
        if ((SelectedPoint is null && model is null) || (SelectedPoint is not null && model is not null && SelectedPoint.Equals(model)))
        {
            return;
        }

        if (SelectedPoint is not null)
        {
            SelectedPoint.Unselect();
            SelectedPoint = null;
        }

        if (model is not null)
        {
            SelectedPoint = model;
            SelectedPoint.Select();

            if (SelectedPath is not null && !CheckSelectedPointOnSelectedPath())
            {
                SelectPath(null);
            }
        }

        SelectedItemChanged?.Invoke();
    }

    public void LinkSelectedPointTo(Guid id)
    {
        if (SelectedPoint is null || SelectedPoint.Id.Equals(id)) return;

        var linkModel = PointModels.FirstOrDefault(m => m.Id.Equals(id));
        if (linkModel is not null)
        {
            SelectedPoint.LinkTo(linkModel);
        }
    }

    public bool CheckSelectedPointOnSelectedPath()
    {
        if (SelectedPoint is null || SelectedPath is null) return false;
        return SelectedPath.StartPointId.Equals(SelectedPoint.Id)
                    || SelectedPath.EndPointId.Equals(SelectedPoint.Id)
                    || SelectedPath.SupportPoint1Id.Equals(SelectedPoint.Id)
                    || SelectedPath.SupportPoint2Id.Equals(SelectedPoint.Id)
                    || SelectedPath.SupportPoint3Id.Equals(SelectedPoint.Id)
                    || SelectedPath.SupportPoint4Id.Equals(SelectedPoint.Id);
    }

    public PathMapCreateModel? GetSelectedPathCreateModel()
    {
        return SelectedPath is null || SelectedPath.StartPoint is null || SelectedPath.EndPoint is null ? null : new PathMapCreateModel()
        {
            AreaId = SelectedPath.AreaId,
            PathType = SelectedPath.PathType,
            StartPositionX = SelectedPath.StartPoint.PositionX,
            StartPositionY = SelectedPath.StartPoint.PositionY,
            EndPositionX = SelectedPath.EndPoint.PositionX,
            EndPositionY = SelectedPath.EndPoint.PositionY,
            Support1PositionX = SelectedPath.SupportPoint1?.PositionX ?? 0,
            Support1PositionY = SelectedPath.SupportPoint1?.PositionY ?? 0,
            Support2PositionX = SelectedPath.SupportPoint2?.PositionX ?? 0,
            Support2PositionY = SelectedPath.SupportPoint2?.PositionY ?? 0,
            Support3PositionX = SelectedPath.SupportPoint3?.PositionX ?? 0,
            Support3PositionY = SelectedPath.SupportPoint3?.PositionY ?? 0,
            Support4PositionX = SelectedPath.SupportPoint4?.PositionX ?? 0,
            Support4PositionY = SelectedPath.SupportPoint4?.PositionY ?? 0,
        };
    }

    public PathMapDto? GetSelectedPath()
    {
        return SelectedPath is null ? null : new PathMapDto()
        {
            Id = SelectedPath.Id,
            AreaId = SelectedPath.AreaId,
            Name = SelectedPath.Name,
            UniqueName = SelectedPath.UniqueName,
            PathType = SelectedPath.PathType,
            StartPointId = SelectedPath.StartPointId,
            EndPointId = SelectedPath.EndPointId,
            SupportPoint1Id = SelectedPath.SupportPoint1Id,
            SupportPoint2Id = SelectedPath.SupportPoint2Id,
            SupportPoint3Id = SelectedPath.SupportPoint3Id,
            SupportPoint4Id = SelectedPath.SupportPoint4Id,
        };
    }

    public void SelectPath(PathModel? model)
    {
        if (model is not null && SelectedPoint is not null)
        {
            SelectedPoint.Unselect();
            SelectedPoint = null;
        }

        if ((SelectedPath is null && model is null) || (SelectedPath is not null && model is not null && SelectedPath.Equals(model)))
        {
            return;
        }

        if (SelectedPath is not null)
        {
            SelectedPath.Unselect();
            SelectedPath = null;
        }

        if (model is not null)
        {
            SelectedPath = model;
            SelectedPath.Select();
        }

        SelectedItemChanged?.Invoke();
    }


    public void MoveSelectedPointTo(double x, double y)
    {
        SelectedPoint?.SetPosition(x, y);
    }

    public void ResetSelectedPointPosition()
    {
        SelectedPoint?.ResetPosition();
    }

    public void UpdateSelectedPointName(Guid id, string name, string uname)
    {
        if (SelectedPoint is not null && SelectedPoint.Id.Equals(id))
        {
            SelectedPoint.UpdateName(name, uname);
        }
    }

    public void UpdateSelectedPathName(Guid id, string name, string uname)
    {
        if (SelectedPath is not null && SelectedPath.Id.Equals(id))
        {
            SelectedPath.UpdateName(name, uname);
        }
    }

    public void UpdateSelectedPointPosition(Guid id, double x, double y)
    {
        if (SelectedPoint is not null && SelectedPoint.Id.Equals(id))
        {
            SelectedPoint.UpdatePosition(x, y);
        }
    }

    public async Task<int> UpdateChangedPoints(Func<Guid, Guid, double, double, Task<bool>> updateFunc)
    {
        int savedCount = 0;
        foreach (var point in PointModels)
        {
            if (point.IsDataChanged)
            {
                if (await updateFunc(point.Id, point.LinkedPointId, point.PositionX, point.PositionY))
                {
                    savedCount++;
                    point.UpdatePosition(point.PositionX, point.PositionY);
                }
            }
        }
        return savedCount;
    }
}

