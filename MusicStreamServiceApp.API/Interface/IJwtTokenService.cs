using MusicStreamServiceApp.BLL.DTOs;

namespace MusicStreamServiceApp.API.Interface
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(UserDTO user);
    }
}
