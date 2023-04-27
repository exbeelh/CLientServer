using Server.Models;
using Server.Data;
using Server.Repository.Contracts;
using Server.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Server.Repository.Data;
public class EmployeeRepository : GeneralRepository<Employee, string, ManagementContext>, IEmployeeRepository
{
    public EmployeeRepository(ManagementContext context) : base(context)
    {
    }

    public async Task<EmployeeProfileVM> GetEmployeeDataByEmailAsync(string email)
    {
        var employee = await _context.Employees
                                .Include(e => e.Profiling)
                                    .ThenInclude(p => p!.Education)
                                        .ThenInclude(ed => ed.University)
                                .FirstOrDefaultAsync(e => e.Email == email);

        return new EmployeeProfileVM
        {
            Nik = employee!.Nik,
            Email = employee!.Email,
            FullName = string.Concat(employee.FirstName, " ", employee.LastName),
            Gender = employee!.Gender,
            Major = employee!.Profiling!.Education!.Major,
            Degree = employee!.Profiling!.Education!.Degree,
            Gpa = employee!.Profiling!.Education!.Gpa,
            University = employee!.Profiling!.Education!.University!.Name,
            HiringDate = employee!.HiringDate
        };
    }

    public async Task<UserDataVM> GetUserDataByEmailAsync(string email)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        return new UserDataVM
        {
            Nik = employee!.Nik,
            Email = employee.Email,
            FullName = string.Concat(employee.FirstName, " ", employee.LastName)
        };
    }

}
