using Server.Data;
using Server.Models;
using Server.Repository.Contracts;

namespace Server.Repository.Data;

public class EducationRepository : GeneralRepository<Education, int, ManagementContext>, IEducationRepository
{
    public EducationRepository(ManagementContext context) : base(context)
    {

    }
}
