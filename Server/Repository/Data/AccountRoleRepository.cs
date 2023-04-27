using Server.Data;
using Server.Models;
using Server.Repository.Contracts;

namespace Server.Repository.Data;

public class AccountRoleRepository : GeneralRepository<AccountRole, int, ManagementContext>, IAccountRoleRepository
{
    private readonly IRoleRepository _roleRepository;
    public AccountRoleRepository(ManagementContext context, IRoleRepository roleRepository) : base(context) 
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<string>> GetRolesByNikAsync(string nik)
    {
        var getAccountRoleByAccountNik = GetAllAsync().Result.Where(x => x.EmployeeNik == nik);
        var getRole = await _roleRepository.GetAllAsync();

        var getRoleByNik = from ar in getAccountRoleByAccountNik
                           join r in getRole on ar.RoleId equals r.Id
                           select r.Name;

        return getRoleByNik;
    }
}
