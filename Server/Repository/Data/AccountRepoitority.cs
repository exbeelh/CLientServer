using EmployeeApp.Handlers;
using Server.Data;
using Server.Models;
using Server.ViewModels;

namespace Server.Repository.Contracts;

public class AccountRepoitority : GeneralRepository<Account, string, ManagementContext>, IAccountRepository
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProfilingReopsitory _profilingReopsitory;
    private readonly IAccountRoleRepository _accountRoleRepository;

    public AccountRepoitority(
        IUniversityRepository universityRepository,
        IEducationRepository educationRepository,
        IEmployeeRepository employeeRepository,
        IProfilingReopsitory profilingReopsitory,
        IAccountRoleRepository accountRoleRepository,
        ManagementContext context
        ) : base(context)
    {
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _employeeRepository = employeeRepository;
        _profilingReopsitory = profilingReopsitory;
        _accountRoleRepository = accountRoleRepository;
    }

    public async Task Register(RegisterVM registerVM)
    {
        await using var transaction = _context.Database.BeginTransaction();
        try
        {

            var university = new University
            {
                Name = registerVM.UniversityName
            };
            await _universityRepository.InsertAsync(university);

            var education = new Education
            {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.GPA,
                UniversityId = university.Id,
            };
            await _educationRepository.InsertAsync(education);

            var employee = new Employee
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Birthdate = registerVM.BirthDate,
                Gender = registerVM.Gender,
                Phone = registerVM.PhoneNumber,
                Email = registerVM.Email,
                HiringDate = DateTime.Now
            };
            await _employeeRepository.InsertAsync(employee);

            var account = new Account
            {
                EmployeeNik = employee.Nik,
                Password = Hashing.HashPassword(registerVM.Password),
            };
            await InsertAsync(account);

            var profiling = new Profiling
            {
                EducationId = education.Id,
                EmployeeNik = employee.Nik
            };
            await _profilingReopsitory.InsertAsync(profiling);

            var accountRole = new AccountRole
            {
                EmployeeNik = employee.Nik,
                RoleId = 2
            };
            await _accountRoleRepository.InsertAsync(accountRole);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
        }

    }
    
    public async Task<bool> IsLogin(LoginVM loginVM)
    {
        var getEmployees = await _employeeRepository.GetAllAsync();
        var getAccounts = await GetAllAsync();

        var getUserData = getEmployees.Join(getAccounts,
                                            e => e.Nik,
                                            a => a.EmployeeNik,
                                            (e, a) => new LoginVM
                                            {
                                                Email = e.Email,
                                                Password = a.Password
                                            })
                                      .FirstOrDefault(ud => ud.Email == loginVM.Email);

        return getUserData is not null && Hashing.ValidatePassword(loginVM.Password, getUserData.Password);
    }

}
