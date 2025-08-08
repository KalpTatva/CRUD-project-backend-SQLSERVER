using crud.repository.ViewModels;

namespace crud.service.Interfaces;

public interface IAuthService
{
    Task<AuthResultViewModel> GenerateToken(LoginRequestViewModel req);
    Task<AuthResultViewModel> refreshTokenService(RefreshRequestViewModel refresh);
    Task deleteRefreshToken(string refreshToken);
}
