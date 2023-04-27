using Server.Models;

namespace Server.Repository.Contracts;
public interface IAccountRoleRepository : IGeneralRepository<AccountRole, int> 
{
    Task<IEnumerable<string>> GetRolesByNikAsync(string nik);
}

