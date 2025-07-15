using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PTCGTracker;
using PTCGTracker.Infrastructure.Configuration;
using PTCGTracker.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

using var http = new HttpClient();
http.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
var config = await http.GetFromJsonAsync<Config>("appsettings.json");

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(config!.ApiSettings.BaseUrl)
});

builder.Services.AddScoped<ISetService, SetService>();
builder.Services.AddScoped<ICardService, CardService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();