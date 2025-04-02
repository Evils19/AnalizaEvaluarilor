using System.Text;
using System.Text.Json;
using AnalizaEvaluarilor.Common.DTO;

namespace AnalizaEvaluarilor.Mobile.Service;

public class AppService : IAppService
{
    private string baseUrl = "http://localhost:5178";

    public async Task<string> Authenticate(UserDTO user)
    {
        string returnValue = string.Empty;

        using (var client = new HttpClient())
        {
            var url = $"{baseUrl}/{APIs.AuthenticateUser}";
            var serialized = JsonSerializer.Serialize(user);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                returnValue = await response.Content.ReadAsStringAsync();
            }
        }

        return returnValue;
    }
    
    public async Task<string> Registration(RegistrDTO user)
    {
        string returnValue = string.Empty;

        using (var client = new HttpClient())
        {
            var url = $"{baseUrl}/{APIs.RegisterUser}";
            var serialized = JsonSerializer.Serialize(user);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                returnValue = await response.Content.ReadAsStringAsync();
            }
        }

        return returnValue;
    }

    
    
    
    
}