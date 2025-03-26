using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnalizaEvaluarilor.Common.DTO;
using AnalizaEvaluarilor.Domain.Entity.User;
using AnalizaEvaluarilor.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AnalizaEvaluarilor.Application.Service;

public class AuthService(ApplicationDbContext _context, IPasswordHasher<User> _passwordHasher,IConfiguration _configuration)
{
    

    public async Task<AuthResponseDto> Login(UserDTO userDto)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u=>u.Email==userDto.Username);
        if (user == null)
        {
            return new AuthResponseDto("", "User not found");

        }
        
      var rezultpassword =  _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDto.Password);
      if (rezultpassword == PasswordVerificationResult.Failed)
      {
          //Parola incorecta
          return new AuthResponseDto("", "Invalid password");
      }
      //Geneream Token JWT

    var token=  GenerateStaticJwtToken(user);

      return new AuthResponseDto(token);
      
    }

    // Instalam Microsoft.AspNetCore.Authentication.JwtBearer
    private string GenerateStaticJwtToken(User user)
    {
        var role = user.UserRoles.FirstOrDefault()?.Role.Name;
        
        //Informatiile pe care le dorim in token
        Claim[] claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role?? "User")
        ];
        //Cheia secreta
        var secretkey = _configuration.GetValue<string>("Jwt:SecretKey");
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
        //Semnatura care are rolul de a verifica integritatea tokenului
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        //Crearea tokenului
        var jwtSecurityToken = new JwtSecurityToken(
            issuer:_configuration.GetValue<string>("Jwt:Issuer"),
            audience:_configuration.GetValue<string>("Jwt:Audience"),
            claims, 
            expires: DateTime.Now.AddHours(8), 
            signingCredentials: signingCredentials);
        //Serializarea tokenului
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        
        return token;
        
    }
}