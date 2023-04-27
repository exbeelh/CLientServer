using Server.Models;
using Server.ViewModels;

namespace Server.Repository.Contracts;
public interface IProfilingReopsitory : IGeneralRepository<Profiling, string> 
{
    Task<IEnumerable<EmployeeProfileVM>> GetHighThanAverageGpaAsync(int year);
    Task<IEnumerable<GroupDataVM>> GetEducationStatisticAsync();
    Task<IEnumerable<EmployeeProfileVM>> GetWorkPeriodeAsync();
}

