using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
using demo_ver1.HubClients;

using demo_ver1;


 
     
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddAntDesign();
        builder.Services.AddScoped<MapHubClients>();

await builder.Build().RunAsync();

public class RobotNetAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public RobotNetAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager nav) : base(provider, nav)
    {
        ConfigureHandler(authorizedUrls: new[] { "https://id.phenikaa.online" });
    }
}
