using crud.repository.Models;

namespace crud.repository.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task<User?> getUserById(int userId);
}
