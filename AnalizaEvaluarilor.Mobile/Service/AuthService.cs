using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using AnalizaEvaluarilor.Common.DTO;


namespace AnalizaEvaluarilor.Mobile.Service;


public class AuthService
{
    public async Task<bool> IsUserAuthenticated()
    {
        string? userDetail = await SecureStorage.GetAsync(nameof(Setting.UserBasicDatail));

        if (string.IsNullOrWhiteSpace(userDetail))
        {
            return false;
        }

        var userBasicDatail = JsonSerializer.Deserialize<UserBasicDatail>(userDetail);
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(userBasicDatail.Token) as JwtSecurityToken;

        return jsonToken.ValidTo >= DateTime.UtcNow;
    }
}
