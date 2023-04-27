using Server.Models;
using Server.ViewModels;

namespace Server.Repository.Contracts;
public interface IAccountRepository : IGeneralRepository<Account, string> 
{
    Task Register(RegisterVM registerVM);
    Task<bool> IsLogin(LoginVM loginVM);
}
