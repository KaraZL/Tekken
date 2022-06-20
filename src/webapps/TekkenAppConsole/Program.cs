using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Globalization;

IConfidentialClientApplication app =
                ConfidentialClientApplicationBuilder.Create("0e5271f0-aaf1-46e5-87e9-7f09ca36744a")
                .WithClientSecret("5QI8Q~~p43u~VyKR.We7oSkclbrl20GNIOtsldwr")
                .WithAuthority(new Uri(String.Format(CultureInfo.InvariantCulture, "https://login.microsoftonline.com/{0}", "53dcff0f-b6b3-4434-8999-2dcd3eee506b")))
                .Build();

string[] ResourceIds = new string[] { "api://939b9a23-2102-4fad-a971-8b5167a8619f/.default" };

AuthenticationResult result = null;
try
{
    result = await app.AcquireTokenForClient(ResourceIds).ExecuteAsync();
}
catch (MsalClientException ex)
{
    Console.WriteLine("-------> " + ex.Message);
}

var accessToken = result.AccessToken;

Console.ReadLine();

//var headers = _client.DefaultRequestHeaders;
//_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

//var response = await _client.GetAsync("characters");
//var characters = await response.Content.ReadFromJsonAsync<IEnumerable<CharacterReadDto>>();

//return characters;
