using crud.repository.ViewModels;

namespace crud.service.Interfaces;

public interface IAuthService
{
    Task<AuthResultViewModel> GenerateToken(LoginRequestViewModel req);
}
