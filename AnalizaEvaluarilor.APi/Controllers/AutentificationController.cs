using AnalizaEvaluarilor.Application.Service;
using AnalizaEvaluarilor.Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class AutentificationController : ControllerBase
{
    
    public AutentificationController(AuthService authService)
    {
        _authService = authService;
    }
  
    private readonly AuthService _authService;
    [HttpPost]
    [Route("api/login")]
    public async Task<IActionResult> Login([FromBody] UserDTO userDto)
    {
        if (userDto==null)
        {
            return BadRequest("Invalid client request");
        }
        
        var result = await _authService.Login(userDto);
        return Ok(result);
    }
    
}