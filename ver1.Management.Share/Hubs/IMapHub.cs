using demo_ver1.Management.Share.Models;
using demo_ver1.Share.Dtos;


namespace demo_ver1.Management.Share.Hubs;

public interface IMapHub
{
    Task<IEnumerable<AreaMapListItemDto>> FindMap();
    Task<ResultDto<AreaMapListItemDto>> CreateMap(string name);
    Task<ResultDto<AreaMapDto>> GetMapByName(string uniqueName);
    Task<ResultDto> UpdateMapName(Guid mapId, string name, string uniqueName);
    Task<ResultDto> UpdateMapOrigin(Guid mapId, double originX, double originY, double resolution);
    Task<bool> CheckExistMapResourceFile(Guid mapId, string fileName);
    Task<IEnumerable<AreaMapResourceDto>> GetMapResourceFiles(Guid mapId, string prefix, string prefixType);
    Task<ResultDto> DeleteMapResource(Guid mapId, string fileName);
    Task<ResultDto<AreaMapResourceImageDto>> UpdateMapImage(Guid mapId, string? fileName);
    Task<IEnumerable<PointMapDto>> GetPoints(Guid mapId);
    Task<PointMapDto?> GetPoint(Guid pointId);
    Task<IEnumerable<PointMapDto>> GetSupportPoints(Guid pathId);
    Task<ResultDto<PointMapDto>> CreatePoint(Guid mapId, double x, double y);
    Task<ResultDto> UpdatePointName(Guid pointId, string name, string uniqueName);
    Task<ResultDto> UpdatePointPosition(Guid pointId, Guid linkedId, double x, double y);
    Task<ResultDto> DeletePoint(Guid pointId);
    Task<IEnumerable<PathMapDto>> GetPaths(Guid mapId);
    Task<ResultDto<PathMapDto>> CreatePath(PathMapCreateModel model);
    Task<ResultDto> UpdatePathName(Guid pathId, string name, string uniqueName);
    Task<ResultDto> DeletePath(Guid pathId);
}

