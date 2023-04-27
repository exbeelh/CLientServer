using Server.Models;
using Server.Data;
using Server.Repository.Contracts;
using Server.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Server.Repository.Data;

public class ProfilingRepository : GeneralRepository<Profiling, string, ManagementContext>, IProfilingReopsitory
{
    public ProfilingRepository(ManagementContext context) : base(context)
    {
    }

    public IQueryable<EmployeeProfileVM> EmployeeDatas => from p in _context.Profilings
                                                          join e in _context.Employees on p.EmployeeNik equals e.Nik
                                                          join edu in _context.Educations on p.EducationId equals edu.Id
                                                          join univ in _context.Universities on edu.UniversityId equals univ.Id
                                                          select new EmployeeProfileVM
                                                          {
                                                              Nik = e.Nik,
                                                              FullName = e.FirstName + " " + e.LastName,
                                                              Gender = e.Gender,
                                                              Email = e.Email,
                                                              Major = edu.Major,
                                                              Degree = edu.Degree,
                                                              Gpa = edu.Gpa,
                                                              University = univ.Name,
                                                              HiringDate = e.HiringDate
                                                          };

    public async Task<IEnumerable<EmployeeProfileVM>> GetHighThanAverageGpaAsync(int year)
    {
        var avg = EmployeeDatas.Average(d => d.Gpa);

        var filteredData = EmployeeDatas.Where(d => d.Gpa >  avg && d.HiringDate.Year == year).OrderBy(d => d.Major);

        return await filteredData.ToListAsync();
    }

    public async Task<IEnumerable<GroupDataVM>> GetEducationStatisticAsync()
    {
        var data = from p in _context.Profilings
                   join edu in _context.Educations on p.EducationId equals edu.Id
                   join univ in _context.Universities on edu.UniversityId equals univ.Id
                   group new { edu.Major, univ.Name } by new { edu.Major, univ.Name } into g
                   orderby g.Count() descending
                   select new GroupDataVM
                   {
                       Major = g.Key.Major,
                       UniversityName = g.Key.Name,
                       TotalEmployees = g.Count(),
                   };

        return await data.ToListAsync();
    }

    public async Task<IEnumerable<EmployeeProfileVM>> GetWorkPeriodeAsync()
    {
        return await EmployeeDatas.OrderByDescending(e => e.HiringDate).ToListAsync();
    }
}
