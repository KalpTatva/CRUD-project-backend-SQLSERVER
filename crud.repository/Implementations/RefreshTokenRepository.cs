using crud.repository.Interfaces;
using crud.repository.Models;
using Microsoft.EntityFrameworkCore;

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
    public async Task updateRefreshToken(RefreshToken token)
    {
        try
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while adding token, {e.Message}");
        }
    }

    public async Task<RefreshToken?> getRefreshToken(string refreshToken)
    {
        try
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while fetching token, {e.Message}");
        }
    }
}
