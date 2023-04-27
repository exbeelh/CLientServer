using Server.Models;

namespace Server.Repository.Contracts;
public interface IUniversityRepository : IGeneralRepository<University, int> 
{
    Task<bool> IsNameExist(string name);
}
