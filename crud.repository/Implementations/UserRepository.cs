using crud.repository.Interfaces;
using crud.repository.Models;
using Microsoft.EntityFrameworkCore;

namespace crud.repository.Implementations;

public class UserRepository : IUserRepository
{
    private readonly StudentCourseContext _context;

    public UserRepository(StudentCourseContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        try
        {
            email = email.Trim().ToLower();
            User? user = await _context.Users.FirstOrDefaultAsync(user => user.Email.Contains(email) && user.IsDeleted == false);
            return user;
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while fatching details : {e.Message}");
        }
    }

    public async Task<User?> getUserById(int userId)
    {
        try
        {
            User? user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userId && user.IsDeleted == false);
            return user;
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while fatching details : {e.Message}");
        }
    }

}
