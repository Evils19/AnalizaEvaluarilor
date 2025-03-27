using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using AnalizaEvaluarilor.Common.DTO;
using Microsoft.AspNetCore.Components;

namespace AnalizaEvaluarilor.Mobile.Service.Atribute;

public class UserAuthorizeAttribute(string Role=null) : ComponentBase
{
    
    [Inject] NavigationManager Navigation { get; set; }

    protected override async Task OnInitializedAsync()
    {
        
        string? userDetail = await SecureStorage.GetAsync(nameof(Setting.UserBasicDatail));

        if (string.IsNullOrWhiteSpace(userDetail))
        {
            Navigation.NavigateTo("/login", true);
            return;
        }

        var userBasicDatail = JsonSerializer.Deserialize<UserBasicDatail>(userDetail);

        // Verificare expirare token
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(userBasicDatail.Token) as JwtSecurityToken;

        if (jsonToken == null || jsonToken.ValidTo < DateTime.UtcNow)
        {
            Navigation.NavigateTo("/login", true);
          
        }
        
        // Validarea rolului
        if (Role != null && userBasicDatail.Role != Role && userBasicDatail.Role != "Admin")
        {
            Setting.IsAcces = false;
            return;
        }
        else
        {
            Setting.IsAcces = true;
        }

    
    
    
    
    }
}