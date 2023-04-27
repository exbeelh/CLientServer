using Server.Models;
using Server.Data;
using Server.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Server.Repository.Data;

public class UniversityRepository : GeneralRepository<University, int, ManagementContext>, IUniversityRepository
{
    public UniversityRepository(ManagementContext context) : base(context)
    {
    }

    public async Task<bool> IsNameExist(string name)
    {
        var entity = await _context.Set<University>().FirstOrDefaultAsync(x => x.Name == name);
        return entity != null;
    }
}
