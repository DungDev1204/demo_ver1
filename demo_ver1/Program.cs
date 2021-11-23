using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
using demo_ver1.HubClients;
using demo_ver1.Services.MapView;
using demo_ver1;


 
     
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");


        builder.Services.AddScoped<RobotNetAuthorizationMessageHandler>();
            builder.Services.AddHttpClient("RobotNetHttpClient", options =>
        {
            //options.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            options.BaseAddress = new Uri(builder.Configuration.GetConnectionString("HostServer"));
        })
        .AddHttpMessageHandler<RobotNetAuthorizationMessageHandler>();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("RobotNetHttpClient"));
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("OidcAuthentication", options.ProviderOptions);
            options.ProviderOptions.DefaultScopes.Add("roles");
            options.ProviderOptions.DefaultScopes.Add("profile");
            options.ProviderOptions.DefaultScopes.Add("email");
            options.ProviderOptions.DefaultScopes.Add("openid");
            options.ProviderOptions.DefaultScopes.Add("RobotNetUserApiScope");
        });
        builder.Services.AddAntDesign();
        builder.Services.AddScoped<MapHubClients>();
        builder.Services.AddSingleton<MapCommon>();
        builder.Services.AddSingleton<MapCoordinateConversion>();
        builder.Services.AddSingleton<MapViewer>();
        builder.Services.AddSingleton<AreaMapProvider>();

await builder.Build().RunAsync();

public class RobotNetAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public RobotNetAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager nav) : base(provider, nav)
    {
        ConfigureHandler(authorizedUrls: new[] { "https://id.phenikaa.online" });
    }
}
