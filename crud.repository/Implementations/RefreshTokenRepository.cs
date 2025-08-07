using crud.repository.Interfaces;
using crud.repository.Models;

namespace crud.repository.Implementations;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly StudentCourseContext _context;

    public RefreshTokenRepository(StudentCourseContext context)
    {
        _context = context;
    }


    public async Task AddRefreshToken(RefreshToken token)
    {
        try
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while adding token, {e.Message}");
        }
    }
}
