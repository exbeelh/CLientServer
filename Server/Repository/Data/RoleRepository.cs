using Server.Models;
using Server.Data;
using Server.Repository.Contracts;

namespace Server.Repository.Data;

public class RoleRepository : GeneralRepository<Role, int, ManagementContext>, IRoleRepository
{
    public RoleRepository(ManagementContext context) : base(context)
    {

    }

}
