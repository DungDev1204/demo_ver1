using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using demo_ver1.Management.Share.Hubs;
using demo_ver1.Management.Share.Models;
using demo_ver1.Share.Dtos;

namespace demo_ver1.HubClients;

public class MapHubClients : IMapHub
{
    private readonly IAccessTokenProvider? AccessTokenProvider;
    private readonly HubConnection hubConnection;
    private readonly string? HostServer;
    public bool IsConnected => hubConnection.State == HubConnectionState.Connected;
    public MapHubClients(IAccessTokenProvider provider, IConfiguration config)
    {
        HostServer = config.GetConnectionString("HostServer");
        AccessTokenProvider = provider;
        hubConnection = new HubConnectionBuilder()
                            .WithAutomaticReconnect()
                            .WithUrl($"{HostServer}/hub/map", options =>
                            {
                                options.AccessTokenProvider = async () =>
                                {
                                    var tokenResult = await AccessTokenProvider.RequestAccessToken();
                                    return tokenResult.TryGetToken(out var token) ? token.Value : "";
                                };
                            })
                            .Build();
    }
    public async Task<bool> CheckExistMapResourceFile(Guid mapId, string fileName)
         => !IsConnected && await hubConnection.InvokeAsync<bool>("CheckExistMapResourceFile", mapId, fileName);
    public Task<ResultDto<AreaMapListItemDto>> CreateMap(string? name)
        => string.IsNullOrEmpty(name)
        ? Task.FromResult(new ResultDto<AreaMapListItemDto>() { IsError = true, Message = "Name parameter is null" })
        : hubConnection.InvokeAsync<ResultDto<AreaMapListItemDto>>("CreateMap", name);
    public Task<ResultDto<PathMapDto>> CreatePath(PathMapCreateModel model)
        => hubConnection.InvokeAsync<ResultDto<PathMapDto>>("CreatePath", model);
    public Task<ResultDto<PointMapDto>> CreatePoint(Guid mapId, double x, double y)
        => hubConnection.InvokeAsync<ResultDto<PointMapDto>>("CreatePoint", mapId, x, y);
    public Task<ResultDto> DeleteMapResource(Guid mapId, string fileName)
        => hubConnection.InvokeAsync<ResultDto>("DeleteMapResource", mapId, fileName);
    public Task<ResultDto> DeletePath(Guid pathId)
       => hubConnection.InvokeAsync<ResultDto>("DeletePath", pathId);
    public Task<ResultDto> DeletePoint(Guid pointId)
        => hubConnection.InvokeAsync<ResultDto>("DeletePoint", pointId);
    public async Task<IEnumerable<AreaMapListItemDto>> FindMap()
    {
        var maps = await hubConnection.InvokeAsync<IEnumerable<AreaMapListItemDto>>("FindMap");
        foreach (var map in maps)
        {
            map.MapImageUrl = $"{HostServer}/{map.MapImageUrl}";
        }
        return maps;

    }
    public async Task<ResultDto<AreaMapDto>> GetMapByName(string uniqueName)
    {
        var response = await hubConnection.InvokeAsync<ResultDto<AreaMapDto>>("GetMapByName", uniqueName);
        if (!response.IsError && response.Result is not null)
        {
            response.Result.MapImageUrl = $"{HostServer}/{response.Result.MapImageUrl}";
        }
        return response;
    }
    public async Task<IEnumerable<AreaMapResourceDto>> GetMapResourceFiles(Guid mapId, string prefix, string prefixType = "")
    {
        var resources = await hubConnection.InvokeAsync<IEnumerable<AreaMapResourceDto>>("GetMapResourceFiles", mapId, prefix, prefixType);
        foreach (var resource in resources)
        {
            resource.FileUrl = $"{HostServer}/{resource.FileUrl}";
        }
        return resources;
    }
    public Task<IEnumerable<PathMapDto>> GetPaths(Guid mapId)
        => hubConnection.InvokeAsync<IEnumerable<PathMapDto>>("GetPaths", mapId);
    public Task<PointMapDto?> GetPoint(Guid pointId)
       => hubConnection.InvokeAsync<PointMapDto?>("GetPoint", pointId);
    public Task<IEnumerable<PointMapDto>> GetPoints(Guid mapId)
       => hubConnection.InvokeAsync<IEnumerable<PointMapDto>>("GetPoints", mapId);
    public Task<IEnumerable<PointMapDto>> GetSupportPoints(Guid pathId)
       => hubConnection.InvokeAsync<IEnumerable<PointMapDto>>("GetSupportPoints", pathId);
    public async Task StartAsync()
    {
        if (hubConnection.State == HubConnectionState.Disconnected)
        {
            await hubConnection.StartAsync();
        }
    }
    public async Task StopAsync()
    {
        if (hubConnection.State != HubConnectionState.Disconnected)
        {
            await hubConnection.StopAsync();
        }
    }
    public async Task<ResultDto<AreaMapResourceImageDto>> UpdateMapImage(Guid mapId, string? fileName)
    {
        var response = await hubConnection.InvokeAsync<ResultDto<AreaMapResourceImageDto>>("UpdateMapImage", mapId, fileName);
        if (!response.IsError && response.Result is not null)
        {
            response.Result.Url = $"{HostServer}/{response.Result.Url}";
        }

        return response;
    }
    public Task<ResultDto> UpdateMapName(Guid mapId, string name, string uniqueName)
        => hubConnection.InvokeAsync<ResultDto>("UpdateMapName", mapId, name, uniqueName);
    public Task<ResultDto> UpdateMapOrigin(Guid mapId, double originX, double originY, double resolution)
        => hubConnection.InvokeAsync<ResultDto>("UpdateMapOrigin", mapId, originX, originY, resolution);
    public Task<ResultDto> UpdatePathName(Guid pathId, string name, string uniqueName)
        => hubConnection.InvokeAsync<ResultDto>("UpdatePathName", pathId, name, uniqueName);
    public Task<ResultDto> UpdatePointName(Guid pointId, string name, string uniqueName)
        => hubConnection.InvokeAsync<ResultDto>("UpdatePointName", pointId, name, uniqueName);
    public Task<ResultDto> UpdatePointPosition(Guid pointId, Guid linkedId, double x, double y)
        => hubConnection.InvokeAsync<ResultDto>("UpdatePointPosition", pointId, linkedId, x, y);



}
