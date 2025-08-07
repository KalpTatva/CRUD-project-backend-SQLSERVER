using crud.repository.Models;

namespace crud.repository.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddRefreshToken(RefreshToken token);
}
