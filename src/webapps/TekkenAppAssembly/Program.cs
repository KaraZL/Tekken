using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Http;
using TekkenAppAssembly;
using TekkenAppAssembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddHttpClient<ICharactersService, CharactersService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["AzureAD:BaseAddress"]);
});


await builder.Build().RunAsync();
