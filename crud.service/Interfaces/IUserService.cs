using crud.repository.ViewModels;
using webapi.Repository.ViewModels;

namespace crud.service.Interfaces;

public interface IUserService
{
    Task<ResponseViewModel> VarifyUser(LoginRequestViewModel req);
}
