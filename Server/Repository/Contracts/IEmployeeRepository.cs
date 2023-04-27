using Server.Models;
using Server.ViewModels;

namespace Server.Repository.Contracts;
public interface IEmployeeRepository : IGeneralRepository<Employee, string> 
{
    Task<UserDataVM> GetUserDataByEmailAsync(string email);
    Task<EmployeeProfileVM> GetEmployeeDataByEmailAsync(string email);
}
