using BCrypt.Net;
using crud.repository.Interfaces;
using crud.repository.Models;
using crud.repository.ViewModels;
using crud.service.Interfaces;
using webapi.Repository.ViewModels;

namespace crud.service.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseViewModel> VarifyUser(LoginRequestViewModel req)
    {
        try
        {
            User? user = await _userRepository.GetUserByEmail(req.Email);
            
            if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(req.Password, user.Password))
            {
                throw new Exception($"No User Found!");
            }
            else return new ResponseViewModel
            {
                success = true,
                message = ""
            };
        }
        catch (Exception e)
        {
            throw new Exception($"{e.Message}");
        }
    }

}
