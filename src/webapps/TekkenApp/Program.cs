using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Identity.Web;
using Microsoft.Net.Http.Headers;
using TekkenApp.Data;
using TekkenApp.HttpHandlers;
using TekkenApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<AuthenticationDelegatingHandler>(); //register delegatinghandler

builder.Services.AddHttpClient<ICharactersService, CharactersService>("Characters API", options =>
{
    options.BaseAddress = new Uri(builder.Configuration["AzureAD:BaseAddress"]);

    options.DefaultRequestHeaders.Clear();
    options.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

builder.Services.AddHttpContextAccessor(); //Access HttpContext in HttpMessageHandler

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//To call api
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
