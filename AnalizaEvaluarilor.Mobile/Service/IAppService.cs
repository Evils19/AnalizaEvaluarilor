using AnalizaEvaluarilor.Common.DTO;

namespace AnalizaEvaluarilor.Mobile.Service;

public interface IAppService
{
    public Task<string> Authenticate(UserDTO user);
    public Task<string> Registration(RegistrDTO user);
}